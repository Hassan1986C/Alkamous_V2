using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Alkamous.Controller
{
    public class ClsOperationsofProducts : IBaseOperation<CTB_Products>, IProducts,IProductsGroupBy
    {
        private readonly string ProcedureName = "SP_TB_Products";
        private readonly string ProcedureName2 = "SP_TB_product_GroupBy";

        public enum ProdectModels
        {
            Primacy_1,
            Primacy_2,
            Primacy_2_with_encoders,
            Primacy_2_Lam,
            Primacy_2_Lam_with_encoders,
            zenius_1,
            zenius_2,
            zenius_2_with_encoders,
            Agilia,
            Agilia_Lam,
            Agilia_with_encoders,
            Agilia_Lam_with_encoders,
            CardPresso_licenses,
            Tripod_Turnstile_security_Gate,
            Parking_System_security,
            Monochrome_Ribbon,
            EDIKIO_Printer,
            PerPrinted_Card,

        }

        CTB_Products MTB_Products = new CTB_Products(CTB_Products.ProductFieldNaming.SqlParameter);

        
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
                {"@Check", "Get_AllProduct_BySearchFavorite" },
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




        //////////////////// product_GroupBy /////////////////

        public async Task<bool> Add_product_GroupByAsync(string product_GroupBy_Name, string product_id)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Add_product_GroupBy" },
                { "@product_GroupBy_Name",product_GroupBy_Name},
                {"@product_id",product_id }
            };

            int result = await DAL.RunProcedureasync(ProcedureName2, SL);
            return result == 1;
        }

        public async Task<DataTable> Get_product_GroupBy_BySearch(string search, int PageNumber = 1, int PageSize = 500000)
        {
            SortedList SL = new SortedList
            {
                {"@Check", "Get_product_GroupBy_BySearch" },
                {"@product_GroupBy_Name",search },
                {"@PageNumber", PageNumber },
                {"@PageSize", PageSize },
                
            };

            DataTable dt = await DAL.SelectDBasync(ProcedureName2, SL);
            return dt;
        }

        public async Task<bool> Delete_product_GroupBy(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Delete_product_GroupBy" },
                { "@product_GroupBy_Name",Text},
            };
            int result = await DAL.RunProcedureasync(ProcedureName2, SL);
            return result == 1;
        }


    }
}
