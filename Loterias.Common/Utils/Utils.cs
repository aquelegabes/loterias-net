using System.Globalization;
using System.Linq;
using System;

namespace Loterias.Common.Utils
{
    public static class Utils
    {
        /// <summary>
        /// Checks if a specified culture is valid.
        /// </summary>
        /// <param name="culture">A culture type.</param>
        /// <returns>True whether the culture is valid, false if not.</returns>
        /// <exception cref="ArgumentNullException" />
        public static bool IsValidCulture(string culture)
        {
            if (string.IsNullOrWhiteSpace(culture))
                throw new ArgumentNullException(nameof(culture), "Culture cannot be null");

            return CultureInfo
                    .GetCultures(CultureTypes.AllCultures)
                    .Any(c => c.Name == culture);
        }
    }
}