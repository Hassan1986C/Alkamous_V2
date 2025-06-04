using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;

namespace Alkamous.Controller
{
    public class ClsOperationsofProducts : IBaseOperation<CTB_Products>, IProducts
    {
        private readonly string ProcedureName = "SP_TB_Products";
        
        CTB_Products MTB_Products = new CTB_Products(CTB_Products.ProductFieldNaming.Plain);

        
        private readonly IDataAccessLayer DAL;        
        public ClsOperationsofProducts(IDataAccessLayer _DAL)
        {
            DAL = _DAL;
            
        }
               

        private SortedList AssignValuesToSortedList(CTB_Products item, string @Check)
        {
            SortedList SL = new SortedList
            {
                { "@Check", @Check },
                { MTB_Products.product_Id, item.product_Id },
                { MTB_Products.product_NameAr, item.product_NameAr },
                { MTB_Products.product_NameEn, item.product_NameEn },
                { MTB_Products.product_Price, item.product_Price },
                { MTB_Products.product_Unit, item.product_Unit },
                { MTB_Products.product_favorite, item.product_favorite },
            };
            return SL;

        }

        public async Task<bool> AddNewAsync(CTB_Products item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Add_NewProduct");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public async Task<bool> DeleteAsync(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Delete_Product" },
                { MTB_Products.product_Id,Text},
            };
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public async Task<bool> UpdateAsync(CTB_Products item)
        {
            SortedList SL = AssignValuesToSortedList(item, "Update_Product");
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public async Task<DataTable> Get_AllProduct_BySearch(string search, int PageNumber = 1, int PageSize = 500000)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllProduct_BySearch" },
                {"@PageNumber",PageNumber },
                {"@PageSize",PageSize},
                {MTB_Products.product_NameAr,search },

            };

            DataTable dt =await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

        public async Task<DataTable> Get_AllProduct_BySearchFavorite(string search, int PageNumber = 1, int PageSize = 500000)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllProduct_BySearchFavorite" },
                {"@PageNumber",PageNumber },
                {"@PageSize",PageSize},
                {MTB_Products.product_NameAr,search },

            };

            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

        public int Get_CountProduct()
        {
            SortedList SL = new SortedList
                {
                { "@Check", "Get_CountProduct" },
            };

            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return Convert.ToInt32(dt.Rows[0][0].ToString());
        }

        public DataTable Get_Product_product_Id(string product_Id, int PageNumber = 1, int PageSize = 50)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_Product_product_Id" },
                {"@PageNumber",PageNumber },
                {"@PageSize",PageSize},
                {MTB_Products.product_Id,product_Id },

            };

            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

        public async Task<DataTable> Get_AllProduct(int PageNumber = 1, int PageSize = 5000)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_AllProduct" },
                { "@PageNumber", PageNumber },
                { "@PageSize", PageSize },
            };

            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }

        public bool Check_ProductIdNotDuplicate(string product_Id)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Check_ProductIdNotDuplicate" },
                 { MTB_Products.product_Id, product_Id },

            };

            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            int result = Convert.ToInt16(dt.Rows[0][0]);
            return result > 0;
        }

        public async Task<DataTable> Get_DistinctProduct()
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_DistinctProduct" },
            };

            DataTable dt = await DAL.SelectDBasync(ProcedureName, SL);
            return dt;
        }


    }
}
