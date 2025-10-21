//using System;
//using System.Data;
//using Alkamous.Controller;
//using System.Collections;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Threading.Tasks;
//using Alkamous.InterfaceForAllClass;

//namespace Alkamous.Model
//{
//    /// <summary>
//    /// Data Access Layer (DAL) class that handles all database operations
//    /// Implements connection management, CRUD operations, and stored procedure execution
//    /// </summary>
//    public class DataAccessLayer: IDataAccessLayer
//    {
//        #region Declare variables ( SqlConnection, SqlCommand,DataTable)
//        /// <summary>
//        /// SQL Connection instance for database connectivity
//        /// </summary>
//        private readonly SqlConnection _sqlcon = new SqlConnection();

//        /// <summary>
//        /// SQL Command instance for executing queries and stored procedures
//        /// </summary>
//        private readonly SqlCommand _cmd = new SqlCommand();

//        /// <summary>
//        /// Database connection configuration parameters loaded from application settings
//        /// </summary>
//        private readonly string _serverName = Properties.Settings.Default.ServerName;
//        private readonly string _database = Properties.Settings.Default.Database;
//        private readonly string _userId = Properties.Settings.Default.Userid;
//        private readonly string _password = Properties.Settings.Default.password;
//        private readonly string _connectTimeout = Properties.Settings.Default.ConnectTimeout;

//        #endregion

//        #region Connection String initialization
//        /// <summary>
//        /// Initializes a new instance of the DataAccessLayer class
//        /// Sets up the database connection and configures the command object
//        /// </summary>
//        public DataAccessLayer()
//        {
//            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder
//            {
//                DataSource = _serverName,
//                InitialCatalog = _database,
//                UserID = _userId,
//                Password = _password
//            };

//            _sqlcon = new SqlConnection(sqlConnectionString.ConnectionString);

//            _cmd.Connection = _sqlcon;
//            _cmd.CommandType = CommandType.StoredProcedure;
//        }
//        #endregion

//        #region Connection Management Methods
//        /// <summary>
//        /// Opens the database connection if it's not already open
//        /// </summary>
//        private void OpenCN()
//        {
//            if (_sqlcon.State != ConnectionState.Open)
//                _sqlcon.Open();
//        }

//        /// <summary>
//        /// Closes the database connection if it's open
//        /// </summary>
//        private void CloseCN()
//        {
//            if (_sqlcon.State == ConnectionState.Open)
//                _sqlcon.Close();
//        }
//        #endregion

//        #region Stored Procedure Execution Methods
//        /// <summary>
//        /// Executes a stored procedure with parameters for INSERT, UPDATE, or DELETE operations
//        /// </summary>
//        /// <param name="StoredProcedureName">Name of the stored procedure to execute</param>
//        /// <param name="ParVal">Parameters for the stored procedure as key-value pairs</param>
//        /// <returns>Number of rows affected or error code</returns>
//        public int RunProcedure(string StoredProcedureName, SortedList ParVal)
//        {
//            try
//            {
//                _cmd.CommandText = StoredProcedureName;
//                _cmd.Parameters.Clear();

//                for (int I = 0; I < ParVal.Count; I++)
//                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));

//                OpenCN();
//                int rRusult = _cmd.ExecuteNonQuery();
//                return rRusult;
//            }
//            catch (SqlException ex)
//            {
//                Chelp.WriteErrorLog("Error in RunProcedure: " + ex.Message);
//                return ex.Number;
//            }
//            finally
//            {
//                CloseCN();
//            }
//        }

//        /// <summary>
//        /// Asynchronously executes a stored procedure with parameters
//        /// </summary>
//        /// <param name="StoredProcedureName">Name of the stored procedure to execute</param>
//        /// <param name="ParVal">Parameters for the stored procedure as key-value pairs</param>
//        /// <returns>Number of rows affected or error code</returns>
//        public async Task<int> RunProcedureasync(string StoredProcedureName, SortedList ParVal)
//        {
//            try
//            {
//                _cmd.CommandText = StoredProcedureName;
//                _cmd.Parameters.Clear();

//                for (int I = 0; I < ParVal.Count; I++)
//                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));

//                OpenCN();
//                int rRusult = await _cmd.ExecuteNonQueryAsync();
//                return rRusult;
//            }
//            catch (SqlException ex)
//            {
//                Chelp.WriteErrorLog("Error in RunProcedure: " + ex.Message);
//                return ex.Number;
//            }
//            finally
//            {
//                CloseCN();
//            }
//        }
//        #endregion

//        #region Bulk Operations
//        /// <summary>
//        /// Executes a stored procedure multiple times with different parameter sets
//        /// Useful for bulk insert, update, or delete operations
//        /// </summary>
//        /// <param name="StoredProcedureName">Name of the stored procedure to execute</param>
//        /// <param name="parameterValues">List of parameter sets for multiple executions</param>
//        /// <returns>Total number of rows affected across all executions</returns>
//        public int RunProcedureBulk(string StoredProcedureName, List<SortedList> parameterValues)
//        {
//            try
//            {
//                int totalRowsAffected = 0;
//                _cmd.CommandText = StoredProcedureName;

//                OpenCN();

//                foreach (SortedList parameterList in parameterValues)
//                {
//                    _cmd.Parameters.Clear();
//                    for (int i = 0; i < parameterList.Count; i++)                    
//                        _cmd.Parameters.AddWithValue(parameterList.GetKey(i).ToString(), parameterList.GetByIndex(i));

//                    int rowsAffected = _cmd.ExecuteNonQuery();
//                    totalRowsAffected += rowsAffected;
//                }

//                return totalRowsAffected;
//            }
//            catch (SqlException ex)
//            {
//                Chelp.WriteErrorLog("Error in RunProcedure Bulk: " + ex.Message);
//                return ex.Number;
//            }
//            finally
//            {
//                CloseCN();
//            }
//        }
//        #endregion

//        #region Data Retrieval Methods
//        /// <summary>
//        /// Executes a stored procedure and returns the results as a DataTable
//        /// </summary>
//        /// <param name="StoredProcedureName">Name of the stored procedure to execute</param>
//        /// <param name="ParVal">Parameters for the stored procedure as key-value pairs</param>
//        /// <returns>DataTable containing the query results, or null if an error occurs</returns>
//        public DataTable SelectDB(string StoredProcedureName, SortedList ParVal)
//        {
//            try
//            {
//                _cmd.CommandText = StoredProcedureName;
//                _cmd.Parameters.Clear();
//                for (int I = 0; I < ParVal.Count; I++)
//                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));

//                OpenCN();
//                using (var dt = new DataTable())
//                {
//                    using (var reader = _cmd.ExecuteReader())
//                        dt.Load(reader);                                        
//                    return dt;
//                }
//            }
//            catch (SqlException ex)
//            {
//                Chelp.WriteErrorLog("Error in SelectDB: " + ex.Message);
//                return null;
//            }
//            finally
//            {
//                CloseCN();
//            }
//        }
//        #endregion

//        /// <summary>
//        /// Asynchronously executes a stored procedure and returns the results as a DataTable
//        /// </summary>
//        /// <param name="StoredProcedureName">Name of the stored procedure to execute</param>
//        /// <param name="ParVal">Parameters for the stored procedure as key-value pairs</param>
//        /// <returns>DataTable containing the query results, or null if an error occurs</returns>
//        public async Task<DataTable> SelectDBasync(string StoredProcedureName, SortedList ParVal)
//        {
//            try
//            {
//                _cmd.CommandText = StoredProcedureName;
//                _cmd.Parameters.Clear();
//                for (int I = 0; I < ParVal.Count; I++)
//                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));
//                OpenCN();
//                using (var dt = new DataTable())
//                {
//                    using (var reader =await _cmd.ExecuteReaderAsync())
//                        dt.Load(reader);                   
//                    return dt;
//                }

//            }
//            catch (SqlException ex)
//            {
//                CloseCN();
//                Chelp.WriteErrorLog("Error in SelectDB: " + ex.Message);
//                return null;
//            }
//            finally
//            {
//                CloseCN();
//            }
//        }

//        #region this function checks if the connection parameter is correct and connection done
//        public bool Is_SQLServer_connection_setting_successful(string txtServerName, string txtDataBase, string txtPassword, string txtUserName,string ConnectTimeout)
//        {
//            try
//            {                
//                string connectionString = $"data source={txtServerName};Password={txtPassword};Persist Security Info=True;User ID={txtUserName};Connect Timeout = {ConnectTimeout}";
//                string cmdText = $"USE {txtDataBase}; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TB_Customers'";

//                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//                {
//                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
//                    {
//                        sqlCommand.CommandTimeout = 5;
//                        sqlConnection.Open();
//                        object obj = sqlCommand.ExecuteScalar();

//                        if (obj == null)
//                        {
//                            return false;
//                        }
//                        return true;
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                return false;
//                throw;
//            }
//        }

//        #region this function checks if the connection parameter is correct and connection done
//        public bool Is_SQLServer_connection_setting_successful(string txtServerName, string txtDataBase, string txtPassword, string txtUserName, string ConnectTimeout, string txtPort)
//        {
//            try
//            {
//                string serverAddress = txtServerName;
//                if (!string.IsNullOrEmpty(txtPort))
//                {
//                    serverAddress = $"{txtServerName},{txtPort}";
//                }

//                string connectionString = $"data source={serverAddress};initial catalog={txtDataBase};Password={txtPassword};Persist Security Info=True;User ID={txtUserName};Connect Timeout = {ConnectTimeout}";

//                string cmdText = "SELECT 1"; // A simple query to check connection and database access.

//                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//                {
//                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
//                    {
//                        sqlCommand.CommandTimeout = 5;
//                        sqlConnection.Open();
//                        object obj = sqlCommand.ExecuteScalar();

//                        if (obj == null || obj.ToString() != "1")
//                        {
//                            return false;
//                        }
//                        return true;
//                    }
//                }
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }
//        #endregion


//        public bool Is_SQLServer_connection_setting_successful()
//        {
//            return Is_SQLServer_connection_setting_successful(_serverName, _database, _password, _userId, _connectTimeout);           
//        }
//        #endregion


//    }
//}

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Alkamous.InterfaceForAllClass;
using Alkamous.Controller;

namespace Alkamous.Model
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private readonly string _serverName = Properties.Settings.Default.ServerName;
        private readonly string _database = Properties.Settings.Default.Database;
        private readonly string _userId = Properties.Settings.Default.Userid;
        private readonly string _password = Properties.Settings.Default.password;
        private readonly string _connectTimeout = Properties.Settings.Default.ConnectTimeout;

        private string ConnectionString =>
            new SqlConnectionStringBuilder
            {
                DataSource = _serverName,
                InitialCatalog = _database,
                UserID = _userId,
                Password = _password,
                ConnectTimeout = int.TryParse(_connectTimeout, out int t) ? t : 15
            }.ConnectionString;

        #region Run Procedure (Sync)
        public int RunProcedure(string storedProcedureName, SortedList parVal)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DictionaryEntry item in parVal)
                        cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value ?? DBNull.Value);

                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Chelp.WriteErrorLog("Error in RunProcedure: " + ex.Message);
                return ex.Number;
            }
        }
        #endregion

        #region Run Procedure (Async)
        public async Task<int> RunProcedureasync(string storedProcedureName, SortedList parVal)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DictionaryEntry item in parVal)
                        cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value ?? DBNull.Value);

                    await con.OpenAsync();
                    return await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (SqlException ex)
            {
                Chelp.WriteErrorLog("Error in RunProcedureAsync: " + ex.Message);
                return ex.Number;
            }
        }
        #endregion

        #region Bulk Operation
        public int RunProcedureBulk(string storedProcedureName, List<SortedList> parameterValues)
        {
            try
            {
                int total = 0;

                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    foreach (var parameters in parameterValues)
                    {
                        cmd.Parameters.Clear();

                        foreach (DictionaryEntry item in parameters)
                            cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value ?? DBNull.Value);

                        total += cmd.ExecuteNonQuery();
                    }
                }

                return total;
            }
            catch (SqlException ex)
            {
                Chelp.WriteErrorLog("Error in RunProcedureBulk: " + ex.Message);
                return ex.Number;
            }
        }
        #endregion

        #region SelectDB (Sync)
        public DataTable SelectDB(string storedProcedureName, SortedList parVal)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DictionaryEntry item in parVal)
                        cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value ?? DBNull.Value);

                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                        dt.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                Chelp.WriteErrorLog("Error in SelectDB: " + ex.Message);
            }
            return dt;
        }
        #endregion

        #region SelectDB (Async)
        public async Task<DataTable> SelectDBasync(string storedProcedureName, SortedList parVal)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                using (SqlCommand cmd = new SqlCommand(storedProcedureName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    foreach (DictionaryEntry item in parVal)
                        cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value ?? DBNull.Value);

                    await con.OpenAsync();
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        dt.Load(reader);
                }
            }
            catch (SqlException ex)
            {
                Chelp.WriteErrorLog("Error in SelectDBasync: " + ex.Message);
            }
            return dt;
        }
        #endregion

        #region Connection Test
        public bool Is_SQLServer_connection_setting_successful(string server, string database, string password, string user, string timeout)
        {
            try
            {
                string connectionString =
                    $"Data Source={server};Initial Catalog={database};User ID={user};Password={password};Connect Timeout={timeout}";

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT 1", con))
                {
                    con.Open();
                    return cmd.ExecuteScalar()?.ToString() == "1";
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Is_SQLServer_connection_setting_successful(string server, string database, string password, string user, string timeout, string port)
        {
            try
            {
                string serverAddr = string.IsNullOrEmpty(port) ? server : $"{server},{port}";
                string connectionString =
                    $"Data Source={serverAddr};Initial Catalog={database};User ID={user};Password={password};Connect Timeout={timeout}";

                using (SqlConnection con = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand("SELECT 1", con))
                {
                    con.Open();
                    return cmd.ExecuteScalar()?.ToString() == "1";
                }
            }
            catch
            {
                return false;
            }
        }

        public bool Is_SQLServer_connection_setting_successful()
        {
            return Is_SQLServer_connection_setting_successful(_serverName, _database, _password, _userId, _connectTimeout);
        }
        #endregion
    }
}

