﻿namespace Alkamous.Model
{
    public class CTB_Products
    {
        public string product_AutoNum { get; set; }

        public string product_Id { get; set; }

        public string product_NameAr { get; set; }

        public string product_NameEn { get; set; }

        public string product_Price { get; set; }

        public string product_Unit { get; set; }

        public string product_favorite { get; set; } = "0";

        public CTB_Products()
        {
            product_AutoNum = "@product_AutoNum";
            product_Id = "@product_Id";
            product_NameAr = "@product_NameAr";
            product_NameEn = "@product_NameEn";
            product_Price = "@product_Price";
            product_Unit = "@product_Unit";
            product_favorite = "@product_favorite";
        }

        public CTB_Products(string ctr2)
        {
            product_AutoNum = "product_AutoNum";
            product_Id = "product_Id";
            product_NameAr = "product_NameAr";
            product_NameEn = "product_NameEn";
            product_Price = "product_Price";
            product_Unit = "product_Unit";
            product_favorite = "product_favorite";
        }



    }

}
