using System.Net.WebSockets;
using System.Text;
using Microsoft.IO;

namespace TheLightingControllerLib.Connection;

internal class LightingControllerWebsocketConnection : ILightingControllerConnection
{
    private const string WebsocketPath = "/websocket";

    private static readonly RecyclableMemoryStreamManager StreamManager = new();

    private readonly ClientWebSocket _client = new();

    private readonly Uri _uri;

    public LightingControllerWebsocketConnection(string host, int port)
    {
        var builder = new UriBuilder
        {
            Scheme = "ws",
            Host = host,
            Port = port,
            Path = WebsocketPath,
        };

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

        if (StreamManager.GetStream() is not RecyclableMemoryStream stream)
        {
            throw new Exception("Could not get memory stream");
        }

        for (;;)
        {
            var buf = stream.GetMemory();

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
                throw;
            }
        }
    }

    public void Dispose()
    {
        _client.Abort();
    }
}