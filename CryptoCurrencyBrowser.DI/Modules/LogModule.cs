using Autofac;
using Microsoft.Extensions.Logging;

namespace CryptoCurrencyBrowser.DI.Modules
{
    public class LogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoggerFactory.Create(x => x.AddConsole());

            builder
                .Register(c => LoggerFactory.Create(x => x.AddConsole()));

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>));
        }
    }
}
