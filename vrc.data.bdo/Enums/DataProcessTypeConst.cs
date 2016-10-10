using vrc.data.bdo.Model;

namespace vrc.data.bdo.Enums
{
    public class DataProcessTypeConst
    {
        public static DataProcessTypeBdo None => new DataProcessTypeBdo
        {
            DataProcessTypeId = 1,
            ProcessType = "None"
        };

        public static DataProcessTypeBdo Csv => new DataProcessTypeBdo
        {
            DataProcessTypeId = 2,
            ProcessType = "csv"
        };

        public static DataProcessTypeBdo Xml => new DataProcessTypeBdo
        {
            DataProcessTypeId = 3,
            ProcessType = "xml"
        };
    }
}
