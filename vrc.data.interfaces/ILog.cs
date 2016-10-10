namespace vrc.data.interfaces
{
    public interface ILog
    {
        void WriteInternalError(string error);
        void WriteInternalInfo(string info);
        void WriteErrorToPublish(string error);
        void WriteErrorForDevTeam(string error);
        void WriteInfoToPublish(string info);
        void Clear();
        void Publish();
    }
}
