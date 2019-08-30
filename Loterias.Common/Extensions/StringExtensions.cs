using System;
using System.Text;
using System.Linq;

#pragma warning disable RCS1220

namespace Loterias.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Splits a string into a maximum number of substrings based on the characters in the bool expression. 
        /// </summary>
        /// <param name="value">String value</param>
        /// <param name="expression">Expression to when split string</param>
        /// <param name="count">The maximum number of substrings to return. Default value is <see cref="Int32.MaxValue"/></param>
        /// <param name="options"><see cref="StringSplitOptions.RemoveEmptyEntries"/> to omit empty array elements from the array returned; or None to include empty array elements in the array returned. Default value is <see cref="StringSplitOptions.None"/></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Any of the parameters are <see cref="null"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Count is negative.</exception>
        /// <exception cref="ArgumentException">options is not one of the <see cref="StringSplitOptions"/> values.</exception>
        /// <returns cref="String[]">An array whose elements contain the substrings in this string that are delimited by one or more characters in separator.</returns>
        public static string[] Split(this string value, Func<char, bool> expression, Int32 count = Int32.MaxValue, StringSplitOptions options = StringSplitOptions.None)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("Base string value cannot be null or white spaced.");
            
            if (expression is null)
                throw new ArgumentNullException("Expression cannot be null");

            var split = value.Where(expression).ToArray();

            try 
            {
                var result = value.Split(split, count, options);
                return result;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

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
