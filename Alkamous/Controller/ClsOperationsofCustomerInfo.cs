using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Alkamous.Controller
{
    /// <summary>
    /// Handles all database operations related to customer information
    /// Implements base operations (Add, Update, Delete) and customer-specific operations
    /// </summary>
    public class ClsOperationsofCustomerInfo : IBaseOperation<CTB_CustomerInfo>, ICustomerInfo
    {
        // Stored procedure name for all customer info operations
        private readonly string ProcedureName = "SP_TB_CustomersInfo";
        // Data access layer instance for database operations
        private DataAccessLayer DAL = new DataAccessLayer();
        // Model instance for customer information
        CTB_CustomerInfo MTB_CustomerInfo = new CTB_CustomerInfo();

        /// <summary>
        /// Creates a SortedList of parameters for stored procedure execution
        /// </summary>
        /// <param name="item">Customer information object containing the data</param>
        /// <param name="Check">Operation type identifier for the stored procedure</param>
        /// <returns>SortedList containing parameter names and values</returns>
        private SortedList AssignValuesToSortedList(CTB_CustomerInfo item, string @Check)
        {
            SortedList SL = new SortedList
            {
                { "@Check", @Check },
                {MTB_CustomerInfo.Customer_AutoNum,item.Customer_AutoNum},
                {MTB_CustomerInfo.Customer_Company , item.Customer_Company },
                {MTB_CustomerInfo.Customer_Name  , item.Customer_Name },
                {MTB_CustomerInfo.Customer_Mob  , item.Customer_Mob },
                {MTB_CustomerInfo.Customer_Email  , item.Customer_Email },

            };
            return SL;
        }

        /// <summary>
        /// Adds a new customer information record to the database
        /// </summary>
        /// <param name="item">Customer information object to be added</param>
        /// <returns>True if operation successful, false otherwise</returns>
        public async Task<bool> AddNewAsync(CTB_CustomerInfo item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Add_CustomerInfo");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        /// <summary>
        /// Deletes a customer information record from the database
        /// </summary>
        /// <param name="Text">Customer auto number to identify the record</param>
        /// <returns>True if operation successful, false otherwise</returns>
        public async Task<bool> DeleteAsync(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Delete_CustomerInfo" },
                {MTB_CustomerInfo.Customer_AutoNum , Text },
            };

            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        /// <summary>
        /// Updates an existing customer information record in the database
        /// </summary>
        /// <param name="item">Customer information object containing updated data</param>
        /// <returns>True if operation successful, false otherwise</returns>
        public async Task<bool> UpdateAsync(CTB_CustomerInfo item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Update_CustomerInfo");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        /// <summary>
        /// Retrieves all customer information records from the database
        /// </summary>
        /// <returns>DataTable containing all customer records</returns>
        public async Task<DataTable> Get_All()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllCustomerInfo" },
            };
            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

        /// <summary>
        /// Searches for customer information records based on customer name
        /// </summary>
        /// <param name="Text">Customer name to search for</param>
        /// <returns>DataTable containing matching customer records</returns>
        public async Task<DataTable> Get_BySearch(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllCustomerInfo_BySearch" },
                {MTB_CustomerInfo.Customer_Name , Text },
            };
            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

        /// <summary>
        /// Checks if a customer record with the specified mobile number already exists
        /// </summary>
        /// <param name="Text">Mobile number to check</param>
        /// <returns>True if customer exists, false otherwise</returns>
        public bool Is_CustomerInfo_Exist(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Check_CustomerInfoNotDuplicate" },
                { MTB_CustomerInfo.Customer_Mob, Text},
            };

            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            int result = Convert.ToInt16(dt.Rows[0][0]);
            return result > 0;
        }

        /// <summary>
        /// Gets the total count of customer information records in the database
        /// </summary>
        /// <returns>Total number of customer records</returns>
        public int Get_CountCustomerInfo()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_CountCustomerInfo" },
            };

            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }
    }
}
