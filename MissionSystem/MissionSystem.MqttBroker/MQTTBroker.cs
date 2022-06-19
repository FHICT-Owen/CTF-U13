using Microsoft.Extensions.Logging;
using MissionSystem.Interface.MQTT;
using MQTTnet;
using MQTTnet.Server;

namespace MissionSystem.MqttBroker;

public class MQTTBroker : IMQTTBroker
{
    private IMqttServer? server = null;
    private readonly ILogger<IMQTTBroker> _logger;

    public MQTTBroker(ILogger<IMQTTBroker> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var mqttFactory = new MqttFactory();

        var mqttServerOptions = new MqttServerOptionsBuilder()
            .WithDefaultEndpoint()
            .WithConnectionValidator(OnNewConnection)
            .Build();

        server = mqttFactory.CreateMqttServer();

        await server.StartAsync(mqttServerOptions);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        if (server != null) await server.StopAsync();
    }

    public void Dispose()
    {
        if (server != null) server.StopAsync();
    }

    private void OnNewConnection(MqttConnectionValidatorContext context)
    {
        _logger.LogInformation("New connection: ClientId = {0}, Endpoint = {1}",
            context.ClientId,
            context.Endpoint);
    }
}
