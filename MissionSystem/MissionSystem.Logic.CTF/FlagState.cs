namespace MissionSystem.Logic.CTF;

public struct FlagState
{
    public long CapturePercentage;
    public long Capturer;
    public long? CapturedBy;
    public string? Address;
    
    public static FlagState FromRaw(Dictionary<string, object> msg)
    {
        try
        {
            return new FlagState
            {
                CapturePercentage = msg["capturePercentage"] as long? ?? 0,
                Capturer = msg["capturer"] as long? ?? 0
            };
        }
        catch (KeyNotFoundException e)
        {
            throw new Exception("could not parse flag state", e);
        }
    }
}
