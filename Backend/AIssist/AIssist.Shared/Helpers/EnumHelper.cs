using System.ComponentModel;
using System.Reflection;

namespace AIssist.Shared.Helpers
{
	public static class EnumHelper
	{
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue
                .GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<DescriptionAttribute>()?
                .Description
                ?? enumValue.ToString();
        }
    }
}

