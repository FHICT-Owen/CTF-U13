namespace TheLightingControllerLib;

public interface ILightingController
{
    /// <summary>
    /// Creates a connection with the lighting controller.
    /// </summary>
    /// <param name="password">The password to connect with</param>
    /// <exception cref="ConnectionException">Could not connect to the lighting controller</exception>
    /// <returns></returns>
    public Task ConnectAsync(string password);
    
    /// <summary>
    /// Creates a connection with the lighting controller.
    /// </summary>
    /// <param name="password">The password to connect with</param>
    /// <param name="token"></param>
    /// <exception cref="ConnectionException">Could not connect to the lighting controller</exception>
    /// <returns></returns>
    public Task ConnectAsync(string password, CancellationToken token);
    
    /// <summary>
    /// Simulates a short press of the given button.
    /// </summary>
    /// <param name="name">The button to press</param>
    /// <returns></returns>
    public Task PressButton(string name);
    
    /// <summary>
    /// Simulates a short press of the given button.
    /// </summary>
    /// <param name="name">The button to press</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task PressButton(string name, CancellationToken token);

    /// <summary>
    /// Closes the connection with the lighting controller
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task CloseAsync(CancellationToken token);
    
    /// <summary>
    /// Closes the connection with the lighting controller
    /// </summary>
    /// <returns></returns>
    public Task CloseAsync();
}