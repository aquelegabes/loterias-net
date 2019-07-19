using System.Globalization;
using System.Linq;

namespace Loterias.Common.Utils
{
    public static class Utils
    {
        public static bool IsValidCulture(string culture)
        {
            return CultureInfo
                    .GetCultures(CultureTypes.AllCultures)
                    .Any(c => c.Name == culture);
        }
    }
}