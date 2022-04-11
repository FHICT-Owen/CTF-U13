using MissionSystem.Main.MQTT.Client;

namespace MissionSystem.Main.MQTT;

public interface IMqttClientService : IHostedService
{
    Task SubscribeAsync(string topic, IDurableMqttClient.MessageCallback callback);
}
