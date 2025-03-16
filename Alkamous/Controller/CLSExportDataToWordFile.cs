using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace Alkamous.Controller
{
    public class CLSExportDataToWordFile
    {
        #region Declare Variables For CLS Of Operations

        private readonly ClsOperationsofCustomers OperationsofCustomers = new ClsOperationsofCustomers();
        private readonly ClsOperationsofInvoices OperationsofInvoices = new ClsOperationsofInvoices();
        private readonly ClsOperationsofConsumable OperationsofConsumable = new ClsOperationsofConsumable();
        private readonly ClsOperationsofTermsInvoices OperationsofTermsInvoices = new ClsOperationsofTermsInvoices();
        private readonly ClsOperationsofBanks OperationsofBanks = new ClsOperationsofBanks();

        #endregion

        #region Declare Variables For Microsoft.Office.Interop.Word 
        
        Microsoft.Office.Interop.Word.Application _wordApp = null;
        Microsoft.Office.Interop.Word.Document _aDoc = null;
        Microsoft.Office.Interop.Word.Table _table = null;
        Microsoft.Office.Interop.Word.Bookmarks _replaceBookmarks = null;

        #endregion

        /// <summary>
        /// Exports invoice data to a Word document
        /// </summary>
        /// <param name="InvoiceNumber">The invoice number to export</param>
        /// <param name="isHeaderAndFooter">Whether to include header and footer</param>
        public void ExportDataToWord(string InvoiceNumber,bool isHeaderAndFooter = true)
        {

            // Retrieve the full path of the document template (Microsoft Word file)
            string folderName = "Temp";
            string fileNameSelected = isHeaderAndFooter ? "TempFileWithHeader.docx" : "TempFileWithoutHeader.docx";
            string fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folderName, fileNameSelected);

            try
            {
                _wordApp = new Microsoft.Office.Interop.Word.Application { Visible = false };
                _aDoc = _wordApp.Documents.Open(fileName, ReadOnly: false, Visible: false);

                _aDoc.Activate();
                _replaceBookmarks = _aDoc.Bookmarks;

               // MessageBox.Show(_aDoc.Tables.Count.ToString()); 
                Cursor.Current = Cursors.WaitCursor;
                View.Frm_Customers.FrmCustomer.LbWaitSaveFile.Visible = true;

                #region Customer info and Invoice Number with rest info Table index [1] as Bookmarks

                DataTable dtTB_CustomerReport = new DataTable();
                dtTB_CustomerReport = OperationsofCustomers.Get_CustomerDetails_ByCustomer_Invoice_Number(InvoiceNumber);

                string Customer_AutoNum = dtTB_CustomerReport.Rows[0]["Customer_AutoNum"].ToString();
                string Customer_Invoice_Number = dtTB_CustomerReport.Rows[0]["Customer_Invoice_Number"].ToString();
                string Customer_Company = dtTB_CustomerReport.Rows[0]["Customer_Company"].ToString();
                string Customer_Name = dtTB_CustomerReport.Rows[0]["Customer_Name"].ToString();
                string Customer_Mob = dtTB_CustomerReport.Rows[0]["Customer_Mob"].ToString();
                string Customer_Email = dtTB_CustomerReport.Rows[0]["Customer_Email"].ToString();
                string Customer_Currency = dtTB_CustomerReport.Rows[0]["Customer_Currency"].ToString();
                string Customer_DateTime = dtTB_CustomerReport.Rows[0]["Customer_DateTime"].ToString();
                string Customer_Quote_Valid = dtTB_CustomerReport.Rows[0]["Customer_Quote_Valid"].ToString();
                string Customer_Payment_Terms = dtTB_CustomerReport.Rows[0]["Customer_Payment_Terms"].ToString();
                string Customer_Discount = dtTB_CustomerReport.Rows[0]["Customer_Discount"].ToString();
                string Customer_BankAccount = dtTB_CustomerReport.Rows[0]["Customer_BankAccount"].ToString();
                string Customer_Language = dtTB_CustomerReport.Rows[0]["Customer_Language"].ToString();
                string PaymentASTermsCostem = dtTB_CustomerReport.Rows[0]["Customer_Note"].ToString();
                // Parse payment percentage with fallback to 100
                int Customer_ValOfPaymentInAdv = int.TryParse(PaymentASTermsCostem, out Customer_ValOfPaymentInAdv) ? Customer_ValOfPaymentInAdv : 100;

                // Generate Export File Name -> Quotation #123 For ALKAMOUS
                string ExportFileName = $"Quotation #{Customer_Invoice_Number} For {(Customer_Company != "" ? Customer_Company : Customer_Name)}";


                _replaceBookmarks["Customer_Invoice_Number"].Range.Text = Customer_Invoice_Number;
                _replaceBookmarks["Customer_Company"].Range.Text = Customer_Company;
                _replaceBookmarks["Customer_Name"].Range.Text = Customer_Name;
                _replaceBookmarks["Customer_Mob"].Range.Text = Customer_Mob;
                _replaceBookmarks["Customer_Email"].Range.Text = Customer_Email;
                _replaceBookmarks["Customer_Currency"].Range.Text = Customer_Currency;
                _replaceBookmarks["Customer_DateTime"].Range.Text = Customer_DateTime;
                _replaceBookmarks["Customer_Quote_Valid"].Range.Text = $"{Customer_Quote_Valid} Days";
                _replaceBookmarks["Customer_Payment_Terms"].Range.Text = Customer_Payment_Terms;

                #endregion

                #region Main Invoices section Table index [2]

                DataTable dtTB_InvoicesReport = new DataTable();
                dtTB_InvoicesReport = OperationsofInvoices.Get_Invoice_ByInvoice_Number(InvoiceNumber);

                // Tables Main Invoice
                _table = _aDoc.Tables[2];
                object objmissval = System.Reflection.Missing.Value;

                for (int i = 0; i < dtTB_InvoicesReport.Rows.Count; i++)
                {
                    // to add new rows to tables
                    _table.Rows.Add(ref objmissval);

                    _table.Cell(2 + i, 1).Range.Text = (1 + i).ToString();
                    _table.Cell(2 + i, 2).Range.Text = dtTB_InvoicesReport.Rows[i]["product_Id"].ToString();
                    _table.Cell(2 + i, 4).Range.Text = dtTB_InvoicesReport.Rows[i]["Invoice_QTY"].ToString();
                    _table.Cell(2 + i, 5).Range.Text = dtTB_InvoicesReport.Rows[i]["Invoice_Unit"].ToString();
                    _table.Cell(2 + i, 6).Range.Text = dtTB_InvoicesReport.Rows[i]["Invoice_Price"].ToString();
                    _table.Cell(2 + i, 7).Range.Text = dtTB_InvoicesReport.Rows[i]["Invoice_Amount"].ToString();

                }

                // set color to header of table
                _table.Rows[1].Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray05;


                #region To get the total and total amount + checking payment method
                // to get the total and total amount
                Decimal Total = 0;
                for (int i = 0; i < dtTB_InvoicesReport.Rows.Count; i++)
                {
                    Total += Convert.ToDecimal(dtTB_InvoicesReport.Rows[i]["Invoice_Amount"]);
                }

                Decimal TotalAmount = 0;

                TotalAmount = Total - Convert.ToDecimal(Customer_Discount);

                List<CurrencyInfo> currencies = new List<CurrencyInfo>();
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Dollar));
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.IRAQ));
                currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.AED));


                int SelectCurrency = 0;

                switch (Customer_Currency)
                {
                    case "USD":
                        SelectCurrency = 0;
                        break;

                    case "IQD":
                        SelectCurrency = 1;
                        break;

                    case "AED":
                        SelectCurrency = 2;
                        break;
                }


                ToWord TotalAmountToWord = new ToWord(TotalAmount, currencies[SelectCurrency]);


                #region check Discount for Client - check if there is No Discount for Client 

                if ((Customer_Discount == "0") || (Customer_Discount == "0.00"))
                {

                    #region Check Payment_Terms in ADVANCE 100% without Discount

                    if (Customer_ValOfPaymentInAdv == 100)
                    {
                        _table.Rows.Add(ref objmissval);

                        _table.Cell(_table.Rows.Count, 1).Merge(_table.Cell(_table.Rows.Count, 2));
                        _table.Cell(_table.Rows.Count, 2).Merge(_table.Cell(_table.Rows.Count, 4));
                        _table.Cell(_table.Rows.Count, 3).Merge(_table.Cell(_table.Rows.Count, 4));

                        // set color to table
                        _table.Rows[_table.Rows.Count].Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray05;

                        // Set the text for the merged cell
                        _table.Cell(_table.Rows.Count, 1).Range.Text = $"Total Amount {Customer_Currency} :";
                        _table.Cell(_table.Rows.Count, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                        if (Customer_Language == "Arabic")
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count, 2).Range.Text = TotalAmountToWord.ConvertToArabic();
                            _table.Cell(_table.Rows.Count, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                        }
                        else
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count, 2).Range.Text = TotalAmountToWord.ConvertToEnglish();
                            _table.Cell(_table.Rows.Count, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }


                        _table.Cell(_table.Rows.Count, 3).Range.Text = Chelp.Format_PriceAndAmount(TotalAmount.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count, 3).Range.Font.Size = 13;
                        _table.Cell(_table.Rows.Count, 3).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;

                    }
                    else
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            _table.Rows.Add(ref objmissval);
                        }

                        _table.Cell(_table.Rows.Count, 1).Merge(_table.Cell(_table.Rows.Count, 5));
                        _table.Cell(_table.Rows.Count, 2).Merge(_table.Cell(_table.Rows.Count, 3));

                        _table.Cell(_table.Rows.Count - 1, 1).Merge(_table.Cell(_table.Rows.Count - 1, 5));
                        _table.Cell(_table.Rows.Count - 1, 2).Merge(_table.Cell(_table.Rows.Count - 1, 3));

                        _table.Cell(_table.Rows.Count - 2, 1).Merge(_table.Cell(_table.Rows.Count - 2, 2));
                        _table.Cell(_table.Rows.Count - 2, 2).Merge(_table.Cell(_table.Rows.Count - 2, 4));
                        _table.Cell(_table.Rows.Count - 2, 3).Merge(_table.Cell(_table.Rows.Count - 2, 4));

                        // set color of table
                        _table.Rows[_table.Rows.Count - 2].Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray05;
                        ////////////////////////////////////////////////////////////////////////////////////////


                        decimal PaymentInADVANCE = 0;
                        decimal PaymentOnDELIVERY = 0;
                        string InADVANCEPercentage = "";
                        string OnDELIVERYPercentage = "";

                        (PaymentInADVANCE, PaymentOnDELIVERY, InADVANCEPercentage, OnDELIVERYPercentage) = Chelp.PaymentTermsSettings(TotalAmount, Customer_ValOfPaymentInAdv);


                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count, 1).Range.Text = $"{OnDELIVERYPercentage} ON DELIVERY ";
                        _table.Cell(_table.Rows.Count, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                        _table.Cell(_table.Rows.Count, 2).Range.Text = Chelp.Format_PriceAndAmount(PaymentOnDELIVERY.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count, 2).Range.Font.Size = 10;



                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 1, 1).Range.Text = $"{InADVANCEPercentage} IN ADVANCE ";
                        _table.Cell(_table.Rows.Count - 1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                        _table.Cell(_table.Rows.Count - 1, 2).Range.Text = Chelp.Format_PriceAndAmount(PaymentInADVANCE.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count - 1, 2).Range.Font.Size = 10;




                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 2, 1).Range.Text = $"Total Amount {Customer_Currency} :";
                        _table.Cell(_table.Rows.Count - 2, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                        if (Customer_Language == "Arabic")
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count - 2, 2).Range.Text = TotalAmountToWord.ConvertToArabic();
                            _table.Cell(_table.Rows.Count - 2, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                        }
                        else
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count - 2, 2).Range.Text = TotalAmountToWord.ConvertToEnglish();
                            _table.Cell(_table.Rows.Count - 2, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }


                        _table.Cell(_table.Rows.Count - 2, 3).Range.Text = Chelp.Format_PriceAndAmount(TotalAmount.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count - 2, 3).Range.Font.Size = 13;
                        _table.Cell(_table.Rows.Count - 2, 3).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;
                    }

                    #endregion

                }
                else
                {

                    #region Check Payment_Terms in ADVANCE with Discount

                    if (Customer_ValOfPaymentInAdv == 100)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            _table.Rows.Add(ref objmissval);

                        }

                        _table.Cell(_table.Rows.Count, 1).Merge(_table.Cell(_table.Rows.Count, 2));
                        _table.Cell(_table.Rows.Count, 2).Merge(_table.Cell(_table.Rows.Count, 4));
                        _table.Cell(_table.Rows.Count, 3).Merge(_table.Cell(_table.Rows.Count, 4));

                        _table.Cell(_table.Rows.Count - 1, 1).Merge(_table.Cell(_table.Rows.Count - 1, 2));
                        _table.Cell(_table.Rows.Count - 1, 2).Merge(_table.Cell(_table.Rows.Count - 1, 4));
                        _table.Cell(_table.Rows.Count - 1, 3).Merge(_table.Cell(_table.Rows.Count - 1, 4));


                        _table.Cell(_table.Rows.Count - 2, 1).Merge(_table.Cell(_table.Rows.Count - 2, 2));
                        _table.Cell(_table.Rows.Count - 2, 2).Merge(_table.Cell(_table.Rows.Count - 2, 4));
                        _table.Cell(_table.Rows.Count - 2, 3).Merge(_table.Cell(_table.Rows.Count - 2, 4));


                        // set color of table
                        _table.Rows[_table.Rows.Count].Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray05;


                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count, 1).Range.Text = $"Total Amount {Customer_Currency} :";
                        _table.Cell(_table.Rows.Count, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;


                        if (Customer_Language == "Arabic")
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count, 2).Range.Text = TotalAmountToWord.ConvertToArabic();
                            _table.Cell(_table.Rows.Count, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                        }
                        else
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count, 2).Range.Text = TotalAmountToWord.ConvertToEnglish();
                            _table.Cell(_table.Rows.Count, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }

                        _table.Cell(_table.Rows.Count, 3).Range.Text = Chelp.Format_PriceAndAmount(TotalAmount.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count, 3).Range.Font.Size = 13;
                        _table.Cell(_table.Rows.Count, 3).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;



                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 1, 1).Range.Text = $"Discount : ";
                        _table.Cell(_table.Rows.Count - 1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        _table.Cell(_table.Rows.Count - 1, 3).Range.Text = Customer_Discount;


                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 2, 1).Range.Text = $"Total :";
                        _table.Cell(_table.Rows.Count - 2, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        _table.Cell(_table.Rows.Count - 2, 3).Range.Text = Chelp.Format_PriceAndAmount(Total.ToString(), Customer_Currency);

                    }
                    else
                    {

                        for (int i = 0; i < 5; i++)
                        {
                            _table.Rows.Add(ref objmissval);
                        }

                        _table.Cell(_table.Rows.Count, 1).Merge(_table.Cell(_table.Rows.Count, 5));
                        _table.Cell(_table.Rows.Count, 2).Merge(_table.Cell(_table.Rows.Count, 3));

                        _table.Cell(_table.Rows.Count - 1, 1).Merge(_table.Cell(_table.Rows.Count - 1, 5));
                        _table.Cell(_table.Rows.Count - 1, 2).Merge(_table.Cell(_table.Rows.Count - 1, 3));

                        _table.Cell(_table.Rows.Count - 2, 1).Merge(_table.Cell(_table.Rows.Count - 2, 2));
                        _table.Cell(_table.Rows.Count - 2, 2).Merge(_table.Cell(_table.Rows.Count - 2, 4));
                        _table.Cell(_table.Rows.Count - 2, 3).Merge(_table.Cell(_table.Rows.Count - 2, 4));

                        _table.Cell(_table.Rows.Count - 3, 1).Merge(_table.Cell(_table.Rows.Count - 3, 2));
                        _table.Cell(_table.Rows.Count - 3, 2).Merge(_table.Cell(_table.Rows.Count - 3, 4));
                        _table.Cell(_table.Rows.Count - 3, 3).Merge(_table.Cell(_table.Rows.Count - 3, 4));

                        _table.Cell(_table.Rows.Count - 4, 1).Merge(_table.Cell(_table.Rows.Count - 4, 2));
                        _table.Cell(_table.Rows.Count - 4, 2).Merge(_table.Cell(_table.Rows.Count - 4, 4));
                        _table.Cell(_table.Rows.Count - 4, 3).Merge(_table.Cell(_table.Rows.Count - 4, 4));


                        // set color of table
                        _table.Rows[_table.Rows.Count - 2].Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray05;
                        ////////////////////////////////////////////////////////////////////////////////////////


                        decimal PaymentInADVANCE = 0;
                        decimal PaymentOnDELIVERY = 0;
                        string InADVANCEPercentage = "";
                        string OnDELIVERYPercentage = "";

                        (PaymentInADVANCE, PaymentOnDELIVERY, InADVANCEPercentage, OnDELIVERYPercentage) = Chelp.PaymentTermsSettings(TotalAmount, Customer_ValOfPaymentInAdv);


                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count, 1).Range.Text = $"{OnDELIVERYPercentage} ON DELIVERY ";
                        _table.Cell(_table.Rows.Count, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                        _table.Cell(_table.Rows.Count, 2).Range.Text = Chelp.Format_PriceAndAmount(PaymentOnDELIVERY.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count, 2).Range.Font.Size = 10;



                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 1, 1).Range.Text = $"{InADVANCEPercentage} IN ADVANCE ";
                        _table.Cell(_table.Rows.Count - 1, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;

                        _table.Cell(_table.Rows.Count - 1, 2).Range.Text = Chelp.Format_PriceAndAmount(PaymentInADVANCE.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count - 1, 2).Range.Font.Size = 10;



                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 2, 1).Range.Text = $"Total Amount {Customer_Currency} :";
                        _table.Cell(_table.Rows.Count - 2, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;


                        if (Customer_Language == "Arabic")
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count - 2, 2).Range.Text = TotalAmountToWord.ConvertToArabic();
                            _table.Cell(_table.Rows.Count - 2, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphRight;
                        }
                        else
                        {
                            // in case we need to set the TotalAmountToWord in arabioc languge just use . TotalAmountToWord.ConvertToArabic()
                            _table.Cell(_table.Rows.Count - 2, 2).Range.Text = TotalAmountToWord.ConvertToEnglish();
                            _table.Cell(_table.Rows.Count - 2, 2).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }

                        _table.Cell(_table.Rows.Count - 2, 3).Range.Text = Chelp.Format_PriceAndAmount(TotalAmount.ToString(), Customer_Currency);
                        _table.Cell(_table.Rows.Count - 2, 3).Range.Font.Size = 13;
                        _table.Cell(_table.Rows.Count - 2, 3).Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorRed;


                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 3, 1).Range.Text = $"Discount : ";
                        _table.Cell(_table.Rows.Count - 3, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        _table.Cell(_table.Rows.Count - 3, 3).Range.Text = Customer_Discount;


                        //// Set the text for the merged cell
                        _table.Cell(_table.Rows.Count - 4, 1).Range.Text = $"Total :";
                        _table.Cell(_table.Rows.Count - 4, 1).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        _table.Cell(_table.Rows.Count - 4, 3).Range.Text = Chelp.Format_PriceAndAmount(Total.ToString(), Customer_Currency);

                    }

                    #endregion
                }

                #endregion

                #endregion


                int NextArCell = 2;
                for (int i = 0; i < dtTB_InvoicesReport.Rows.Count; i++)
                {
                    if (Customer_Language == "English")
                    {
                        // Set the alignment of the second column cells to left
                        _table.Cell(2 + i, 3).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        _table.Cell(2 + i, 3).Range.Text = dtTB_InvoicesReport.Rows[i]["product_NameEn"].ToString();
                    }
                    else if (Customer_Language == "Arabic")
                    {
                        // Set the text direction to right                              

                        _table.Cell(2 + i, 3).Range.ParagraphFormat.ReadingOrder = Microsoft.Office.Interop.Word.WdReadingOrder.wdReadingOrderRtl;
                        _table.Cell(2 + i, 3).Range.Text = dtTB_InvoicesReport.Rows[i]["product_NameAr"].ToString();
                        _table.Cell(2 + i, 3).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    }
                    else
                    {

                        // Set the alignment of the second column cells to left
                        _table.Cell(NextArCell, 3).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        _table.Cell(NextArCell, 3).Range.Text = dtTB_InvoicesReport.Rows[i]["product_NameEn"].ToString();

                        // Split the cell 2 cells
                        _table.Cell(NextArCell, 3).Split(2, 1);

                        // Set the text direction to right                        
                        _table.Cell(NextArCell + 1, 3).Range.ParagraphFormat.ReadingOrder = Microsoft.Office.Interop.Word.WdReadingOrder.wdReadingOrderRtl;
                        _table.Cell(NextArCell + 1, 3).Range.Text = dtTB_InvoicesReport.Rows[i]["product_NameAr"].ToString();


                        NextArCell = NextArCell + 2;

                    }
                }


                #endregion

                #region Consumable Table index [3]


                DataTable dtTB_ConsumableReport = new DataTable();
                dtTB_ConsumableReport = OperationsofConsumable.Get_Consumable_ByConsumable_Number(InvoiceNumber);

                //Tables Optional consumables
                _table = _aDoc.Tables[3];
                if (dtTB_ConsumableReport.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTB_ConsumableReport.Rows.Count; i++)
                    {

                        // to add new rows to tables
                        _table.Rows.Add(ref objmissval);

                        _table.Cell(3 + i, 1).Range.Text = (1 + i).ToString();
                        _table.Cell(3 + i, 2).Range.Text = dtTB_ConsumableReport.Rows[i]["product_Id"].ToString();
                        _table.Cell(3 + i, 4).Range.Text = dtTB_ConsumableReport.Rows[i]["Consumable_QTY"].ToString();
                        _table.Cell(3 + i, 5).Range.Text = dtTB_ConsumableReport.Rows[i]["Consumable_Unit"].ToString();
                        _table.Cell(3 + i, 6).Range.Text = dtTB_ConsumableReport.Rows[i]["Consumable_Price"].ToString();
                        _table.Cell(3 + i, 7).Range.Text = dtTB_ConsumableReport.Rows[i]["Consumable_Amount"].ToString();

                    }

                    // set color to header of table
                    _table.Rows[2].Range.Shading.BackgroundPatternColor = Microsoft.Office.Interop.Word.WdColor.wdColorGray05;

                    NextArCell = 3;
                    for (int i = 0; i < dtTB_ConsumableReport.Rows.Count; i++)
                    {
                        if (Customer_Language == "English")
                        {
                            // table header
                            _table.Cell(1, 1).Range.Text = "Optional Consumables";


                            // Set the alignment of the second column cells to left
                            _table.Cell(3 + i, 3).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            _table.Cell(3 + i, 3).Range.Text = dtTB_ConsumableReport.Rows[i]["product_NameEn"].ToString();
                        }
                        else if (Customer_Language == "Arabic")
                        {
                            // table header
                            _table.Cell(1, 1).Range.Text = "مواد اختيارية";

                            // Set the text direction to right

                            _table.Cell(3 + i, 3).Range.ParagraphFormat.ReadingOrder = Microsoft.Office.Interop.Word.WdReadingOrder.wdReadingOrderRtl;
                            _table.Cell(3 + i, 3).Range.Text = dtTB_ConsumableReport.Rows[i]["product_NameAr"].ToString();
                            _table.Cell(3 + i, 3).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                        }
                        else
                        {
                            // table header
                            _table.Cell(1, 1).Range.Text = "Optional Consumables  -  مواد اختيارية";




                            // Set the alignment of the second column cells to left
                            _table.Cell(NextArCell, 3).Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
                            _table.Cell(NextArCell, 3).Range.Text = dtTB_ConsumableReport.Rows[i]["product_NameEn"].ToString();


                            // Split the cell 2 cells
                            _table.Cell(NextArCell, 3).Split(2, 1);

                            // Set the text direction to right                            
                            _table.Cell(NextArCell + 1, 3).Range.ParagraphFormat.ReadingOrder = Microsoft.Office.Interop.Word.WdReadingOrder.wdReadingOrderRtl;
                            _table.Cell(NextArCell + 1, 3).Range.Text = dtTB_ConsumableReport.Rows[i]["product_NameAr"].ToString();


                            NextArCell = NextArCell + 2;

                        }
                    }
                }
                else
                {
                    _table.Delete();
                }

                #endregion

                #region terms and conditions 

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

                    //foreach (DataRow row in dtTB_Terms_InvoicesReport.Rows)
                    //{
                    //    builderEN.Append("  -  ").Append(row["Term_En"]).AppendLine();
                    //    builderAR.Append("  -  ").Append(row["Term_Ar"]).AppendLine();
                    //}

                    // Add total in words (Arabic or English based on selected language)
                    if (Customer_Language == "English")
                    {
                        _replaceBookmarks["Terms_and_conditions_Arabic"].Range.Delete();
                        _replaceBookmarks["Terms_and_conditions_English"].Range.Text = builderEN.ToString();

                    }
                    else if (Customer_Language == "Arabic")
                    {

                        _replaceBookmarks["Terms_and_conditions_English"].Range.Delete();
                        _replaceBookmarks["Terms_and_conditions_Arabic"].Range.Text = builderAR.ToString();
                    }
                    else
                    {
                        _replaceBookmarks["Terms_and_conditions_English"].Range.Text = builderEN.ToString();
                        _replaceBookmarks["Terms_and_conditions_Arabic"].Range.Text = builderAR.ToString();                        

                    }
                }
                else
                {
                    _replaceBookmarks["Terms_and_conditions_English"].Range.Delete();
                    _replaceBookmarks["Terms_and_conditions_Arabic"].Range.Delete();
                }


                #endregion

                #region Bank Table index [4] or [3] in case count of Consumable = 0

                // Check the count of Consumable DataTable 
                _table = dtTB_ConsumableReport.Rows.Count > 0 ? _aDoc.Tables[4] : _aDoc.Tables[3];


                var SelectResult = Customer_BankAccount;

                if (SelectResult != "Select No Account Bank")
                {

                    DataTable dtTB_BankAccountReport = new DataTable();
                    dtTB_BankAccountReport = OperationsofBanks.Get_ByBank_Definition(SelectResult);


                    _table.Cell(1, 1).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Definition"].ToString();

                    _table.Cell(2, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Beneficiary_Name"].ToString();

                    _table.Cell(3, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Bank_Name"].ToString();

                    _table.Cell(4, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Branch"].ToString();

                    _table.Cell(5, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Branch_Code"].ToString();

                    _table.Cell(6, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Bank_Address"].ToString();

                    _table.Cell(7, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Swift_Code"].ToString();

                    _table.Cell(8, 1).Range.Text = $"Account Number {Customer_Currency}";
                    _table.Cell(8, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_Account_Number"].ToString();

                    _table.Cell(9, 1).Range.Text = $"IBAN Number {Customer_Currency}";
                    _table.Cell(9, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_IBAN_Number"].ToString();

                    _table.Cell(10, 2).Range.Text = dtTB_BankAccountReport.Rows[0]["Bank_COUNTRY"].ToString();


                }
                else
                {
                    _table.Delete();
                }

                #endregion

                #region Logic for Saving Microsoft Word Files

                // variables to store the folder name, base directory path, and export path
                string _folderName = "Export_File";
                string _baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string _exportPath = Properties.Settings.Default.ExportPath;

                // If the export path doesn't exist, use the base directory
                if (!Directory.Exists(_exportPath))                
                    _exportPath = Path.Combine(_baseDirectory, _folderName);                

                // Create the directory if it doesn't exist
                Directory.CreateDirectory(_exportPath);

                // Generate file name and Save document
                fileName = Path.Combine(_exportPath, $"{ExportFileName}.docx");
                _aDoc.SaveAs2(fileName);
                
                MessageBox.Show("Export Completed", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // open the folder in Windows Explorer.
                Process.Start(_exportPath);                              
                #endregion


            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog("ExportDataToWord => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // Close the document and the application
                if (_aDoc != null)
                {
                    _aDoc.Close();
                    _wordApp.Quit();
                }

                View.Frm_Customers.FrmCustomer.LbWaitSaveFile.Visible = false;
                Cursor.Current = Cursors.Default;
            }


        }
    }
}
