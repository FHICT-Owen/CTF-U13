namespace MissionSystem.Main.Time;

public class Ticker : ITicker
{
    private System.Threading.Timer timer = null!;
    public event EventHandler? Tick;
    public static Ticker? _Ticker;

    public Ticker()
    {
        _Ticker = this;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        const int INTERVAL = 1000;
        timer = new System.Threading.Timer(async (sender) => await HandleTimer(), null, TimeSpan.Zero, TimeSpan.FromMilliseconds(INTERVAL));
        return Task.CompletedTask;
    }

    private async Task HandleTimer()
    {
        Tick?.Invoke(this, EventArgs.Empty);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}