using System.Diagnostics.CodeAnalysis;

namespace TheLightingControllerLib;

[ExcludeFromCodeCoverage]
public class MessageType
{
    private static Dictionary<string, MessageType> _messages = new();
    
    static MessageType()
    {
        Hello = new MessageType("HELLO");
        Error = new MessageType("ERROR");
        Bpm = new MessageType("BPM");
        BpmTap = new MessageType("BPM_TAP");
        Beat = new MessageType("BEAT");
        AutoBpmOn = new MessageType("AUTO_BPM_ON");
        AutoBpmOff = new MessageType("AUTO_BPM_OFF");
        FreezeOn = new MessageType("FREEZE_ON");
        FreezeOff = new MessageType("FREEZE_OFF");
        Cue = new MessageType("CUE");
        ButtonPress = new MessageType("BUTTON_PRESS");
        ButtonRelease = new MessageType("BUTTON_RELEASE");
        FaderChange = new MessageType("FADER_CHANGE");
        TimelinePlayFrom = new MessageType("TIMELINE_PLAYFROM");
        TimelinePlay = new MessageType("TIMELINE_PLAY");
        TimelineStop = new MessageType("TIMELINE_STOP");
        SequentialGo = new MessageType("SEQUENTIAL_GO");
        SequentialPause = new MessageType("SEQUENTIAL_PAUSE");
        SequentialStop = new MessageType("SEQUENTIAL_STOP");
        ButtonList = new MessageType("BUTTON_LIST");
        BeatOn = new MessageType("BEAT_ON");
        BeatOff = new MessageType("BEAT_OFF");
        InterfaceChange = new MessageType("INTERFACE_CHANGE");
    }

    private MessageType(string name)
    {
        Name = name;
        _messages[name] = this;
    }

    public static MessageType FromName(string name)
    {
        return _messages[name];
    }

    public static MessageType Hello { get; }
    
    public static MessageType Error { get; }

    public static MessageType Bpm { get; }

    public static MessageType BpmTap { get; }

    public static MessageType Beat { get; }

    public static MessageType AutoBpmOn { get; }

    public static MessageType AutoBpmOff { get; }

    public static MessageType FreezeOn { get; }

    public static MessageType FreezeOff { get; }

    public static MessageType Cue { get; }

    public static MessageType ButtonPress { get; }

    public static MessageType ButtonRelease { get; }

    public static MessageType FaderChange { get; }

    public static MessageType TimelinePlayFrom { get; }

    public static MessageType TimelinePlay { get; }

    public static MessageType TimelineStop { get; }

    public static MessageType SequentialGo { get; }

    public static MessageType SequentialPause { get; }

    public static MessageType SequentialStop { get; }

    public static MessageType ButtonList { get; }
    
    public static MessageType BeatOn { get; }
    
    public static MessageType BeatOff { get; }
    
    public static MessageType InterfaceChange { get; }
    
    public string Name { get; }
}
