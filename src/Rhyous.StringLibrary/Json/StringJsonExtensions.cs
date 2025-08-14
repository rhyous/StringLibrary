namespace Rhyous.StringLibrary
{
    /// <summary>Extension methods for Json that don't require a third party library.</summary>
    public static class StringJsonExtensions
    {
        /// <summary>Checks if the string is a Json array.</summary>
        /// <param name="value">The string value.</param>
        /// <returns>True if it is appears naively to be a json array, false otherwise.</returns>
        public static bool IsJsonArray(this string value)
        {
            value = value?.Trim();
            return !string.IsNullOrWhiteSpace(value)
                && value.Length - value.Unwrap('[', ']').Length == 2;
        }

        /// <summary>Checks if the string is an empty Json array.</summary>
        /// <param name="value">The string value.</param>
        /// <returns>True if it is an empty array, false otherwise.</returns>
        /// <remarks>
        /// Supports: [], [ ] "[\r\n]", or any trimmed string that starts with
        /// [, and ends with ], and has nothing or only whitespace in between.
        /// </remarks>
        public static bool IsEmptyJsonArray(this string value)
        {
            value = value?.Trim();
            return !string.IsNullOrWhiteSpace(value)
                && value.Unwrap('[', ']').Trim().Length == 0;
        }

        /// <summary>Checks if the string is a Json object.</summary>
        /// <param name="value">The string value.</param>
        /// <returns>True if it is appears naively to be a json object, false otherwise.</returns>
        public static bool IsJsonObject(this string value)
        {
            value = value?.Trim();
            return !string.IsNullOrWhiteSpace(value)
                && value.Length - value.Unwrap('{', '}').Length == 2;
        }

        /// <summary>Checks if the string is an empty Json object.</summary>
        /// <param name="value"></param>
        /// <returns>True if it is an empty array, false otherwise.</returns>
        /// <remarks>
        /// Supports: {}, { }, "{\r\n}, or any trimmed string that starts with
        /// {, and ends with }, and has nothing or only whitespace in between.".
        /// </remarks>
        public static bool IsEmptyJsonObject(this string value)
        {
            value = value?.Trim();
            return !string.IsNullOrWhiteSpace(value)
                && value.Unwrap('{', '}').Trim().Length == 0;
        }
    }
}
