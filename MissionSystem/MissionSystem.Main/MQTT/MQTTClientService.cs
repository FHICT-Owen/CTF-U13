using MissionSystem.Interface.MQTT;
using MissionSystem.Main.MQTT.Client;
using MissionSystem.Util;

namespace MissionSystem.Main.MQTT;

public class MqttClientService : IMqttClientService, IDisposable
{
    private readonly IDurableMqttClient _client;

    private readonly ILogger<MqttClientService> _logger;

    private const string Host = "localhost";
    private const int Port = 1883;

    public MqttClientService(ILogger<MqttClientService> logger)
    {
        _logger = logger;

        _client = new DurableMqttClient(host: Host, port: Port);

        _client.Connect += ClientOnConnect;
        _client.Disconnect += ClientOnDisconnect;
    }


    private void ClientOnConnect()
    {
        _logger.LogInformation("Connected to mqtt://{}:{}", Host, Port);
    }

    private void ClientOnDisconnect()
    {
        _logger.LogInformation("Disconnected from MQTT server");
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _client.ConnectAsync();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.CloseAsync();
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public async Task<IUnsubscribable> SubscribeAsync(string topic, IDurableMqttClient.MessageCallback callback)
    {
        _logger.LogInformation("Subscribed to MQTT topic {}", topic);
        await _client.SubscribeTopic(topic, callback);

        // TODO: unsubscribe
        return null;
    }
}
