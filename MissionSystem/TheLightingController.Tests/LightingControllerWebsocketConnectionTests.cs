using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TheLightingControllerLib.Connection;

namespace TheLightingControllerTests;

public class LightingControllerWebsocketConnectionTests
{
    private static readonly string TestMessage = "fool";
    private static readonly byte[] TestMessageBytes = {0x66, 0x6f, 0x6f, 0x6c};

    [Test]
    public async Task ShouldConnect()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);

        await client.ConnectAsync(CancellationToken.None);

        connectionMock.Verify(c =>
            c.ConnectAsync(new Uri("ws://localhost:2020/websocket"), It.IsAny<CancellationToken>()));
    }

    [Test]
    public void ShouldHandleConnectError()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);

        connectionMock.Setup(c => c.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new WebSocketException(WebSocketError.Faulted));

        Assert.ThrowsAsync<ConnectionException>(async () => { await client.ConnectAsync(CancellationToken.None); });
    }

    [Test]
    public async Task ShouldSendMessage()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);
        var cancel = new CancellationToken();

        connectionMock.SetupGet(c => c.State).Returns(WebSocketState.Open);

        await client.SendMessageAsync(TestMessage, cancel);

        connectionMock.Verify(c => c.SendAsync(TestMessageBytes, WebSocketMessageType.Text, true, cancel));
    }

    [Test]
    public void ShouldGiveExceptionWhenSendingOnClosedConnection()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);

        connectionMock.SetupGet(c => c.State).Returns(WebSocketState.Closed);

        Assert.ThrowsAsync<ConnectionException>(async () =>
        {
            await client.SendMessageAsync(TestMessage, CancellationToken.None);
        });
    }

    [Test]
    public async Task ShouldReceiveMessage()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);

        connectionMock.SetupGet(c => c.State).Returns(WebSocketState.Open);
        connectionMock.Setup(c => c.ReceiveAsync(It.IsAny<Memory<byte>>(), It.IsAny<CancellationToken>()))
            .Callback<Memory<byte>, CancellationToken>((buf, token) => { TestMessageBytes.CopyTo(buf); })
            .ReturnsAsync(new ValueWebSocketReceiveResult(TestMessageBytes.Length, WebSocketMessageType.Text, true));

        var msg = await client.ReceiveMessageAsync(CancellationToken.None);

        Assert.That(msg, Is.EqualTo(TestMessage));
    }

    [Test]
    public async Task ShouldReceiveMessageFromMultipleReads()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);

        var sequence = new MockSequence();

        connectionMock.InSequence(sequence).SetupGet(c => c.State).Returns(WebSocketState.Open);

        connectionMock.InSequence(sequence)
            .Setup(c => c.ReceiveAsync(It.IsAny<Memory<byte>>(), It.IsAny<CancellationToken>()))
            .Callback<Memory<byte>, CancellationToken>((buf, token) => { TestMessageBytes[..1].CopyTo(buf); })
            .ReturnsAsync(new ValueWebSocketReceiveResult(TestMessageBytes[..1].Length, WebSocketMessageType.Text,
                false));

        connectionMock.InSequence(sequence)
            .Setup(c => c.ReceiveAsync(It.IsAny<Memory<byte>>(), It.IsAny<CancellationToken>()))
            .Callback<Memory<byte>, CancellationToken>((buf, token) => { TestMessageBytes[1..].CopyTo(buf); })
            .ReturnsAsync(
                new ValueWebSocketReceiveResult(TestMessageBytes[1..].Length, WebSocketMessageType.Text, true));

        var msg = await client.ReceiveMessageAsync(CancellationToken.None);

        Assert.That(msg, Is.EqualTo(TestMessage));
    }

    [Test]
    public void ShouldGiveExceptionWhenReceivingOnClosedConnection()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);

        var sequence = new MockSequence();

        connectionMock.InSequence(sequence).SetupGet(c => c.State).Returns(WebSocketState.Closed);


        Assert.ThrowsAsync<ConnectionException>(async () =>
        {
            await client.ReceiveMessageAsync(CancellationToken.None);
        });
    }


    [Test]
    public async Task ShouldClose()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);
        var cancel = new CancellationToken();

        await client.CloseAsync(cancel);

        connectionMock.Verify(c =>
            c.CloseAsync(WebSocketCloseStatus.NormalClosure, "bye", cancel));
    }

    [Test]
    public void ShouldWrapCloseErrors()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);

        connectionMock.Setup(c =>
                c.CloseAsync(It.IsAny<WebSocketCloseStatus>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new WebSocketException(WebSocketError.Faulted));

        var exception = Assert.ThrowsAsync<ConnectionException>(async () =>
        {
            await client.CloseAsync(It.IsAny<CancellationToken>());
        });

        Assert.That(exception?.InnerException, Is.InstanceOf<WebSocketException>());
    }

    [Test]
    public async Task ShouldIgnoreAbnormalCloseHandshake()
    {
        var connectionMock = new Mock<IWebsocketClient>();
        using var client = new LightingControllerWebsocketConnection(connectionMock.Object, "localhost", 2020);
        var cancel = new CancellationToken();

        connectionMock.Setup(c =>
                c.CloseAsync(It.IsAny<WebSocketCloseStatus>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new WebSocketException(WebSocketError.ConnectionClosedPrematurely));

        await client.CloseAsync(cancel);

        Assert.DoesNotThrowAsync(async () => { await client.CloseAsync(It.IsAny<CancellationToken>()); });
    }
}
