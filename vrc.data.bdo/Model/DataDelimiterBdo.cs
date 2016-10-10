using System.Collections.Generic;

namespace vrc.data.bdo.Model
{
    public class DataDelimiterBdo
    {
        public DataDelimiterBdo()
        {
            DataPropertyBdos = new List<DataPropertyBdo>();
        }

        public int DataDelimiterId { get; set; }
        public string Description { get; set; }
        public string Character { get; set; }

        public virtual ICollection<DataPropertyBdo> DataPropertyBdos { get; set; }
    }
}
