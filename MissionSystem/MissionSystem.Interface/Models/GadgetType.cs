using System.ComponentModel.DataAnnotations;

namespace MissionSystem.Interface.Models;

public class GadgetType
{
    /// <summary>
    /// Primary key for the gadget type
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Unique string ID which can be used to refer to this gadget type via code.
    /// </summary>
    public string RefId { get; set; }

    /// <summary>
    /// Name of the gadget type
    /// </summary>
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
