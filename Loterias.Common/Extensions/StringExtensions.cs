using System;
using System.Text;

namespace Loterias.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Cast the string as enum
        /// </summary>
        /// <returns>The enum.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T ToEnum<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), $"String cannot be null to cast as {typeof(T)}");
            return (T)Enum.Parse(typeof(T), value, true);
        }

        /// <summary>
        /// Try to cast as enum
        /// </summary>
        /// <returns><c>true</c>, if to enum was casted, <c>false</c> otherwise.</returns>
        /// <param name="val">Value.</param>
        /// <param name="enu">Enu.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool TryToEnum<T>(this string val, out T enu)
        {
            enu = default(T);
            if (string.IsNullOrWhiteSpace(val))
                throw new ArgumentNullException(nameof(val), $"String cannot be null to cast as {typeof(T)}");

            var tr = Enum.TryParse(typeof(T), val, true, out object result);
            if (result is T)
            {
                enu = (T)result;
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns a byte[] string encoded with a chosen charset
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding" cref="Encoding">Check System.Text.Encoding properties members</param>
        /// <example>
        /// <c>foo.ToByteArray(Encoding.UTF8);</c>
        /// </example>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <returns>The string as byte[]</returns>
        public static byte[] ToByteArray(this string str, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str), "In order to encode value can not be null");
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding), "Encoding can not be null");
            try
            {
                return encoding.GetBytes(str);
            }
            catch (EncoderFallbackException ex)
            {
                throw new Exception("Wasn't possible to encode specified value, see inner exception for details", ex);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
