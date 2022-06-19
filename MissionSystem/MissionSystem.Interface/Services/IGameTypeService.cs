namespace MissionSystem.Interface.Services;

public interface IGameTypeService
{
    public Dictionary<string, IGameType> GameTypes { get; }

    public void RegisterGameType(string key, IGameType type);
}
