using Microsoft.Extensions.Hosting;

namespace Nok.Core.Extensions;

public static class EnvironmentExtensions
{
    /// <summary>
    /// Checks if the current host environment name is Local.
    /// </summary>
    /// <param name="hostEnvironment">An instance of <see cref="IHostEnvironment"/>.</param>
    /// <returns>True if the environment name is Local, otherwise false.</returns>
    public static bool IsLocal(this IHostEnvironment hostEnvironment) => hostEnvironment.IsEnvironment("Local");
}
