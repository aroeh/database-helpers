﻿using System.Data;
using System.Data.SqlClient;

namespace MsSqlServerHelpers.DataAccess
{
    public interface ISqlHelperService
    {
        /// <summary>
        /// Validates that the data table has data before trying to parse through and create objects
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        bool DateTableHasData(DataTable table);

        /// <summary>
        /// Validates that the data row has data before trying to parse through and create objects
        /// </summary>
        /// <param name="row"></param>
        /// <returns>bool</returns>
        bool DateRowHasData(DataRow row);

        /// <summary>
        /// Executes the specified stored procedure using the provided parameters
        /// Returns multiple items
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>DataTable</returns>
        DataTable Select(string schema, string storedProcName, SqlParameter[] sqlParameters);

        /// <summary>
        /// Executes the specified stored procedure
        /// Returns multiple items
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <returns>DataTable</returns>
        DataTable Select(string schema, string storedProcName);

        /// <summary>
        /// Executes the supplied query using the provided parameters
        /// Returns multiple items
        /// </summary>
        /// <param name="query"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>DataTable</returns>
        DataTable Select(string query, SqlParameter[] sqlParameters);

        /// <summary>
        /// Executes the sql query
        /// Returns multiple items
        /// </summary>
        /// <param name="query"></param>
        /// <returns>DataTable</returns>
        DataTable Select(string query);

        /// <summary>
        /// Executes the specified stored procedure using the provided parameters
        /// Returns one result
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>DataRow</returns>
        DataRow SelectSingle(string schema, string storedProcName, SqlParameter[] sqlParameters);

        /// <summary>
        /// Executes the specified stored procedure
        /// Returns one result
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <returns>DataRow</returns>
        DataRow SelectSingle(string schema, string storedProcName);

        ///// <summary>
        ///// Executes the supplied query using the provided parameters
        ///// Returns one result
        ///// </summary>
        ///// <param name="query"></param>
        ///// <param name="sqlParameters"></param>
        ///// <returns>DataRow</returns>
        //Task<DataRow> SelectSingle(string query, SqlParameter[] sqlParameters);

        ///// <summary>
        ///// Executes the sql query
        ///// Returns one result
        ///// </summary>
        ///// <param name="query"></param>
        ///// <returns>DataRow</returns>
        //Task<DataRow> SelectSingle(string query);

        /// <summary>
        /// Executes the specified insert stored procedure using the provided parameters
        /// Returns one result
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>int - the id of the new record</returns>
        int Insert(string schema, string storedProcName, SqlParameter[] sqlParameters);

        ///// <summary>
        ///// Executes the supplied insert statement using the provided parameters
        ///// Returns one result
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="sqlParameters"></param>
        ///// <returns>int - the id of the new record</returns>
        //Task<int> Insert(string transaction, SqlParameter[] sqlParameters);

        /// <summary>
        /// Executes the specified update stored procedure using the provided parameters
        /// Returns one result
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="storedProcName"></param>
        /// <param name="sqlParameters"></param>
        /// <returns>int - number of rows affected</returns>
        int Update(string schema, string storedProcName, SqlParameter[] sqlParameters);

        ///// <summary>
        ///// Executes the supplied update statement using the provided parameters
        ///// Returns one result
        ///// </summary>
        ///// <param name="transaction"></param>
        ///// <param name="sqlParameters"></param>
        ///// <returns>bool</returns>
        //Task<bool> Update(string transaction, SqlParameter[] sqlParameters);
    }
}