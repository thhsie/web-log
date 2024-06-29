using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Weblog.Core.AppSettings;
using Weblog.Core.SharedKernel;

namespace Weblog.Core;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection ConfigureAppSettings(this IServiceCollection services) =>
        services
            .AddOptionsWithValidation<ConnectionOptions>()
            .AddOptionsWithValidation<CacheOptions>();

    /// <summary>
    /// Adds options with validation to the service collection.
    /// </summary>
    /// <typeparam name="TOptions">The type of options to add.</typeparam>
    /// <param name="services">The service collection.</param>
    private static IServiceCollection AddOptionsWithValidation<TOptions>(this IServiceCollection services)
        where TOptions : class, IAppOptions
    {
        return services
            .AddOptions<TOptions>()
            .BindConfiguration(TOptions.ConfigSectionPath, binderOptions => binderOptions.BindNonPublicProperties = true)
            .ValidateDataAnnotations()
            .ValidateOnStart()
            .Services;
    }
}