using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;

namespace Alkamous.Controller
{
    // this class belong to invose like (Customer_Invoice_Number ,Customer_Currency ,Customer_ExchangeRate ...)
    public class ClsOperationsofCustomers : IBaseOperation<CTB_Customers>, ICustomers
    {
        private readonly string ProcedureName = "SP_TB_Customers";
        DataAccessLayer DAL = new DataAccessLayer();
        CTB_Customers MTB_Customers = new CTB_Customers();

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
        public async Task<bool> AddNewAsync(CTB_Customers item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Add_NewCustomer");

            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

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

        public async Task<bool> UpdateAsync(CTB_Customers item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Update_CustomerData");

            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public DataTable Get_MaxCustomer_AutoNum()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_MaxCustomer_AutoNum" },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

        public int Get_CountCustomer()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_CountCustomer" },

            };
            DataTable dt = DAL.SelectDB(ProcedureName, SL);

            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }

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

        public async Task<DataTable> Get_AllCustomer(int PageNumber = 1, int PageSize = 5000)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllCustomer" },


            };
            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

    }
}
