using Microsoft.Extensions.Hosting;

namespace MissionSystem.Interface;
public interface IMQTTBroker : IHostedService, IDisposable { }
