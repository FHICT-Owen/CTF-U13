using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;

namespace TheLightingControllerLib.Connection;

[ExcludeFromCodeCoverage]
public class WebsocketAdapter : IWebsocketClient
{
    private readonly ClientWebSocket _client = new();

    public WebSocketState State => _client.State;

    public Task ConnectAsync(Uri uri, CancellationToken cancellationToken) =>
        _client.ConnectAsync(uri, cancellationToken);

    public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage,
        CancellationToken cancellationToken) => _client.SendAsync(buffer, messageType, endOfMessage, cancellationToken);

    public ValueTask<ValueWebSocketReceiveResult>
        ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken) =>
        _client.ReceiveAsync(buffer, cancellationToken);

    public Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription,
        CancellationToken cancellationToken) => _client.CloseAsync(closeStatus, statusDescription, cancellationToken);

    public void Abort() => _client.Abort();
}