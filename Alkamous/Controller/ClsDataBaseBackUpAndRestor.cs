using Alkamous.InterfaceForAllClass;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace Alkamous.Controller
{
    public class ClsDataBaseBackUpAndRestor : IBackUpAndRestor
    {

        #region Declare variables
        private readonly string _connectionString;
        private readonly string _serverName = Properties.Settings.Default.ServerName;
        private readonly string _database = Properties.Settings.Default.Database;
        private readonly string _userId = Properties.Settings.Default.Userid;
        private readonly string _password = Properties.Settings.Default.password;
        #endregion

        public ClsDataBaseBackUpAndRestor()
        {
            SqlConnectionStringBuilder sqlConnectionString = new SqlConnectionStringBuilder
            {
                DataSource = _serverName,
                InitialCatalog = _database,
                UserID = _userId,
                Password = _password
            };

            _connectionString = sqlConnectionString.ConnectionString;


        }

        public async Task<bool> DataBaseBackUp(string PathFileName)
        {

            var backupFileName = PathFileName;

            try
            {
                using (var sqlcon = new SqlConnection(_connectionString))
                {
                    await sqlcon.OpenAsync();
                    var strBACKUPQuery = "BACKUP DATABASE @DatabaseName TO DISK=@FileName";
                    using (var cmd = new SqlCommand(strBACKUPQuery, sqlcon))
                    {
                        cmd.Parameters.AddWithValue("@DatabaseName", Properties.Settings.Default.Database);
                        cmd.Parameters.AddWithValue("@FileName", backupFileName);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                return true; // Backup successful
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception appropriately
                return false; // Backup failed
            }
        }


        public async Task<bool> DataBaseRestore(string backupFilePath)
        {

            try
            {
                using (var sqlcon = new SqlConnection(_connectionString))
                {

                    await sqlcon.OpenAsync();


                    // Step 1: Set the database to single-user mode with immediate rollback
                    string sqlStep1 = $"ALTER DATABASE [{_database}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                    using (var cmd1 = new SqlCommand(sqlStep1, sqlcon))
                    {
                        await cmd1.ExecuteNonQueryAsync();
                    }


                    // Step 2: Restore the database from the backup file
                    string sqlStep2 = $@"USE MASTER RESTORE DATABASE [{_database}] FROM DISK=@backupFilePath WITH REPLACE";
                    using (var cmd2 = new SqlCommand(sqlStep2, sqlcon))
                    {
                        cmd2.CommandText = sqlStep2;
                        cmd2.Parameters.AddWithValue("@backupFilePath", backupFilePath);
                        await cmd2.ExecuteNonQueryAsync();
                    }


                    // Step 3: Set the database back to multi-user mode
                    string sqlStep3 = $"ALTER DATABASE [{_database}] SET MULTI_USER";
                    using (var cmd3 = new SqlCommand(sqlStep3, sqlcon))
                    {
                        await cmd3.ExecuteNonQueryAsync();
                    }

                }

                return true; // Restoration successful
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Log or handle the exception appropriately
                return false; // Restoration failed
            }
        }


      

    }
}