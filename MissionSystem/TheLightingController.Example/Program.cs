using TheLightingControllerLib;

using var client = new LightingControllerClient("localhost", 7351);

// Connect to QuickDMX with set-password "test"
await client.ConnectAsync("test");

// Press "Test" button
await client.UpdateBpm(120);

// Disconnect
await client.CloseAsync();