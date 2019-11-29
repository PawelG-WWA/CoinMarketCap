using Autofac;
using CryptoCurrencyBrowser.Application.Cryptocurrencies.AddOrUpdate;
using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.DataJob.Jobs.Abstractions;
using CryptoCurrencyBrowser.DataJob.Jobs.CryptoMartketCapJob.Services;
using CryptoCurrencyBrowser.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CryptoCurrencyBrowser.DataJob
{
    public static class Host
    {
        public static IConfiguration Configuration { get; private set; }

        public static IContainer Container { get; private set; }

        public static bool IsBootstrapSuccessful { get; private set; }

        private static ContainerBuilder _containerBuilder;

        private static ILogger _logger = new LoggerFactory().AddConsole().CreateLogger("Host");

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
                _logger.LogError(ex.Message);
                _logger.LogError(ex.StackTrace);
                return;
            }

            IsBootstrapSuccessful = true;
        }

        public static async Task StartJobs()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var jobs = scope.Resolve<IEnumerable<IJob>>();

                if (jobs == null || !jobs.Any())
                {
                    _logger.LogInformation("No job to run.");
                    return;
                }

                var jobTasks = new List<Task>();

                foreach (var job in jobs)
                {
                    jobTasks.Add(job.DoWork());
                }

                await Task.WhenAll(jobTasks);
            }
        }

        private static void ConfigureServices<T>(T bootstrapper) where T : IBootstrapper
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.Register(c => Configuration).As<IConfiguration>();
            _containerBuilder.RegisterType(typeof(Client))
                .AsImplementedInterfaces();
            _containerBuilder.RegisterType(typeof(AddOrUpdateService))
                .AsImplementedInterfaces();
            _containerBuilder.RegisterType(typeof(CryptoCurrencyMapperService))
                .AsImplementedInterfaces();
            _containerBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Contains(typeof(IJob)))
                .AsImplementedInterfaces();
            _containerBuilder.Register(ctx =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<CryptocurrencyBrowserDbContext>();
                    optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:CryptoCurrencyBrowserDatabase"]);

                    return new CryptocurrencyBrowserDbContext(optionsBuilder.Options);
                })
            .As<ICryptocurrencyBrowserDbContext>()
            .SingleInstance();
            bootstrapper.ConfigureServices(_containerBuilder);
            Container = _containerBuilder.Build();
        }
    }
}
