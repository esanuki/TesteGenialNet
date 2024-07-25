using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace TesteGenialNet.Business.Helpers
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }
    }
    
}
