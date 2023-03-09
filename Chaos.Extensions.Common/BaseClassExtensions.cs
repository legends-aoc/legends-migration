using Chaos.Common.Definitions;

namespace Chaos.Extensions.Common;

/// <summary>
///     Provides extensions methods for <see cref="Chaos.Common.Definitions.BaseClass" />
/// </summary>
public static class BaseClassExtensions
{
    /// <summary>
    ///     Determines whether or not a BaseClass contains another BaseClass
    /// </summary>
    /// <param name="baseClass">The encompassing baseClass</param>
    /// <param name="other">The other baseClass</param>
    /// <returns><c>true</c> if <paramref name="baseClass" /> contains <paramref name="other" />, otherwise <c>false</c></returns>
    public static bool ContainsClass(this BaseClass baseClass, BaseClass other) => other is BaseClass.Peasant
                                                                                   || baseClass switch
                                                                                   {
                                                                                       //Diacht "is" all classes
                                                                                       BaseClass.Diacht => true,
                                                                                       _                => baseClass == other
                                                                                   };
}