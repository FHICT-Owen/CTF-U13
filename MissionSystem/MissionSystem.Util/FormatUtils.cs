using System.Net.NetworkInformation;

namespace MissionSystem.Util;

public static class FormatUtils
{
    public static string ToFormattedString(this PhysicalAddress addr, string separator = ":")
    {
        var bytes = addr.GetAddressBytes();
        return string.Join(separator, bytes.Select(z => z.ToString("X2")).ToArray());
    }

    // Adapted from https://stackoverflow.com/a/18074585
    public static string ToRelativeString(this DateTime then, DateTime now)
    {
        var ts = now - then;

        if (ts.TotalMinutes < 1)
            return (int) ts.TotalSeconds < 1 ? "just now" : (int) ts.TotalSeconds + " seconds ago";
        if (ts.TotalHours < 1)
            return (int) ts.TotalMinutes == 1 ? "1 minute ago" : (int) ts.TotalMinutes + " minutes ago";
        return ts.TotalDays switch
        {
            < 1 => (int) ts.TotalHours == 1 ? "1 hour ago" : (int) ts.TotalHours + " hours ago",
            < 7 => (int) ts.TotalDays == 1 ? "1 day ago" : (int) ts.TotalDays + " days ago",
            < 30.4368 => (int) (ts.TotalDays / 7) == 1 ? "1 week ago" : (int) (ts.TotalDays / 7) + " weeks ago",
            < 365.242 => (int) (ts.TotalDays / 30.4368) == 1
                ? "1 month ago"
                : (int) (ts.TotalDays / 30.4368) + " months ago",
            _ => (int) (ts.TotalDays / 365.242) == 1 ? "1 year ago" : (int) (ts.TotalDays / 365.242) + " years ago"
        };
    }
}
