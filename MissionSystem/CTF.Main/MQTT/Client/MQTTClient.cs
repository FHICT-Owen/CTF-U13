using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using System.Security.Authentication;

namespace CTF.Main.MQTT
{
    public class MQTTClient : IMQTTClient
    {
        public IMqttClient Client { get; private set; }
        private string ID;
        private string Username;
        private string Password;
        private string Host;
        private int Port;

        public MQTTClient(string ID = null, string Username = null, string Password = null, string Host = null, int? Port = null)
        {
            this.ID = string.IsNullOrEmpty(ID) ? "testID" : ID;
            this.Username = string.IsNullOrEmpty(Username) ? "" : Username;
            this.Password = string.IsNullOrEmpty(Password) ? "" : Password;

            this.Host = string.IsNullOrEmpty(Host) ? "localhost" : Host;

            if (Port == null || Port < 1 || Port > 65535) this.Port = 1883;
            else this.Port = (int)Port;

            var factory = new MqttFactory();
            this.Client = factory.CreateMqttClient();
        }

        public async void ConnectAsync(IMqttClientOptions options = null)
        {
            if (options == null)
                options = new MqttClientOptionsBuilder()
                    .WithClientId(this.ID)
                    .WithTcpServer(this.Host, this.Port)
                    .WithCleanSession()
                    .Build();

            await Client.ConnectAsync(options, CancellationToken.None);

            Client.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### DISCONNECTED FROM SERVER ###");
                await Task.Delay(TimeSpan.FromSeconds(5));

                try
                {
                    await Client.ConnectAsync(options, CancellationToken.None); // Since 3.0.5 with CancellationToken
                }
                catch
                {
                    Console.WriteLine("### RECONNECTING FAILED ###");
                }
            });
        }

        public void SetSubscribeTopic(string topic)
        {
            this.Client.UseConnectedHandler(async e =>
            {
                var filter = new TopicFilterBuilder()
                    .WithTopic(topic)
                    .Build();

                this.Client.SubscribeAsync(filter);
            });
        }
    }
}
