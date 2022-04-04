using CTF.Interface;
using CTF.MqttBroker;

namespace CTF.Factory
{
    public class MQTTBrokerFactory
    {
        public static IMQTTBroker GetMQTTBroker()
        {
            return new MQTTBroker();
        }
    }
}
