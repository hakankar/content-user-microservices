using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static partial class StringExtensions
    {
        private static readonly Dictionary<char, char> TurkishToEnglishCharMap = new()
        {
            { 'ç', 'c' }, { 'Ç', 'C' },
            { 'ğ', 'g' }, { 'Ğ', 'G' },
            { 'ı', 'i' }, { 'İ', 'I' },
            { 'ö', 'o' }, { 'Ö', 'O' },
            { 'ş', 's' }, { 'Ş', 'S' },
            { 'ü', 'u' }, { 'Ü', 'U' }
        };

        [GeneratedRegex("([a-z0-9])([A-Z])")]
        private static partial Regex PascalCaseToSnakeRegex();

        [GeneratedRegex("^_+")]
        private static partial Regex LeadingUnderscoreRegex();


        public static string ToLowerSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string snake = PascalCaseToSnakeRegex().Replace(input, "$1_$2");
            snake = snake.Replace("İ", "I").Replace("ı", "i");
            snake = LeadingUnderscoreRegex().Replace(snake, "");
            return snake.ToLowerInvariant();
        }

        public static string ToUpperSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            string snake = PascalCaseToSnakeRegex().Replace(input, "$1_$2");
            snake = snake.Replace("İ", "I").Replace("ı", "i");
            snake = LeadingUnderscoreRegex().Replace(snake, "");
            return snake.ToUpperInvariant();
        }

        public static T? GetEnumValueFromDescription<T>(this string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidEnumArgumentException();

            var match = (from f in type.GetFields()
                         from a in f.GetCustomAttributes(typeof(DescriptionAttribute), false)
                         let desc = (DescriptionAttribute)a
                         where desc.Description == description
                         select f).FirstOrDefault();

            return match?.GetRawConstantValue() is T value ? value : default;
        }

        public static int ToInt32(this string str) => int.TryParse(str, out var result) ? result : default;
        public static long ToInt64(this string str) => long.TryParse(str, out var result) ? result : default;
        public static decimal ToDecimal(this string str) => decimal.TryParse(str, out var result) ? result : default;
        public static double ToDouble(this string str) => double.TryParse(str, out var result) ? result : default;
        public static DateTime ToDateTime(this string str) => DateTime.TryParse(str, out var result) ? result : default;

        public static bool IsNotNull(this string? value) => !string.IsNullOrWhiteSpace(value);
        public static bool IsNull(this string? value) => string.IsNullOrWhiteSpace(value);
        public static bool IsNumeric(this string? value) => !string.IsNullOrEmpty(value) && value.All(char.IsNumber);

        public static bool ToBoolean(this string str) => bool.TryParse(str, out var result) && result;
        public static bool ToBoolean(this object obj) => obj != null && Convert.ToBoolean(obj);

        public static string MaskEmail(this string email, int visibleLength = 3)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("Invalid email address.");

            int atIndex = email.IndexOf('@');
            if (visibleLength < 0 || visibleLength > atIndex)
                throw new ArgumentOutOfRangeException(nameof(visibleLength));

            return email[..visibleLength] + "***" + email[atIndex..];
        }

        public static string StringEncoding(this string s) =>
            string.IsNullOrEmpty(s) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(s));

        public static bool Decoding(this string s, out string result)
        {
            result = string.Empty;
            if (string.IsNullOrEmpty(s) || s.Length % 4 != 0) return false;
            if (!Regex.IsMatch(s, "^[a-zA-Z0-9+/]*={0,2}$")) return false;

            Span<byte> bytes = stackalloc byte[s.Length];
            if (Convert.TryFromBase64String(s, bytes, out var written))
            {
                result = Encoding.UTF8.GetString(bytes[..written]);
                return true;
            }
            return false;
        }

        public static string ReplaceTurkishCharacters(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var sb = new StringBuilder(input.Length);
            foreach (var c in input)
                sb.Append(TurkishToEnglishCharMap.TryGetValue(c, out var mapped) ? mapped : c);

            return sb.ToString();
        }
    }
}
