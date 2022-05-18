using MissionSystem.Main.MQTT.Client;
using MissionSystem.Util;

namespace MissionSystem.Main.MQTT;

public interface IMqttClientService : IHostedService
{
    Task<IUnsubscribable> SubscribeAsync(string topic, IDurableMqttClient.MessageCallback callback);

    Task SendMessageAsync(string topic, Dictionary<string, object> message);
}
