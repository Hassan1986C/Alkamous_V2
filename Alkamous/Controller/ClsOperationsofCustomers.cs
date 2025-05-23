using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;

namespace Alkamous.Controller
{
    /// <summary>
    /// Handles all customer-related database operations including invoices, currency, and payment details
    /// Implements base CRUD operations and customer-specific functionality
    /// </summary>
    public class ClsOperationsofCustomers : IBaseOperation<CTB_Customers>, ICustomers
    {
        /// <summary>
        /// Name of the stored procedure that handles all customer operations
        /// </summary>
        private readonly string ProcedureName = "SP_TB_Customers";

        /// <summary>
        /// Data access layer instance for database operations
        /// </summary>
        private readonly IDataAccessLayer DAL;

        /// <summary>
        /// Customer model instance for field mappings
        /// </summary>
        private readonly CTB_Customers MTB_Customers = new CTB_Customers();

        public ClsOperationsofCustomers(IDataAccessLayer _DAL)
        {
            DAL = _DAL;
        }

        /// <summary>
        /// Creates a SortedList of parameters for stored procedure execution
        /// Maps customer properties to their database counterparts
        /// </summary>
        /// <param name="item">Customer object containing the data</param>
        /// <param name="Check">Operation type identifier for the stored procedure</param>
        /// <returns>SortedList containing parameter names and values</returns>
        private SortedList AssignValuesToSortedList(CTB_Customers item, string @Check)
        {
            SortedList SL = new SortedList
            {
                { "@Check", @Check },
                {MTB_Customers.Customer_Invoice_Number , item.Customer_Invoice_Number },
                {MTB_Customers.Customer_Company , item.Customer_Company },
                {MTB_Customers.Customer_Name  , item.Customer_Name },
                {MTB_Customers.Customer_Mob  , item.Customer_Mob },
                {MTB_Customers.Customer_Email , item.Customer_Email },
                {MTB_Customers.Customer_Currency ,item. Customer_Currency },
                {MTB_Customers.Customer_ExchangeRate , item.Customer_ExchangeRate },
                {MTB_Customers.Customer_Taxes , item.Customer_Taxes },
                {MTB_Customers.Customer_DateTime , item.Customer_DateTime },
                {MTB_Customers.Customer_Quote_Valid , item.Customer_Quote_Valid },
                {MTB_Customers.Customer_Payment_Terms , item.Customer_Payment_Terms },
                {MTB_Customers.Customer_Discount , item.Customer_Discount },
                {MTB_Customers.Customer_BankAccount , item.Customer_BankAccount },
                {MTB_Customers.Customer_Language , item.Customer_Language },
                {MTB_Customers.Customer_Note , item.Customer_Note },
            };
            return SL;
        }

        /// <summary>
        /// Adds a new customer record to the database
        /// </summary>
        /// <param name="item">Customer object containing the new customer's information</param>
        /// <returns>True if addition was successful, false otherwise</returns>
        public async Task<bool> AddNewAsync(CTB_Customers item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Add_NewCustomer");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        /// <summary>
        /// Deletes a customer record from the database
        /// </summary>
        /// <param name="Text">Invoice number of the customer to delete</param>
        /// <returns>True if deletion was successful, false otherwise</returns>
        public async Task<bool> DeleteAsync(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Delete_CustomerData" },
                {MTB_Customers.Customer_Invoice_Number , Text },
            };
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        /// <summary>
        /// Updates an existing customer record in the database
        /// </summary>
        /// <param name="item">Customer object containing updated information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        public async Task<bool> UpdateAsync(CTB_Customers item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Update_CustomerData");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        /// <summary>
        /// Retrieves the maximum auto-generated customer number
        /// Used for generating new customer IDs
        /// </summary>
        /// <returns>DataTable containing the maximum customer number</returns>
        public DataTable Get_MaxCustomer_AutoNum()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_MaxCustomer_AutoNum" },
            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

        /// <summary>
        /// Gets the total count of customers in the database
        /// </summary>
        /// <returns>Total number of customer records</returns>
        public int Get_CountCustomer()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_CountCustomer" },
            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// Retrieves detailed customer information by invoice number
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to search for</param>
        /// <returns>DataTable containing customer details</returns>
        public DataTable Get_CustomerDetails_ByCustomer_Invoice_Number(string Customer_Invoice_Number)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_CustomerDetails_ByCustomer_Invoice_Number" },
                {MTB_Customers.Customer_Invoice_Number, Customer_Invoice_Number}
            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

        /// <summary>
        /// Searches for customers with pagination support
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to search for</param>
        /// <param name="PageNumber">Page number to retrieve</param>
        /// <param name="PageSize">Number of records per page</param>
        /// <returns>DataTable containing matching customer records</returns>
        public async Task<DataTable> Get_AllCustomer_BySearch(string Customer_Invoice_Number, int PageNumber = 1, int PageSize = 50)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllCustomer_BySearch" },
                {"@PageNumber",PageNumber },
                {"@PageSize",PageSize},
                {MTB_Customers.Customer_Invoice_Number, Customer_Invoice_Number}
            };
            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

        /// <summary>
        /// Checks if an invoice number is already in use
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to check</param>
        /// <returns>True if invoice number exists, false otherwise</returns>
        public async Task<bool> Check_Customer_Invoice_NumberNotDuplicate(string Customer_Invoice_Number)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Check_Customer_Invoice_NumberNotDuplicate" },
                { MTB_Customers.Customer_Invoice_Number, Customer_Invoice_Number },
            };
            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            int result = Convert.ToInt16(dt.Rows[0][0]);
            return result > 0;
        }

        /// <summary>
        /// Retrieves all customer records for a specific invoice number
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to search for</param>
        /// <returns>DataTable containing customer records</returns>
        public DataTable Get_AllCustomer_ByCustomer_Invoice_Number(string Customer_Invoice_Number)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllCustomer_ByCustomer_Invoice_Number" },
                {MTB_Customers.Customer_Invoice_Number, Customer_Invoice_Number}
            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

        /// <summary>
        /// Retrieves all customer records with optional pagination
        /// </summary>
        /// <param name="PageNumber">Page number to retrieve</param>
        /// <param name="PageSize">Number of records per page</param>
        /// <returns>DataTable containing customer records</returns>
        public async Task<DataTable> Get_AllCustomer(int PageNumber = 1, int PageSize = 5000)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllCustomer" },
                 {"@PageNumber",PageNumber },
                {"@PageSize",PageSize},
            };
            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }
    }
}
