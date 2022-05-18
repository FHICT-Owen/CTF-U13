using System.ComponentModel.DataAnnotations;

namespace MissionSystem.Data.Models;

public class Arena
{
    /// <summary>
    /// Primary key for the arena
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Name of the arena
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gadgets which can be used in this arena
    /// </summary>
    public ICollection<Gadget> Gadgets { get; set; }
}
