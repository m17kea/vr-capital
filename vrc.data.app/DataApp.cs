using System;
using Autofac.Features.Indexed;
using vrc.data.app.DataHandlers;
using vrc.data.app.Utility;
using vrc.data.bdo.Enums;
using vrc.data.bdo.Model;
using vrc.data.interfaces;

namespace vrc.data.app
{
    public class DataApp : IApp
    {
        private readonly IIndex<string, IDataHandler> _dataHandlerDictionary;
        private readonly ILog _log;
        private readonly ISqlInjector _sqlInjector;

        public DataApp(IIndex<string, IDataHandler> dataHandlerDictionary, ISqlInjector sqlInjector, ILog log)
        {
            _dataHandlerDictionary = dataHandlerDictionary;
            _sqlInjector = sqlInjector;
            _log = log;
        }

        public void Run()
        {
            var dataPropertyBdo = new DataPropertyBdo
            {
                DataDelimiterBdo = DataDelimiterConst.Comma,
                DataPath = @"C:\Source\vr-capital\vrc.data.console\input.csv",
                DataProcessTypeBdo = DataProcessTypeConst.Csv,
                DataExtensionBdo = DataExtensionConst.Csv,
                OutputTableName = "TestTable"
            };

            try
            {
                ProcessDataHandler(dataPropertyBdo);
            }
            catch (Exception ex)
            {
                _log.WriteErrorToPublish("The following DataProperty could not be loaded: " +
                                         dataPropertyBdo.OutputTableName + " (DataPropertyId: " +
                                         dataPropertyBdo.DataPropertyId + ")");
                _log.WriteErrorForDevTeam(ex.ToString());
                _log.WriteErrorToPublish("");
            }
        }

        private void ProcessDataHandler(DataPropertyBdo dataPropertyBdo)
        {
            var dataHandler = _dataHandlerDictionary[dataPropertyBdo.DataProcessTypeBdo.ProcessType];
            var dataSet = dataHandler.ExtractDataTables(dataPropertyBdo);
            _sqlInjector.Inject(dataSet);
        }
    }
}
