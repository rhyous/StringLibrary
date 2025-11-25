using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// A class that allows strings to act somewhat similar to enums, in that only certain strings can be allowed in set.
    /// </summary>
    public class AllowedString
    {
        /// <summary>
        /// The constructor for the AllowedString
        /// </summary>
        /// <param name="allowedValues"></param>
        /// <param name="defaultValue"></param>
        public AllowedString(IEnumerable<string> allowedValues, string defaultValue = null)
        {
            if (allowedValues == null || !allowedValues.Any())
                throw new ArgumentNullException("allowedValues", "You must specificy one or more allowed values.");
            AllowedValues = new ReadOnlyCollection<string>(allowedValues.ToList());
            DefaultValue = string.IsNullOrWhiteSpace(defaultValue) ? allowedValues.First() : defaultValue;
            _Value = DefaultValue; // Default value could be a value that is outside of the allowed values.
        }

        /// <summary>
        /// The property holding the current string value.
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set
            {
                if (AllowedValues.Contains(value))
                    _Value = value;
                else
                    throw new ValueNotAllowedException($"Value ${value} is not allowed. Allowed values include: {string.Join(", ", AllowedValues)}", nameof(value));
            }
        }
        private string _Value;

        /// <summary>
        /// A list of allowed strings.
        /// </summary>
#if NET40
        public ICollection<string> AllowedValues { get; }
#else
        public IReadOnlyCollection<string> AllowedValues { get; }
#endif

        /// <summary>
        /// The propertly holding the default value.
        /// </summary>
        public string DefaultValue { get; }
    }
}