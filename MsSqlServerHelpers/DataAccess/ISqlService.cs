using MsSqlServerHelpers.Constants;
using System.Data;
using System.Data.SqlClient;

namespace MsSqlServerHelpers.DataAccess
{
    public interface ISqlService
    {
        #region Stored Proc Members

        /// <summary>
        /// Executes Stored Procedure READ with additional parameters to retrieve a DataTable of results
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">schema the stored procedure belongs to.  defaults to dbo</param>
        /// <param name="sqlParameters">additional parameters for the stored procedure</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteReadRows(string storedProcedure, SqlParameter[] sqlParameters, string schema = DataAccessConstants.DefaultSchema);

        /// <summary>
        /// Executes Stored Procedure READ to retrieve a DataTable of results
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">schema the stored procedure belongs to.  defaults to dbo</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteReadRows(string storedProcedure, string schema = DataAccessConstants.DefaultSchema);

        /// <summary>
        /// Executes a transactional stored procedure that expects a single id value to be returned
        /// Well suited for creates and inserts where an entity is normally returned
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
        /// <returns>object</returns>
        object ExecuteScalar(string storedProcedure, SqlParameter[] sqlParameters, string schema = DataAccessConstants.DefaultSchema);

        /// <summary>
        /// Executes a transactional stored procedure that expects a single id value to be returned
        /// Well suited for creates and inserts where an entity is normally returned
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <returns>object</returns>
        object ExecuteScalar(string storedProcedure, string schema = DataAccessConstants.DefaultSchema);

        /// <summary>
        /// Executes a transact SQL statement that returns the number of rows affected
        /// Well suited for updates where we might need to check if any rows were changed to determined success
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
        /// <returns>int - number of rows affected</returns>
        int ExecuteNonQuery(string storedProcedure, SqlParameter[] sqlParameters, string schema = DataAccessConstants.DefaultSchema);

        /// <summary>
        /// Executes a transact SQL statement that returns the number of rows affected
        /// Well suited for updates where we might need to check if any rows were changed to determined success
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
        /// <returns>int - number of rows affected</returns>
        int ExecuteNonQuery(string storedProcedure, string schema = DataAccessConstants.DefaultSchema);

        #endregion Stored Proc Members

        #region Text Command Members

        /// <summary>
        /// Executes text command READ with additional parameters to retrieve a DataTable of results
        /// </summary>
        /// <param name="query">name of the stored procedure to be called</param>
        /// <param name="sqlParameters">additional parameters for the stored procedure</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteTextCommandReadRows(string query, SqlParameter[] sqlParameters);

        /// <summary>
        /// Executes text command READ with additional parameters to retrieve a DataTable of results
        /// </summary>
        /// <param name="query">name of the stored procedure to be called</param>
        /// <returns>DataTable</returns>
        DataTable ExecuteTextCommandReadRows(string query);

        #endregion Text Command Members
    }
}
