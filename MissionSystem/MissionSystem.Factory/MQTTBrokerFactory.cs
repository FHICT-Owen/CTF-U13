using MissionSystem.Interface.MQTT;
using MissionSystem.MqttBroker;

namespace MissionSystem.Factory
{
    public class MQTTBrokerFactory
    {
        public static IMQTTBroker GetMQTTBroker()
        {
            return new MQTTBroker();
        }
    }
}
