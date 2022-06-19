namespace MissionSystem.Main.Time;

public interface ITicker : IHostedService, IDisposable
{
    public event EventHandler? Tick;
    public static Ticker? _Ticker;
}