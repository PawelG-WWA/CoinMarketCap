using Autofac;
using CryptoCurrencyBrowser.DI.Modules;

namespace CryptoCurrencyBrowser.DI.AutofacExtensions.Builder
{
    public static class LogBootstrapper
    {
        public static void AddLogModule(this ContainerBuilder builder)
        {
            builder.RegisterModule<LogModule>();
        }
    }
}
