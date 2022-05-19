namespace MissionSystem.Main;

public class Match
{
    public string GameTypeId { get; set; }

    public int Duration { get; set; }

    public int ArenaId { get; set; }

    public string[] GadgetIds { get; set; }

    public bool IsEnglish { get; set; }
    
    public bool IsCodeGame { get; set; }
}
