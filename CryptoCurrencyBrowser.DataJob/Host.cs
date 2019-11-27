using Autofac;
using CryptoCurrencyBrowser.Application.Persistence;
using CryptoCurrencyBrowser.DataJob.Jobs.Abstractions;
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

        private static ILogger _logger = LoggerFactory.Create(x => x.AddConsole()).CreateLogger("Host");

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

        public static void StartJobs()
        {
            using (var scope = Container.BeginLifetimeScope())
            {
                var jobs = scope.Resolve<IEnumerable<IJob>>();

                if (jobs == null || !jobs.Any())
                {
                    _logger.LogInformation("No job to run.");
                    return;
                }

                foreach (var job in jobs)
                {
                    // Just to note I know what I'm doing
                    //
                    // I want just to schedule the work of any job to the thread pool
                    // so I don't await in this static void method
                    //
                    // I want to start all possible jobs (currently, there is only one job)
                    // and want them to work on separate threads from thread pool
                    //
                    // Async/Await from bottom to top is possible in the console application
                    // buit either the Main has to be async (I don't want it to be one) or I should call
                    // .Wait() method on the StartJobs() in the Main
                    //
                    // I know Wait() is not desireable, because it stops the main thread from
                    // doing things, but for console apps we can make an exception
                    Task.Run(() => job.DoWork());
                }
            }
        }

        private static void ConfigureServices<T>(T bootstrapper) where T : IBootstrapper
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.Register(c => Configuration).As<IConfiguration>();
            _containerBuilder.RegisterType(typeof(Client))
                .AsImplementedInterfaces();
            _containerBuilder
                .RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.GetInterfaces().Contains(typeof(IJob)))
                .AsImplementedInterfaces();
            _containerBuilder.Register(ctx =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<CryptoCurrencyBrowserDbContext>();
                    optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:CryptoCurrencyBrowserDatabase"]);

                    return new CryptoCurrencyBrowserDbContext(optionsBuilder.Options);
                })
            .As<ICryptoCurrencyBrowserDbContext>()
            .SingleInstance();
            bootstrapper.ConfigureServices(_containerBuilder);
            Container = _containerBuilder.Build();
        }
    }
}
