using MissionSystem.Interface.MQTT;
using MQTTnet;
using MQTTnet.Server;

namespace MissionSystem.MqttBroker;

public class MQTTBroker : IMQTTBroker
{

    private IMqttServer? server = null;

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

    private static void OnNewConnection(MqttConnectionValidatorContext context)
    {
        Console.WriteLine("New connection: ClientId = {0}, Endpoint = {1}",
                context.ClientId,
                context.Endpoint);

    }

}