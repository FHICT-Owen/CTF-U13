namespace MissionSystem.Main.Gadgets;

public struct FlagState
{
    public bool BeingCaptured;
    public string CapturedBy;
    
    public static FlagState FromRaw(Dictionary<string, object> msg)
    {
        try
        {
            return new FlagState
            {
                BeingCaptured = msg["beingCaptured"] as bool? ?? false,
                CapturedBy = msg["capturedBy"] as string ?? string.Empty
            };
        }
        catch (KeyNotFoundException e)
        {
            throw new Exception("could not parse flag state", e);
        }
    }
}
