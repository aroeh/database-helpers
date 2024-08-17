using System.Data.SqlClient;
using System.Data;

namespace MsSqlServerHelpers.DataAccess
{
    /// <summary>
    /// Helper SQL class to make it easier to run SQL commands
    /// </summary>
    public class SqlHelperService(ISqlService sql) : ISqlHelperService
    {
        private readonly ISqlService sqlService = sql;

        /// <summary>
        /// Validates that the data table has data before trying to parse through and create objects
        /// </summary>
        /// <param name="table"></param>
        /// <returns>bool</returns>
        public bool DateTableHasData(DataTable table)
        {
            if (table is null || table.Rows is null || table.Rows.Count == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Validates that the data row has data before trying to parse through and create objects
        /// </summary>
        /// <param name="row"></param>
        /// <returns>bool</returns>
        public bool DateRowHasData(DataRow row)
        {
            if (row is null || row.ItemArray is null || row.ItemArray.Count() == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Executes the specified stored procedure using the provided parameters
        /// Returns multiple items
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>DataTable</returns>
        public DataTable Select(string schema, string storedProcName, SqlParameter[] sqlParameters)
        {
            return sqlService.ExecuteReadRows(storedProcName, sqlParameters, schema);
        }

        /// <summary>
        /// Executes the specified stored procedure
        /// Returns multiple items
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <returns>DataTable</returns>
        public DataTable Select(string schema, string storedProcName)
        {
            return sqlService.ExecuteReadRows(storedProcName, schema);
        }

        /// <summary>
        /// Executes the supplied query using the provided parameters
        /// Returns multiple items
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>DataTable</returns>
        public DataTable Select(string query, SqlParameter[] sqlParameters)
        {
            return sqlService.ExecuteTextCommandReadRows(query, sqlParameters);
        }

        /// <summary>
        /// Executes the sql query
        /// Returns multiple items
        /// </summary>
        /// <param name="query"></param>
        /// <returns>DataTable</returns>
        public DataTable Select(string query)
        {
            return sqlService.ExecuteTextCommandReadRows(query);
        }

        /// <summary>
        /// Executes the specified stored procedure using the provided parameters
        /// Returns one result
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>DataRow</returns>
        public DataRow SelectSingle(string schema, string storedProcName, SqlParameter[] sqlParameters)
        {
            DataTable table = sqlService.ExecuteReadRows(storedProcName, sqlParameters, schema);

            bool hasData = DateTableHasData(table);
            if(!hasData)
            {
                return null;
            }

            // The stored procedure being called should only be returning one result
            return table.Rows[0];
        }

        /// <summary>
        /// Executes the specified stored procedure
        /// Returns one result
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <returns>DataRow</returns>
        public DataRow SelectSingle(string schema, string storedProcName)
        {
            DataTable table = sqlService.ExecuteReadRows(storedProcName, schema);

            bool hasData = DateTableHasData(table);
            if (!hasData)
            {
                return null;
            }

            // The stored procedure being called should only be returning one result
            return table.Rows[0];
        }

        ///// <summary>
        ///// Executes the supplied query using the provided parameters
        ///// Returns one result
        ///// </summary>
        ///// <param name="query"></param>
        ///// <param name="sqlParameters"></param>
        ///// <returns>DataRow</returns>
        //public Task<DataRow> SelectSingle(string query, SqlParameter[] sqlParameters)
        //{

        //}

        ///// <summary>
        ///// Executes the sql query
        ///// Returns one result
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns>DataRow</returns>
        //public Task<DataRow> SelectSingle(string query)
        //{

        //}

        /// <summary>
        /// Executes the specified insert stored procedure using the provided parameters
        /// Returns integer id of the newly insert record
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>int - the id of the new record</returns>
        public int Insert(string schema, string storedProcName, SqlParameter[] sqlParameters)
        {
            return (int)sqlService.ExecuteScalar(storedProcName, sqlParameters, schema);
        }

        ///// <summary>
        ///// Executes the supplied insert statement using the provided parameters
        ///// Returns integer id of the newly insert record
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="sqlParameters"></param>
        ///// <returns>int - the id of the new record</returns>
        //public int Insert(string transaction, SqlParameter[] sqlParameters)
        //{

        //}

        /// <summary>
        /// Executes the specified update stored procedure using the provided parameters
        /// Returns one result
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>int - number of rows affected</returns>
        public int Update(string schema, string storedProcName, SqlParameter[] sqlParameters)
        {
            return sqlService.ExecuteNonQuery(storedProcName, sqlParameters, schema);
        }

        ///// <summary>
        ///// Executes the supplied update statement using the provided parameters
        ///// Returns one result
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="sqlParameters"></param>
        ///// <returns>bool</returns>
        //public Task<bool> Update(string transaction, SqlParameter[] sqlParameters)
        //{

        //}
    }
}
