using System;
using System.Text;

#pragma warning disable RCS1220

namespace Loterias.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Cast the string as enum.
        /// </summary>
        /// <returns>The enum.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">A valid type of <see cref="Enum"/></typeparam>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static T ToEnum<T>(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), $"String cannot be null to cast as {typeof(T)}");

            try
            {
                return (T)Enum.Parse(typeof(T), value, true);
            }
            catch (ArgumentException ex)
            {
                ex.Data["param1"] = value;
                ex.Data["typeparam"] = typeof(T);
                throw;
            }
        }

        /// <summary>
        /// Try to cast as enum.
        /// </summary>
        /// <returns><c>true</c>, if to enum was casted, <c>false</c> otherwise.</returns>
        /// <param name="val">Value.</param>
        /// <param name="enu">A valid <see cref="Enum"/> type.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool TryToEnum<T>(this string val, out T enu)
        {
            enu = default(T);

            Enum.TryParse(typeof(T), val, true, out object result);
            if (result is T)
            {
                enu = (T)result;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Returns a <see cref="byte"/> array string encoded with a chosen charset.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding">Check <see cref="Encoding"/> properties members.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="EncoderFallbackException"></exception>
        /// <returns>The string as <see cref="byte" /> array.</returns>
        public static byte[] ToByteArray(this string str, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(str))
                throw new ArgumentNullException(nameof(str), "In order to encode value cannot be null.");
            if (encoding == null)
                throw new ArgumentNullException(nameof(encoding), "Encoding can not be null.");
            try
            {
                return encoding.GetBytes(str);
            }
            catch (EncoderFallbackException ex)
            {
                ex.Data["param1"] = str;
                ex.Data["param2"] = encoding;
                throw;
            }
            catch (Exception ex)
            {
                ex.Data["param1"] = str;
                ex.Data["param2"] = encoding;
                throw;
            }
        }

        /// <summary>
        /// Returns a <see cref="bool"/> value indicating whether a specified substring occurs within this string.
        /// </summary>
        /// <param name="str">This string.</param>
        /// <param name="substr">Specified string.</param>
        /// <param name="comp">Any <see cref="StringComparison"/> type.</param>
        /// <returns><see cref="true"/> whether contains <see cref="false"/> if not</returns>
        /// <exception cref="ArgumentException">Comparison is not a member of <see cref="StringComparison"/>.</exception>
        /// <exception cref="ArgumentNullException">Value is null</exception>
        public static bool Contains(this string str, string substr, StringComparison comp)
        {
            if (substr == null)
            {
                throw new ArgumentNullException(
                    paramName: nameof(substr),
                    message: "Substring cannot be null.");
            }
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
            {
                throw new ArgumentException(
                    message: "Specified comparison is not a member of StringComparison.",
                    paramName: nameof(comp));
            }

            return str.IndexOf(substr.Trim(), comp) >= 0;
        }
    }
}
