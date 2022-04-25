using System.ComponentModel.DataAnnotations;

namespace MissionSystem.Data.Models;

public class GadgetType
{
    /// <summary>
    /// Primary key for the gadget type
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Name of the gadget type
    /// </summary>
    public string Name { get; set; }
}
