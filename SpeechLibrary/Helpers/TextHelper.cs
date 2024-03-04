using SpeechLibrary.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpeechLibrary.Helpers
{
    public static class TextHelper
    {
        public static string GetLanguageDescription(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();
            }

            var attribute = field.GetCustomAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }

        public static string GetLanguageName(this Enum value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return value.ToString();
            }

            return Enum.GetName(value.GetType(),value) ?? value.ToString();
        }

        public static List<(Enum Value, string Locale, string Name)> GetEnumDescriptions(this Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException("The specified type is not an enum.");
            }

            return Enum.GetValues(enumType).Cast<Enum>()
                       .Select(value => (value, GetLanguageDescription(value), Enum.GetName(enumType, value) ?? string.Empty))
                       .ToList();
        }
    }
}
