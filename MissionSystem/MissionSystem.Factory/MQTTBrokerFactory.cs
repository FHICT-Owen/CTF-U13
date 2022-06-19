using Microsoft.Extensions.Logging;
using MissionSystem.Interface.MQTT;
using MissionSystem.MqttBroker;

namespace MissionSystem.Factory
{
    public class MQTTBrokerFactory
    {
        public static IMQTTBroker GetMQTTBroker(ILogger<IMQTTBroker> logger)
        {
            return new MQTTBroker(logger);
        }
    }
}
