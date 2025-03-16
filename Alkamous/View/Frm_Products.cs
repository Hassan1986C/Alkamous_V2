using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_Products : Form
    {
        private readonly ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts();
        private readonly CTB_Products MCTB_Products = new CTB_Products("ctr2");
        public static bool isAddNewInvoices, isMainQuotation;
        public static string ExChangeRate, Taxes, Currency;

        public Frm_Products()
        {
            InitializeComponent();


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

        private void ReColoreDGV(DataGridView dataGridView)
        {

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Index % 2 == 0)
                {
                    dataGridView.Rows[row.Index].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }
            }
        }

        private void DGVColumnHeaderTextAndWidth()
        {

            DGVProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
            //DGVProducts.EnableHeadersVisualStyles = false;
            //DGVProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
            //DGVProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            DGVProducts.RowHeadersVisible = false;
            DGVProducts.Columns[0].HeaderText = "Product ID";
            DGVProducts.Columns[1].HeaderText = "Product Name English";
            DGVProducts.Columns[2].HeaderText = "Product Name Arabic";
            DGVProducts.Columns[3].HeaderText = "Pric";
            DGVProducts.Columns[4].HeaderText = "Unit";
            DGVProducts.Columns[5].HeaderText = "favorite";

            DGVProducts.Columns[0].Width = 25;
            DGVProducts.Columns[1].Width = 60;
            DGVProducts.Columns[2].Width = 60;
            DGVProducts.Columns[3].Width = 20;
            DGVProducts.Columns[4].Width = 20;
            
            DGVProducts.Columns[5].Visible = false;

            for (int i = 0; i < DGVProducts.Columns.Count; i++)            
                DGVProducts.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            DGVProducts.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGVProducts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGVProducts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGVProducts.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        private async Task<Task> LoadData(string Search = "")
        {
            try
            {

                var ResultOfData = string.IsNullOrEmpty(Search)
                   ? await OperationsofProducts.Get_AllProduct(1, 500000)
                   : OperationsofProducts.Get_AllProduct_BySearch(Search, 1, 50000);

                if (BtnFavorite.Checked)
                {
                    // إنشاء DataView لتصفية البيانات
                    var filteredView = new DataView(ResultOfData)
                    {
                        RowFilter = "product_favorite = 1" 
                    };

                    // تحويل DataView إلى DataTable للحصول على النتائج المفلترة
                    ResultOfData = filteredView.ToTable();
                }
                DGVProducts.DataSource = ResultOfData;
                LbCount.Text = ResultOfData.Rows.Count.ToString();
                ReColoreDGV(DGVProducts);
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
                return null;
            }
        }
               
        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadData(TxtSearch.Text);
        }

        private async void Frm_Products_Load(object sender, EventArgs e)
        {
            var ResultOfData = await LoadData();
                        
            if (ResultOfData.Status == TaskStatus.RanToCompletion)
            {
                DGVColumnHeaderTextAndWidth();
            }
        }

        #region  SelectProductAndSendParameterToForms

        private void SelectProductAndSendParameterToForms()
        {
            try
            {
                var isMultiSelect = BtnMultiSelectItem.Checked;
                if (isAddNewInvoices)
                {
                    HandleProductsSelection(isMainQuotation,
                                            isMultiSelect, MCTB_Products,
                                            Frm_CustomersAddNewInvoices.FrmCustomerAddNewInvoices,
                                            Frm_CustomersAddNewInvoices.dtProducts,
                                            Frm_CustomersAddNewInvoices.dtProductsConsumable);
                }
                else
                {
                    HandleProductsSelection(isMainQuotation,
                                            isMultiSelect, MCTB_Products,
                                            Frm_CustomersUpdateInvoices.FrmCustomersUpdateInvoices,
                                            Frm_CustomersUpdateInvoices.dtProducts,
                                            Frm_CustomersUpdateInvoices.dtProductsConsumable);
                }

                Close();
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Chelp.WriteErrorLog($"{Name} => {methodName} => {ex.Message}");
                MessageBox.Show(ex.Message);
            }
        }

        private void HandleProductsSelection(bool isMainQuotation, bool isMultiSelect, CTB_Products MCTB_Products,
                                             Form targetForm, DataTable targetTable, DataTable consumableTable)
        {
            // who sends the request Quotation or consumable 
            if (isMainQuotation)
            {
                if (isMultiSelect)
                {
                    AddMultiProducts(MCTB_Products, targetTable,
                                     targetForm.Controls.Find("DGVProducts", true).FirstOrDefault() as DataGridView,
                                     targetForm.Controls.Find("LbCountProdects", true).FirstOrDefault() as Label,
                                     targetForm.Controls.Find("TxtTotalAmount", true).FirstOrDefault() as TextBox);

                }
                else
                {
                    AddSingleProduct(MCTB_Products, DGVProducts, targetForm);
                }
            }
            else
            {
                if (isMultiSelect)
                {
                    AddMultiProducts(MCTB_Products, consumableTable,
                                     targetForm.Controls.Find("DGVProductsConsumable", true).FirstOrDefault() as DataGridView,
                                     targetForm.Controls.Find("LbCountProdectsConsumable", true).FirstOrDefault() as Label,
                                     targetForm.Controls.Find("TxtTotalAmount", true).FirstOrDefault() as TextBox);
                }
                else
                {
                    AddSingleProduct(MCTB_Products, DGVProducts, targetForm, true);
                }
            }
        }

        private void AddMultiProducts(CTB_Products MCTB_Products, DataTable targetTable, DataGridView targetDGV, Label targetLabel, TextBox TotalAmount)
        {

            var productsToAdd = new List<DataGridViewRow>();

            // Collect rows to be added
            foreach (DataGridViewRow row in DGVProducts.SelectedRows.Cast<DataGridViewRow>().Reverse())
            {
                string productId = row.Cells[MCTB_Products.product_Id].Value.ToString().Trim();

                // Check if the product is already in the targetTable by Using Linq
                bool productExists = targetTable.AsEnumerable().Any(dr => dr.Field<string>(0) == productId);

                if (!productExists) productsToAdd.Add(row);

            }

            // Add collected products to the targetTable
            foreach (var row in productsToAdd)
            {
                var (STxtPrice, STxtAmount) = Chelp.ExchangeAndTaxesToForward(ExChangeRate, Taxes, row.Cells[MCTB_Products.product_Price].Value.ToString(), "1", Currency);
                targetTable.Rows.Add(
                    row.Cells[MCTB_Products.product_Id].Value.ToString(),
                    row.Cells[MCTB_Products.product_NameEn].Value.ToString(),
                    row.Cells[MCTB_Products.product_NameAr].Value.ToString(),
                    "1",
                    row.Cells[MCTB_Products.product_Unit].Value.ToString(),
                    STxtPrice, STxtAmount);
            }

            // Update the target label and total amount
            targetLabel.Text = targetTable.Rows.Count.ToString(); // Updated to targetTable.Rows.Count instead of targetDGV.Rows.Count
            TotalAmount.Text = Chelp.DGVProductsChangededReSumTotalAmount(Currency, targetDGV);
        }


        private void AddSingleProduct(CTB_Products MCTB_Products, DataGridView targetDGV, Form targetForm, bool isConsumable = false)
        {

            var currentRow = targetDGV.CurrentRow;
            var productId = currentRow.Cells[MCTB_Products.product_Id].Value.ToString();
            string suffix = isConsumable ? "Consumable" : string.Empty;

            targetForm.Controls.Find($"TxtProduct_Id{suffix}", true).FirstOrDefault().Text = productId;
            targetForm.Controls.Find($"TxtProduct_NameEn{suffix}", true).FirstOrDefault().Text = currentRow.Cells[MCTB_Products.product_NameEn].Value.ToString();
            targetForm.Controls.Find($"TxtProduct_NameAr{suffix}", true).FirstOrDefault().Text = currentRow.Cells[MCTB_Products.product_NameAr].Value.ToString();
            targetForm.Controls.Find($"TxtProduct_Price{suffix}", true).FirstOrDefault().Text = currentRow.Cells[MCTB_Products.product_Price].Value.ToString();
            targetForm.Controls.Find($"TxtProduct_Unit{suffix}", true).FirstOrDefault().Text = currentRow.Cells[MCTB_Products.product_Unit].Value.ToString();

            if (targetForm.Controls.Find($"TxtQTY{suffix}", true).FirstOrDefault() is TextBox qtyTextBox)
            {
                switch (productId)
                {
                    case "EV-C4511":
                    case "EV-C8001":
                    case "FA-81754":
                        qtyTextBox.Text = "100";
                        break;
                    default:
                        qtyTextBox.Text = "1";
                        break;
                }
            }
        }
        #endregion

        private void DGVProducts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectProductAndSendParameterToForms();
            }
        }

        private void DGVProducts_DoubleClick(object sender, EventArgs e)
        {
            SelectProductAndSendParameterToForms();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SelectProductAndSendParameterToForms();
            }
        }

        private void BtnMultiSelectItem_CheckedChanged(object sender, EventArgs e)
        {
            DGVProducts.MultiSelect = BtnMultiSelectItem.Checked;
            BtnMultiSelectItem.ForeColor = BtnMultiSelectItem.Checked ? Color.Red : Color.Black;

        }

        private async void BtnFavorite_CheckedChanged(object sender, EventArgs e)
        {
            BtnFavorite.ForeColor = BtnFavorite.Checked ? Color.Red : Color.Black;
            await LoadData();
        }

    }
}
