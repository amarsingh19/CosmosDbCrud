namespace Cosmos.Apis.Installers
{
    #region <Usings>
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    #endregion

    public class ConfigureSwagger : IInstaller
    {
        public void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cosmos Crud Apis", Version = "v1" });
            });
        }
    }
}
