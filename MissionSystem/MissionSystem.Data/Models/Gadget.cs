using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace MissionSystem.Data.Models;

public class Gadget
{
    /// <summary>
    /// The unique MAC-address of the gadget
    /// </summary>
    [Key]
    public PhysicalAddress MacAddress { get; set; }

    /// <summary>
    /// The type of gadget
    /// </summary>
    public GadgetType Type { get; set; }

    /// <summary>
    /// User-defined name of the gadget
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Arenas in which this gadget can be used
    /// </summary>
    public ICollection<Arena> Arenas { get; set; }
}
