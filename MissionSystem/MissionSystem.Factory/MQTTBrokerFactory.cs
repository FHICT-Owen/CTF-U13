using MissionSystem.Interface;
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
