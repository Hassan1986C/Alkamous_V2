
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

