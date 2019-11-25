using Autofac;
using CryptoCurrencyBrowser.DataJob.Jobs;
using CryptoCurrencyBrowser.DataJob.Jobs.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CryptoCurrencyBrowser.DataJob
{
    public static class Host
    {
        public static IConfiguration Configuration { get; private set; }
        
        public static IContainer Container { get; private set; }

        private static ContainerBuilder _containerBuilder { get; set; }

        public static void Bootstrap()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            
            try
            {
                Configuration = builder.Build();
                ConfigureServices(new AgentBootstrapper(Configuration));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void RunJobs()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var coinMarketCapJob = scope.Resolve<CoinMarketCapJob>();
                coinMarketCapJob.Run();
            }
        }

        private static void ConfigureServices<T>(T bootstrapper) where T : IBootstrapper
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.Register(c => Configuration).As<IConfiguration>();
            _containerBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Contains(typeof(IJob)))
                .AsSelf();
            bootstrapper.ConfigureServices(_containerBuilder);
            Container = _containerBuilder.Build();
        }
    }
}
