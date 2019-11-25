using Autofac;
using CryptoCurrencyBrowser.DI.AutofacExtensions.Builder;
using Microsoft.Extensions.Configuration;

namespace CryptoCurrencyBrowser.DataJob
{
    public interface IBootstrapper
    {
        void ConfigureServices(ContainerBuilder services);
    }

    public class AgentBootstrapper : IBootstrapper
    {
        private IConfiguration _configuration;

        public AgentBootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(ContainerBuilder services)
        {
            services.AddLogModule();
        }
    }
}
