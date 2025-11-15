using System;
using System.Data;
using System.Data.SqlClient;
using BBQandGrill.Helpers;

namespace BBQandGrill.Services
{
    /// <summary>
    /// Service for database operations with proper connection management
    /// </summary>
    public class DatabaseService : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DatabaseService()
        {
            _connectionString = ConfigurationHelper.GetConnectionString("bbqConnectionString");
        }

        /// <summary>
        /// Gets an open database connection
        /// </summary>
        public SqlConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(_connectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }

        /// <summary>
        /// Executes a stored procedure and returns a DataSet
        /// </summary>
        public DataSet ExecuteStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(procedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet dataSet = new DataSet();
                        conn.Open();
                        adapter.Fill(dataSet);
                        return dataSet;
                    }
                }
            }
        }

        /// <summary>
        /// Executes a stored procedure and returns the number of rows affected
        /// </summary>
        public int ExecuteNonQuery(string procedureName, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand(procedureName, conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    conn.Open();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                }
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}
