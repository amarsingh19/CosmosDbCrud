namespace Cosmos.Apis
{
    #region <Usings>
    using Cosmos.Apis.Installers;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;
    #endregion

    public static class InstallerExtensions
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Startup)
                                .Assembly
                                .GetExportedTypes()
                                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                                .Select(Activator.CreateInstance)
                                .Cast<IInstaller>()
                                .ToList();

            installers.ForEach(i => i.Configure(services, configuration));
        }
    }
}
