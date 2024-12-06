using Alkamous.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Alkamous.Controller
{
    public class Chelp
    {
        #region Write Error Logs file
        public static void WriteErrorLog(string strErrorText, string strFileName = "ErrorLog.txt")
        {
            try
            {
                string strPath = AppDomain.CurrentDomain.BaseDirectory;

                string logFilePath = Path.Combine(strPath, strFileName);

                using (StreamWriter sw = new StreamWriter(logFilePath, true, Encoding.UTF8))
                {
                    sw.WriteLine($"{DateTime.UtcNow.ToString()} - {strErrorText}");
                }
            }
            catch (Exception ex)
            {
                // If an exception occurs, write an error message to the console
                MessageBox.Show($"Error in WriteErrorLog: {ex.Message}");
            }
        }
        #endregion

        #region Open and close rest form

        public void ShowForm(Form form)
        {
            try
            {
                if (CheckOpened(form.Name)) return;
                if (CloseAllFormsExceptMain())
                {
                    form.TopLevel = false;
                    form.Dock = DockStyle.Fill;
                    Frm_Main.FrmMain.PainleContener.Controls.Add(form);
                    Frm_Main.FrmMain.PainleContener.Tag = form;
                    form.Show();
                    form.Activate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                WriteErrorLog($"ShowForm => {ex.Message}");
            }
        }

        private bool CloseAllFormsExceptMain()
        {

            var openForms = Application.OpenForms.Cast<Form>().ToList();
            List<Form> formsToClose = openForms.Where(frm => frm.Name != "Frm_Main").ToList();

            //foreach (Form form in formsToClose) form.Close();

            // Use LINQ to close each form
            formsToClose.ForEach(form => form.Close());

            // Re-check open forms after closing the desired forms
            var FormsNotClosedYet = Application.OpenForms.Cast<Form>().ToList();
            var remainingOpenForms = FormsNotClosedYet.Where(frm => frm.Name != "Frm_Main").ToList();
            return !remainingOpenForms.Any();
        }

        private bool CheckOpened(string frmName)
        {
            try
            {
                var openForms = Application.OpenForms.Cast<Form>();
                return openForms.Any(frm => frm.Name == frmName);
            }
            catch (Exception ex)
            {
                WriteErrorLog($"CheckOpened {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Format the currency USD IQD        
        public static string Format_PriceAndAmount(string input, string currency)
        {
            // Keep only digits and a single decimal point
            var output = new string(input.Where(c => char.IsDigit(c) || c == '.').ToArray());

            // Ensure there is at most one decimal point
            if (output.Count(c => c == '.') > 1) return "";

            // Parse the cleaned string
            if (decimal.TryParse(output, out decimal value))
            {
                // Determine format based on currency
                string format = currency == "IQD" ? "#,##0" : "#,##0.00";

                // Format and return the value
                return value.ToString(format);
            }

            // Return an empty string if parsing fails
            return "";
        }
        #endregion

        #region Calculate the Amount by QTY* Pric
        public static string CalculateAmount(string QTY, string Price, string currency = "USD")
        {
            if (QTY != string.Empty && Price != string.Empty)
            {
                if (int.TryParse(QTY, out int IntQTY) && decimal.TryParse(Price, out decimal DecmalPrice))
                {
                    Decimal Amount = DecmalPrice * IntQTY;
                    return Chelp.Format_PriceAndAmount(Convert.ToString(Amount), currency);
                }
            }
            return "0";
        }
        #endregion

        #region Exchange if select USD or IQD and updated TxtProduct_Price.Text and TxtAmount.Text
        public static (string STxtPrice, string STxtAmount) ExchangeAndTaxesToForward
                      (string TxtExchange, string TxtTaxes, string TxtProduct_Price, string TxtQTY, string currency)
        {
            // Convert exchange rate to decimal. Default to 0 if input is null or empty.
            var exchangeRate = string.IsNullOrEmpty(TxtExchange) ? 0 : Convert.ToDecimal(TxtExchange);

            // Convert tax rate to decimal and divide by 100 to get percentage. Default to 0 if input is null or empty.
            var taxRate = string.IsNullOrEmpty(TxtTaxes) ? 0 : Convert.ToDecimal(TxtTaxes) / 100m;

            // Convert product price to decimal.
            var Product_Price = Convert.ToDecimal(TxtProduct_Price);

            // Convert quantity to integer.
            var QTY = Convert.ToInt32(TxtQTY);

            decimal Product_PriceUpdated;

            // Calculate product price with exchange rate if exchange rate is not 0.
            var Product_PriceWithEXchage = exchangeRate != 0 ? Product_Price * exchangeRate : Product_Price;

            // Calculate product price with taxes if tax rate is not 0.
            var Product_PriceWithEXchageWithTaxes = taxRate != 0 ? Product_PriceWithEXchage * taxRate : taxRate;

            // Sum the product price with exchange rate and the product price with taxes.
            Product_PriceUpdated = Product_PriceWithEXchage + Product_PriceWithEXchageWithTaxes;

            // Calculate the total amount by multiplying the updated product price by the quantity.
            decimal AmountUpdated = Product_PriceUpdated * QTY;

            // Format the updated product price and total amount according to the currency.
            string STxtPrice = Format_PriceAndAmount(Product_PriceUpdated.ToString(), currency);
            string STxtAmount = Format_PriceAndAmount(AmountUpdated.ToString(), currency);

            // Return the formatted updated product price and total amount as a tuple.
            return (STxtPrice, STxtAmount);
        }


        #endregion

        #region Update TxtTotalAmount return with Format
        public static string DGVProductsChangededReSumTotalAmount(string Currency, DataGridView DGV)
        {
            Decimal TotalAmount = 0;
            foreach (DataGridViewRow row in DGV.Rows)
            {
                TotalAmount += Convert.ToDecimal(row.Cells[6].Value);
            }

            return Format_PriceAndAmount(TotalAmount.ToString(), Currency);

        }
        #endregion

        #region Payment Terms Setting

        public static List<object> LoadDataToComboBox()
        {
            // Define a list to hold the items payment terms
            var items = new List<object>
            {
                new { Text = $"{100} % in ADVANCE", Value = 100 }
            };

            // Create an anonymous type object and add it to the list
            for (int advance = 90; advance >= 10; advance -= 10)
            {
                int onDelivery = 100 - advance;
                items.Add(new { Text = $"{advance}% in ADVANCE {onDelivery}% on DELIVERY", Value = advance });
            }

            items.Add(new { Text = $"{100} % on DELIVERY", Value = 0 });
            items.Add(new { Text = $"As per Terms", Value = -1 });

            return items;
        }



        // need check ValOfPaymentInAdv should be 100 % defult
        public static (decimal PaymentInADVANCE, decimal PaymentOnDELIVERY, string InADVANCEPercentage, string OnDELIVERYPercentage)
            PaymentTermsSettings(decimal TotalWithDiscount, int ValOfPaymentInAdv = 100)
        {
            decimal PaymentInADVANCE = TotalWithDiscount * ValOfPaymentInAdv / 100;
            decimal PaymentOnDELIVERY = TotalWithDiscount - PaymentInADVANCE;
            string InADVANCEPercentage = $"{ValOfPaymentInAdv}%";
            string OnDELIVERYPercentage = $"{100 - ValOfPaymentInAdv}%";
            return (PaymentInADVANCE, PaymentOnDELIVERY, InADVANCEPercentage, OnDELIVERYPercentage);
        }

        #endregion

        #region Update the Prices And Calculate the Amount of manual DataGridView


        public static (string STxtTotalAmount, bool Result) UpdatePricesAndRecalculate(DataGridView dataGridView, string currency)
        {
            // bool success
            if (UpdatePricesAndRecalculateAmounts(dataGridView, currency))
            {
                if (dataGridView.Name == "DGVProducts")
                {
                    return (DGVProductsChangededReSumTotalAmount(currency, dataGridView), true);
                }
                else
                {
                    return ("", true);
                }

            }

            return ("Error", false);
        }


        private static bool UpdatePricesAndRecalculateAmounts(DataGridView dataGridView, string currency)
        {

            // Return immediately if there are no rows in the DataGridView
            if (dataGridView.Rows.Count == 0) return true;

            // Define the column indices for Quantity, Price, and Amount
            const int QuantityColumnIndex = 3; // The index of the column containing the quantity
            const int UnitColumnIndex = 4;     // The index of the column containing the UnitColumnIndex
            const int PriceColumnIndex = 5;    // The index of the column containing the price
            const int AmountColumnIndex = 6;   // The index of the column containing the total amount           
            // Iterate through each row in the DataGridView
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                try
                {
                    // Check if the necessary cells are null or empty
                    if (!ValidateCell(dataGridView, row, QuantityColumnIndex) || !ValidateCell(dataGridView, row, PriceColumnIndex) || !ValidateCell(dataGridView, row, UnitColumnIndex))
                        return false;

                    // Try to parse the price and quantity values from the respective columns
                    if (decimal.TryParse(row.Cells[PriceColumnIndex].Value?.ToString(), out decimal price) &&
                        int.TryParse(row.Cells[QuantityColumnIndex].Value?.ToString(), out int quantity))
                    {
                        // Calculate the total amount by multiplying price and quantity
                        var Amount = price * quantity;

                        // Format the quantity and total amount using a custom helper method and the specified currency
                        row.Cells[PriceColumnIndex].Value = Format_PriceAndAmount(price.ToString(), currency);
                        row.Cells[AmountColumnIndex].Value = Format_PriceAndAmount(Amount.ToString(), currency);

                    }
                }
                catch (Exception ex)
                {
                    // Log the error and show a message to the user indicating which row failed to update
                    MessageBox.Show($"Error updating row: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // Exit the method if an error occurs
                }
            }
            return true;
        }

        private static bool ValidateCell(DataGridView dataGridView, DataGridViewRow row, int columnIndex)
        {
            if (row.Cells[columnIndex].Value == null || string.IsNullOrWhiteSpace(row.Cells[columnIndex].Value.ToString()))
            {
                dataGridView.CurrentCell = row.Cells[columnIndex];
                dataGridView.BeginEdit(true);
                return false;
            }
            return true;
        }

        #endregion

        #region Generate a random 256-bit encryption key and initialization vector (IV) 
        public static (byte[] Password, byte[] UserKey, byte[] UserIV) EncryptedPassword(string UserPassword)
        {
            // Generate a random 256-bit encryption key and initialization vector (IV)
            byte[] key = new byte[32];
            byte[] iv = new byte[16];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(iv);
            }

            byte[] encryptedData = ClsAESEncryption.AESEncrypt(UserPassword, key, iv);
            return (encryptedData, key, iv);
        }
        #endregion

        #region Register Users Action Logs
        public static void RegisterUsersActionLogs(string Opration_type, string Opration)
        {
            ClsOperationsofUserLogFile ClsOperationsofUserLogFile = new ClsOperationsofUserLogFile();
            var ActionResult = new Model.CTB_UserLog
            {
                UserLog_UserName = View.Frm_Main.FrmMain.LBUserName.Text.Trim(),
                UserLog_Opration_type = Opration_type,
                UserLog_opration = Opration
            };
            ClsOperationsofUserLogFile.AddNew(ActionResult);
        }
        #endregion

        #region Fix WordTemplate Files docx
        public static void FixWordTempletefiles()
        {
            string basePathFolderTemp = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Temp");
            string tempFilePathwithheader = Path.Combine(basePathFolderTemp, "TempFileWithHeader.docx");
            string tempFilePathwithoutheader = Path.Combine(basePathFolderTemp, "TempFileWithoutHeader.docx");            
            string zipFilePath = Path.Combine(basePathFolderTemp, "TempFile.zip");

            if (MessageBox.Show("Make sure you have closed all Word files before clicking Yes?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                try
                {
                    string[] filesToDelete = { tempFilePathwithheader, tempFilePathwithoutheader };

                    foreach (string filePath in filesToDelete)
                    {
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }

                    ZipFile.ExtractToDirectory(zipFilePath, basePathFolderTemp);

                    MessageBox.Show("Extraction successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                    WriteErrorLog(MethodNames + " => " + ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }

        }
        #endregion
    }
}

