using MissionSystem.Util;

namespace MissionSystem.Main.MQTT.Client
{
    public interface IDurableMqttClient : IDisposable
    {
        delegate void MessageCallback(string topic, Dictionary<string, object> message);

        public event Action Connect;
        public event Action Disconnect;

        public Task ConnectAsync();
        public Task<IUnsubscribable> SubscribeTopic(string topic, MessageCallback callback);
        public Task CloseAsync();
        Task SendMessageAsync(string topic, Dictionary<string, object> message);
    }
}
