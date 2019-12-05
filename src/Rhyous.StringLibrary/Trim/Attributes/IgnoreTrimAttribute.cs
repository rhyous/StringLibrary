using System;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An attribute used by method in the trim extension classes to help them
    /// know when to ignore trimming of a string property.
    /// </summary>
    /// <remarks>This is needed when TrimSettings.TrimByDefault is true.
    /// TrimSettings.TrimByDefault is true by default.</remarks>
    public class IgnoreTrimAttribute : Attribute
    {
    }
}
