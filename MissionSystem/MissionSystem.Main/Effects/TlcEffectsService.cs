using MissionSystem.Interface.Services;
using TheLightingControllerLib;
using TheLightingControllerLib.Connection;

namespace MissionSystem.Main.Effects;

public class TlcEffectsService : IEffectsService
{
    private readonly ILightingController _controller;
    private readonly string _password;
    private readonly ILogger<TlcEffectsService> _logger;

    private string _host;
    private int _port;
    private bool _connected;

    public TlcEffectsService(string host, int port, string password, ILogger<TlcEffectsService> logger)
    {
        _password = password;
        _logger = logger;

        _host = host;
        _port = port;

        var connection = new LightingControllerWebsocketConnection(host, port);
        _controller = new LightingControllerClient(connection);
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await _controller.ConnectAsync(_password, cancellationToken);
            _logger.LogInformation("Connected to TheLightingController at ws://{Host}:{Port}", _host, _port);
            _connected = true;
        }
        catch (ConnectionException)
        {
            _logger.LogError("Could not connect to TheLightingController at ws://{Host}:{Port}", _host, _port);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (!_connected)
        {
            return;
        }

        await _controller.CloseAsync(cancellationToken);
        _logger.LogInformation("Disconnected from TheLightingController");
    }

    public async Task TriggerEffectAsync(string effect)
    {
        await _controller.PressButton(effect);
    }
}
