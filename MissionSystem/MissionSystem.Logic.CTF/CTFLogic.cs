using System.Net.NetworkInformation;
using MissionSystem.Interface.Models;
using MissionSystem.Util;
using Newtonsoft.Json;

namespace MissionSystem.Logic.CTF;

public class CTFLogic : BaseGame
{
    private List<Gadget> Gadgets = new List<Gadget>();

    private int capturedBy;
    private Dictionary<string, FlagState> FlagStates = new();
    private int team1Score = 0;
    private int team2Score = 0;
    private IUnsubscribable update;

    public CTFLogic(IServiceProvider provider) : base(provider)
    {

    }

    public override event EventHandler<string>? updateHandler;
    public override event EventHandler<string>? init;

    public override T Get<T>(string variable)
    {
        return default(T);
    }

    public async override Task Setup()
    {
        CreateTimer(1200);
        timer.Update += Update;

        Gadgets = await gadgetService.GetGadgetsAsync();

        foreach (Gadget gadget in Gadgets)
        {
            string address = gadget.MacAddress.ToString();

            update = gadgetStateService.StateUpdatesOf(PhysicalAddress.Parse(address), (timestamp, callback) =>
            {
                FlagState f = FlagState.FromRaw(callback);
                Console.WriteLine($"{f.CapturePercentage}, {f.Capturer}");

                if (f.CapturePercentage == 0 && f.Capturer == 0)
                {
                    f.CapturedBy = 0;
                }

                if (f.CapturePercentage >= 100)
                {
                    f.CapturedBy = (int)f.Capturer;
                }

                f.Address = address;

                if (!FlagStates.ContainsKey(address)) FlagStates.Add(address, f);
                else FlagStates[address] = f;

                Data d = new Data()
                {
                    Team1Score = team1Score,
                    Team2Score = team2Score,
                    FlagStates = FlagStates
                };

                updateHandler?.Invoke(this, JsonConvert.SerializeObject(d));
            });

            gadgetSettingsService.SetSettings(PhysicalAddress.Parse(address), new Dictionary<string, object>()
            {
                {"teamROGColor", 0xFF0000},
                {"teamSFAColor", 0x00FF00},
                {"captured", false},
                {"teamROGCode", "1111"},
                {"teamSFACode", "7777"},
                {"isCodeGame", false},
                {"isEnglish", false},
            });

        }

        // score multiplier?
        // linking IoT devices here?
        // linking teams with codes sent by the devices?
    }

    private async Task GetGadgets()
    {
        Gadgets = await gadgetService.GetGadgetsAsync();
    }

    public override string GetData()
    {
        GetGadgets();
        List<string> addresses = new List<string>();

        foreach (Gadget gadget in Gadgets)
        {
            addresses.Add(gadget.MacAddress.ToString());
        }

        return JsonConvert.SerializeObject(addresses);
    } 

    public async override Task Start()
    {
        //score.SetAll(0);

        // score service:

        // score.Set(score, team)
        // score.Add(score, team)
        // score.Remove(score, team)
        // score.Get(team)
        // score.SetAll(score)
        // score.AddAll(score)
        // score.RemoveAll(score)
        // score.GetAll()
    }

    private async void Update(object? sender, EventArgs args)
    {
        foreach (FlagState state in FlagStates.Values)
        {
            if (state.CapturedBy == 1) team1Score++;
            if (state.CapturedBy == 2) team2Score++;
        }

        if (timer.TimeRemaining == 60) ; // run 1 minute left event

        //score.Add(iotService.devices["flag1"], "capturedBy");
        //score.Add(iotService.devices["flag2"], "capturedBy");
        //score.Add(iotService.devices["flag3"], "capturedBy");

        // score service, add points based on subtopic
        // more specific score logic?

        Data d = new Data()
        {
            Team1Score = team1Score,
            Team2Score = team2Score,
            FlagStates = FlagStates
        };

        updateHandler?.Invoke(this, JsonConvert.SerializeObject(d));
    }

    public struct Data
    {
        public int Team1Score;
        public int Team2Score;
        public Dictionary<string, FlagState> FlagStates;
    }
}
