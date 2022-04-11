using System.Text;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;

namespace MissionSystem.Main.MQTT.Client
{
    public class DurableMqttClient : IDurableMqttClient
    {
        private readonly IManagedMqttClient _client;

        private readonly string _id;
        private readonly string _username;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;

        private readonly Dictionary<string, List<IDurableMqttClient.MessageCallback>> _subscriptions = new();

        public DurableMqttClient(string id = "testID", string username = "", string password = "",
            string host = "localhost",
            int port = 1883)
        {
            _id = id;
            _username = username;
            _password = password;
            _host = host;
            _port = port;

            _client = new MqttFactory().CreateManagedMqttClient();
        }

        public event Action? Connect;
        public event Action? Disconnect;

        public async Task ConnectAsync()
        {
            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(builder =>
                    builder.WithClientId(_id)
                        .WithTcpServer(_host, _port)
                        .WithCredentials(_username, _password)
                        .WithCleanSession())
                .Build();
            
            _client.UseApplicationMessageReceivedHandler(args =>
            {
                var msg = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);

                try
                {
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, object>>(msg);
                    
                    if (obj == null)
                    {
                        return;
                    }

                    if (!_subscriptions.ContainsKey(args.ApplicationMessage.Topic)) return;

                    // TODO: this does not work with any type of wildcard subscription
                    var callbacks = _subscriptions[args.ApplicationMessage.Topic];

                    foreach (var messageCallback in callbacks)
                    {
                        messageCallback(obj);
                    }
                }
                catch (JsonException e)
                {
                    Console.Error.WriteLine("Could not deserialize message on topic {0}", args.ApplicationMessage.Topic);
                }
            });

            _client.UseConnectedHandler(e => { Connect?.Invoke(); });

            _client.UseDisconnectedHandler(e => { Disconnect?.Invoke(); });

            await _client.StartAsync(options);
        }

        public async Task CloseAsync()
        {
            await _client.StopAsync();
        }

        public async Task SubscribeTopic(string topic, IDurableMqttClient.MessageCallback callback)
        {
            if (!_subscriptions.ContainsKey(topic))
            {
                _subscriptions[topic] = new List<IDurableMqttClient.MessageCallback>();
            }

            _subscriptions[topic].Add(callback);

            await _client.SubscribeAsync(topic);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
