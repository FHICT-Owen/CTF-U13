using MissionSystem.Main.Time;
using MissionSystem.Main.MQTT;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using MissionSystem.Interface;
using MissionSystem.Factory;

var builder = WebApplication.CreateBuilder(args);

// For overriding appsettings
builder.Configuration.AddJsonFile("appsettings.User.json", true);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<ITicker, Ticker>();
builder.Services.AddHostedService(provider => provider.GetRequiredService<ITicker>());

builder.Services.AddSingleton<GameTimerService>();

builder.Services.AddScoped<MQTTBrokerFactory>();
builder.Services.AddHostedService<IMQTTBroker>((provider) => MQTTBrokerFactory.GetMQTTBroker());

builder.Services.AddHostedService<MQTTClientService>();

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
