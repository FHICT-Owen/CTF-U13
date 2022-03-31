using TheLightingControllerLib;

using var client = new LightingControllerClient("localhost", 7351);

// Connect to QuickDMX with set-password "test"
await client.ConnectAsync("test");

// Press "Test" button
await client.PressButton("test");

// Disconnect
await client.CloseAsync();