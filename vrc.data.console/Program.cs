using Autofac;
using vrc.data.app;
using vrc.data.interfaces;

namespace vrc.data.console
{
    class Program
    {
        static void Main(string[] args)
        {
            var externalDataConfigOptions = new DataAppConfigOptions
            {
                SqlConnectionString = "Data Source=.;Integrated Security=True",
                OutputDatabaseName = "TestDB"
            };

            var builder = DataAppConfig.AppContainerBuilder(externalDataConfigOptions);
            builder = AppConfig.AddConsoleRegistrations(builder);

            var container = builder.Build();
            var app = container.Resolve<IApp>();
            app.Run();
        }
    }
}
