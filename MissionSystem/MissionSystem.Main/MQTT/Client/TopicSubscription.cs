namespace MissionSystem.Main.MQTT.Client;

internal class TopicSubscription : IDisposable
{
    private List<IDurableMqttClient.MessageCallback> _callbacks;
    private IDurableMqttClient.MessageCallback _callback;

    public TopicSubscription(List<IDurableMqttClient.MessageCallback> callbacks, IDurableMqttClient.MessageCallback callback)
    {
        _callbacks = callbacks;
        _callback = callback;
    }
    
    public void Dispose()
    {
        _callbacks.Remove(_callback);
    }
}
