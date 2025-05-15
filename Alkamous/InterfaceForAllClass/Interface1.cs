using Alkamous.Model;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Alkamous.InterfaceForAllClass
{
    interface IBaseOperation<T> where T : class
    {
        Task<bool> AddNewAsync(T item);
        Task<bool> DeleteAsync(string Text);
        Task<bool> UpdateAsync(T item);
    }
    interface ICustomerInfo
    {
        Task<DataTable> Get_All();
        Task<DataTable> Get_BySearch(string Text);        
        bool Is_CustomerInfo_Exist(string Text);
        int Get_CountCustomerInfo();
    }

    interface ICustomers
    {
        DataTable Get_MaxCustomer_AutoNum();
        int Get_CountCustomer();
        Task<DataTable> Get_AllCustomer(int PageNumber = 1, int PageSize = 500);
        Task<DataTable> Get_AllCustomer_BySearch(string Customer_Invoice_Number, int PageNumber = 1, int PageSize = 500);
        DataTable Get_CustomerDetails_ByCustomer_Invoice_Number(string Customer_Invoice_Number);
        DataTable Get_AllCustomer_ByCustomer_Invoice_Number(string Customer_Invoice_Number);
        Task<bool> Check_Customer_Invoice_NumberNotDuplicate(string Customer_Invoice_Number);
    }

    interface IInvoices
    {       
        void Add_NewInvoiceLIST(CTB_Invoices item);
        void InsertBulk();        
        DataTable Get_Invoice_ByInvoice_Number(string Invoice_Number);
        DataTable Get_Invoice_Byproduct_Id(string Invoice_product_Id);
    }

    interface IConsumable
    {
        void Add_NewConsumableLIST(CTB_Consumable item);
        void InsertBulk();
        void Delete_ConsumableByConsumable_Number(string Consumable_Number);
        DataTable Get_Consumable_ByConsumable_Number(string Consumable_Number);
    }

    interface ITerms_Invoices
    {
        void Add_NewTerms_InvoiceLIST(CTB_Terms_Invoices item);
        void InsertBulk();
        void Delete_Terms_Invoice(string Term_Invoice_Number);
        DataTable Get_AllTerms_Invoice(int PageNumber = 1, int PageSize = 500);
        DataTable Get_AllTerms_Invoice_ByTerm_Invoice_Number(string Term_Invoice_Number);
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
