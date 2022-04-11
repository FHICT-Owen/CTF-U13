namespace MissionSystem.Main.MQTT.Client
{
    public interface IDurableMqttClient : IDisposable
    {
        delegate void MessageCallback(Dictionary<string, object> message);

        public event Action Connect;
        public event Action Disconnect;

        public Task ConnectAsync();
        public Task SubscribeTopic(string topic, MessageCallback callback);
        public Task CloseAsync();
    }
}
