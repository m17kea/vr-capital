using System.Collections.Generic;

namespace vrc.data.bdo.Model
{
    public class DataPropertyBdo
    {
        public DataPropertyBdo()
        {
            ColumnsToProcess = new List<string>();
        }

        public int DataPropertyId { get; set; }
        public int DataDelimiterId { get; set; }
        public int DataExtensionId { get; set; }
        public string OutputTableName { get; set; }
        public string DataPath { get; set; }
        public int DataProcessTypeId { get; set; }

        public virtual DataDelimiterBdo DataDelimiterBdo { get; set; }
        public virtual DataExtensionBdo DataExtensionBdo { get; set; }
        public virtual DataProcessTypeBdo DataProcessTypeBdo { get; set; }

        public List<string> ColumnsToProcess { get; set; }
    }
}
