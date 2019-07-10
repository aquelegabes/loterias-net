using System;
using System.ComponentModel;

namespace Loterias.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <returns>The description.</returns>
        /// <param name="someEnum">Some enum.</param>
        public static string GetDescription(this Enum someEnum)
        {
            var memInfo = someEnum.GetType().GetMember(someEnum.ToString());

            if (memInfo?.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs?.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return someEnum.ToString();
        }
    }
}