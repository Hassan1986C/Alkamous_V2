using Alkamous.Model;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Alkamous.InterfaceForAllClass
{
    /// <summary>
    /// Generic interface for basic CRUD operations
    /// </summary>
    /// <typeparam name="T">The type of entity to perform operations on</typeparam>
    interface IBaseOperation<T> where T : class
    {
        /// <summary>
        /// Adds a new entity to the database asynchronously
        /// </summary>
        /// <param name="item">The entity to add</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        Task<bool> AddNewAsync(T item);

        /// <summary>
        /// Deletes an entity from the database asynchronously
        /// </summary>
        /// <param name="Text">Identifier of the entity to delete</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        Task<bool> DeleteAsync(string Text);

        /// <summary>
        /// Updates an existing entity in the database asynchronously
        /// </summary>
        /// <param name="item">The entity with updated information</param>
        /// <returns>True if operation was successful, false otherwise</returns>
        Task<bool> UpdateAsync(T item);
    }

    /// <summary>
    /// Interface for customer information operations
    /// Handles basic customer data queries and validations
    /// </summary>
    interface ICustomerInfo
    {
        /// <summary>
        /// Retrieves all customer information records
        /// </summary>
        /// <returns>DataTable containing all customer records</returns>
        Task<DataTable> Get_All();

        /// <summary>
        /// Searches for customer information based on search text
        /// </summary>
        /// <param name="Text">Search criteria</param>
        /// <returns>DataTable containing matching customer records</returns>
        Task<DataTable> Get_BySearch(string Text);        

        /// <summary>
        /// Checks if a customer record exists
        /// </summary>
        /// <param name="Text">Customer identifier to check</param>
        /// <returns>True if customer exists, false otherwise</returns>
        bool Is_CustomerInfo_Exist(string Text);

        /// <summary>
        /// Gets the total count of customer information records
        /// </summary>
        /// <returns>Number of customer records</returns>
        int Get_CountCustomerInfo();
    }

    /// <summary>
    /// Interface for customer operations
    /// Handles detailed customer data management including invoices and pagination
    /// </summary>
    interface ICustomers
    {
        /// <summary>
        /// Gets the maximum auto-generated customer number
        /// </summary>
        /// <returns>DataTable containing the maximum customer number</returns>
        DataTable Get_MaxCustomer_AutoNum();

        /// <summary>
        /// Gets the total count of customers
        /// </summary>
        /// <returns>Number of customers</returns>
        int Get_CountCustomer();

        /// <summary>
        /// Retrieves paginated customer records
        /// </summary>
        /// <param name="PageNumber">Page number to retrieve</param>
        /// <param name="PageSize">Number of records per page</param>
        /// <returns>DataTable containing customer records for the specified page</returns>
        Task<DataTable> Get_AllCustomer(int PageNumber = 1, int PageSize = 500);

        /// <summary>
        /// Searches for customers with pagination
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to search for</param>
        /// <param name="PageNumber">Page number to retrieve</param>
        /// <param name="PageSize">Number of records per page</param>
        /// <returns>DataTable containing matching customer records</returns>
        Task<DataTable> Get_AllCustomer_BySearch(string Customer_Invoice_Number, int PageNumber = 1, int PageSize = 500);

        /// <summary>
        /// Gets detailed customer information by invoice number
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to search for</param>
        /// <returns>DataTable containing customer details</returns>
        DataTable Get_CustomerDetails_ByCustomer_Invoice_Number(string Customer_Invoice_Number);

        /// <summary>
        /// Gets all customer records for a specific invoice number
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to search for</param>
        /// <returns>DataTable containing customer records</returns>
        DataTable Get_AllCustomer_ByCustomer_Invoice_Number(string Customer_Invoice_Number);

        /// <summary>
        /// Validates that an invoice number is not duplicated
        /// </summary>
        /// <param name="Customer_Invoice_Number">Invoice number to check</param>
        /// <returns>True if invoice number is unique, false otherwise</returns>
        Task<bool> Check_Customer_Invoice_NumberNotDuplicate(string Customer_Invoice_Number);
    }

    /// <summary>
    /// Interface for invoice operations
    /// Handles creation and retrieval of invoice data
    /// </summary>
    interface IInvoices
    {       
        /// <summary>
        /// Adds a new invoice to the list for bulk processing
        /// </summary>
        /// <param name="item">Invoice information to add</param>
        void Add_NewInvoiceLIST(CTB_Invoices item);

        /// <summary>
        /// Performs bulk insert of all invoices in the list
        /// </summary>
        void InsertBulk();        

        /// <summary>
        /// Retrieves invoice information by invoice number
        /// </summary>
        /// <param name="Invoice_Number">Invoice number to search for</param>
        /// <returns>DataTable containing invoice information</returns>
        DataTable Get_Invoice_ByInvoice_Number(string Invoice_Number);

        /// <summary>
        /// Retrieves invoice information by product ID
        /// </summary>
        /// <param name="Invoice_product_Id">Product ID to search for</param>
        /// <returns>DataTable containing invoice information</returns>
        DataTable Get_Invoice_Byproduct_Id(string Invoice_product_Id);
    }

    /// <summary>
    /// Interface for consumable item operations
    /// Handles management of consumable products
    /// </summary>
    interface IConsumable
    {
        /// <summary>
        /// Adds a new consumable item to the list for bulk processing
        /// </summary>
        /// <param name="item">Consumable item information to add</param>
        void Add_NewConsumableLIST(CTB_Consumable item);

        /// <summary>
        /// Performs bulk insert of all consumable items in the list
        /// </summary>
        void InsertBulk();

        /// <summary>
        /// Deletes a consumable item by its number
        /// </summary>
        /// <param name="Consumable_Number">Consumable number to delete</param>
        void Delete_ConsumableByConsumable_Number(string Consumable_Number);

        /// <summary>
        /// Retrieves consumable item information by its number
        /// </summary>
        /// <param name="Consumable_Number">Consumable number to search for</param>
        /// <returns>DataTable containing consumable item information</returns>
        DataTable Get_Consumable_ByConsumable_Number(string Consumable_Number);
    }

    /// <summary>
    /// Interface for invoice terms operations
    /// Handles management of invoice terms and conditions
    /// </summary>
    interface ITerms_Invoices
    {
        /// <summary>
        /// Adds new invoice terms to the list for bulk processing
        /// </summary>
        /// <param name="item">Invoice terms information to add</param>
        void Add_NewTerms_InvoiceLIST(CTB_Terms_Invoices item);

        /// <summary>
        /// Performs bulk insert of all invoice terms in the list
        /// </summary>
        void InsertBulk();

        /// <summary>
        /// Deletes invoice terms by invoice number
        /// </summary>
        /// <param name="Terms_Invoice_Number">Invoice number to delete terms for</param>
        void Delete_Terms_InvoiceByTerms_Invoice_Number(string Terms_Invoice_Number);

        /// <summary>
        /// Retrieves invoice terms by invoice number
        /// </summary>
        /// <param name="Terms_Invoice_Number">Invoice number to search for</param>
        /// <returns>DataTable containing invoice terms</returns>
        DataTable Get_Terms_Invoice_ByTerms_Invoice_Number(string Terms_Invoice_Number);
    }

    interface IBankAccounts
    {     
        DataTable Get_All();
        DataTable Get_BySearch(string Text);
        DataTable Get_ByBank_Definition(string Text);
        bool Check_Bank_DefinitionNotDuplicate(string Text);
    }

    interface ITerms
    {
        bool Add_Term(string Term_En, string Term_Ar);
        void Add_TermLIST(CTB_Terms item);
        bool Update_Term(string Term_AutoNum, string Term_En, string Term_Ar);
        bool Delete_Term(string Term_AutoNum);
        DataTable Get_AllTerm_BySearch(string search);
        DataTable Get_AllTerms();
    }

    interface IProducts
    {      
        int Get_CountProduct();
        Task<DataTable> Get_AllProduct(int PageNumber = 1, int PageSize = 5000);
        Task<DataTable> Get_AllProduct_BySearch(string search, int PageNumber = 1, int PageSize = 500000);
        Task<DataTable> Get_AllProduct_BySearchFavorite(string search, int PageNumber = 1, int PageSize = 500000);
        DataTable Get_Product_product_Id(string product_Id, int PageNumber = 1, int PageSize = 5000);
        Task<DataTable> Get_DistinctProduct();
        bool Check_ProductIdNotDuplicate(string product_Id);
    }


    //this interface created to apply the Solid principle Design pattern
    /// <summary>
    /// for All both
    /// </summary>
    /// <typeparam name="T">class Name person or employ or ..</typeparam>
    interface IUsers<T> where T : class
    {       
        T Get_AllBySearch(string Text);
        T Get_ByID(string Text);
        List<T> Get_ALL();
        int Get_CountUsersLog();
    }

    interface IBackUpAndRestor
    {
        Task<bool> DataBaseBackUp(string PathFileName);
        Task<bool> DataBaseRestore(string backupFilePath);
    }

    interface IDataAccessLayer
    {
        int RunProcedure(string StoredProcedureName, SortedList ParVal);
        int RunProcedureBulk(string StoredProcedureName, List<SortedList> parameterValues);
        DataTable SelectDB(string StoredProcedureName, SortedList ParVal);
        Task<DataTable> SelectDBasync(string StoredProcedureName, SortedList ParVal);
        bool Is_SQLServer_connection_setting_successful(string txtServerName, string txtDataBase, string txtPassword, string txtUserName, string ConnectTimeout);
        bool Is_SQLServer_connection_setting_successful();
    }

}
