using System.ComponentModel;
using System.Globalization;
using System.Net.NetworkInformation;

namespace MissionSystem.Util;

public class PhysicalAddressTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
    {
        return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
    }

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
    {
        return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
    }

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return value is string casted
            ? PhysicalAddress.Parse(casted)
            : base.ConvertFrom(context, culture, value);
    }

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value,
        Type destinationType)
    {
        return destinationType == typeof(string)
               && value is PhysicalAddress casted
            ? casted.ToFormattedString()
            : base.ConvertTo(context, culture, value, destinationType);
    }
}
