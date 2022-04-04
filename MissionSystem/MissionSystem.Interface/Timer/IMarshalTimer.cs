namespace MissionSystem.Interface.Timer;
public interface IMarshalTimer : IBaseTimer
{
    public void StartTimer();
    public void StopTimer();
    public void PauseTimer();
    public void ContinueTimer();
}
