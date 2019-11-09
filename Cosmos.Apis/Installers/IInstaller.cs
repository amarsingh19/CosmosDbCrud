namespace Cosmos.Apis.Installers
{
    #region <Usings>
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    #endregion

    public interface IInstaller
    {
        void Configure(IServiceCollection services, IConfiguration configuration);
    }
}
