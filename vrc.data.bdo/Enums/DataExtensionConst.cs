using vrc.data.bdo.Model;

namespace vrc.data.bdo.Enums
{
    public class DataExtensionConst
    {
        public static DataExtensionBdo Zip => new DataExtensionBdo
        {
            DataExtensionId = 1,
            Extension = ".zip"
        };

        public static DataExtensionBdo Xls => new DataExtensionBdo
        {
            DataExtensionId = 2,
            Extension = ".xls"
        };

        public static DataExtensionBdo Xlsx => new DataExtensionBdo
        {
            DataExtensionId = 3,
            Extension = ".xlsx"
        };

        public static DataExtensionBdo Csv => new DataExtensionBdo
        {
            DataExtensionId = 4,
            Extension = ".csv"
        };

        public static DataExtensionBdo Xml => new DataExtensionBdo
        {
            DataExtensionId = 5,
            Extension = ".xml"
        };

        public static DataExtensionBdo Txt => new DataExtensionBdo
        {
            DataExtensionId = 6,
            Extension = ".txt"
        };
    }
}
