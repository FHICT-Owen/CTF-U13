using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace CTF.Main.MQTT
{
    public interface IMQTTClient
    {
        public IMqttClient Client { get; }
        public void ConnectAsync(IMqttClientOptions options);
        public void SetSubscribeTopic(string topic);
    }
}
