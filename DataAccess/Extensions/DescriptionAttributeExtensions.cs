using System.ComponentModel;
using System.Reflection;

namespace DataAccess.Extensions;

public static class DescriptionAttributeExtensions
{
    public static string GetDescription<T>(this T value) where T : Enum
    {
        var descriptionAttribute = (DescriptionAttribute)value.GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttributes(typeof(DescriptionAttribute), inherit: false)
            .First();

        return descriptionAttribute.Description;
    }
    
    public static T GetEnumValueFromDescription<T>(this string description) where T : Enum
    {
        if (description is null)
            throw new ArgumentNullException(nameof(description));

        var value = (T)typeof(T)
            .GetFields()
            .First(f 
                => f.GetCustomAttribute<DescriptionAttribute>(false)?.Description == description)
            .GetValue(null);
        
        if (value is null)
            throw new ArgumentException("Not found Enum value for Description attribute.", nameof(description));

        return value;
    }
}