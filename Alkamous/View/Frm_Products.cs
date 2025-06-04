using Alkamous.Controller;
using Alkamous.InterfaceForAllClass;
using Alkamous.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Alkamous.View
{
    public partial class Frm_Products : Form
    {        
        private readonly ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts(new DataAccessLayer());
        private readonly CTB_Products MCTB_Products = new CTB_Products(CTB_Products.ProductFieldNaming.Plain);
        public static bool isAddNewInvoices, isMainQuotation;
        public static string ExChangeRate, Taxes, Currency;

        private readonly LazyLoading LazyDataLoader = new LazyLoading();       

        private CancellationTokenSource _cancellationTokenSource;


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

        private void InitializeDataGridView()
        {
            DGVProducts.AutoGenerateColumns = false;
            DGVProducts.Columns.Clear();
            CTB_Products MCTB_Products = new CTB_Products(CTB_Products.ProductFieldNaming.Plain);
            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {

                Name = MCTB_Products.product_Id,
                DataPropertyName = MCTB_Products.product_Id,
                HeaderText = "Product ID",
                Width = 10
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Products.product_NameEn,
                DataPropertyName = MCTB_Products.product_NameEn,
                HeaderText = "Product Name English",
                Width = 60
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Products.product_NameAr,
                DataPropertyName = MCTB_Products.product_NameAr,
                HeaderText = "Product Name Arabic",
                Width = 60
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Products.product_Price,
                DataPropertyName = MCTB_Products.product_Price,
                HeaderText = "Pric",
                Width = 20
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Products.product_Unit,
                DataPropertyName = MCTB_Products.product_Unit,
                HeaderText = "Unit",
                Width = 20
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Products.product_favorite,
                DataPropertyName = MCTB_Products.product_favorite,
                HeaderText = "Favorite",
                Width = 20,
                Visible = false
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalCount",
                DataPropertyName = "TotalCount",
                HeaderText = "TotalCount",
                Width = 20,
                Visible = false
            });

            // Styling only
            DGVProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
            DGVProducts.RowHeadersVisible = false;

            for (int i = 0; i < DGVProducts.Columns.Count; i++)
            {
                DGVProducts.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DGVProducts.Columns[MCTB_Products.product_NameEn].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGVProducts.Columns[MCTB_Products.product_NameAr].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGVProducts.Columns[MCTB_Products.product_Price].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGVProducts.Columns[MCTB_Products.product_Unit].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }

        
        
        private  void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LazyDataLoader.TxtSearch_Fun(DGVProducts,LoadNextPageAsync );            

        }

        private async void Frm_Products_Load(object sender, EventArgs e)
        {
          
            // Load initial data
            await LoadNextPageAsync();
            InitializeDataGridView();
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

            if (await LazyDataLoader.PerformSearchAsync(DGVProducts))
            {
                // Load first page of new search results
                await LoadNextPageAsync();
            }
        }


        private async void DGVProducts_Scroll(object sender, ScrollEventArgs e)
        {
            // Only handle vertical scrolling events
            if (e.ScrollOrientation != ScrollOrientation.VerticalScroll)
                return;

            // Don't proceed if already loading or at end of data
            if (LazyDataLoader.IsLoading || LazyDataLoader.EndOfData)
                return;

            var dgv = (DataGridView)sender;

            // Calculate how close we are to the bottom - load when within 3 rows
            int lastVisibleRowIndex = dgv.FirstDisplayedScrollingRowIndex + dgv.DisplayedRowCount(true) - 1;
            int totalRows = dgv.RowCount;


            // Load next page when scrolled near the bottom
            if (totalRows > 0 && lastVisibleRowIndex >= totalRows - 3)
            {
                await LoadNextPageAsync();
            }

        }

        private async Task LoadNextPageAsync()
        {
            bool Isfavorite = BtnFavorite.Checked;

            await LazyDataLoader.LoadNextPageAsync(

                         "product_Id",
                          TxtSearch.Text.Trim(),
                          Isfavorite,
                          DGVProducts,
                          OperationsofProducts.Get_AllProduct,                      // Matches Func<int, int, Task<DataTable>>
                          OperationsofProducts.Get_AllProduct_BySearch,             // Matches Func<string, int, int, Task<DataTable>>
                          OperationsofProducts.Get_AllProduct_BySearchFavorite      // Matches Func<string, int, int, Task<DataTable>>
                          );


            LbCount.Text = LazyDataLoader.TotalCount;
        }
    }
}
