using System.Net.NetworkInformation;

namespace MissionSystem.Util;

public static class FormatUtils
{
    public static string ToFormattedString(this PhysicalAddress addr, string separator = ":")
    {
        var bytes = addr.GetAddressBytes();
        return string.Join(separator, bytes.Select(z => z.ToString("X2")).ToArray());
    }
}
