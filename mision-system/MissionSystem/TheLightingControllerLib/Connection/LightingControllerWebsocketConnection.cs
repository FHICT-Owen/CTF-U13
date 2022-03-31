using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using System.Text;
using Microsoft.IO;

namespace TheLightingControllerLib.Connection;

public class LightingControllerWebsocketConnection : ILightingControllerConnection
{
    private const string WebsocketPath = "/websocket";

    private static readonly RecyclableMemoryStreamManager StreamManager = new();

    private readonly IWebsocketClient _client;

    private readonly Uri _uri;

    [ExcludeFromCodeCoverage]
    public LightingControllerWebsocketConnection(string host, int port) : this(new WebsocketAdapter(), host, port)
    {
    }

    public LightingControllerWebsocketConnection(IWebsocketClient client, string host, int port)
    {
        var builder = new UriBuilder
        {
            Scheme = "ws",
            Host = host,
            Port = port,
            Path = WebsocketPath,
        };

        _client = client;
        _uri = builder.Uri;
    }

    public async Task ConnectAsync(CancellationToken token)
    {
        try
        {
            await _client.ConnectAsync(_uri, token);
        }
        catch (WebSocketException exception)
        {
            throw new ConnectionException($"Could not connect to {_uri}", exception);
        }
    }

    public async Task SendMessageAsync(string message, CancellationToken token)
    {
        if (_client.State != WebSocketState.Open)
        {
            throw new ConnectionException("Not connected");
        }

        await _client.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, token);
    }

    public async Task<string> ReceiveMessageAsync(CancellationToken token)
    {
        if (_client.State != WebSocketState.Open)
        {
            throw new ConnectionException("Not connected");
        }

        var stream = StreamManager.GetStream() as RecyclableMemoryStream;

        for (;;)
        {
            var buf = stream!.GetMemory();

            var res = await _client.ReceiveAsync(buf, token);

            stream.Advance(res.Count);

            if (res.EndOfMessage)
            {
                break;
            }
        }

        var str = Encoding.UTF8.GetString(stream.GetReadOnlySequence());

        await stream.DisposeAsync();

        return str;
    }

    public async Task CloseAsync(CancellationToken token)
    {
        try
        {
            await _client.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", token);
        }
        catch (WebSocketException e)
        {
            // TheLightingController does not handle disconnects gracefully
            if (e.WebSocketErrorCode != WebSocketError.ConnectionClosedPrematurely)
            {
                throw new ConnectionException("Could not close the connection", e);
            }
        }
    }

    public void Dispose()
    {
        _client.Abort();
    }
}