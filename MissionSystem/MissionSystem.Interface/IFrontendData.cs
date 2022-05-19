namespace MissionSystem.Interface;
public interface IFrontendData
{
    public event EventHandler<string>? init;
    public event EventHandler<string>? updateHandler;
}

