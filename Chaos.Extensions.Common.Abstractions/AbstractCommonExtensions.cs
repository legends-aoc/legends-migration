using Chaos.Common.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

// ReSharper disable once CheckNamespace
namespace Chaos.Extensions.DependencyInjection;

/// <summary>
///     <see cref="Chaos.Common.Abstractions">Abstract Common</see> DI extensions
/// </summary>
public static class AbstractCommonExtensions
{
    /// <summary>
    ///     Adds an option object based on a configuration section to the service collection and ensures any directory bound options are based off
    ///     of a staging directory
    /// </summary>
    /// <param name="services">The service collection to add the options object to</param>
    /// <param name="subSection">If the section is not at the root level, supply the subsection here</param>
    /// <typeparam name="T">The type of the options object</typeparam>
    public static OptionsBuilder<T> AddDirectoryBoundOptionsFromConfig<T>(this IServiceCollection services, string? subSection = null)
        where T: class, IDirectoryBound
    {
        var typeName = typeof(T).Name;
        var path = typeName;

        if (!string.IsNullOrWhiteSpace(subSection))
            path = $"{subSection}:{typeName}";

        return services.AddOptions<T>()
                       .Configure<IConfiguration, IStagingDirectory>(
                           (options, config, stagingDir) =>
                           {
                               config.GetRequiredSection(path).Bind(options, binder => binder.ErrorOnUnknownConfiguration = true);
                               options.UseBaseDirectory(stagingDir.StagingDirectory);
                           });
    }
}