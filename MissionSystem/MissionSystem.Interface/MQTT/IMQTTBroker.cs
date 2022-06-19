using Microsoft.Extensions.Hosting;

namespace MissionSystem.Interface.MQTT;
public interface IMQTTBroker : IHostedService, IDisposable { }
