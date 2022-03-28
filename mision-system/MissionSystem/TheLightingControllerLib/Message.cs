namespace TheLightingControllerLib;

public class Message
{
    private static Dictionary<string, Message> _messages = new();
    
    static Message()
    {
        Hello = new Message("HELLO");
        Bpm = new Message("BPM");
        BpmTap = new Message("BPM_TAP");
        Beat = new Message("BEAT");
        AutoBpmOn = new Message("AUTO_BPM_ON");
        AutoBpmOff = new Message("AUTO_BPM_OFF");
        FreezeOn = new Message("FREEZE_ON");
        FreezeOff = new Message("FREEZE_OFF");
        Cue = new Message("CUE");
        ButtonPress = new Message("BUTTON_PRESS");
        ButtonRelease = new Message("BUTTON_RELEASE");
        FaderChange = new Message("FADER_CHANGE");
        TimelinePlayFrom = new Message("TIMELINE_PLAYFROM");
        TimelinePlay = new Message("TIMELINE_PLAY");
        TimelineStop = new Message("TIMELINE_STOP");
        SequentialGo = new Message("SEQUENTIAL_GO");
        SequentialPause = new Message("SEQUENTIAL_PAUSE");
        SequentialStop = new Message("SEQUENTIAL_STOP");
        ButtonList = new Message("BUTTON_LIST");
        BeatOn = new Message("BEAT_ON");
        BeatOff = new Message("BEAT_OFF");
        InterfaceChange = new Message("INTERFACE_CHANGE");
    }

    private Message(string name)
    {
        Name = name;
        _messages[name] = this;
    }

    public static Message FromName(string name)
    {
        return _messages[name];
    }

    public static Message Hello { get; }

    public static Message Bpm { get; }

    public static Message BpmTap { get; }

    public static Message Beat { get; }

    public static Message AutoBpmOn { get; }

    public static Message AutoBpmOff { get; }

    public static Message FreezeOn { get; }

    public static Message FreezeOff { get; }

    public static Message Cue { get; }

    public static Message ButtonPress { get; }

    public static Message ButtonRelease { get; }

    public static Message FaderChange { get; }

    public static Message TimelinePlayFrom { get; }

    public static Message TimelinePlay { get; }

    public static Message TimelineStop { get; }

    public static Message SequentialGo { get; }

    public static Message SequentialPause { get; }

    public static Message SequentialStop { get; }

    public static Message ButtonList { get; }
    
    public static Message BeatOn { get; }
    
    public static Message BeatOff { get; }
    
    public static Message InterfaceChange { get; }

    public string Name { get; }
}