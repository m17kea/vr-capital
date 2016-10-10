using System.Collections.Generic;

namespace vrc.data.bdo.Model
{
    public class DataProcessTypeBdo
    {
        public DataProcessTypeBdo()
        {
            DataPropertyBdos = new List<DataPropertyBdo>();
        }

        public int DataProcessTypeId { get; set; }
        public string ProcessType { get; set; }

        public virtual ICollection<DataPropertyBdo> DataPropertyBdos { get; set; }
    }
}
