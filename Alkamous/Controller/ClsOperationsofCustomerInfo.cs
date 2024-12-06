using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Alkamous.Controller
{
    public class ClsOperationsofCustomerInfo : IBaseOperation<CTB_CustomerInfo>, ICustomerInfo
    {
        private readonly string ProcedureName = "SP_TB_CustomersInfo";
        private DataAccessLayer DAL = new DataAccessLayer();
        CTB_CustomerInfo MTB_CustomerInfo = new CTB_CustomerInfo();

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
        public async Task<bool> AddNewAsync(CTB_CustomerInfo item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Add_CustomerInfo");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

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

        public async Task<bool> UpdateAsync(CTB_CustomerInfo item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Update_CustomerInfo");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;

        }



        public async Task<DataTable> Get_All()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllCustomerInfo" },

            };
            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

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
