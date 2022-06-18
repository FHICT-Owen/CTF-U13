using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using MissionSystem.Data.Repository;
using MissionSystem.Interface.Models;
using MissionSystem.Main.Gadgets;
using Moq;
using NUnit.Framework;

namespace MissionSystem.Tests;

[TestFixture]
public class GadgetServiceTests
{
    [Test]
    public async Task TestGetGadgets()
    {
        var repoMock = new Mock<IRepository<Gadget, PhysicalAddress>>();

        // List of mock gadgets to be returned
        var mockGadgets = new List<Gadget>
        {
            new()
            {
                MacAddress = PhysicalAddress.Parse("00:00:00:00:00:00"),
            },
            new()
            {
                MacAddress = PhysicalAddress.Parse("00:00:00:00:00:01"),
            },
            new()
            {
                MacAddress = PhysicalAddress.Parse("00:00:00:00:00:02"),
            }
        };

        // Setup repository mock to return mock gadgets
        repoMock.Setup(r => r.Get()).ReturnsAsync(mockGadgets);

        var sut = new GadgetService(() => repoMock.Object);

        // Call method under test
        var gadgets = await sut.GetGadgetsAsync();

        // Verify that the gadget service returns the gadgets returned by the repository
        Assert.AreEqual(mockGadgets, gadgets);
    }

    [Test]
    public async Task TestFindGadget()
    {
        var repoMock = new Mock<IRepository<Gadget, PhysicalAddress>>();

        var address = PhysicalAddress.Parse("00:00:00:00:00:02");
        var gadgetToFind = new Gadget()
        {
            MacAddress = address,
        };

        // Setup repository mock to return mock gadget
        repoMock.Setup(r => r.GetById(address)).ReturnsAsync(gadgetToFind);

        var sut = new GadgetService(() => repoMock.Object);
        
        // Call method under test
        var gadget = await sut.FindGadgetAsync(address);

        // Verify that the gadget service correctly returns the gadget returned by the repository
        Assert.AreEqual(gadgetToFind, gadget);
    }

    [Test]
    public async Task TestAddGadget()
    {
        var repoMock = new Mock<IRepository<Gadget, PhysicalAddress>>();

        var gadgetToAdd = new Gadget
        {
            MacAddress = PhysicalAddress.Parse("00:00:00:00:00:00"),
            Name = "test",
            TypeId = 1
        };

        var sut = new GadgetService(() => repoMock.Object);

        var eventTriggered = false;

        sut.Added += gadget =>
        {
            Assert.AreEqual(gadgetToAdd, gadget);
            eventTriggered = true;
        };

        // Call method under test
        await sut.AddGadgetAsync(gadgetToAdd);

        // Make sure the gadget service calls the repository to add the gadget
        repoMock.Verify(r => r.Add(gadgetToAdd));
        
        // Verify that the add-event got triggered for the deleted gadget
        Assert.IsTrue(eventTriggered);
    }

    [Test]
    public async Task TestDeleteGadget()
    {
        var repoMock = new Mock<IRepository<Gadget, PhysicalAddress>>();

        var sut = new GadgetService(() => repoMock.Object);

        var gadgetToDelete = new Gadget()
        {
            MacAddress = PhysicalAddress.Parse("00:00:00:00:00:00"),
            Name = "Flag 1",
            TypeId = 1
        };

        var eventTriggered = false;
        // Detect delete events
        sut.Deleted += gadget =>
        {
            Assert.AreEqual(gadgetToDelete, gadget);
            eventTriggered = true;
        };

        // Call method under test
        await sut.DeleteGadgetAsync(gadgetToDelete);

        // Make sure the gadget service calls the repository to delete the gadget
        repoMock.Verify(r => r.Remove(gadgetToDelete));
        
        // Verify that the delete-event got triggered for the deleted gadget
        Assert.IsTrue(eventTriggered);
    }

    [Test]
    public async Task TestUpdateGadget()
    {
        var repoMock = new Mock<IRepository<Gadget, PhysicalAddress>>();

        var sut = new GadgetService(() => repoMock.Object);
        
        var gadgetToUpdate = new Gadget()
        {
            MacAddress = PhysicalAddress.Parse("00:00:00:00:00:00"),
            Name = "Flag 1",
            TypeId = 1
        };

        // Call method under test
        await sut.UpdateGadgetAsync(gadgetToUpdate);
        
        // Make sure the gadget service calls the repository to update the gadget
        repoMock.Verify(r => r.Update(gadgetToUpdate));
    }
}
