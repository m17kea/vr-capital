using System;
using System.IO;
using vrc.data.interfaces;

namespace vrc.data.console
{
    public class BrtConsoleLog : ILog
    {
        private StringWriter _logText;

        public BrtConsoleLog()
        {
            _logText = new StringWriter { NewLine = "<br/>" };
        }

        public void WriteInternalError(string error)
        {
            Console.WriteLine(error);
        }

        public void WriteInternalInfo(string info)
        {
            Console.WriteLine(info);
        }

        public void WriteErrorToPublish(string error)
        {
            _logText.Write(error);
        }

        public void WriteErrorForDevTeam(string error)
        {
            _logText.Write(error);
        }

        public void WriteInfoToPublish(string info)
        {
            _logText.Write(info);
        }

        public void Clear()
        {
            _logText = new StringWriter { NewLine = "<br/>" };
        }

        public void Publish()
        {
            if (_logText.ToString() == "") return;
            Console.WriteLine(_logText.ToString());
        }
    }
}
