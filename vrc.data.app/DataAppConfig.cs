using System.Data.SqlClient;
using Autofac;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using vrc.data.app.DataHandlers;
using vrc.data.app.Utility;
using vrc.data.interfaces;

namespace vrc.data.app
{
    public class DataAppConfig
    {
        public static ContainerBuilder AppContainerBuilder(DataAppConfigOptions dataAppConfigOptions)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DataApp>().As<IApp>();
            builder.RegisterType<SqlInjector>().As<ISqlInjector>();
            builder.Register(c => GetSqlConnection(dataAppConfigOptions.SqlConnectionString)).As<SqlConnection>();
            builder.RegisterInstance(GetDatabase(dataAppConfigOptions.SqlConnectionString, dataAppConfigOptions.OutputDatabaseName)).As<Database>();
            builder.RegisterType<CsvDataHandler>().Keyed<IDataHandler>("csv");
            return builder;
        }

        private static SqlConnection GetSqlConnection(string sqlConnectionString)
        {
            return new SqlConnection(sqlConnectionString);
        }

        private static Database GetDatabase(string sqlConnectionString, string databaseName)
        {
            var serverConnection = new ServerConnection(GetSqlConnection(sqlConnectionString));
            var server = new Server(serverConnection);
            if (!server.Databases.Contains(databaseName))
            {
                var db = new Database(server, databaseName);
                db.Create();
            }
            return server.Databases[databaseName];
        }
    }
}
