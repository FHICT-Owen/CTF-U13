using System.Diagnostics.CodeAnalysis;
using TheLightingControllerLib.Connection;

namespace TheLightingControllerLib;

/// <summary>
/// Client for interfacing with TheLightingController software.
/// </summary>
public class LightingControllerClient : ILightingController, IDisposable
{
    private const string ClientId = "Live_Mobile_3";

    private readonly ILightingControllerConnection _connection;

    [ExcludeFromCodeCoverage]
    public LightingControllerClient(string host, int port) : this(new LightingControllerWebsocketConnection(host, port))
    {
    }

    public LightingControllerClient(ILightingControllerConnection conn)
    {
        _connection = conn;
    }

    public async Task ConnectAsync(string password) =>
        await ConnectAsync(password, CancellationToken.None);

    public async Task ConnectAsync(string password, CancellationToken token)
    {
        // Connect
        await _connection.ConnectAsync(token);

        // Send client hello
        await SendMessageAsync(MessageType.Hello, token, ClientId, password);

        // Receive server hello or error response
        var response = await ReceiveMessageAsync(token);

        if (response.MessageType == MessageType.Error)
        {
            throw new ConnectionException($"Returned error: {response.Args[0]}");
        }
    }

    public async Task PressButton(string name) => await PressButton(name, CancellationToken.None);

    public async Task PressButton(string name, CancellationToken token)
    {
        await SendMessageAsync(MessageType.ButtonPress, token, name);
        await SendMessageAsync(MessageType.ButtonRelease, token, name);
    }

    public async Task CloseAsync() => await CloseAsync(CancellationToken.None);

    public async Task CloseAsync(CancellationToken token) => await _connection.CloseAsync(token);

    public void Dispose()
    {
        _connection.Dispose();
    }

    private async Task SendMessageAsync(MessageType type, CancellationToken token, params string[] args) =>
        await SendMessageAsync(new Message {MessageType = type, Args = args}, token);

    private async Task SendMessageAsync(Message message, CancellationToken token)
    {
        await _connection.SendMessageAsync(message.ToString(), token);
    }

    private async Task<Message> ReceiveMessageAsync(CancellationToken token)
    {
        var message = await _connection.ReceiveMessageAsync(token);
        return Message.FromString(message);
    }
}