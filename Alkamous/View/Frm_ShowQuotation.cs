using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using static Alkamous.Model.CTB_Products;

namespace Alkamous.View
{
    public partial class Frm_ShowQuotation : Form
    {

        #region Declare variables

        ClsOperationsofInvoices OperationsofInvoices = new ClsOperationsofInvoices(new DataAccessLayer());

        public static DataTable dtProducts = new DataTable();

        private static Frm_ShowQuotation frmCustomerShowInvoices;
        public static string Invoice_NumberToGetData { get; set; }

        #endregion

        // this methode to make a new form same as orgnal from
        public static Frm_ShowQuotation FrmCustomerShowInvoices
        {
            get { return frmCustomerShowInvoices; }
        }


        public Frm_ShowQuotation()
        {
            InitializeComponent();
            frmCustomerShowInvoices = this;

            try
            {
                // مهنم لوضع اكون للبرنامج
                Icon = Icon.ExtractAssociatedIcon($"{System.Diagnostics.Process.GetCurrentProcess().ProcessName}.exe");
            }
            catch (Exception ex)
            {
                Chelp.WriteErrorLog("ICon =>" + ex.Message);
            }
        }
        private void DGVColumnHeaderTextAndWidthProductes()
        {
            try
            {
                Model.CTB_Products MCTB_Products = new Model.CTB_Products(ProductFieldNaming.SqlParameter);


                DGVProducts.RowHeadersVisible = false;

                using (dtProducts = new DataTable())
                {
                    dtProducts.Columns.Add(MCTB_Products.product_Id);
                    dtProducts.Columns.Add(MCTB_Products.product_NameEn);
                    dtProducts.Columns.Add(MCTB_Products.product_NameAr);
                    dtProducts.Columns.Add("Invoice_QTY");
                    dtProducts.Columns.Add("Invoice_Unit");
                    dtProducts.Columns.Add(MCTB_Products.product_Price);
                    dtProducts.Columns.Add("Invoice_Amount");

                    DGVProducts.DataSource = dtProducts;
                }


                DGVProducts.Columns[0].HeaderText = "Code";
                DGVProducts.Columns[1].HeaderText = "product En";
                DGVProducts.Columns[2].HeaderText = "product Ar";
                DGVProducts.Columns[3].HeaderText = "QTY";
                DGVProducts.Columns[4].HeaderText = "Unit";
                DGVProducts.Columns[5].HeaderText = "Price";
                DGVProducts.Columns[6].HeaderText = "Amount";

                DGVProducts.Columns[0].Width = 50;  // Code
                DGVProducts.Columns[1].Width = 200; // Product En
                DGVProducts.Columns[2].Width = 200; // Product Ar
                DGVProducts.Columns[3].Width = 40;  // QTY
                DGVProducts.Columns[4].Width = 40;  // Unit
                DGVProducts.Columns[5].Width = 60;  // Price
                DGVProducts.Columns[6].Width = 100; // Amount

                // Set the font and alignment for the first column                
                DGVProducts.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // Set the font and alignment for the second column               
                DGVProducts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                DGVProducts.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DGVProducts.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


                DGVProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                // Manually set the height of any rows that exceed the default maximum height
                DGVProducts.RowTemplate.Height = 50; // set a default height for rows

                DGVProducts.Columns[0].ReadOnly = true;
                DGVProducts.Columns[1].ReadOnly = true;
                DGVProducts.Columns[2].ReadOnly = true;
                //DGVProducts.Columns[3].ReadOnly = true;
                //DGVProducts.Columns[4].ReadOnly = true;
                //DGVProducts.Columns[5].ReadOnly = true;
                DGVProducts.Columns[6].ReadOnly = true;

                for (int i = 0; i < DGVProducts.Columns.Count; i++)
                    DGVProducts.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void LoadDataToFrmEdit(string Invoice_Number)
        {

            try
            {
                //OperationsofInvoices
                dtProducts = OperationsofInvoices.Get_Invoice_ByInvoice_Number(Invoice_Number);
                DGVProducts.DataSource = dtProducts;

            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void Frm_ShowQuotation_Load(object sender, EventArgs e)
        {
            DGVColumnHeaderTextAndWidthProductes();
            LoadDataToFrmEdit(Invoice_NumberToGetData);

        }
    }
}
