using Microsoft.Extensions.Hosting;

namespace CTF.Interface;
public interface IMQTTBroker : IHostedService, IDisposable { }
