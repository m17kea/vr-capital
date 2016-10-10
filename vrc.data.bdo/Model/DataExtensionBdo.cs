using System.Collections.Generic;

namespace vrc.data.bdo.Model
{
    public class DataExtensionBdo
    {
        public DataExtensionBdo()
        {
            DataPropertyBdos = new List<DataPropertyBdo>();
        }

        public int DataExtensionId { get; set; }
        public string Extension { get; set; }

        public virtual ICollection<DataPropertyBdo> DataPropertyBdos { get; set; }
    }
}
