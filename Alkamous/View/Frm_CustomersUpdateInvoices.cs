﻿using Alkamous.Controller;
using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_CustomersUpdateInvoices : Form
    {
        #region Declare variables

        ClsOperationsofCustomers OperationsofCustomers = new ClsOperationsofCustomers();
        readonly ClsOperationsofInvoices OperationsofInvoices = new ClsOperationsofInvoices();
        readonly ClsOperationsofConsumable OperationsofConsumable = new ClsOperationsofConsumable();
        readonly ClsOperationsofTermsInvoices OperationsofTermsInvoices = new ClsOperationsofTermsInvoices();
        readonly ClsOperationsofBanks OperationsofBanks = new ClsOperationsofBanks();

        public static DataTable dtCustomer = new DataTable();
        public static DataTable dtProducts = new DataTable();
        public static DataTable dtProductsConsumable = new DataTable();
        public static DataTable dtTermsInvoices = new DataTable();

        private static Frm_CustomersUpdateInvoices frmCustomersUpdateInvoices;
        public static string Invoice_NumberToGetData;
        private bool Is_Edit_Invoices_FormOpen = true;

        #endregion

        // this methode to make a new form same as orgnal from
        public static Frm_CustomersUpdateInvoices FrmCustomersUpdateInvoices  // this methode to make a new form same as orgnal from
        {
            get { return frmCustomersUpdateInvoices; }
        }


        public Frm_CustomersUpdateInvoices()
        {
            InitializeComponent();
            frmCustomersUpdateInvoices = this;
        }

        #region DGVColumnHeaderTextAndWidth

        private void DGVColumnHeaderTextAndWidthProductes()
        {
            try
            {
                Model.CTB_Products MCTB_Products = new Model.CTB_Products("ctr2");

                dtProducts.Clear();
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

                //DGVProducts.Columns[0].Width = 50;
                //DGVProducts.Columns[1].Width = 100;
                //DGVProducts.Columns[2].Width = 100;
                //DGVProducts.Columns[3].Width = 30;
                //DGVProducts.Columns[4].Width = 20;
                //DGVProducts.Columns[5].Width = 30;
                //DGVProducts.Columns[6].Width = 100;


                // Set the font and alignment for the first column                
                DGVProducts.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // Set the font and alignment for the second column               
                DGVProducts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;



                DGVProducts.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DGVProducts.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;





                DGVProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                // Manually set the height of any rows that exceed the default maximum height
                DGVProducts.RowTemplate.Height = 50; // set a default height for rows

                //
                DGVProducts.Columns[0].ReadOnly = true;
                DGVProducts.Columns[1].ReadOnly = true;
                DGVProducts.Columns[2].ReadOnly = true;
                //DGVProducts.Columns[3].ReadOnly = true;
                //DGVProducts.Columns[4].ReadOnly = true;
                //DGVProducts.Columns[5].ReadOnly = true;
                DGVProducts.Columns[6].ReadOnly = true;


            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void DGVColumnHeaderTextAndWidthProductesConsumable()
        {
            try
            {
                Model.CTB_Products MCTB_Products = new Model.CTB_Products("ctr2");


                DGVProductsConsumable.RowHeadersVisible = false;

                using (dtProductsConsumable = new DataTable())
                {
                    dtProductsConsumable.Columns.Add(MCTB_Products.product_Id);
                    dtProductsConsumable.Columns.Add(MCTB_Products.product_NameEn);
                    dtProductsConsumable.Columns.Add(MCTB_Products.product_NameAr);
                    dtProductsConsumable.Columns.Add("Invoice_QTY");
                    dtProductsConsumable.Columns.Add(MCTB_Products.product_Unit);
                    dtProductsConsumable.Columns.Add(MCTB_Products.product_Price);
                    dtProductsConsumable.Columns.Add("Invoice_Amount");

                    DGVProductsConsumable.DataSource = dtProductsConsumable;
                }


                DGVProductsConsumable.Columns[0].HeaderText = "Code";
                DGVProductsConsumable.Columns[1].HeaderText = "product En";
                DGVProductsConsumable.Columns[2].HeaderText = "product Ar";
                DGVProductsConsumable.Columns[3].HeaderText = "QTY";
                DGVProductsConsumable.Columns[4].HeaderText = "Unit";
                DGVProductsConsumable.Columns[5].HeaderText = "Price";
                DGVProductsConsumable.Columns[6].HeaderText = "Amount";


                // Set the font and alignment for the first column                
                DGVProductsConsumable.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // Set the font and alignment for the second column               
                DGVProductsConsumable.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                DGVProductsConsumable.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                DGVProductsConsumable.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


                DGVProductsConsumable.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                // Manually set the height of any rows that exceed the default maximum height
                DGVProductsConsumable.RowTemplate.Height = 50; // set a default height for rows

                DGVProductsConsumable.Columns[0].ReadOnly = true;
                DGVProductsConsumable.Columns[1].ReadOnly = true;
                DGVProductsConsumable.Columns[2].ReadOnly = true;
                //DGVProductsConsumable.Columns[3].ReadOnly = true;
                //DGVProductsConsumable.Columns[4].ReadOnly = true;
                //DGVProductsConsumable.Columns[5].ReadOnly = true;
                DGVProductsConsumable.Columns[6].ReadOnly = true;




            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void DGVColumnHeaderTextAndWidthTermsInvo()
        {
            Model.CTB_Terms_Invoices MCTB_Terms_Invoices = new Model.CTB_Terms_Invoices("ctr2");

            // dtTermsInvoices = new DataTable();

            using (dtTermsInvoices = new DataTable())
            {
                dtTermsInvoices.Columns.Add(MCTB_Terms_Invoices.Term_Invoice_Number);
                dtTermsInvoices.Columns.Add(MCTB_Terms_Invoices.Term_En);
                dtTermsInvoices.Columns.Add(MCTB_Terms_Invoices.Term_Ar);
                DGVTermsInvose.DataSource = dtTermsInvoices;
            }

            DGVTermsInvose.ColumnHeadersDefaultCellStyle.Padding = new Padding(0, 5, 0, 5);
            DGVTermsInvose.RowHeadersVisible = false;
            DGVTermsInvose.Columns[0].Visible = false;


            DGVTermsInvose.Columns[1].HeaderText = "Terms English";
            DGVTermsInvose.Columns[2].HeaderText = "Terms Arabic";


            // Set the font and alignment for the first column
            DGVTermsInvose.Columns[1].DefaultCellStyle.Font = new Font("Arial", 12);
            DGVTermsInvose.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Set the font and alignment for the second column
            DGVTermsInvose.Columns[2].DefaultCellStyle.Font = new Font("Arial", 12);
            DGVTermsInvose.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;



            // Set the wrap mode for the first and second columns
            DGVTermsInvose.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            DGVTermsInvose.Columns[2].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            // Set the auto-size mode for the rows to adjust the height based on the text
            DGVTermsInvose.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Manually set the height of any rows that exceed the default maximum height
            DGVTermsInvose.RowTemplate.Height = 50; // set a default height for rows


            //DGVTermsInvose.Columns[1].Width = (int)(DGVTermsInvose.Width * 0.5);
            //DGVTermsInvose.Columns[2].Width = (int)(DGVTermsInvose.Width * 0.5);



        }

        #endregion

        #region MoveToNextText and AddQtyAndPriceAndAmountToDG by sender

        private void MoveToNextText(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                TextBox textBox = sender as TextBox;
                if (!(string.IsNullOrEmpty(textBox.Text)))
                {
                    SelectNextControl((Control)sender, true, true, true, true);
                    e.Handled = e.SuppressKeyPress = true;
                }

                //prepare to add the Qty & price & amount to DGVProducts 
                AddQtyAndPriceAndAmountToDGV(textBox.Name);
            }
        }

        private void AddQtyAndPriceAndAmountToDGV(string SenderName)
        {
            //check who sender
            if (SenderName == "TxtProduct_Id" || SenderName == "TxtProduct_NameAr" ||
                SenderName == "TxtProduct_NameEn" || SenderName == "TxtQTY" || SenderName == "TxtProduct_Unit" ||
                SenderName == "TxtProduct_Price" || SenderName == "TxtAmount")
            {
                AddDataToDatagradviweAfterCheck();
            }
            else
            {
                AddDataToDatagradviweAfterCheckConsumable();
            }

        }

        private void AddDataToDatagradviweAfterCheck()
        {

            TxtAmount.Text = Chelp.CalculateAmount(TxtQTY.Text, TxtProduct_Price.Text);

            foreach (var textBox in new[] { TxtProduct_Id, TxtProduct_Unit, TxtQTY, TxtProduct_Price, TxtAmount })
            {
                if (string.IsNullOrEmpty(textBox.Text) || (textBox.Text == "0") || (textBox.Text == "0.00"))
                {
                    textBox.Focus();
                    return;
                }
            }

            var (STxtPrice, STxtAmount) = Chelp.ExchangeAndTaxesToForward(TxtExchange.Text, TxtTaxes.Text,
               TxtProduct_Price.Text, TxtQTY.Text, TxtCustomer_Currency.Text);
            TxtProduct_PriceWithEXchange.Text = STxtPrice;
            TxtAmountEX.Text = STxtAmount;

            if (TxtAmount.Focused)
            {
                ExchangeAndTaxesToForward();

                AddNewRowsToDataTable();

                DGVProductsChangededReSumTotalAmount();

            }

        }

        private void AddDataToDatagradviweAfterCheckConsumable()
        {

            TxtAmountConsumable.Text = Chelp.CalculateAmount(TxtQTYConsumable.Text, TxtProduct_PriceConsumable.Text);

            foreach (var textBox in new[] { TxtProduct_IdConsumable, TxtProduct_UnitConsumable, TxtQTYConsumable, TxtProduct_PriceConsumable, TxtAmountConsumable })
            {
                if (string.IsNullOrEmpty(textBox.Text) || (textBox.Text == "0") || (textBox.Text == "0.00"))
                {
                    textBox.Focus();
                    return;
                }
            }

            var (STxtPrice, STxtAmount) = Chelp.ExchangeAndTaxesToForward(TxtExchange.Text, TxtTaxes.Text,
                TxtProduct_PriceConsumable.Text, TxtQTYConsumable.Text, TxtCustomer_Currency.Text);
            TxtConsumable_PriceWithEXchange.Text = STxtPrice;
            TxtAmountConsumableEXchange.Text = STxtAmount;

            if (TxtAmountConsumable.Focused)
            {
                ExchangeAndTaxesToForwardConsumable();

                AddNewRowsToDataTableConsumable();

                ClearTextBoxAfterAddedDataToDGVProducts(groupBoxConsumable);
            }

        }

        #endregion

        #region Exchange if select USD or IQD and updated TxtProduct_Price.Text and TxtAmount.Text

        private void ExchangeAndTaxesToForward()
        {
            var (STxtPrice, STxtAmount) = Chelp.ExchangeAndTaxesToForward(TxtExchange.Text, TxtTaxes.Text,
                  TxtProduct_Price.Text, TxtQTY.Text, TxtCustomer_Currency.Text);

            TxtProduct_Price.Text = STxtPrice;
            TxtAmount.Text = STxtAmount;
        }

        private void ExchangeAndTaxesToForwardConsumable()
        {
            var (STxtPrice, STxtAmount) = Chelp.ExchangeAndTaxesToForward(TxtExchange.Text, TxtTaxes.Text,
                  TxtProduct_PriceConsumable.Text, TxtQTYConsumable.Text, TxtCustomer_Currency.Text);

            TxtProduct_PriceConsumable.Text = STxtPrice;
            TxtAmountConsumable.Text = STxtAmount;
        }

        #endregion

        #region Update TxtTotalAmount

        private void DGVProductsChangededReSumTotalAmount(bool WhoSendOrder = true)
        {
            if (WhoSendOrder)
            {
                ClearTextBoxAfterAddedDataToDGVProducts(groupBoxAddDataToDGV);
            }

            TxtTotalAmount.Text = Controller.Chelp.DGVProductsChangededReSumTotalAmount(TxtCustomer_Currency.Text, DGVProducts);

        }

        #endregion

        #region Add Products TO DataGridView DGVProducts 

        private void AddNewRowsToDataTable()
        {
            try
            {
                for (int i = 0; i < DGVProducts.Rows.Count; i++)
                {
                    if (DGVProducts.Rows[i].Cells[0].Value.ToString().Trim() == TxtProduct_Id.Text.Trim())
                    {
                        MessageBox.Show("The Product already on the list !!");
                        return;
                    }
                }


                dtProducts.Rows.Add(
                            TxtProduct_Id.Text,
                            TxtProduct_NameEn.Text,
                            TxtProduct_NameAr.Text,
                            TxtQTY.Text,
                            TxtProduct_Unit.Text,
                            TxtProduct_Price.Text,
                            TxtAmount.Text);
                DGVProducts.DataSource = dtProducts;
                LbCountProdects.Text = DGVProducts.RowCount.ToString();
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Add Consumable TO DataGridView DGVProductsConsumable

        private void AddNewRowsToDataTableConsumable()
        {
            try
            {
                for (int i = 0; i < DGVProductsConsumable.Rows.Count; i++)
                {
                    if (DGVProductsConsumable.Rows[i].Cells[0].Value.ToString().Trim() == TxtProduct_IdConsumable.Text.Trim())
                    {
                        MessageBox.Show("The Product already on the list !!");
                        return;
                    }
                }


                dtProductsConsumable.Rows.Add(
                            TxtProduct_IdConsumable.Text,
                            TxtProduct_NameEnConsumable.Text,
                            TxtProduct_NameArConsumable.Text,
                            TxtQTYConsumable.Text,
                            TxtProduct_UnitConsumable.Text,
                            TxtProduct_PriceConsumable.Text,
                            TxtAmountConsumable.Text);
                DGVProductsConsumable.DataSource = dtProductsConsumable;
                LbCountProdectsConsumable.Text = DGVProductsConsumable.RowCount.ToString();
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Add Terms TO DataGridView DGVTermsInvose 

        private void BtnShowFrm_Terms_Click(object sender, EventArgs e)
        {
            Frm_Terms frm = new Frm_Terms();
            Frm_Terms.WhoSendOrderTerms = "Frm_CustomersUpdateInvoices";
            frm.ShowDialog();
        }

        private void BtnAddToDVG_Click(object sender, EventArgs e)
        {
            AddTermsInvoicesToDGVTermsinv();
        }

        private void AddTermsInvoicesToDGVTermsinv()
        {
            if (!(string.IsNullOrEmpty(TxtTerm_En.Text) && string.IsNullOrEmpty(TxtTerms_Ar.Text)))
            {
                dtTermsInvoices.Columns[1].MaxLength = 5000;
                dtTermsInvoices.Columns[2].MaxLength = 5000;
                dtTermsInvoices.Rows.Add("", TxtTerm_En.Text, TxtTerms_Ar.Text);
                DGVTermsInvose.DataSource = dtTermsInvoices;
                LbCountTerms.Text = DGVTermsInvose.RowCount.ToString();

                if (BtnAddTermsToServerAsNewTerm.Checked)
                {
                    ClsOperationsofTerms OperationsofTerms = new ClsOperationsofTerms();
                    BtnAddTermsToServerAsNewTerm.Checked = false;
                    BtnAddTermsToServerAsNewTerm.ForeColor = Color.Black;
                    OperationsofTerms.Add_Term(TxtTerm_En.Text.Trim(), TxtTerms_Ar.Text.Trim());
                    MessageBox.Show("Terms Added To server as a new");
                }
                TxtTerm_En.Clear();
                TxtTerms_Ar.Clear();
            }
        }

        #endregion

        #region Add BankAcount

        int DataHaveBeenloaded = 0;
        private void Load_ALLBankAccount()
        {
            if (DataHaveBeenloaded == 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                ClsOperationsofBanks bnk = new ClsOperationsofBanks();
                var result = bnk.Get_All();

                txtSelectAcount.Items.Add("Select No Account Bank");

                for (int i = 0; i < result.Rows.Count; i++)
                {
                    txtSelectAcount.Items.Add(result.Rows[i]["Bank_Definition"]);
                }

                DataHaveBeenloaded++;
                Cursor.Current = Cursors.Default;
            }


        }

        private void LoadAcountInfoByDefinition(string AcountBankSelected)
        {

            ClsOperationsofBanks bnk = new ClsOperationsofBanks();


            if (AcountBankSelected == "Select No Account Bank")
            {
                foreach (Control control in groupBoxAcount.Controls)
                {
                    if (control is TextBox txtBox)
                    {
                        txtBox.Clear();
                    }
                }
            }
            else
            {
                var result = bnk.Get_ByBank_Definition(AcountBankSelected);
                TxtBeneficiary_Name.Text = result.Rows[0]["Bank_Beneficiary_Name"].ToString();
                TxtBank_Name.Text = result.Rows[0]["Bank_Bank_Name"].ToString();
                TxtBranch.Text = result.Rows[0]["Bank_Branch"].ToString();
                TxtBranch_Code.Text = result.Rows[0]["Bank_Branch_Code"].ToString();
                TxtBank_Address.Text = result.Rows[0]["Bank_Bank_Address"].ToString();
                TxtSwift_Code.Text = result.Rows[0]["Bank_Swift_Code"].ToString();
                TxtAccount_Number.Text = result.Rows[0]["Bank_Account_Number"].ToString();
                TxtIBAN_Number.Text = result.Rows[0]["Bank_IBAN_Number"].ToString();
                TxtCOUNTRY.Text = result.Rows[0]["Bank_COUNTRY"].ToString();
            }

        }

        private void txtSelectAcount_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataHaveBeenloaded > 0)
            {
                if (txtSelectAcount.SelectedIndex == -1)
                {
                    return;
                }
                LoadAcountInfoByDefinition(txtSelectAcount.SelectedItem.ToString());
            }
        }

        #endregion

        #region Add Customer & Invoices  & Terms  TO SQL

        private async Task<bool> UpdateCustomerToDataBaseSQL()
        {

            try
            {
                Model.CTB_Customers MTB_Customers = new Model.CTB_Customers
                {
                    Customer_Invoice_Number = TxtCustomer_Invoice.Text,
                    Customer_Company = TxtCustomer_Company.Text,
                    Customer_Name = TxtCustomer_Name.Text,
                    Customer_Mob = TxtCustomer_Mob.Text,
                    Customer_Email = TxtCustomer_Email.Text,
                    Customer_Currency = TxtCustomer_Currency.Text,
                    Customer_ExchangeRate = TxtExchange.Text != "" ? TxtExchange.Text : "0",
                    Customer_Taxes = TxtTaxes.Text != "" ? TxtTaxes.Text : "0",
                    Customer_DateTime = TxtCustomer_DateTime.Text,
                    Customer_Quote_Valid = TxtCustomer_Quote_Valid.Text,
                    Customer_Payment_Terms = TxtCustomer_Payment_Terms.Text,
                    Customer_Discount = TxtDiscount.Text,
                    Customer_BankAccount = txtSelectAcount.SelectedItem.ToString(),
                    Customer_Language = TxtCustomer_Language.Text,
                    Customer_Note = TXTValOfPaymentInAdv.Text
                };
                return await OperationsofCustomers.UpdateAsync(MTB_Customers);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
                return false;
            }

        }

        private async void UpdateInvoicesDetilsToDataBaseSQL()
        {
            try
            {
                string InvoiceNO = TxtCustomer_Invoice.Text.Trim();
                await OperationsofInvoices.DeleteAsync(InvoiceNO);

                if (DGVProducts.RowCount > 0)
                {
                    for (int i = 0; i < DGVProducts.RowCount; i++)
                    {
                        Model.CTB_Invoices cTB_Invoices = new Model.CTB_Invoices();

                        cTB_Invoices.Invoice_Number = TxtCustomer_Invoice.Text;
                        cTB_Invoices.Invoice_product_Id = DGVProducts.Rows[i].Cells[0].Value.ToString();
                        cTB_Invoices.Invoice_Unit = DGVProducts.Rows[i].Cells[4].Value.ToString();
                        cTB_Invoices.Invoice_QTY = DGVProducts.Rows[i].Cells[3].Value.ToString();
                        cTB_Invoices.Invoice_Price = DGVProducts.Rows[i].Cells[5].Value.ToString();
                        cTB_Invoices.Invoice_Amount = DGVProducts.Rows[i].Cells[6].Value.ToString();

                        OperationsofInvoices.Add_NewInvoiceLIST(cTB_Invoices);
                    }

                    OperationsofInvoices.InsertBulk();
                }
            }
            catch (Exception ex)
            {

                Controller.Chelp.WriteErrorLog(Name + " => Updata Invose detils to Databse SQL=> " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void UpdateConsumableDetilsToDataBaseSQL()
        {
            try
            {
                string InvoiceNO = TxtCustomer_Invoice.Text.Trim();
                OperationsofConsumable.Delete_ConsumableByConsumable_Number(InvoiceNO);

                if (DGVProductsConsumable.RowCount > 0)
                {

                    for (int i = 0; i < DGVProductsConsumable.RowCount; i++)
                    {

                        Model.CTB_Consumable cTB_Consumable = new Model.CTB_Consumable();

                        cTB_Consumable.Consumable_Number = TxtCustomer_Invoice.Text;
                        cTB_Consumable.Consumable_product_Id = DGVProductsConsumable.Rows[i].Cells[0].Value.ToString();
                        cTB_Consumable.Consumable_Unit = DGVProductsConsumable.Rows[i].Cells[4].Value.ToString();
                        cTB_Consumable.Consumable_QTY = DGVProductsConsumable.Rows[i].Cells[3].Value.ToString();
                        cTB_Consumable.Consumable_Price = DGVProductsConsumable.Rows[i].Cells[5].Value.ToString();
                        cTB_Consumable.Consumable_Amount = DGVProductsConsumable.Rows[i].Cells[6].Value.ToString();

                        OperationsofConsumable.Add_NewConsumableLIST(cTB_Consumable);
                    }

                    OperationsofConsumable.InsertBulk();
                }
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }


        }

        private void UpdateTermsDetilsToDataBaseSQL()
        {
            try
            {
                string InvoiceNO = TxtCustomer_Invoice.Text.Trim();
                OperationsofTermsInvoices.Delete_Terms_Invoice(InvoiceNO);
                for (int i = 0; i < DGVTermsInvose.Rows.Count; i++)
                {
                    Model.CTB_Terms_Invoices cTB_Terms_Invoices = new Model.CTB_Terms_Invoices();
                    cTB_Terms_Invoices.Term_Invoice_Number = TxtCustomer_Invoice.Text.Trim();
                    cTB_Terms_Invoices.Term_En = DGVTermsInvose.Rows[i].Cells[1].Value.ToString();
                    cTB_Terms_Invoices.Term_Ar = DGVTermsInvose.Rows[i].Cells[2].Value.ToString();
                    OperationsofTermsInvoices.Add_NewTerms_InvoiceLIST(cTB_Terms_Invoices);
                }
                OperationsofTermsInvoices.InsertBulk();
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Buttons Click

        private async void BtnSaveData_Click(object sender, EventArgs e)
        {

            try
            {
                if (CheckDataEntryFileds())
                {

                    var TotalAmount = Convert.ToDecimal(TxtTotalAmount.Text);
                    var Discount = Convert.ToDecimal(TxtDiscount.Text);
                    if (TotalAmount < Discount)
                    {
                        MessageBox.Show($"The Discount {Discount} Bigger than TotalAmount {TotalAmount}");
                        return;
                    }

                    var TotalWithDiscount = TotalAmount - Discount;

                    decimal PaymentInADVANCE = 0;
                    decimal PaymentOnDELIVERY = 0;
                    string InADVANCEPercentage = "";
                    string OnDELIVERYPercentage = "";
                    int PaymentASTermsCostem = int.TryParse(TXTValOfPaymentInAdv.Text, out PaymentASTermsCostem) ? PaymentASTermsCostem : 100;

                    (PaymentInADVANCE, PaymentOnDELIVERY, InADVANCEPercentage, OnDELIVERYPercentage) = Chelp.PaymentTermsSettings(TotalWithDiscount, PaymentASTermsCostem);

                    string TotalWithDiscountwithformate = Chelp.Format_PriceAndAmount(TotalWithDiscount.ToString(), TxtCustomer_Currency.Text);
                    string PaymentInADVANCEwithformate = Chelp.Format_PriceAndAmount(PaymentInADVANCE.ToString(), TxtCustomer_Currency.Text);
                    string PaymentOnDELIVERYwithformate = Chelp.Format_PriceAndAmount(PaymentOnDELIVERY.ToString(), TxtCustomer_Currency.Text);

                    if (TxtCustomer_Payment_Terms.Text == "As per Terms" & PaymentASTermsCostem == 100)
                    {
                        PaymentInADVANCEwithformate = "As per Terms";
                        PaymentOnDELIVERYwithformate = "As per Terms";
                    }

                    var Result = "";
                    Result += $" Update Quotation  {TxtCustomer_Invoice.Text} \n\n";
                    Result += $" Total                =  {TxtTotalAmount.Text} \n";
                    Result += $" Discount         =  {TxtDiscount.Text} \n";
                    Result += $"- - - - - - - - - - - - - - - - - - - - - - - - \n";
                    Result += $"Total Amount  =  {TotalWithDiscountwithformate}  {TxtCustomer_Currency.Text} \n";
                    Result += $"IN ADVANCE    =  {PaymentInADVANCEwithformate} \n";
                    Result += $"ON DELIVERY   =  {PaymentOnDELIVERYwithformate} \n";


                    if (MessageBox.Show(Result, "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {

                        Cursor.Current = Cursors.WaitCursor;

                        if (await UpdateCustomerToDataBaseSQL())
                        {

                            UpdateInvoicesDetilsToDataBaseSQL();

                            UpdateConsumableDetilsToDataBaseSQL();

                            UpdateTermsDetilsToDataBaseSQL();

                            Chelp.RegisterUsersActionLogs("Update Quotation", TxtCustomer_Invoice.Text);

                            MessageBox.Show("Data Updated Successfully", "Data updated");


                            Chelp chelp = new Chelp();
                            chelp.ShowForm(new Frm_Customers());


                        }
                        else
                        {
                            MessageBox.Show("Sorry, the data could not be updated successfully.", "Data Not updated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        Cursor.Current = Cursors.Default;

                    }

                }
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnShowAllProdcts_Click(object sender, EventArgs e)
        {

            try
            {
                Frm_Products frm = new Frm_Products();
                Frm_Products.isAddNewInvoices = false;
                Frm_Products.isMainQuotation = true;
                Frm_Products.ExChangeRate = TxtExchange.Text;
                Frm_Products.Taxes = TxtTaxes.Text;
                Frm_Products.Currency = TxtCustomer_Currency.Text;
                frm.ShowDialog();
                TxtQTY.Focus();
                //TxtQTY.Clear();
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnShowAllProdctsForCounsumables_Click(object sender, EventArgs e)
        {
            try
            {
                Frm_Products frm = new Frm_Products();
                Frm_Products.isAddNewInvoices = false;
                Frm_Products.isMainQuotation = false;
                Frm_Products.ExChangeRate = TxtExchange.Text;
                Frm_Products.Taxes = TxtTaxes.Text;
                Frm_Products.Currency = TxtCustomer_Currency.Text;
                frm.ShowDialog();
                TxtQTYConsumable.Focus();

            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region KeyPress OnlyNumber and OnlyNumberand with Dot and CountDots
        public static int CountDots(string text)
        {
            Regex dotRegex = new Regex("\\.");
            MatchCollection matches = dotRegex.Matches(text);
            return matches.Count;
        }

        private void OnlyNumberByKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }

        }

        private void OnlyNumberandDotByKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 46)
            {
                e.Handled = true;
            }

            if (e.KeyChar == 46)
            {
                TextBox Textsender = (TextBox)sender;
                if (CountDots(Textsender.Text) == 1)
                    e.Handled = true;
            }
        }
        #endregion

        #region DeleteRowFromDGVBySender

        private void BtnDeleteRowTerms_Click(object sender, EventArgs e)
        {
            DeleteRowFromDGVBySender(DGVTermsInvose, dtTermsInvoices);
            LbCountTerms.Text = DGVTermsInvose.RowCount.ToString();
        }

        private void BtnDeleteRowFromDGVProducts_Click(object sender, EventArgs e)
        {
            DeleteRowFromDGVBySender(DGVProducts, dtProducts);
            LbCountProdects.Text = DGVProducts.RowCount.ToString();
            DGVProductsChangededReSumTotalAmount();

        }

        private void BtnDeleteRowFromDGVProductsConsumable_Click(object sender, EventArgs e)
        {
            DeleteRowFromDGVBySender(DGVProductsConsumable, dtProductsConsumable);
            LbCountProdectsConsumable.Text = DGVProductsConsumable.RowCount.ToString();
        }

        private void DeleteRowFromDGVBySender(DataGridView DGVSender, DataTable dt)
        {
            try
            {
                if (DGVSender.RowCount > 0)
                {
                    int rowindex = DGVSender.CurrentRow.Index;
                    dt.Rows.RemoveAt(rowindex);
                }
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Move Rows Up And Down

        private void BtnUpMoveRows_Click(object sender, EventArgs e)
        {

            try
            {
                MoveRowUp(DGVProducts, dtProducts);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnDownMoveRows_Click(object sender, EventArgs e)
        {
            try
            {
                MoveRowDown(DGVProducts, dtProducts);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnMoveTermRowUp_Click(object sender, EventArgs e)
        {
            try
            {
                MoveRowUp(DGVTermsInvose, dtTermsInvoices);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnMoveTermRowDown_Click(object sender, EventArgs e)
        {
            try
            {
                MoveRowDown(DGVTermsInvose, dtTermsInvoices);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnMoveConsumableRowUP_Click(object sender, EventArgs e)
        {
            try
            {
                MoveRowUp(DGVProductsConsumable, dtProductsConsumable);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnMoveConsumableRowDown_Click(object sender, EventArgs e)
        {
            try
            {
                MoveRowDown(DGVProductsConsumable, dtProductsConsumable);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void MoveRowUp(DataGridView dgv, DataTable dt)
        {
            if (dgv.Rows.Count == 0) return;

            int rowIndex = dgv.SelectedCells[0].OwningRow.Index;

            if (rowIndex == 0) return;

            DataRow NewRowHolder = dt.NewRow();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                NewRowHolder[i] = dgv.Rows[rowIndex].Cells[i].Value;
            }

            dt.Rows.RemoveAt(rowIndex);
            dt.Rows.InsertAt(NewRowHolder, rowIndex - 1);

            dgv.ClearSelection();
            dgv.Rows[rowIndex - 1].Selected = true;

        }

        private void MoveRowDown(DataGridView dgv, DataTable dt)
        {
            if (dgv.Rows.Count == 0) return;

            int rowIndex = dgv.SelectedCells[0].OwningRow.Index;


            if (rowIndex < dgv.Rows.Count - 1)
            {

                DataRow NewRowHolder = dt.NewRow();

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    NewRowHolder[i] = dgv.Rows[rowIndex].Cells[i].Value;
                }

                dt.Rows.RemoveAt(rowIndex);
                dt.Rows.InsertAt(NewRowHolder, rowIndex + 1);

                dgv.ClearSelection();
                dgv.Rows[rowIndex + 1].Selected = true;
            }
        }

        #endregion

        private void ClearTextBoxAfterAddedDataToDGVProducts(GroupBox groupBox)
        {
            foreach (Control item in groupBox.Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)(item)).Clear();
                }
            }
        }

        private void Frm_CustomersUpdateInvoices_Load(object sender, EventArgs e)
        {
            DGVColumnHeaderTextAndWidthProductes();
            DGVColumnHeaderTextAndWidthProductesConsumable();
            DGVColumnHeaderTextAndWidthTermsInvo();

            // Set the DataSource property after the loop to include all items
            TxtCustomer_Payment_Terms.DisplayMember = "Text";
            TxtCustomer_Payment_Terms.ValueMember = "Value";
            TxtCustomer_Payment_Terms.DataSource = Chelp.LoadDataToComboBox();


            loadDataToFrmEdit(Invoice_NumberToGetData);
        }

        private void loadDataToFrmEdit(string Invoice_Number)
        {

            try
            {
                // OperationsofCustomers
                Model.CTB_Customers MCTB_Customers = new Model.CTB_Customers("ctr2");
                dtCustomer = OperationsofCustomers.Get_CustomerDetails_ByCustomer_Invoice_Number(Invoice_Number);

                TxtCustomer_Name.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Name].ToString();
                TxtCustomer_Company.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Company].ToString();
                TxtCustomer_Email.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Email].ToString();
                TxtCustomer_Mob.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Mob].ToString();
                TxtCustomer_Invoice.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Invoice_Number].ToString();
                TxtCustomer_Payment_Terms.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Payment_Terms].ToString();
                TXTValOfPaymentInAdv.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Note].ToString();
                TxtExchange.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_ExchangeRate].ToString();
                TxtCustomer_Quote_Valid.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Quote_Valid].ToString();
                TxtTaxes.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Taxes].ToString();


                string Currency = dtCustomer.Rows[0][MCTB_Customers.Customer_Currency].ToString();

                switch (Currency)
                {
                    case "USD":

                        TxtCustomer_Currency.SelectedIndex = 0;
                        break;

                    case "IQD":

                        TxtCustomer_Currency.SelectedIndex = 1;
                        break;

                    case "AED":

                        TxtCustomer_Currency.SelectedIndex = 2;
                        break;

                }


                DateTime dateTime = DateTime.ParseExact(dtCustomer.Rows[0][MCTB_Customers.Customer_DateTime].ToString(), "dd/MM/yyyy", null);
                TxtCustomer_DateTime.Value = dateTime;

                TxtDiscount.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Discount].ToString();

                string selectBankaccount = dtCustomer.Rows[0][MCTB_Customers.Customer_BankAccount].ToString();

                TxtCustomer_Language.Text = dtCustomer.Rows[0][MCTB_Customers.Customer_Language].ToString();


                //OperationsofInvoices
                dtProducts = OperationsofInvoices.Get_Invoice_ByInvoice_Number(Invoice_Number);
                DGVProducts.DataSource = dtProducts;
                TxtCustomer_CurrencyEnabledToChange();
                DGVProductsChangededReSumTotalAmount();

                //OperationsofConsumable
                dtProductsConsumable = OperationsofConsumable.Get_Consumable_ByConsumable_Number(Invoice_Number);
                DGVProductsConsumable.DataSource = dtProductsConsumable;


                //OperationsofTermsInvoices 
                dtTermsInvoices = OperationsofTermsInvoices.Get_AllTerms_Invoice_ByTerm_Invoice_Number(Invoice_Number);
                DGVTermsInvose.DataSource = dtTermsInvoices;

                // show Count number of DataGradview 
                LbCountProdects.Text = DGVProducts.RowCount.ToString();
                LbCountProdectsConsumable.Text = DGVProductsConsumable.RowCount.ToString();
                LbCountTerms.Text = DGVTermsInvose.RowCount.ToString();

                //OperationsofBanks
                Load_ALLBankAccount();
                txtSelectAcount.SelectedItem = txtSelectAcount.Text = selectBankaccount;
                LoadAcountInfoByDefinition(txtSelectAcount.SelectedItem.ToString());

            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void tabControlCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {

            TxtCustomer_CurrencyEnabledToChange();
        }

        private void TxtCustomer_CurrencyEnabledToChange()
        {
            if (DGVProducts.RowCount > 0)
            {
                TxtCustomer_Currency.Enabled = false;
                TxtExchange.Enabled = false;
                TxtTaxes.Enabled = false;
            }
            else
            {
                TxtCustomer_Currency.Enabled = true;
                TxtExchange.Enabled = true;
                TxtTaxes.Enabled = true;
            }

        }

        private void TxtCustomer_Currency_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrencySettings(TxtCustomer_Currency.Text);
        }

        private void CurrencySettings(string Currency)
        {
            bool boolResult = Currency == "USD" ? false : true;

            var boolCurrencyResult = Currency == "USD" ? false : true;

            switch (Currency)
            {
                case "USD":

                    LbCustomer_Currency.Text = "USD";
                    LbcurrencywithexchangeInfo.Text = $"The currency with exchange USD";
                    LbcurrencywithexchangeInfoConsumable.Text = $"The currency with exchange USD";
                    break;

                case "IQD":

                    LbCustomer_Currency.Text = "IQD";
                    LbcurrencywithexchangeInfo.Text = $"The currency with exchange IQD";
                    LbcurrencywithexchangeInfoConsumable.Text = $"The currency with exchange IQD";
                    break;

                case "AED":

                    LbCustomer_Currency.Text = "AED";
                    LbcurrencywithexchangeInfo.Text = $"The currency with exchange AED";
                    LbcurrencywithexchangeInfoConsumable.Text = $"The currency with exchange AED";

                    break;

            }

            if (boolCurrencyResult)
            {
                DGVProducts.Size = new Size(DGVProducts.Size.Width, 265);
                DGVProducts.Location = new Point(6, 214);

                DGVProductsConsumable.Size = new Size(DGVProducts.Size.Width, 265);
                DGVProductsConsumable.Location = new Point(6, 214);


            }
            else
            {
                DGVProducts.Size = new Size(DGVProducts.Size.Width, 300);
                DGVProducts.Location = new Point(7, 179);

                DGVProductsConsumable.Size = new Size(DGVProducts.Size.Width, 300);
                DGVProductsConsumable.Location = new Point(7, 179);

            }

            // only show if Currency not "USD"
            LbcurrencywithexchangeInfo.Visible = boolCurrencyResult;
            TxtAmountEX.Visible = boolCurrencyResult;
            TxtProduct_PriceWithEXchange.Visible = boolCurrencyResult;

            LbcurrencywithexchangeInfoConsumable.Visible = boolCurrencyResult;
            TxtAmountConsumableEXchange.Visible = boolCurrencyResult;
            TxtConsumable_PriceWithEXchange.Visible = boolCurrencyResult;

            BtnUpdateProductPrice.Visible = boolCurrencyResult;
            BtnUpdateConsumablePrice.Visible = boolCurrencyResult;


            LbExchange.Visible = boolResult;
            TxtExchange.Visible = boolResult;


            LBTaxes.Visible = boolResult;
            TxtTaxes.Visible = boolResult;

            if (Is_Edit_Invoices_FormOpen)
            {
                return;
            }
            TxtExchange.Clear();
            TxtTaxes.Clear();
        }

        #region Checking all the data entry fields is required before processing

        private bool CheckDataEntryFileds()
        {
            string totalAmount;
            bool resultDGVProducts, resultDGVProductsConsumable;

            (totalAmount, resultDGVProducts) = Chelp.UpdatePricesAndRecalculate(DGVProducts, TxtCustomer_Currency.Text);
            (_, resultDGVProductsConsumable) = Chelp.UpdatePricesAndRecalculate(DGVProductsConsumable, TxtCustomer_Currency.Text);

            if (IsFieldEmpty(TxtCustomer_Name, "Customer Name required", 0)) return false;
            if (IsFieldEmpty(TxtCustomer_Invoice, "Customer Invoice No required", 0)) return false;
            if (IsFieldEmpty(TxtCustomer_Quote_Valid, "Customer Quote Valid required", 0)) return false;
            if (TxtCustomer_Currency.Text == "IQD" && IsFieldEmpty(TxtExchange, "EX change required", 0)) return false;

            if (string.IsNullOrEmpty(TxtCustomer_Payment_Terms.Text)) return ShowError("Payment Terms required", 0);

            if (DGVProducts.RowCount == 0) return ShowError("Insert One Products to Invoice it is required", 1);

            if (!resultDGVProducts) return ShowError("All Data is Required product items.", 1);

            if (!resultDGVProductsConsumable) return ShowError("All Data is Required Consumable.", 2);

            TxtTotalAmount.Text = totalAmount;
            return true;
        }

        private bool IsFieldEmpty(TextBox field, string message, int tabIndex)
        {
            if (string.IsNullOrEmpty(field.Text))
            {
                tabControlCustomers.SelectedIndex = tabIndex;
                field.Focus();
                MessageBox.Show(message, "Message");
                return true;
            }
            return false;
        }

        private bool ShowError(string message, int tabIndex)
        {
            tabControlCustomers.SelectedIndex = tabIndex;
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        #endregion

        private void TxtExchange_Validated(object sender, EventArgs e)
        {
            TxtExchange.Text = Chelp.Format_PriceAndAmount(TxtExchange.Text, TxtCustomer_Currency.Text);
        }

        private void DGVTermsInvose_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (DGVTermsInvose.RowCount > 0)
                {

                    TxtTerm_En.Text = DGVTermsInvose.CurrentRow.Cells[1].Value.ToString();
                    TxtTerms_Ar.Text = DGVTermsInvose.CurrentRow.Cells[2].Value.ToString();

                    DeleteRowFromDGVBySender(DGVTermsInvose, dtTermsInvoices);
                    LbCountTerms.Text = DGVTermsInvose.RowCount.ToString();

                }
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Controller.Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void TxtDiscount_Validated(object sender, EventArgs e)
        {
            TxtDiscount.Text = Controller.Chelp.Format_PriceAndAmount(TxtDiscount.Text, TxtCustomer_Currency.Text);
        }

        private void BtnLoadCustomerInfo_Click(object sender, EventArgs e)
        {
            Frm_CustomerInfoLoad frm = new Frm_CustomerInfoLoad();
            Frm_CustomerInfoLoad.WhoSendOrderProducts = "Frm_CustomersUpdateInvoices";
            frm.ShowDialog();
        }

        private void TxtCustomer_Payment_Terms_SelectedIndexChanged(object sender, EventArgs e)
        {
            TXTValOfPaymentInAdv.Text = TxtCustomer_Payment_Terms.SelectedValue.ToString();
            Payment_TermsSettings(TxtCustomer_Payment_Terms.Text);
        }

        private void Payment_TermsSettings(string Payment_Terms)
        {
            bool boolResult = Payment_Terms == "As per Terms" ? true : false;
            LBAsPerTerms.Visible = boolResult;
            TXTValOfPaymentInAdv.Visible = boolResult;
            if (boolResult)
            {
                TXTValOfPaymentInAdv.Clear();
                TXTValOfPaymentInAdv.Focus();
            }
        }

        private void DatagridviewAcceptNumbersOnly(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox textBox)
            {
                // Detach the event handler if the column index is not 3 or 5
                textBox.KeyPress -= TextBox_KeyPress;

                if (sender is DataGridView dataGridView &&
                    (dataGridView.CurrentCell.ColumnIndex == 3 ||
                     dataGridView.CurrentCell.ColumnIndex == 5))
                {
                    // Attach the event handler
                    textBox.KeyPress += TextBox_KeyPress;
                }

            }
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) /*&& e.KeyChar !='.'*/)
            {
                e.Handled = true;
            }
        }

        private void BtnUpdateProductPrice_Click(object sender, EventArgs e)
        {
            if (DGVProducts.Rows.Count > 0)
            {
                string totalAmount;
                bool Result;
                (totalAmount, Result) = Chelp.UpdatePricesAndRecalculate(DGVProducts, TxtCustomer_Currency.Text);
                if (Result)
                {
                    TxtTotalAmount.Text = totalAmount;

                }
            }
        }

        private void BtnUpdateConsumablePrice_Click(object sender, EventArgs e)
        {
            if (DGVProductsConsumable.Rows.Count > 0)
            {
                Chelp.UpdatePricesAndRecalculate(DGVProductsConsumable, TxtCustomer_Currency.Text);
            }
        }

        private void BtnAddTermsToServerAsNewTerm_CheckedChanged(object sender, EventArgs e)
        {
            BtnAddTermsToServerAsNewTerm.ForeColor = BtnAddTermsToServerAsNewTerm.Checked ? Color.Red : Color.Black;
        }
    }

}
