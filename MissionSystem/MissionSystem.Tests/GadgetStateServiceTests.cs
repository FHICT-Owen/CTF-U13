using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using MissionSystem.Main.Gadgets;
using MissionSystem.Main.MQTT;
using MissionSystem.Main.MQTT.Client;
using MissionSystem.Util;
using Moq;
using NUnit.Framework;

namespace MissionSystem.Tests;

[TestFixture]
public class GadgetStateServiceTests
{
    [Test]
    public void TestNew()
    {
        var mqttServiceClientMock = new Mock<IMqttClientService>();

        // Call constructor under test
        var _ = new GadgetStateService(mqttServiceClientMock.Object);

        // Make sure the gadget state service correctly subscribed to gadget updates on MQTT
        mqttServiceClientMock.Verify(m =>
            m.SubscribeAsync("gadgets/+/state", It.IsAny<IDurableMqttClient.MessageCallback>()));
    }

    [Test]
    public void TestStateUpdatesOfTrigger()
    {
        var mqttServiceClientMock = new Mock<IMqttClientService>();

        IDurableMqttClient.MessageCallback? mqttCallback = null;

        mqttServiceClientMock
            .Setup(m => m.SubscribeAsync(It.IsAny<string>(), It.IsAny<IDurableMqttClient.MessageCallback>()))
            .Callback<string, IDurableMqttClient.MessageCallback>((_, cb) => mqttCallback = cb);

        var address = PhysicalAddress.Parse("00:00:00:00:00:00");
        var messageToReceive = new Dictionary<string, object>
        {
            {"foo", "bar"}
        };

        var sut = new GadgetStateService(mqttServiceClientMock.Object);

        var gotTriggered = false;
        var triggeredAt = DateTime.UnixEpoch;
        Dictionary<string, object>? updateMessage = null;

        // Call method under test
        sut.StateUpdatesOf(address, (stamp, msg) =>
        {
            gotTriggered = true;
            triggeredAt = stamp;
            updateMessage = msg;
        });

        // Early assert to make sure that the mqtt callback got registered by the state service like expected
        Assert.NotNull(mqttCallback);

        // Trigger state update
        mqttCallback!.Invoke("gadgets/00:00:00:00:00:00/state", messageToReceive);

        // Check if state update callback got triggered
        Assert.IsTrue(gotTriggered);
        
        // Update should be of the last update, which is most likely a few nanoseconds
        Assert.LessOrEqual(DateTime.Now - triggeredAt, TimeSpan.FromMilliseconds(5));
        
        // Verify that the state update has the correct message
        Assert.AreSame(messageToReceive, updateMessage);
    }
    
    [Test]
    public void TestStateUpdatesOfWithRememberedState()
    {
        var mqttServiceClientMock = new Mock<IMqttClientService>();

        IDurableMqttClient.MessageCallback? mqttCallback = null;

        mqttServiceClientMock
            .Setup(m => m.SubscribeAsync(It.IsAny<string>(), It.IsAny<IDurableMqttClient.MessageCallback>()))
            .Callback<string, IDurableMqttClient.MessageCallback>((_, cb) => mqttCallback = cb);

        var address = PhysicalAddress.Parse("00:00:00:00:00:00");
        var messageToReceive = new Dictionary<string, object>
        {
            {"foo", "bar"}
        };

        var sut = new GadgetStateService(mqttServiceClientMock.Object);
        
        // Early assert to make sure that the mqtt callback got registered by the state service like expected
        Assert.NotNull(mqttCallback);

        // Trigger state update
        mqttCallback!.Invoke("gadgets/00:00:00:00:00:00/state", messageToReceive);

        var gotTriggered = false;
        Dictionary<string, object>? updateMessage = null;

        // Call method under test
        sut.StateUpdatesOf(address, (stamp, msg) =>
        {
            gotTriggered = true;
            updateMessage = msg;
        });

        // Check if state update callback got triggered
        Assert.IsTrue(gotTriggered);
        
        // Verify that the state update has the correct message with the remembered state
        Assert.AreSame(messageToReceive, updateMessage);
    }
    
    [Test]
    public void TestStateUpdatesOfWrongDevice()
    {
        var mqttServiceClientMock = new Mock<IMqttClientService>();

        IDurableMqttClient.MessageCallback? mqttCallback = null;

        mqttServiceClientMock
            .Setup(m => m.SubscribeAsync(It.IsAny<string>(), It.IsAny<IDurableMqttClient.MessageCallback>()))
            .Callback<string, IDurableMqttClient.MessageCallback>((_, cb) => mqttCallback = cb);

        var messageToReceive = new Dictionary<string, object>
        {
            {"foo", "bar"}
        };

        var sut = new GadgetStateService(mqttServiceClientMock.Object);
        
        var gotTriggered = false;

        // Call method under test
        sut.StateUpdatesOf(PhysicalAddress.Parse("00:00:00:00:00:00"), (_, _) =>
        {
            gotTriggered = true;
        });
        
        // Early assert to make sure that the mqtt callback got registered by the state service like expected
        Assert.NotNull(mqttCallback);
        
        mqttCallback!.Invoke("gadgets/11:11:11:11:11:11/state", messageToReceive);
        
        // Should not get triggered by callback for other devices
        Assert.IsFalse(gotTriggered);
    }
    
    [Test]
    public void TestDispose()
    {
        var mockUnsubscribeable = new Mock<IUnsubscribable>();
        var mqttServiceClientMock = new Mock<IMqttClientService>();


        mqttServiceClientMock
            .Setup(m => m.SubscribeAsync(It.IsAny<string>(), It.IsAny<IDurableMqttClient.MessageCallback>()))
            .ReturnsAsync(() => mockUnsubscribeable.Object);
        

        var sut = new GadgetStateService(mqttServiceClientMock.Object);

        sut.Dispose();
        
        mockUnsubscribeable.Verify(u => u.Dispose());
    }
}
