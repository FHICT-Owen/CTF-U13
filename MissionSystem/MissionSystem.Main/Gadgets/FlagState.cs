namespace MissionSystem.Main.Gadgets;

public struct FlagState
{
    public long CapturePercentage;
    public long CapturedBy;
    
    public static FlagState FromRaw(Dictionary<string, object> msg)
    {
        try
        {
            return new FlagState
            {
                CapturePercentage = msg["capturePercentage"] as long? ?? 0,
                CapturedBy = msg["capturer"] as long? ?? 0
            };
        }
        catch (KeyNotFoundException e)
        {
            throw new Exception("could not parse flag state", e);
        }
    }
}
