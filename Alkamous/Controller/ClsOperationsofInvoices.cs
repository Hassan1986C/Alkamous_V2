using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Alkamous.Controller
{
    public class ClsOperationsofInvoices : IBaseOperation<CTB_Invoices>, IInvoices
    {
        private readonly string ProcedureName = "SP_TB_Invoices";
       
        private readonly IDataAccessLayer DAL;

        private CTB_Invoices MTB_Invoices = new CTB_Invoices();
        private readonly List<SortedList> Listofsortedlis = new List<SortedList>();

        public ClsOperationsofInvoices(IDataAccessLayer _DAL)
        {
            DAL = _DAL;
        }

        private bool CheckResult(SortedList SL)
        {
            int result = DAL.RunProcedure(ProcedureName, SL);
            return result == 1;
        }


        public async Task<bool> AddNewAsync(CTB_Invoices item)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Add_NewInvoice" },
                { MTB_Invoices.Invoice_Number, item.Invoice_Number },
                { MTB_Invoices.Invoice_product_Id, item.Invoice_product_Id },
                { MTB_Invoices.Invoice_Unit, item.Invoice_Unit},
                { MTB_Invoices.Invoice_QTY , item.Invoice_QTY },
                { MTB_Invoices.Invoice_Price ,item. Invoice_Price },
                { MTB_Invoices.Invoice_Amount, item.Invoice_Amount },
            };

            int result =await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public async Task<bool> DeleteAsync(string Text)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Delete_InvoiceByInvoice_Number" },
                { MTB_Invoices.Invoice_Number, Text},

            };
            int result = await DAL.RunProcedureasync(ProcedureName, SL);
            return result == 1;
        }

        public Task<bool> UpdateAsync(CTB_Invoices item)
        {
            throw new System.NotImplementedException();
        }


        public void Add_NewInvoiceLIST(CTB_Invoices item)
        {

            SortedList SL = new SortedList
            {
                { "@Check", "Add_NewInvoice" },
                { MTB_Invoices.Invoice_Number, item.Invoice_Number },
                { MTB_Invoices.Invoice_product_Id, item.Invoice_product_Id },
                { MTB_Invoices.Invoice_Unit, item.Invoice_Unit},
                { MTB_Invoices.Invoice_QTY , item.Invoice_QTY },
                { MTB_Invoices.Invoice_Price , item.Invoice_Price },
                { MTB_Invoices.Invoice_Amount, item.Invoice_Amount },
            };

            Listofsortedlis.Add(SL);

        }

        public void InsertBulk()
        {
            int result = DAL.RunProcedureBulk(ProcedureName, Listofsortedlis);
            Listofsortedlis.Clear();
        }

        public DataTable Get_Invoice_ByInvoice_Number(string Invoice_Number)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_Invoice_ByInvoice_Number" },

                {MTB_Invoices.Invoice_Number, Invoice_Number}
            };

            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

       

        public DataTable Get_Invoice_Byproduct_Id(string Invoice_product_Id)
        {
            SortedList SL = new SortedList
            {
                { "@Check", "Get_Invoice_Byproduct_Id" },

                {MTB_Invoices.Invoice_product_Id, Invoice_product_Id}
            };

            DataTable dt = DAL.SelectDB(ProcedureName, SL);
            return dt;
        }

    }
}
