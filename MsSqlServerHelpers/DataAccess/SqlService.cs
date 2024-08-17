﻿using MsSqlServerHelpers.Constants;
using System.Data;
using System.Data.SqlClient;

namespace MsSqlServerHelpers.DataAccess
{
    /// <summary>
    /// SQL Service class that is solely responsible for executing database operations
    /// </summary>
    public class SqlService : ISqlService
    {
        private readonly string connectionString = Environment.GetEnvironmentVariable(DataAccessConstants.MsSqlConn);


        #region Stored Procedure Members

        /// <summary>
        /// Creates a Command with a type of Stored Procedure and adds additional parameters
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storedProcedure"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        private static SqlCommand StoredProcCommand(SqlConnection connection, string storedProcedure, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new(storedProcedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (sqlParameters != null && sqlParameters.Length > 0)
            {
                command.Parameters.AddRange(sqlParameters);
            }

            return command;
        }

        /// <summary>
        /// Creates a Command with a type of Stored Procedure
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        private static SqlCommand StoredProcCommand(SqlConnection connection, string storedProcedure)
        {
            return new SqlCommand(storedProcedure, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }

        /// <summary>
        /// Executes Stored Procedure READ with additional parameters to retrieve a DataTable of results
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">schema the stored procedure belongs to.  defaults to dbo</param>
        /// <param name="sqlParameters">additional parameters for the stored procedure</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteReadRows(string storedProcedure, SqlParameter[] sqlParameters, string schema = DataAccessConstants.DefaultSchema)
        {
            DataTable table = new();
            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = StoredProcCommand(connection, $"{schema}.{storedProcedure}", sqlParameters);

                connection.Open();

                SqlDataAdapter adapter = new(command);
                adapter.Fill(table);

                connection.Close();
            }

            return table;
        }

        /// <summary>
        /// Executes Stored Procedure READ to retrieve a DataTable of results
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">schema the stored procedure belongs to.  defaults to dbo</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteReadRows(string storedProcedure, string schema = DataAccessConstants.DefaultSchema)
        {
            DataTable table = new();
            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = StoredProcCommand(connection, $"{schema}.{storedProcedure}");

                connection.Open();

                SqlDataAdapter adapter = new(command);
                adapter.Fill(table);

                connection.Close();
            }

            return table;
        }

        /// <summary>
        /// Executes a transactional stored procedure that expects a single id value to be returned
        /// Well suited for creates and inserts where an entity is normally returned
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
        /// <returns>object</returns>
        public object ExecuteScalar(string storedProcedure, SqlParameter[] sqlParameters, string schema = DataAccessConstants.DefaultSchema)
        {
            object result;

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = StoredProcCommand(connection, $"{schema}.{storedProcedure}", sqlParameters);

                connection.Open();

                result = command.ExecuteScalar();

                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Executes a transactional stored procedure that expects a single id value to be returned
        /// Well suited for creates and inserts where an entity is normally returned
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <returns>object</returns>
        public object ExecuteScalar(string storedProcedure, string schema = DataAccessConstants.DefaultSchema)
        {
            object result;

            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = StoredProcCommand(connection, $"{schema}.{storedProcedure}");

                connection.Open();

                result = command.ExecuteScalar();

                connection.Close();
            }

            return result;
        }

        /// <summary>
        /// Executes a transact SQL statement that returns the number of rows affected
        /// Well suited for updates where we might need to check if any rows were changed to determined success
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
        /// <returns>int - number of rows affected</returns>
        public int ExecuteNonQuery(string storedProcedure, SqlParameter[] sqlParameters, string schema = DataAccessConstants.DefaultSchema)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = StoredProcCommand(connection, $"{schema}.{storedProcedure}", sqlParameters);

                connection.Open();

                affectedRows = command.ExecuteNonQuery();

                connection.Close();
            }

            return affectedRows;
        }

        /// <summary>
        /// Executes a transact SQL statement that returns the number of rows affected
        /// Well suited for updates where we might need to check if any rows were changed to determined success
        /// </summary>
        /// <param name="storedProcedure">name of the stored procedure to be called</param>
        /// <param name="schema">default parameter for the schema</param>
        /// <param name="sqlParameters">Optional parameter - if included params are added to the SQL command</param>
        /// <returns>int - number of rows affected</returns>
        public int ExecuteNonQuery(string storedProcedure, string schema = DataAccessConstants.DefaultSchema)
        {
            int affectedRows = 0;

            using (var connection = new SqlConnection(connectionString))
            {
                var command = StoredProcCommand(connection, $"{schema}.{storedProcedure}");

                connection.Open();

                affectedRows = command.ExecuteNonQuery();

                connection.Close();
            }

            return affectedRows;
        }

        #endregion Stored Procedure Members

        #region Text Command Members

        /// <summary>
        /// Creates a Command with a type of Stored Procedure and adds additional parameters
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlCommand"></param>
        /// <param name="sqlParameters"></param>
        /// <returns></returns>
        private static SqlCommand TextCommand(SqlConnection connection, string sqlCommand, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new(sqlCommand, connection)
            {
                CommandType = CommandType.Text
            };

            if (sqlParameters != null && sqlParameters.Length > 0)
            {
                command.Parameters.AddRange(sqlParameters);
            }

            return command;
        }

        /// <summary>
        /// Creates a Command with a type of Stored Procedure
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlCommand"></param>
        /// <returns></returns>
        private static SqlCommand TextCommand(SqlConnection connection, string sqlCommand)
        {
            return new SqlCommand(sqlCommand, connection)
            {
                CommandType = CommandType.Text
            };
        }

        /// <summary>
        /// Executes text command READ with additional parameters to retrieve a DataTable of results
        /// </summary>
        /// <param name="query">name of the stored procedure to be called</param>
        /// <param name="sqlParameters">additional parameters for the stored procedure</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteTextCommandReadRows(string query, SqlParameter[] sqlParameters)
        {
            DataTable table = new();
            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = TextCommand(connection, query, sqlParameters);

                connection.Open();

                SqlDataAdapter adapter = new(command);
                adapter.Fill(table);

                connection.Close();
            }

            return table;
        }

        /// <summary>
        /// Executes text command READ with additional parameters to retrieve a DataTable of results
        /// </summary>
        /// <param name="query">name of the stored procedure to be called</param>
        /// <returns>DataTable</returns>
        public DataTable ExecuteTextCommandReadRows(string query)
        {
            DataTable table = new();
            using (SqlConnection connection = new(connectionString))
            {
                SqlCommand command = TextCommand(connection, query);

                connection.Open();

                SqlDataAdapter adapter = new(command);
                adapter.Fill(table);

                connection.Close();
            }

            return table;
        }

        #endregion Text Command Members
    }
}
