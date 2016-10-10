using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;

namespace vrc.data.app.Utility
{
    public class SqlInjector : ISqlInjector
    {
        private readonly Database _database;
        private readonly SqlConnection _sqlConnection;

        public SqlInjector(Database database, SqlConnection sqlConnection)
        {
            _database = database;
            _sqlConnection = sqlConnection;
        }

        public void Inject(DataSet dataSet)
        {
            foreach (DataTable dataTable in dataSet.Tables)
            {
                CheckAndCreateSqlSchema(dataTable);
                CreateOrUpdateSqlTable(dataTable);
                InsertDataTableToSql(dataTable);
            }
        }

        private void CheckAndCreateSqlSchema(DataTable dataTable)
        {
            if (_database.Schemas.Contains(dataTable.Prefix)) return;
            var schema = new Schema(_database, dataTable.Prefix);
            schema.Create();
        }

        private void CreateOrUpdateSqlTable(DataTable dataTable)
        {
            if (!IsExistingTable(dataTable))
            {
                CreateSqlTable(dataTable);
            }
            else
            {
                AddAnyNewColumns(dataTable);
            }
        }

        private bool IsExistingTable(DataTable dataTable)
        {
            return _database.Tables.Contains(dataTable.TableName, dataTable.Prefix);
        }

        private void CreateSqlTable(DataTable dataTable)
        {
            var newTable = new Table(_database, dataTable.TableName, dataTable.Prefix);
            foreach (var column in dataTable.Columns.OfType<DataColumn>())
            {
                newTable.Columns.Add(new Column(newTable, column.ColumnName) { Nullable = true, DataType = DataType.NVarCharMax });
            }
            newTable.Create();
        }

        private void AddAnyNewColumns(DataTable dataTable)
        {
            var sqlColumnNames = (from Column column in _database.Tables[dataTable.TableName, dataTable.Prefix].Columns select TitleCase(column.Name)).ToList();
            var dataTableColumnNames = (from DataColumn dataColumn in dataTable.Columns select TitleCase(dataColumn.ColumnName)).ToList();
            var newColumns = dataTableColumnNames.Except(sqlColumnNames).ToList();
            if (!newColumns.Any()) return;
            foreach (var newColumn in newColumns)
            {
                _database.Tables[dataTable.TableName, dataTable.Prefix].Columns.Add(new Column(_database.Tables[dataTable.TableName, dataTable.Prefix],
                    newColumn) { Nullable = true, DataType = DataType.NVarCharMax });
            }
            _database.Tables[dataTable.TableName, dataTable.Prefix].Alter();
        }

        private static string TitleCase(string input)
        {
            var textInfo = new CultureInfo("en-GB", false).TextInfo;
            return textInfo.ToTitleCase(input);
        }

        private void InsertDataTableToSql(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 0) return;
            try
            {
                _sqlConnection.Open();
                _sqlConnection.ChangeDatabase(_database.Name);
                using (var sqlBulkCopy = new SqlBulkCopy(_sqlConnection, SqlBulkCopyOptions.TableLock, null))
                {
                    sqlBulkCopy.DestinationTableName = "[" + dataTable.Prefix + "].[" + dataTable.TableName + "]";
                    foreach (DataColumn dataColumn in dataTable.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                    }
                    sqlBulkCopy.WriteToServer(dataTable);
                }
            }
            finally
            {
                _sqlConnection.Close();
            }
        }
    }
}
