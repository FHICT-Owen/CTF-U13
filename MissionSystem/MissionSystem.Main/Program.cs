using MissionSystem.Factory;
using MissionSystem.Interface.MQTT;
using MissionSystem.Interface.Services;
using MissionSystem.Main;
using MissionSystem.Main.Arenas;
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
builder.Services.AddHostedService<IMQTTBroker>((provider) => MQTTBrokerFactory.GetMQTTBroker());

builder.Services.AddSingleton<IMqttClientService, MqttClientService>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<IMqttClientService>());

builder.Services.AddSingleton<IGadgetTypeService, GadgetTypeService>();
builder.Services.AddSingleton<IGadgetService, GadgetService>();
builder.Services.AddSingleton<IGadgetStateService, GadgetStateService>();
builder.Services.AddSingleton<IGadgetSettingsService, GadgetSettingsService>();
builder.Services.AddSingleton<IGameTypeService, GameTypeService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IArenaService, ArenaService>();

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
