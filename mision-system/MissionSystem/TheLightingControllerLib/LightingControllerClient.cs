using System.Net.WebSockets;
using System.Text;
using Microsoft.IO;

namespace TheLightingControllerLib;

public struct MessageWithArgs
{
    public Message Message;
    public string[] Args;

    public override string ToString()
    {
        return string.Join(LightingControllerClient.ArgSeparator, Args.Prepend(Message.Name));
    }
}

public class LightingControllerClient : IDisposable
{
    public const char ArgSeparator = '|';
    private const string ClientId = "Live_Mobile_3";
    private readonly ClientWebSocket _client = new();

    private static readonly RecyclableMemoryStreamManager StreamManager = new();

    public async Task ConnectAsync(Uri uri, string password) =>
        await ConnectAsync(uri, password, CancellationToken.None);
    
    public async Task ConnectAsync(Uri uri, string password, CancellationToken token)
    {
        await _client.ConnectAsync(uri, token);

        await SendMessageAsync(new MessageWithArgs
        {
            Message = Message.Hello,
            Args = new[] {ClientId, password}
        }, token);

        var response = await ReceiveMessageAsync(token);

        if (response.Message != Message.Hello)
        {
            throw new Exception("Did not get HELLO result");
        }
    }

    public async Task SendMessageAsync(Message msg, params string[] args) =>
        await SendMessageAsync(new MessageWithArgs {Message = msg, Args = args}, CancellationToken.None);

    public async Task SendMessageAsync(MessageWithArgs cmd) =>
        await SendMessageAsync(cmd, CancellationToken.None);

    public async Task SendMessageAsync(MessageWithArgs cmd, CancellationToken token)
    {
        if (_client.State != WebSocketState.Open)
        {
            // throw error
        }

        await _client.SendAsync(Encoding.UTF8.GetBytes(cmd.ToString()), WebSocketMessageType.Text, true, token);
    }

    public async Task<MessageWithArgs> ReceiveMessageAsync() => await ReceiveMessageAsync(CancellationToken.None);

    public async Task<MessageWithArgs> ReceiveMessageAsync(CancellationToken token)
    {
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

        var parts = str.Split(ArgSeparator);
        
        await stream.DisposeAsync();

        return new MessageWithArgs()
        {
            Message = Message.FromName(parts[0]),
            Args = parts.Skip(1).ToArray(),
        };
    }

    public async Task CloseAsync() => await CloseAsync(CancellationToken.None);

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