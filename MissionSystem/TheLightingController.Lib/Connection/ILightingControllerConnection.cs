namespace TheLightingControllerLib;

/// <summary>
/// Represents a connection/transport for TheLightingController
/// </summary>
public interface ILightingControllerConnection : IDisposable
{
    /// <summary>
    /// Connects to the Lighting Controller.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task ConnectAsync(CancellationToken token);

    /// <summary>
    /// Sends a message to The Lighting Controller.
    /// </summary>
    /// <param name="message">The message to send</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task SendMessageAsync(string message, CancellationToken token);

    /// <summary>
    /// Receive a message from The Lighting Controller.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<string> ReceiveMessageAsync(CancellationToken token);

    /// <summary>
    /// Disconnect from The Lighting Controller.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task CloseAsync(CancellationToken token);
}