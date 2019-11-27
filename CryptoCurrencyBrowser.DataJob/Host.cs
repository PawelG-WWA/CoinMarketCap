using Autofac;
using CryptoCurrencyBrowser.DataJob.Jobs.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
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

                foreach(var job in jobs)
                {
                    // I want just to schedule the work of any job to the thread pool
                    // so I don't await
                    //
                    // I want to start all possible jobs (currently, there is only one job)
                    // and want them to work on separate threads from thread pool
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
            bootstrapper.ConfigureServices(_containerBuilder);
            Container = _containerBuilder.Build();
        }
    }
}
