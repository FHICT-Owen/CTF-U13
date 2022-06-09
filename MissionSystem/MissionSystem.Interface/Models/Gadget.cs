using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using MissionSystem.Util;

namespace MissionSystem.Interface.Models;

public class Gadget
{
    /// <summary>
    /// The unique MAC-address of the gadget
    /// </summary>
    [Key]
    [Required]
    public PhysicalAddress MacAddress { get; set; }

    /// <summary>
    /// The type of gadget
    /// </summary>
    [ForeignKey("TypeId")]
    public GadgetType Type { get; set; }

    /// <summary>
    /// Foreign key for <see cref="Type"/>
    /// </summary>
    [Required]
    [Range(0, Double.MaxValue)] // Trick as [required] does not work
    public int TypeId { get; set; }

    /// <summary>
    /// User-defined name of the gadget
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Arenas in which this gadget can be used
    /// </summary>
    public ICollection<Arena> Arenas { get; set; }
    
    public ICollection<Match> Matches { get; set; }

    public override string ToString()
    {
        return $"{Type} {MacAddress.ToFormattedString()} - {Name}";
    }
}
