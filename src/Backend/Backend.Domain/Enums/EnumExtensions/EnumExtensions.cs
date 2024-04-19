using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Enums.EnumExtensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get the Description from the DescriptionAttribute.
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var desAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (desAttribute.Length > 0)
            {
                return desAttribute[0].Description;
            }
            return string.Empty;
        }
    }
}
