using Alkamous.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Alkamous.Controller;
using System.Threading;

namespace Alkamous.View
{
    public partial class Frm_Customers : Form
    {
        #region Declare variables and initialize a new instance of classes
        ClsOperationsofCustomers OperationsofCustomers = new ClsOperationsofCustomers(new DataAccessLayer());
        ClsOperationsofInvoices OperationsofInvoices = new ClsOperationsofInvoices(new DataAccessLayer());
        ClsOperationsofConsumable OperationsofConsumable = new ClsOperationsofConsumable(new DataAccessLayer());
        ClsOperationsofTermsInvoices OperationsofTermsInvoices = new ClsOperationsofTermsInvoices(new DataAccessLayer());
        ClsOperationsofBanks OperationsofBanks = new ClsOperationsofBanks(new DataAccessLayer());
        ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts(new DataAccessLayer());
        CLSExportDataToWordFile CLSExportDataToWordFile = new CLSExportDataToWordFile();
        private DataTable dt = new DataTable();


        private readonly LazyLoading LazyDataLoader = new LazyLoading();

        private CancellationTokenSource _cancellationTokenSource;



        #endregion

        #region this method will make a form same original form without creating a new instance
        private static Frm_Customers frmCustomer;
        public static Frm_Customers FrmCustomer
        {
            get { return frmCustomer; }
        }
        #endregion

        // private readonly ContextMenuStrip contextMenuStrip;
        public Frm_Customers()
        {
            InitializeComponent();
            frmCustomer = this;
            InitializeContextMenu();
        }

        #region ContextMenuStrip Clone quotation as new
        private void InitializeContextMenu()
        {
            var contextMenuStrip = new ContextMenuStrip();

            var cloneQuotation = new ToolStripMenuItem(" Clone Quotation as New ");

            cloneQuotation.Click += CloneQuotation_Click;

            contextMenuStrip.Items.Add(cloneQuotation);

            //var QuotationCP = new ToolStripMenuItem(" Quotation copy ");
            //QuotationCP.Click += QuotationCP_Click;
            //contextMenuStrip.Items.Add(QuotationCP);

            // Associate ContextMenuStrip with the DataGridView (DGVCustomers)
            DGVCustomers.ContextMenuStrip = contextMenuStrip;
        }

        private void CloneQuotation_Click(object sender, EventArgs e)
        {
            if (DGVCustomers.SelectedRows.Count > 0)
            {
                var MCTB_Customers = new CTB_Customers("ctr2");
                Frm_CustomersAddNewInvoices.Invoice_NumberToGetData = DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString();
                Frm_CustomersAddNewInvoices.Is_Edit_Invoices_FormOpen = true;
                Controller.Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_CustomersAddNewInvoices());
            }
        }
        #endregion


        private void InitializeDataGridView()
        {
            DGVCustomers.AutoGenerateColumns = false;
            DGVCustomers.Columns.Clear();
            CTB_Customers MCTB_Customers = new CTB_Customers("ctr2");
            DGVCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Customers.Customer_Invoice_Number,
                DataPropertyName = MCTB_Customers.Customer_Invoice_Number,
                HeaderText = "Quotation NO",
                Width = 30
            });

            DGVCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Customers.Customer_Company,
                DataPropertyName = MCTB_Customers.Customer_Company,
                HeaderText = "Company Name",
                Width = 30
            });

            DGVCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Customers.Customer_Name,
                DataPropertyName = MCTB_Customers.Customer_Name,
                HeaderText = "Costomer Name",
                Width = 30
            });

            DGVCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Customers.Customer_Mob,
                DataPropertyName = MCTB_Customers.Customer_Mob,
                HeaderText = "Costomer Mobel",
                Width = 30
            });

            DGVCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Customers.Customer_Email,
                DataPropertyName = MCTB_Customers.Customer_Email,
                HeaderText = "Costomer Email",
                Width = 30
            });

            DGVCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = MCTB_Customers.Customer_DateTime,
                DataPropertyName = MCTB_Customers.Customer_DateTime,
                HeaderText = "Quotation Data",
                Width = 20
            });

            DGVCustomers.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalCount",
                DataPropertyName = "TotalCount",
                HeaderText = "TotalCount",
                Width = 10,
                Visible = false
            });

            // Styling only
            DGVCustomers.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 4, 0, 4);
            DGVCustomers.RowHeadersVisible = false;

            for (int i = 0; i < DGVCustomers.Columns.Count; i++)
            {
                DGVCustomers.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            for (int i = 0; i < DGVCustomers.Columns.Count; i++)
            {

                DGVCustomers.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        private async void Frm_Customers_Load(object sender, EventArgs e)
        {
            await GetDistinctProductAsync();

            // Load first page of new search results
            await LoadNextPageAsync();
            InitializeDataGridView();

        }

        #region LoadData And Search


        private async Task LoadNextPageAsync()
        {
            bool Isfavorite = false;

            await LazyDataLoader.LoadNextPageAsync(

                         "Customer_Invoice_Number",
                          TxtSearch.Text.Trim(),
                          Isfavorite,
                          DGVCustomers,
                          OperationsofCustomers.Get_AllCustomer,              // Matches Func<int, int, Task<DataTable>>
                          OperationsofCustomers.Get_AllCustomer_BySearch,     // Matches Func<string, int, int, Task<DataTable>>
                          null                                                // Matches Func<string, int, int, Task<DataTable>>
                          );


            LbCount.Text = LazyDataLoader.TotalCount;

        }


        private async void DGVCustomers_Scroll(object sender, ScrollEventArgs e)
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


        private  void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            LazyDataLoader.TxtSearch_Fun(DGVCustomers, LoadNextPageAsync);
        }

        private async Task GetDistinctProductAsync()
        {
            DataTable result = await OperationsofProducts.Get_DistinctProduct();
            TxtDistinctProduct.ValueMember = "Invoice_product_Id";
            TxtDistinctProduct.DisplayMember = "product_NameEn";
            TxtDistinctProduct.DataSource = result;
            TxtDistinctProduct.SelectedIndex = -1;
        }

        #endregion

        private async void BtnDeleteRowFromDGVProducts_Click(object sender, EventArgs e)
        {

            try
            {
                if (DGVCustomers.RowCount > 0)
                {
                    Model.CTB_Customers MCTB_Customers = new Model.CTB_Customers("ctr2");
                    if (MessageBox.Show("Are you sure you want to Delete  " + Environment.NewLine
                        + Environment.NewLine + "Invoice Number    : " + DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString()
                        + Environment.NewLine + "Name   : " + DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Name].Value.ToString()

                        , "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        bool Result = await OperationsofCustomers.DeleteAsync(DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString());
                        if (Result)
                        {
                            await OperationsofInvoices.DeleteAsync(DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString());
                            OperationsofConsumable.Delete_ConsumableByConsumable_Number(DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString());
                            OperationsofTermsInvoices.Delete_Terms_Invoice(DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString());

                            Controller.Chelp.RegisterUsersActionLogs("Delete Quotation", DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString());
                            MessageBox.Show("Data Deleted Successfully ");
                            await LazyDataLoader.PerformSearchAsync(DGVCustomers);
                            await LoadNextPageAsync();
                        }
                        else
                        {
                            MessageBox.Show("Sorry, there is a mistake !!");
                        }
                        Cursor.Current = Cursors.Default;
                    }

                }
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Controller.Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnEditRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVCustomers.SelectedRows.Count > 0)
                {
                    var MCTB_Customers = new CTB_Customers("ctr2");
                    Frm_CustomersUpdateInvoices.Invoice_NumberToGetData = DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString();

                    Controller.Chelp chelp = new Chelp();
                    chelp.ShowForm(new Frm_CustomersUpdateInvoices());
                }
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Controller.Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        #region Export Report by using crystal Report Viewer
        private void BtnShowQuotationReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVCustomers.RowCount > 0)
                {
                    MessageBox.Show("optional consumable not supported yet On PDF ");
                    Model.CTB_Customers MCTB_Customers = new Model.CTB_Customers("ctr2");
                    string InvosNO = DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString();
                    ShowReport(InvosNO);

                }
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Controller.Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowReport(string InvoiceNumber)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                LocalDataSet.DataSetForReport dtSet = new LocalDataSet.DataSetForReport();

                #region Customer

                DataTable dtTB_CustomerReport = new DataTable();
                dtTB_CustomerReport = OperationsofCustomers.Get_CustomerDetails_ByCustomer_Invoice_Number(InvoiceNumber);

                string MyCurrency = dtTB_CustomerReport.Rows[0]["Customer_Currency"].ToString();
                string MyAccountBankSelected = dtTB_CustomerReport.Rows[0]["Customer_BankAccount"].ToString();
                string MyDiscount = dtTB_CustomerReport.Rows[0]["Customer_Discount"].ToString();
                string MyInvoice_Number = dtTB_CustomerReport.Rows[0]["Customer_Invoice_Number"].ToString();
                string MyCompanyName = dtTB_CustomerReport.Rows[0]["Customer_Company"].ToString();
                string MyCustomerName = dtTB_CustomerReport.Rows[0]["Customer_Name"].ToString();
                string PaymentASTermsCostem = dtTB_CustomerReport.Rows[0]["Customer_Note"].ToString();
                int Customer_ValOfPaymentInAdv = int.TryParse(PaymentASTermsCostem, out Customer_ValOfPaymentInAdv) ? Customer_ValOfPaymentInAdv : 100;



                string PDFName = $"Quotation #{MyInvoice_Number} For {(MyCompanyName != "" ? MyCompanyName : MyCustomerName)}.pdf";
                //Quotation #  2020331 Primacy Duplex - Melli iran bank
                dtSet.TB_Customers.Rows.Add(
                dtTB_CustomerReport.Rows[0]["Customer_AutoNum"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Invoice_Number"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Company"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Name"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Mob"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Email"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Currency"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_DateTime"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Quote_Valid"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Payment_Terms"].ToString(),
                dtTB_CustomerReport.Rows[0]["Customer_Discount"].ToString()
                );

                #endregion

                #region Invoices
                DataTable dtTB_InvoicesReport = new DataTable();
                dtTB_InvoicesReport = OperationsofInvoices.Get_Invoice_ByInvoice_Number(InvoiceNumber);



                Decimal Total = 0;
                for (int i = 0; i < dtTB_InvoicesReport.Rows.Count; i++)
                {
                    Total += Convert.ToDecimal(dtTB_InvoicesReport.Rows[i]["Invoice_Amount"]);
                }

                Decimal TotalAmount = 0;

                TotalAmount = Total - Convert.ToDecimal(MyDiscount);

                List<Controller.CurrencyInfo> currencies = new List<Controller.CurrencyInfo>();
                currencies.Add(new CurrencyInfo(Controller.CurrencyInfo.Currencies.Dollar));
                currencies.Add(new CurrencyInfo(Controller.CurrencyInfo.Currencies.IRAQ));

                int SelectUSDORIQD = MyCurrency == "USD" ? 0 : 1;

                Controller.ToWord TotalAmountToWord = new ToWord(TotalAmount, currencies[SelectUSDORIQD]);


                decimal PaymentInADVANCE = 0;
                decimal PaymentOnDELIVERY = 0;
                string InADVANCEPercentage = "";
                string OnDELIVERYPercentage = "";

                (PaymentInADVANCE, PaymentOnDELIVERY, InADVANCEPercentage, OnDELIVERYPercentage) = Chelp.PaymentTermsSettings(TotalAmount, Customer_ValOfPaymentInAdv);



                for (int i = 0; i < dtTB_InvoicesReport.Rows.Count; i++)
                {
                    dtSet.TB_Invoices.Rows.Add(dtTB_InvoicesReport.Rows[i]["product_Id"].ToString(),
                                               dtTB_InvoicesReport.Rows[i]["product_NameEn"].ToString(),
                                               dtTB_InvoicesReport.Rows[i]["product_NameAr"].ToString(),
                                               dtTB_InvoicesReport.Rows[i]["Invoice_QTY"].ToString(),
                                               dtTB_InvoicesReport.Rows[i]["Invoice_Unit"].ToString(),
                                               dtTB_InvoicesReport.Rows[i]["Invoice_Price"].ToString(),
                                               dtTB_InvoicesReport.Rows[i]["Invoice_Amount"].ToString(),
                                               Chelp.Format_PriceAndAmount(Total.ToString(), MyCurrency),
                                               Chelp.Format_PriceAndAmount(TotalAmount.ToString(), MyCurrency),
                                               TotalAmountToWord.ConvertToEnglish(),
                                               PaymentInADVANCE,
                                               PaymentOnDELIVERY,
                                               InADVANCEPercentage,
                                               OnDELIVERYPercentage
                                               );
                }

                #endregion

                #region Terms

                DataTable dtTB_Terms_InvoicesReport = new DataTable();
                dtTB_Terms_InvoicesReport = OperationsofTermsInvoices.Get_AllTerms_Invoice_ByTerm_Invoice_Number(InvoiceNumber);

                StringBuilder builderEN = new StringBuilder();
                StringBuilder builderAR = new StringBuilder();
                if (dtTB_Terms_InvoicesReport.Rows.Count > 0)
                {
                    builderEN.Append("Terms").Append(Environment.NewLine);
                    builderAR.Append("الشروط").Append(Environment.NewLine);

                    for (int i = 0; i < dtTB_Terms_InvoicesReport.Rows.Count; i++)
                    {
                        builderEN.Append("  -  ").
                                Append(dtTB_Terms_InvoicesReport.Rows[i]["Term_En"].ToString()).
                                Append(Environment.NewLine);

                        builderAR.Append("  -  ").
                                  Append(dtTB_Terms_InvoicesReport.Rows[i]["Term_Ar"].ToString()).
                                  Append(Environment.NewLine);
                    }
                }
                dtSet.TB_Terms_Invoices.Rows.Add(InvoiceNumber, builderEN, builderAR);

                #endregion

                #region Bank

                DataTable dtTB_BankAccountReport = new DataTable();

                var SelectResult = MyAccountBankSelected;
                if (SelectResult != "Select No Account Bank")
                {
                    dtTB_BankAccountReport = OperationsofBanks.Get_ByBank_Definition(SelectResult);

                    dtSet.TB_BANKAccount.Rows.Add(
                    dtTB_BankAccountReport.Rows[0]["Bank_Definition"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Beneficiary_Name"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Bank_Name"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Branch"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Branch_Code"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Bank_Address"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Swift_Code"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Account_Number"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_IBAN_Number"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_COUNTRY"].ToString(),
                    dtTB_BankAccountReport.Rows[0]["Bank_Account_currency"].ToString()
                    );
                }

                #endregion

                #region Frm_ReportViewerHoder and CustomersInvoiceReport

                Reports.Frm_ReportViewerHoder frm = new Reports.Frm_ReportViewerHoder();
                Reports.CustomersInvoiceReport repot = new Reports.CustomersInvoiceReport();

                repot.SetDataSource(dtSet);

                //  مصدر التقرير من الداتاسيت التي تم تعبة بياناتها من الداتا تيبل
                frm.crystalReportViewer1.ReportSource = repot;  // repot هو التقرير نخسة منه
                frm.crystalReportViewer1.Refresh(); // تحديث التقرير

                // this to send repot and file name to Frm_ReportViewerHoder
                Reports.Frm_ReportViewerHoder.reportDocument = repot;
                Reports.Frm_ReportViewerHoder.FileNameOfPDF = PDFName;

                frm.ShowDialog();

                #endregion

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        #endregion

        private void DGVCustomers_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (DGVCustomers.RowCount > 0)
                {
                    Model.CTB_Customers MCTB_Customers = new Model.CTB_Customers("ctr2");
                    string InvosNO = DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number].Value.ToString();
                    ShowReport(InvosNO);

                }
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Controller.Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        #region Export the quotation in Microsoft Word format.
        private void BtnExportAsWordFile_Click(object sender, EventArgs e)
        {
            if (DGVCustomers.RowCount > 0 && DGVCustomers.CurrentRow != null)
            {
                string showMessage = "Do you want to export with header and footer?";
                CTB_Customers MCTB_Customers = new CTB_Customers("ctr2");

                // Ensure the cell is not null and get the invoice number
                var cell = DGVCustomers.CurrentRow.Cells[MCTB_Customers.Customer_Invoice_Number];
                if (cell != null && cell.Value != null)
                {
                    string invoiceNumber = cell.Value.ToString();

                    var resultOfDialogResult = MessageBox.Show(showMessage, "Data Export As Word File", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    switch (resultOfDialogResult)
                    {
                        case DialogResult.Yes:
                            CLSExportDataToWordFile.ExportDataToWord(invoiceNumber);
                            break;
                        case DialogResult.No:
                            CLSExportDataToWordFile.ExportDataToWord(invoiceNumber, false);
                            break;
                            // You can handle the Cancel case if needed
                    }
                }
                else
                {
                    // Handle the case where the cell or its value is null
                    MessageBox.Show("Invalid Invoice Number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


        }
        #endregion

        private void BtnSearchTxtDistinctProduct_Click(object sender, EventArgs e)
        {
            if (TxtDistinctProduct.SelectedValue != null)
            {
                // to get all invoices search by product id 
                DataTable InvoiceNumber = OperationsofInvoices.Get_Invoice_Byproduct_Id(TxtDistinctProduct.SelectedValue.ToString());

                List<DataRow> resultRows = new List<DataRow>();

                foreach (DataRow invoiceRow in InvoiceNumber.Rows)
                {
                    DataTable customerData = OperationsofCustomers.Get_AllCustomer_ByCustomer_Invoice_Number(invoiceRow[0].ToString());
                    // Assuming that the structure of customerData is the same for all iterations

                    // Add all rows from customerData to the resultRows list
                    resultRows.AddRange(customerData.AsEnumerable());
                }

                // Create a new DataTable with the same structure as the first customerData DataTable
                DataTable mergedResultData = resultRows.Any() ? resultRows.CopyToDataTable() : new DataTable();

                DGVCustomers.DataSource = mergedResultData;
                DGVCustomers.AutoGenerateColumns = true;
                LbCount.Text = DGVCustomers.RowCount.ToString();
            }
            else
            {
                DGVCustomers.DataSource = null;
            }
        }


    }
}
