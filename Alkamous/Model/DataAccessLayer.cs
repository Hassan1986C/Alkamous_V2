using System;
using System.Data;
using Alkamous.Controller;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Alkamous.InterfaceForAllClass;

namespace Alkamous.Model
{
    public class DataAccessLayer: IDataAccessLayer
    {
        #region Declare variables ( SqlConnection, SqlCommand,DataTable)
        private readonly SqlConnection _sqlcon = new SqlConnection();
        private readonly SqlCommand _cmd = new SqlCommand();

        private readonly string _serverName = Properties.Settings.Default.ServerName;
        private readonly string _database = Properties.Settings.Default.Database;
        private readonly string _userId = Properties.Settings.Default.Userid;
        private readonly string _password = Properties.Settings.Default.password;
        private readonly string _connectTimeout = Properties.Settings.Default.ConnectTimeout;

        #endregion

        #region Connection String this constructor initialize the connection object
        // this constructor initialize the connection object
        public DataAccessLayer()
        {
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = _serverName,
                InitialCatalog = _database,
                UserID = _userId,
                Password = _password
            };

            _sqlcon = new SqlConnection(sqlConnectionString.ConnectionString);

            _cmd.Connection = _sqlcon;
            _cmd.CommandType = CommandType.StoredProcedure;


        }
        #endregion

        #region method to Open connection 
        private void OpenCN()
        {
            if (_sqlcon.State != ConnectionState.Open)
                _sqlcon.Open();
        }
        #endregion

        #region method to Close connection 
        private void CloseCN()
        {
            if (_sqlcon.State == ConnectionState.Open)
                _sqlcon.Close();

        }

        #endregion

        #region Method To Method To ADD Delelte Update By StordProseder
        public int RunProcedure(string StoredProcedureName, SortedList ParVal)
        {
            try
            {
                _cmd.CommandText = StoredProcedureName;
                _cmd.Parameters.Clear();

                for (int I = 0; I < ParVal.Count; I++)
                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));
                     
                OpenCN();
                int rRusult = _cmd.ExecuteNonQuery();
                CloseCN();
                return rRusult;


            }
            catch (SqlException ex)
            {
                CloseCN();

                Chelp.WriteErrorLog("Error in RunProcedure: " + ex.Message);
                return ex.Number;
            }
            finally
            {
                CloseCN();
            }

        }

        // Method is async
        public async Task<int> RunProcedureasync(string StoredProcedureName, SortedList ParVal)
        {
            try
            {
                _cmd.CommandText = StoredProcedureName;
                _cmd.Parameters.Clear();

                for (int I = 0; I < ParVal.Count; I++)
                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));

                OpenCN();
                int rRusult =await _cmd.ExecuteNonQueryAsync();
                CloseCN();
                return rRusult;


            }
            catch (SqlException ex)
            {
                CloseCN();

                Chelp.WriteErrorLog("Error in RunProcedure: " + ex.Message);
                return ex.Number;
            }
        }

        #endregion

        #region Method To Method To ADD Delelte Update List<SortedList> By StordProseder
        public int RunProcedureBulk(string StoredProcedureName, List<SortedList> parameterValues)
        {
            try
            {

                int totalRowsAffected = 0;
                _cmd.CommandText = StoredProcedureName;

                OpenCN();

                foreach (SortedList parameterList in parameterValues)
                {
                    _cmd.Parameters.Clear();
                    for (int i = 0; i < parameterList.Count; i++)                    
                        _cmd.Parameters.AddWithValue(parameterList.GetKey(i).ToString(), parameterList.GetByIndex(i));
                    

                    int rowsAffected = _cmd.ExecuteNonQuery();
                    totalRowsAffected += rowsAffected;
                }

                CloseCN();
                return totalRowsAffected;
            }
            catch (SqlException ex)
            {
                CloseCN();
                Chelp.WriteErrorLog("Error in RunProcedure Bulk: " + ex.Message);
                return ex.Number;
            }
            finally
            {
                CloseCN();
            }
        }
        #endregion

        #region SelectDB and Read Data By StordProseder
        public DataTable SelectDB(string StoredProcedureName, SortedList ParVal)
        {
            try
            {
                _cmd.CommandText = StoredProcedureName;
                _cmd.Parameters.Clear();
                for (int I = 0; I < ParVal.Count; I++)
                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));

                OpenCN();
                using (var dt = new DataTable())
                {
                    using (var reader = _cmd.ExecuteReader())
                        dt.Load(reader);                                        
                    return dt;
                }


            }
            catch (SqlException ex)
            {
                CloseCN();
                Chelp.WriteErrorLog("Error in SelectDB: " + ex.Message);
                return null;
            }
            finally
            {
                CloseCN();
            }
        }
        #endregion

        // methode async
        public async Task<DataTable> SelectDBasync(string StoredProcedureName, SortedList ParVal)
        {
            try
            {
                _cmd.CommandText = StoredProcedureName;
                _cmd.Parameters.Clear();
                for (int I = 0; I < ParVal.Count; I++)
                    _cmd.Parameters.AddWithValue(ParVal.GetKey(I).ToString(), ParVal.GetByIndex(I));
                OpenCN();
                using (var dt = new DataTable())
                {
                    using (var reader =await _cmd.ExecuteReaderAsync())
                        dt.Load(reader);                   
                    return dt;
                }

            }
            catch (SqlException ex)
            {
                CloseCN();
                Chelp.WriteErrorLog("Error in SelectDB: " + ex.Message);
                return null;
            }
            finally
            {
                CloseCN();
            }
        }

        #region this function checks if the connection parameter is correct and connection done
        public bool Is_SQLServer_connection_setting_successful(string txtServerName, string txtDataBase, string txtPassword, string txtUserName,string ConnectTimeout)
        {
            try
            {
                
                string connectionString = $"data source={txtServerName};Password={txtPassword};Persist Security Info=True;User ID={txtUserName};Connect Timeout = {ConnectTimeout}";
                string cmdText = $"USE {txtDataBase}; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'TB_Customers'";

                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlCommand.CommandTimeout = 5;
                        sqlConnection.Open();
                        object obj = sqlCommand.ExecuteScalar();
                        
                        if (obj == null)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        
        public bool Is_SQLServer_connection_setting_successful()
        {
            return Is_SQLServer_connection_setting_successful(_serverName, _database, _password, _userId, _connectTimeout);           
        }
        #endregion


    }
}
