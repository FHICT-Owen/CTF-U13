using MissionSystem.Data;
using MissionSystem.Data.Repository;
using MissionSystem.Factory;
using MissionSystem.Interface.Models;
using MissionSystem.Interface.MQTT;
using MissionSystem.Interface.Services;
using MissionSystem.Main;
using MissionSystem.Main.Arenas;
using MissionSystem.Main.Effects;
using MissionSystem.Main.Gadgets;
using MissionSystem.Main.GameTypes;
using MissionSystem.Main.MQTT;
using MissionSystem.Main.Time;

var builder = WebApplication.CreateBuilder(args);

// For overriding appsettings
builder.Configuration.AddJsonFile("appsettings.User.json", true);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHostedService<StartupService>();
builder.Services.AddSingleton<ITicker, Ticker>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<ITicker>());

builder.Services.AddSingleton<IGameTimerService, GameTimerService>();

builder.Services.AddScoped<MQTTBrokerFactory>();
builder.Services.AddHostedService<IMQTTBroker>((provider) =>
    MQTTBrokerFactory.GetMQTTBroker(provider.GetRequiredService<ILogger<IMQTTBroker>>()));
builder.Services.AddSingleton<IEffectsService, TlcEffectsService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();

    return new TlcEffectsService(
        config.GetValue("Dmx:Hostname", "localhost"),
        config.GetValue("Dmx:Port", 7351),
        config.GetValue("Dmx:Password", "test"),
        provider.GetRequiredService<ILogger<TlcEffectsService>>()
    );
});
builder.Services.AddHostedService(provider => provider.GetRequiredService<IEffectsService>());

builder.Services.AddSingleton<IMqttClientService, MqttClientService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<IMqttClientService>());

DataStore CreateDatastore(IServiceProvider sp)
{
    var cfg = sp.GetRequiredService<IConfiguration>();
    return new DataStore(cfg);
}

builder.Services.AddSingleton<IGadgetTypeService, GadgetTypeService>(sp =>
    new GadgetTypeService(() => new Repository<GadgetType, int>(CreateDatastore(sp))));
builder.Services.AddSingleton<IGadgetService, GadgetService>(sp =>
    new GadgetService(() => new GadgetRepository(CreateDatastore(sp))));
builder.Services.AddSingleton<IArenaService, ArenaService>(sp =>
    new ArenaService(sp.GetRequiredService<IGameService>(), ()=> new Repository<Arena, int>(CreateDatastore(sp))));

builder.Services.AddSingleton<IGadgetStateService, GadgetStateService>();
builder.Services.AddSingleton<IGadgetSettingsService, GadgetSettingsService>();
builder.Services.AddSingleton<IGameTypeService, GameTypeService>();
builder.Services.AddSingleton<IGameService, GameService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
