using Alkamous.Controller;
using Alkamous.Model;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_ProductsImportExport : Form
    {
        private readonly ClsOperationsofProducts operationsofProducts = new ClsOperationsofProducts(new DataAccessLayer());
        private List<CTB_Products> csvDataList = null;

        public Frm_ProductsImportExport()
        {
            InitializeComponent();
            ResetToDefault();
        }

        private void ResetToDefault()
        {

            csvDataList = null;
            txtNewPth.Text = "";

            BtnExportProducts.Checked = false;
            BtnImportProducts.Checked = false;

            BtnExportProducts.Enabled = true;
            BtnImportProducts.Enabled = true;

            BtnSaveConfiguration.Enabled = false;
            LbWaitSaveFile.Visible = false;
            progressBar1.Visible = false;
            progressBar1.Value = 0;
        }

        private void CheckedIsSelected(bool IsExportProducts)
        {
            //directly assign the result true if ExportProducts
            bool Result = IsExportProducts;

            txtNewPth.Text = "";
            csvDataList = null;

            BtnExportProducts.Checked = Result;
            BtnImportProducts.Checked = !(Result);

            BtnExportProducts.Enabled = !(Result);
            BtnImportProducts.Enabled = (Result);

            BtnSaveConfiguration.Enabled = false;
        }

        private bool CheckInputAndPath()
        {
            if ((BtnExportProducts.Checked == false) && (BtnImportProducts.Checked == false))
            {
                MessageBox.Show("please select one of the options of below");
                return false;
            }
            else if (txtNewPth.Text == string.Empty)
            {
                MessageBox.Show("please select the Path");
                return false;
            }
            else
            {
                return true;
            }
        }

        private void BtnResetToDefault_Click(object sender, EventArgs e)
        {
            ResetToDefault();
        }

        private async void BtnSaveConfiguration_Click(object sender, EventArgs e)
        {
            if (CheckInputAndPath())
            {
                Cursor.Current = Cursors.WaitCursor;
                progressBar1.Visible = true;
                LbWaitSaveFile.Visible = true;


                if (BtnExportProducts.Checked)
                {
                    progressBar1.Visible = false;
                    InitializeToProcess(false);

                    await ExportProductsDataAsync();

                    InitializeToProcess(true);

                }
                else
                {

                    InitializeToProcess(false);

                    var (importedProducts, totalProducts) = await ImportProductsToServerasync();
                    MessageBox.Show($"{importedProducts} Products have been imported successfully from {totalProducts} Products", "Message Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    InitializeToProcess(true);

                }
                ResetToDefault();

            }
        }

        private void InitializeToProcess(bool isProcessingFinished)
        {
            BtnSaveConfiguration.Enabled = isProcessingFinished;
            BtnBackToImportAndExport.Enabled = isProcessingFinished;
            BtnResetToDefault.Enabled = isProcessingFinished;
        }

        private void BtnExportProducts_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnExportProducts.Checked)
            {
                CheckedIsSelected(true);
            }
        }

        private void BtnImportProducts_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnImportProducts.Checked)
            {
                CheckedIsSelected(false);
            }
        }

        private void BtnOpenPath_Click(object sender, EventArgs e)
        {
            if ((BtnExportProducts.Checked == false) & (BtnImportProducts.Checked == false))
            {
                MessageBox.Show("please select one of the options above");
                return;
            }
            if (BtnExportProducts.Checked)
            {
                using (var folderBrowser = new FolderBrowserDialog
                {
                    Description = "Choose the path to export the CSV File"
                })
                {
                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        txtNewPth.Text = folderBrowser.SelectedPath;
                        BtnSaveConfiguration.Enabled = true;
                    }
                }
            }
            else
            {
                using (var _openFileDialog = new OpenFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    Title = "Select Products CSV file"
                })
                {
                    if (_openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string filePath = _openFileDialog.FileName;
                    txtNewPth.Text = filePath;

                    using (var reader = new StreamReader(filePath, Encoding.Default))
                    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                    {
                        var records = csv.GetRecords<CTB_Products>().ToList();

                        if (records.Count > 0)
                        {
                            BtnSaveConfiguration.Enabled = true;
                            csvDataList = records;
                        }
                        else
                        {
                            MessageBox.Show("No data found in the CSV file.", "Error");
                        }
                    }
                }
            }

        }

        private async Task ExportProductsDataAsync()
        {
            try
            {
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();

                await Task.Run(async () =>
                {
                    DataTable ResultOfData = new DataTable();
                    ResultOfData =await operationsofProducts.Get_AllProduct();

                    if (ResultOfData.Rows.Count > 0)
                    {
                        string strPath = txtNewPth.Text.Trim();
                        string csvFilePath = Path.Combine(strPath, "ALKAMOUS Products.CSV");

                        int TotalRows = ResultOfData.Rows.Count;

                        csvDataList = ResultOfData.ConvertDataTableToList<CTB_Products>();


                        using (var fileStream = new FileStream(csvFilePath, FileMode.Create, FileAccess.Write))
                        {
                            using (var writer = new StreamWriter(fileStream, Encoding.UTF8))
                            {
                                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                                {
                                    csv.WriteRecords(csvDataList);
                                }
                                MessageBox.Show($"{TotalRows} Products has been Export successfully", "Info");

                            }
                        }
                        Debug.WriteLine("Total Rows " + TotalRows);
                    }
                    else
                    {
                        MessageBox.Show($" NO Products Export !", "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                });
                //stopwatch.Stop();
                //TimeSpan elapsedTime = stopwatch.Elapsed;
                //Debug.WriteLine($"Time elapsed: {elapsedTime.TotalMilliseconds} ms");
            }
            catch (Exception ex)
            {

                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
                Cursor.Current = Cursors.Default;
            }
        }


        private async Task<(int importedProducts, int totalProducts)> ImportProductsToServerasync()
        {
            try
            {
                if (csvDataList != null)
                {
                    int totalProducts = csvDataList.Count;
                    int importedProducts = 0;
                    int progresscount = 0;
                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = totalProducts;
                    await Task.Run(async () =>
                     {
                         foreach (var _Products in csvDataList)
                         {
                             if (!operationsofProducts.Check_ProductIdNotDuplicate(_Products.product_Id))
                             {
                                 var result =await operationsofProducts.AddNewAsync(_Products);
                                 if (result) importedProducts++;
                             }
                             progresscount++;
                             Invoke(new Action(() => { progressBar1.Value = progresscount; }));                             
                         }
                     });
                    return (importedProducts, totalProducts);
                }
                else
                {
                    return (0, 0); // No data found.
                }
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                throw; // Rethrow the exception.
            }
        }



        private void BtnBackToImportAndExport_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_CustomersOptionsImportExportForm());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

    }
}
