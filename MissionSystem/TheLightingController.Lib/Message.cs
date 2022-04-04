namespace TheLightingControllerLib;

public struct Message
{
    public const char ArgSeparator = '|';
    
    public MessageType MessageType;
    public string[] Args;

    public override string ToString()
    {
        return string.Join(ArgSeparator, Args.Prepend(MessageType.Name));
    }

    public static Message FromString(string msg)
    {
        var parts = msg.Split(ArgSeparator);
        
        return new Message()
        {
            MessageType = MessageType.FromName(parts[0]),
            Args = parts.Skip(1).ToArray(),
        };
    }
}
