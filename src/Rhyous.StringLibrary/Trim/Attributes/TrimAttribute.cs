using System;

namespace Rhyous.StringLibrary
{
    /// <summary>
    /// An attribute used by method in the trim extension classes to help them
    /// know when to trim of a string property.
    /// </summary>
    /// <remarks>This is only needed when TrimSettings.TrimByDefault is false.
    /// TrimSettings.TrimByDefault is true by default.</remarks>
    public class TrimAttribute : Attribute
    {
    }
}
