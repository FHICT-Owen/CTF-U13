using TheLightingControllerLib;

var client = new LightingControllerClient();

// Connect to QuickDMX with set-password "test"
await client.ConnectAsync(new Uri("ws://localhost:7351/websocket"), "test");

// Set BPM To 120
await client.SendMessageAsync(Message.Bpm, "120");

// Disconnect
await client.CloseAsync();