using System.Data;
using vrc.data.bdo.Model;

namespace vrc.data.app.DataHandlers
{
    public interface IDataHandler
    {
        DataSet ExtractDataTables(DataPropertyBdo dataPropertyBdo);
    }
}
