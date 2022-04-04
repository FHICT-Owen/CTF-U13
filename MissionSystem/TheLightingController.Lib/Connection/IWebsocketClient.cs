using System.Net.WebSockets;

namespace TheLightingControllerLib.Connection;

public interface IWebsocketClient
{
    public WebSocketState State { get; }

    public Task ConnectAsync(Uri uri, CancellationToken cancellationToken);

    public Task SendAsync(ArraySegment<byte> buffer, WebSocketMessageType messageType, bool endOfMessage,
        CancellationToken cancellationToken);

    public ValueTask<ValueWebSocketReceiveResult> ReceiveAsync(Memory<byte> buffer, CancellationToken cancellationToken);

    public Task CloseAsync(WebSocketCloseStatus closeStatus, string? statusDescription,
        CancellationToken cancellationToken);

    public void Abort();
}