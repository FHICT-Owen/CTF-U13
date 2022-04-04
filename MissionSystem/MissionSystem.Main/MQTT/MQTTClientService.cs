using MissionSystem.Main.MQTT;
using MQTTnet.Client;
using System.Text;

namespace MissionSystem.Main.MQTT;
public class MQTTClientService : IHostedService, IDisposable
{

    public Task StartAsync(CancellationToken cancellationToken)
    {
        MQTTClient client = new MQTTClient();

        client.SetSubscribeTopic("#");

        client.Client.UseApplicationMessageReceivedHandler(async e =>
        {
            Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
        });

        client.ConnectAsync();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}
