using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using vrc.data.bdo.Model;

namespace vrc.data.app.DataHandlers
{
    public class CsvDataHandler : IDataHandler
    {
        private DataSet _dataSet;
        private DataTable _dataTable;

        private char _delimiter;
        private DataPropertyBdo _dataPropertyBdo;
        private List<string> _headers;
        private StreamReader _streamFile;

        public DataSet ExtractDataTables(DataPropertyBdo dataPropertyBdo)
        {
            _dataSet = new DataSet();
            _dataPropertyBdo = dataPropertyBdo;
            _delimiter = char.Parse(_dataPropertyBdo.DataDelimiterBdo.Character);
            if (File.Exists(_dataPropertyBdo.DataPath))
            {
                using (_streamFile = new StreamReader(_dataPropertyBdo.DataPath))
                {
                    _dataTable = new DataTable
                    {
                        Prefix = "dbo",
                        TableName = dataPropertyBdo.OutputTableName
                    };
                    if (_streamFile.Peek() != -1)
                    {
                        ConfigureColumns();
                        AddRowData();
                        FilterColumns(); //TODO: refactor into SQL injector
                    }
                }
                _dataSet.Tables.Add(_dataTable);
            }
            return _dataSet;
        }

        private void ConfigureColumns()
        {
            var headerLine = _streamFile.ReadLine();

            AddColumnHeadings(headerLine);
            _dataTable.AcceptChanges();
        }

        private void AddColumnHeadings(string headerLine)
        {
            _headers = SplitLine(headerLine, _delimiter);
            foreach (var header in _headers)
            {
                _dataTable.Columns.Add(header);
            }
        }

        private void AddRowData()
        {
            string line;
            while ((line = _streamFile.ReadLine()) != null)
            {
                if (_dataPropertyBdo.DataDelimiterBdo.Description == "None")
                {
                    _dataTable.Rows.Add(line);
                }
                else
                {
                    var row = SplitLine(line, _delimiter);
                    _dataTable.Rows.Add(row.ToArray());
                }
            }
            _dataTable.AcceptChanges();
        }

        private static List<string> SplitLine(string line, char delimiter)
        {
            const RegexOptions options =
                ((RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline) | RegexOptions.IgnoreCase);
            if (line[0] == delimiter)
            {
                line = " " + line;
            }
            if (delimiter == '\t')
            {
                return line.Split(delimiter).ToList();
            }
            var regexString = "(?:^|" + Regex.Escape(delimiter.ToString()) + ")(\\\"(?:[^\\\"]+|\\\"\\\")*\\\"|[^" +
                              Regex.Escape(delimiter.ToString()) + "]*)";
            var reg = new Regex(regexString, options);
            var coll = reg.Matches(line);
            return
                (from Match m in coll select m.Groups[0].Value.Trim('"').Trim(delimiter).Trim('"').Trim()).ToList();
        }

        private void FilterColumns()
        {
            if (_dataPropertyBdo.ColumnsToProcess.Any())
            {
                RemoveColumns();
            }
        }

        private void RemoveColumns()
        {
            var columnsToRemove = _headers.Where(w => !_dataPropertyBdo.ColumnsToProcess.Contains(w));
            foreach (var columnToRemove in columnsToRemove)
            {
                _dataTable.Columns.Remove(columnToRemove);
            }
        }
    }
}
