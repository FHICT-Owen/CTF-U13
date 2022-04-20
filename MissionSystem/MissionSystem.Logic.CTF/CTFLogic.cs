using MissionSystem.Interface;
using Newtonsoft.Json;
using System.Net.NetworkInformation;

namespace MissionSystem.Logic.CTF;
public class CTFLogic : BaseGame
{
    private int capturedBy;
    private FlagState current;
    private int team1Score = 0;
    private int team2Score = 0;
    private IUnsubscribable update;

    public CTFLogic(IServiceProvider provider) : base(provider) { }

    public override event EventHandler<string>? data;

    public async override Task Setup()
    {
        CreateTimer(600);
        timer.Update += Update;

        update = gadgetStateService.StateUpdatesOf(PhysicalAddress.Parse("01-23-45-67-89-0A"), (callback) => {
            current = FlagState.FromRaw(callback);
            Console.WriteLine(current.CapturePercentage);

            if (current.CapturePercentage >= 100)
            {
                capturedBy = (int)current.Capturer;
            }
        });

        // score multiplier?
        // linking IoT devices here?
        // linking teams with codes sent by the devices?
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
        if (capturedBy == 0) return;

        if (capturedBy == 1) team1Score += 1;
        if (capturedBy == 2) team2Score += 1;

        Data d = new Data() {
            Team1Score = team1Score,
            Team2Score = team2Score,
            CapturedBy = capturedBy,
            FlagState = current
        };

        data?.Invoke(this, JsonConvert.SerializeObject(d));

        if (timer.TimeRemaining == 60) ; // run 1 minute left event

        //score.Add(iotService.devices["flag1"], "capturedBy");
        //score.Add(iotService.devices["flag2"], "capturedBy");
        //score.Add(iotService.devices["flag3"], "capturedBy");

        // score service, add points based on subtopic
        // more specific score logic?


    }

    public struct Data
    {
        public int Team1Score;
        public int Team2Score;
        public int CapturedBy;
        public FlagState FlagState;
    }
}