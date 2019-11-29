using Autofac;
using Microsoft.Extensions.Logging;

namespace CryptoCurrencyBrowser.DI.Modules
{
    public class LogModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => new LoggerFactory().AddConsole()).SingleInstance();

            builder.RegisterGeneric(typeof(Logger<>))
                .As(typeof(ILogger<>));
        }
    }
}
