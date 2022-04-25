using System.Net.NetworkInformation;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MissionSystem.Util;

namespace MissionSystem.Data.Converters;

public class PhysicalAddressConverter : ValueConverter<PhysicalAddress, string>
{
    public PhysicalAddressConverter()
        : base(
            v => v.ToFormattedString(":"),
            v => PhysicalAddress.Parse(v))
    {
    }
}
