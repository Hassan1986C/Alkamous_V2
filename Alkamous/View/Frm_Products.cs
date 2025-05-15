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


        // Define these class-level variables at the top of your form class
        private int currentPage = 1;
        private int pageSize = 100; // Consider reducing from 100 to improve performance
        private bool isLoading = false;
        private bool endOfData = false;
        private Timer _searchTimer;

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

        //private void DGVColumnHeaderTextAndWidth()
        //{
            
        //    DGVProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
        //    //DGVProducts.EnableHeadersVisualStyles = false;
        //    //DGVProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue;
        //    //DGVProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


        //    DGVProducts.RowHeadersVisible = false;
        //    DGVProducts.Columns[0].HeaderText = "Product ID";
        //    DGVProducts.Columns[1].HeaderText = "Product Name English";
        //    DGVProducts.Columns[2].HeaderText = "Product Name Arabic";
        //    DGVProducts.Columns[3].HeaderText = "Pric";
        //    DGVProducts.Columns[4].HeaderText = "Unit";
        //    DGVProducts.Columns[5].HeaderText = "favorite";

        //    DGVProducts.Columns[0].Width = 25;
        //    DGVProducts.Columns[1].Width = 60;
        //    DGVProducts.Columns[2].Width = 60;
        //    DGVProducts.Columns[3].Width = 20;
        //    DGVProducts.Columns[4].Width = 20;

        //    DGVProducts.Columns[5].Visible = false;

        //    for (int i = 0; i < DGVProducts.Columns.Count; i++)
        //        DGVProducts.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;


        //    DGVProducts.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
        //    DGVProducts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    DGVProducts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    DGVProducts.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        //}

        private void InitializeDGVProducts()
        {
            DGVProducts.AutoGenerateColumns = false;
            DGVProducts.Columns.Clear();

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "product_Id",
                DataPropertyName = "product_Id",
                HeaderText = "Product ID",
                Width = 10
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "name_en",
                DataPropertyName = "product_NameEn",
                HeaderText = "Product Name English",
                Width = 60
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "name_ar",
                DataPropertyName = "product_NameAr",
                HeaderText = "Product Name Arabic",
                Width = 60
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "price",
                DataPropertyName = "product_Price",
                HeaderText = "Pric",
                Width = 20
            });

            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "unit",
                DataPropertyName = "product_Unit",
                HeaderText = "Unit",
                Width = 20                
            });

            DGVProducts.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "favorite",
                DataPropertyName = "product_favorite",
                HeaderText = "Favorite",
                Visible = false
            });

            // Styling only
            DGVProducts.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
            DGVProducts.RowHeadersVisible = false;

            for (int i = 0; i < DGVProducts.Columns.Count; i++)
            {
                DGVProducts.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            DGVProducts.Columns["name_en"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGVProducts.Columns["name_ar"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGVProducts.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGVProducts.Columns["unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }


        private async Task<Task> LoadData(string Search = "")
        {
            try
            {

                var ResultOfData = string.IsNullOrEmpty(Search)
                   ? await OperationsofProducts.Get_AllProduct(1, 5000000)
                   : await OperationsofProducts.Get_AllProduct_BySearch(Search, 1, 50000);

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

        private async Task<DataTable> LoadData2(int page = 1, int pageSize = 100, string search = "")
        {
            try
            {
                DataTable resultOfData;
                bool Isfavorite = BtnFavorite.Checked;
                // Apply consistent pagination for both search and non-search scenarios
                if (string.IsNullOrEmpty(search) && !Isfavorite)
                {
                    // No search term - get all products with pagination
                    resultOfData = await OperationsofProducts.Get_AllProduct(page, pageSize);
                }
                else if (!Isfavorite)
                {
                    // Apply search with pagination
                    resultOfData = await OperationsofProducts.Get_AllProduct_BySearch(search, page, pageSize);
                }
                else
                {
                    // Apply search + favorite with pagination
                    resultOfData = await OperationsofProducts.Get_AllProduct_BySearchFavorite(search, page, pageSize);
                }                

                return resultOfData;
            }
            catch (Exception ex)
            {
                string methodName = System.Reflection.MethodBase.GetCurrentMethod().Name;
                Chelp.WriteErrorLog(Name + " => " + methodName + " => " + ex.Message);
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Reset and perform a new search
        private async Task PerformSearch()
        {
            // Reset pagination state
            currentPage = 1;
            endOfData = false;

            // Clear the DataGridView
            if (DGVProducts.DataSource != null)
            {
                ((DataTable)DGVProducts.DataSource).Clear();
                DGVProducts.DataSource = null;
            }

            // Load first page of new search results
            await LoadNextPageAsync();
           
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {

            // Set up debounce timer if not already created
            if (_searchTimer == null)
            {
                _searchTimer = new Timer();
                _searchTimer.Interval = 500; // 500ms delay
                _searchTimer.Tick += async (s, args) =>
                {
                    _searchTimer.Stop();
                    await PerformSearch();
                };
            }

            // Restart the timer
            _searchTimer.Stop();
            _searchTimer.Start();

        }

        private async void Frm_Products_Load(object sender, EventArgs e)
        {

            // Reset pagination state
            currentPage = 1;
            endOfData = false;

            // Load initial data
            await LoadNextPageAsync();           
            InitializeDGVProducts();
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
            await PerformSearch();
        }

        // Improved scroll handler 
        private async void DGVProducts_Scroll(object sender, ScrollEventArgs e)
        {
            // Only handle vertical scrolling events
            if (e.ScrollOrientation != ScrollOrientation.VerticalScroll)
                return;

            // Don't proceed if already loading or at end of data
            if (isLoading || endOfData)
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
            if (isLoading || endOfData) return;

            isLoading = true;

            try
            {
                string searchText = TxtSearch.Text.Trim();

                // Get the next page of data using the current page counter
                var newData = await LoadData2(currentPage, pageSize, searchText);

                // Check if we got any data back
                if (newData == null || newData.Rows.Count == 0)
                {
                    endOfData = true;
                    isLoading = false;
                    return;
                }

                // For first page, just set as DataSource
                if (DGVProducts.DataSource == null || currentPage == 1)
                {
                    DGVProducts.DataSource = newData;
                }
                else
                {
                    // For subsequent pages, append to existing DataSource
                    DataTable currentData = (DataTable)DGVProducts.DataSource;

                    // Check for and exclude any duplicate rows before adding
                    foreach (DataRow newRow in newData.Rows)
                    {
                        // Get the product_Id to check for duplicates
                        string productId = newRow["product_Id"].ToString();

                        // Skip if we already have this product_Id
                        if (currentData.Select($"product_Id = '{productId}'").Length == 0)
                        {
                            currentData.ImportRow(newRow);
                        }
                    }
                }              
               
                // Increment current page for next load
                currentPage++;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isLoading = false;
            }
        }


    }
}
