using MissionSystem.Interface.Models;

namespace MissionSystem.Interface;

public interface IGameType
{
    public string Name { get; }
    public int DefaultDuration { get; }

    public bool CanUseGadgetType(GadgetType type);
}
