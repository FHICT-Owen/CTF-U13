using System.Text;
using MissionSystem.Util;
using MQTTnet;
using MQTTnet.Extensions.ManagedClient;
using Newtonsoft.Json;

namespace MissionSystem.Main.MQTT.Client;

public class DurableMqttClient : IDurableMqttClient
{
    private readonly IManagedMqttClient _client;

    private readonly string _id;
    private readonly string _username;
    private readonly string _password;
    private readonly string _host;
    private readonly int _port;

    private readonly Dictionary<TopicFilter, IDurableMqttClient.MessageCallback> _subscriptions = new();

    public DurableMqttClient(string id = "MissionSystem", string username = "", string password = "",
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

                foreach (var (topic, cb) in _subscriptions)
                {
                    if (!topic.IsTopicMatch(args.ApplicationMessage.Topic))
                    {
                        continue;
                    }

                    cb(args.ApplicationMessage.Topic, obj);
                }
            }
            catch (JsonException)
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

    public async Task<IUnsubscribable> SubscribeTopic(string topic, IDurableMqttClient.MessageCallback callback)
    {
        TopicFilter filter = new TopicFilter(topic);
        _subscriptions[filter] = callback;

        await _client.SubscribeAsync(topic);

        return new MqttTopicUnsubscribable(() => _client.UnsubscribeAsync(), _subscriptions, filter);
    }

    public async Task SendMessageAsync(string topic, Dictionary<string, object> message)
    {
        var msg = JsonConvert.SerializeObject(message);
        await _client.PublishAsync(topic, msg);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    private class MqttTopicUnsubscribable : IUnsubscribable
    {
        private readonly Action _unsubscribe;
        private readonly Dictionary<TopicFilter, IDurableMqttClient.MessageCallback> _subscriptions;
        private readonly TopicFilter _topic;

        public MqttTopicUnsubscribable(Action unsubscribe,
            Dictionary<TopicFilter, IDurableMqttClient.MessageCallback> subscriptions,
            TopicFilter topic)
        {
            _unsubscribe = unsubscribe;
            _subscriptions = subscriptions;
            _topic = topic;
        }

        public void Dispose()
        {
            _subscriptions.Remove(_topic);
            _unsubscribe();
        }
    }
}
