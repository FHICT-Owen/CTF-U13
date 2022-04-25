using MissionSystem.Interface;
using MissionSystem.Main.Gadgets;
using MissionSystem.Main.MQTT.Client;

namespace MissionSystem.Main.MQTT;

public interface IMqttClientService : IHostedService
{
    Task<IUnsubscribable> SubscribeAsync(string topic, IDurableMqttClient.MessageCallback callback);
}
