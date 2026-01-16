using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_Products : Form
    {
        private readonly ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts(new DataAccessLayer());
        private readonly CTB_Products MCTB_Products = new CTB_Products(CTB_Products.ProductFieldNaming.Plain);

        public static string ExChangeRate, Taxes, Currency, WhoSendOrder, isMainQuotation;


        private readonly LazyLoading LazyDataLoader = new LazyLoading();

        private readonly CancellationTokenSource _cancellationTokenSource;
        bool isScrolling = false;

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

        //private void ReColoreDGV(DataGridView dataGridView)
        //{

        //    foreach (DataGridViewRow row in dataGridView.Rows)
        //    {
        //        if (row.Index % 2 == 0)
        //        {
        //            dataGridView.Rows[row.Index].DefaultCellStyle.BackColor = Color.WhiteSmoke;
        //        }
        //    }
        //}

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

            // 1. تلوين الصفوف المتبادلة (الذي شرحناه سابقاً)
            DGVProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            // 2. تلوين الصف عند اختياره (التصحيح)
            DGVProducts.DefaultCellStyle.SelectionBackColor = Color.LightSteelBlue;
            DGVProducts.DefaultCellStyle.SelectionForeColor = Color.Black;

            // 3. جعل كامل الصف يتم تحديده عند الضغط على أي خلية
            DGVProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        }



        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LazyDataLoader.TxtSearch_Fun(DGVProducts, LoadNextPageAsync);
            isScrolling = false;
        }

        private void Frm_Products_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadEnumProdectsModules();

        }

        #region  SelectProductAndSendParameterToForms

        private void SelectProductAndSendParameterToForms()
        {
            try
            {
                var isMultiSelect = BtnMultiSelectItem.Checked;

                // Group
                // NewInvoices
                // UpdateInvoices
                // who sends the order Group or NewInvoices or UpdateInvoices
                switch (WhoSendOrder)
                {
                    case "Group":

                        HandleProductsSelection(isMainQuotation,
                                                isMultiSelect, MCTB_Products,
                                                Frm_Products_group.FrmProductsgroup,
                                                Frm_Products_group.dtProducts,
                                                null);
                        break;

                    case "NewInvoices":

                        HandleProductsSelection(isMainQuotation,
                                                isMultiSelect, MCTB_Products,
                                                Frm_CustomersAddNewInvoices.FrmCustomerAddNewInvoices,
                                                Frm_CustomersAddNewInvoices.dtProducts,
                                                Frm_CustomersAddNewInvoices.dtProductsConsumable);
                        break;

                    case "UpdateInvoices":

                        HandleProductsSelection(isMainQuotation,
                                                isMultiSelect, MCTB_Products,
                                                Frm_CustomersUpdateInvoices.FrmCustomersUpdateInvoices,
                                                Frm_CustomersUpdateInvoices.dtProducts,
                                                Frm_CustomersUpdateInvoices.dtProductsConsumable);
                        break;

                    default:
                        MessageBox.Show("لم يتم تحديد نوع العملية!");
                        break;
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



        private void HandleProductsSelection(string isMainQuotation, bool isMultiSelect, CTB_Products MCTB_Products,
                                             Form targetForm, DataTable targetTable, DataTable consumableTable)
        {
            // Group
            // MainQuotation
            // Consumable
            // who sends the request Group MainQuotation or consumable 
            switch (isMainQuotation)
            {
                case "Group":

                    AddMultiProducts(MCTB_Products, targetTable,
                                        targetForm.Controls.Find("DGVProducts", true).FirstOrDefault() as DataGridView,
                                        targetForm.Controls.Find("LbCountProdects", true).FirstOrDefault() as Label,
                                        targetForm.Controls.Find("TxtTotalAmount", true).FirstOrDefault() as TextBox);

                    break;

                case "MainQuotation":
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

                    break;
                case "Consumable":
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

                    break;
                default:
                    MessageBox.Show("لم يتم تحديد نوع العملية!");
                    break;
            }

        }

       
        private void AddMultiProducts(CTB_Products MCTB_Products, DataTable targetTable, DataGridView targetDGV, Label targetLabel, TextBox TotalAmount)
        {
            var productsToAdd = new List<DataGridViewRow>();

            // 1. Collect selected rows and filter out products already present in the target table
            foreach (DataGridViewRow row in DGVProducts.SelectedRows.Cast<DataGridViewRow>().Reverse())
            {
                // Safely extract the Product ID
                string productId = row.Cells[MCTB_Products.product_Id].Value?.ToString()?.Trim() ?? string.Empty;

                if (string.IsNullOrEmpty(productId)) continue;

                // Use LINQ to check if the product already exists in the target DataTable
                bool productExists = targetTable.AsEnumerable().Any(dr => dr.Field<string>(0) == productId);

                if (!productExists)
                {
                    productsToAdd.Add(row);
                }
            }

            // 2. Process collected products and add them to targetTable
            foreach (var row in productsToAdd)
            {
                // Extract Product ID for the switch logic
                string productId = row.Cells[MCTB_Products.product_Id].Value?.ToString() ?? string.Empty;
                string quantity = "1"; // Default quantity

                // 3. Determine quantity based on Product ID (Specific business logic)
                switch (productId)
                {
                    case "EV-C4511":
                    case "EV-C8001":
                    case "FA-81754":
                    case "SL-CR80-BK":
                    case "EV-C8152":
                    case "ID-CARD-033":
                        quantity = "100";
                        break;
                    default:
                        quantity = "1";
                        break;
                }

                // 4. Extract price and handle potential null values
                string priceStr = row.Cells[MCTB_Products.product_Price].Value?.ToString() ?? "0";

                // 5. Calculate Price and Total Amount using the dynamic quantity determined above
                // The method handles currency exchange and taxes based on the quantity (1 or 100)
                var (STxtPrice, STxtAmount) = Chelp.ExchangeAndTaxesToForward(
                    ExChangeRate,
                    Taxes,
                    priceStr,
                    quantity,
                    Currency
                );

                // 6. Map data and add the new row to the target DataTable
                targetTable.Rows.Add(
                    productId,
                    row.Cells[MCTB_Products.product_NameEn].Value?.ToString() ?? string.Empty,
                    row.Cells[MCTB_Products.product_NameAr].Value?.ToString() ?? string.Empty,
                    quantity,
                    row.Cells[MCTB_Products.product_Unit].Value?.ToString() ?? string.Empty,
                    STxtPrice,
                    STxtAmount
                );
            }

            // 7. Update UI elements (Row count and Total Amount)
            targetLabel.Text = targetTable.Rows.Count.ToString();

            // Recalculate total sum if the quotation type is not "Group"
            if (isMainQuotation != "Group")
            {
                TotalAmount.Text = Chelp.DGVProductsChangededReSumTotalAmount(Currency, targetDGV);
            }
        }

        private void AddSingleProduct(CTB_Products MCTB_Products, DataGridView targetDGV, Form targetForm, bool isConsumable = false)
        {
            // 1. Guard Clause: Ensure there is a selected row to process
            var currentRow = targetDGV.CurrentRow;
            if (currentRow == null) return;

            // 2. Define the naming suffix and extract common values
            string suffix = isConsumable ? "Consumable" : string.Empty;
            string productId = currentRow.Cells[MCTB_Products.product_Id].Value?.ToString() ?? string.Empty;

            // Local helper function to safely find a control and set its text
            void SetControlText(string controlBaseName, object value)
            {
                var control = targetForm.Controls.Find($"{controlBaseName}{suffix}", true).FirstOrDefault();
                if (control != null)
                {
                    control.Text = value?.ToString() ?? string.Empty;
                }
            }

            // 3. Map DataGridView cell values to Form controls
            SetControlText("TxtProduct_Id", productId);
            SetControlText("TxtProduct_NameEn", currentRow.Cells[MCTB_Products.product_NameEn].Value);
            SetControlText("TxtProduct_NameAr", currentRow.Cells[MCTB_Products.product_NameAr].Value);
            SetControlText("TxtProduct_Price", currentRow.Cells[MCTB_Products.product_Price].Value);
            SetControlText("TxtProduct_Unit", currentRow.Cells[MCTB_Products.product_Unit].Value);

            // 4. Handle Quantity logic based on Product ID
            var qtyControl = targetForm.Controls.Find($"TxtQTY{suffix}", true).FirstOrDefault();
            if (qtyControl is TextBox qtyTextBox)
            {
                switch (productId)
                {
                    case "EV-C4511":
                    case "EV-C8001":
                    case "FA-81754":
                    case "SL-CR80-BK":
                    case "EV-C8152":
                    case "ID-CARD-033":
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

        int DataHaveBeenloaded = 0;
       
        public async void LoadEnumProdectsModules()
        {
            if (DataHaveBeenloaded == 0)
            {
                // Load data from database (await the task)
                DataTable dtDep = await OperationsofProducts.Get_product_item_Group();

                // Add a default "Select Printer Type" row at the top
                DataRow newRow = dtDep.NewRow();
                newRow["product_Group_Name"] = "Load ALL Prodects";
                newRow["product_item_Group_AutoNum"] = "0";
                dtDep.Rows.InsertAt(newRow, 0);

                // Bind to ComboBox
                TxtGroupByItem.DataSource = dtDep;
                TxtGroupByItem.DisplayMember = "product_Group_Name";
                TxtGroupByItem.ValueMember = "product_item_Group_AutoNum";


                DataHaveBeenloaded++;
                Cursor.Current = Cursors.Default;
                TxtGroupByItem.SelectedIndex = 0;
            }
        }

        private async void TxtGroupByItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            await Task.Delay(400);

            if (TxtGroupByItem.SelectedIndex == 0)
            {
                
                await LazyDataLoader.PerformSearchAsync(DGVProducts);
                await LoadNextPageAsync();
                TxtSearch.Clear();
                isScrolling = false;

            }
            else
            {
                
                LoadDataGroupByItem(TxtGroupByItem.SelectedValue.ToString());
                isScrolling = true;
            }
          
        }

        private void Frm_Products_FormClosing(object sender, FormClosingEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

       

        private async void LoadDataGroupByItem(string Search)
        {
            using (var dt = await OperationsofProducts.Get_product_GroupBy_BySearch(Search))
            {
                DGVProducts.DataSource = dt;
                LbCount.Text = dt.Rows.Count.ToString();
            }

        }
        private async void BtnFavorite_CheckedChanged(object sender, EventArgs e)
        {
            BtnFavorite.ForeColor = BtnFavorite.Checked ? Color.Red : Color.Black;
            await Task.Delay(200);
            
            if (await LazyDataLoader.PerformSearchAsync(DGVProducts))
            {
                // Load first page of new search results
                await LoadNextPageAsync();
            }
            isScrolling = false;
        }

       
        private async void DGVProducts_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                if (isScrolling) return;


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
            catch (Exception)
            {

            }

        }

        private async Task LoadNextPageAsync()
        {
            string uniqueIdColumnName = "product_Id";
            string Search = TxtSearch.Text.Trim();
            bool Isfavorite = BtnFavorite.Checked;

            await LazyDataLoader.LoadNextPageAsync(

                          uniqueIdColumnName,
                          Search,
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
