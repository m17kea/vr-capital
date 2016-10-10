using Autofac;
using vrc.data.interfaces;

namespace vrc.data.console
{
    public class AppConfig
    {
        public static ContainerBuilder AddConsoleRegistrations(ContainerBuilder builder)
        {
            builder.RegisterType<BrtConsoleLog>().As<ILog>();
            return builder;
        }
    }
}