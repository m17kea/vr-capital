using System.Data;

namespace vrc.data.app.Utility
{
    public interface ISqlInjector
    {
        void Inject(DataSet dataSet);
    }
}
