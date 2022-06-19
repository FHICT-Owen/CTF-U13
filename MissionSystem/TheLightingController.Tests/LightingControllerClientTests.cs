using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TheLightingControllerLib;
using TheLightingControllerLib.Connection;

namespace TheLightingControllerTests;

public class LightingControllerClientTests
{
    [Test]
    public async Task ConnectShouldConnect()
    {
        var connectionMock = new Mock<ILightingControllerConnection>();
        using var client = new LightingControllerClient(connectionMock.Object);

        connectionMock
            .Setup(c => c.ReceiveMessageAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("HELLO");

        await client.ConnectAsync("foo");

        connectionMock.Verify(c => c.ConnectAsync(It.IsAny<CancellationToken>()));
    }

    [Test]
    public async Task ConnectShouldSendHello()
    {
        const string password = "test";

        var connectionMock = new Mock<ILightingControllerConnection>();
        using var client = new LightingControllerClient(connectionMock.Object);

        connectionMock
            .Setup(c => c.ReceiveMessageAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("HELLO");

        await client.ConnectAsync(password);

        connectionMock.Verify(c =>
            c.SendMessageAsync("HELLO|Live_Mobile_3|test", It.IsAny<CancellationToken>()));
    }

    [Test]
    public void ConnectShouldThrowOnError()
    {
        const string password = "test";

        var connectionMock = new Mock<ILightingControllerConnection>();
        using var client = new LightingControllerClient(connectionMock.Object);

        connectionMock
            .Setup(c => c.ReceiveMessageAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("ERROR|Invalid password");

        Assert.ThrowsAsync<ConnectionException>(async () => { await client.ConnectAsync(password); });
    }

    [Test]
    public async Task ShouldReleaseAndPressButton()
    {
        var connectionMock = new Mock<ILightingControllerConnection>(MockBehavior.Strict);
        var sequence = new MockSequence();

        connectionMock.InSequence(sequence)
            .Setup(c => c.SendMessageAsync("BUTTON_RELEASE|foo", It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        connectionMock.InSequence(sequence)
            .Setup(c => c.SendMessageAsync("BUTTON_PRESS|foo", It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        
        var client = new LightingControllerClient(connectionMock.Object);

        await client.PressButton("foo");
    }

    [Test]
    public async Task ShouldClose()
    {
        var connectionMock = new Mock<ILightingControllerConnection>();
        using var client = new LightingControllerClient(connectionMock.Object);
        
        await client.CloseAsync();

        connectionMock
            .Verify(c => c.CloseAsync(It.IsAny<CancellationToken>()));

        client.Dispose();
    }
}
