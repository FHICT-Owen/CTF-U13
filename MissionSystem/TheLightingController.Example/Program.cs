using TheLightingControllerLib;

using var client = new LightingControllerClient("localhost", 7351);

// Connect to QuickDMX with set-password "test"
await client.ConnectAsync("test");

// Update BPM to 120
await client.UpdateBpm(120);

// Disconnect
await client.CloseAsync();
