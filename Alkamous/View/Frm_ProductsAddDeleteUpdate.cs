using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_ProductsAddDeleteUpdate : Form
    {
        ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts();


        public Frm_ProductsAddDeleteUpdate()
        {
            InitializeComponent();
        }

        private void ReColoreDGV(DataGridView dataGridView)
        {

            foreach (DataGridViewRow row in DGVProducts.Rows)
            {
                if (row.Index % 2 == 0)
                {
                    DGVProducts.Rows[row.Index].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                }

                if (DGVProducts.Rows[row.Index].Cells[5].Value.ToString() == "Yes")
                {
                    DGVProducts.Rows[row.Index].Cells[5].Style.ForeColor = Color.Red;
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
            DGVProducts.Columns[5].HeaderText = "Favorite";
            
            DGVProducts.Columns[0].Width = 25;
            DGVProducts.Columns[1].Width = 60;
            DGVProducts.Columns[2].Width = 60;
            DGVProducts.Columns[3].Width = 15;
            DGVProducts.Columns[4].Width = 15;
            DGVProducts.Columns[5].Width = 50;


            for (int i = 0; i < DGVProducts.Columns.Count; i++)            
                DGVProducts.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            DGVProducts.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            DGVProducts.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DGVProducts.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGVProducts.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DGVProducts.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

      


        private bool CheckDataEntry()
        {
            var textBoxes = groupBox1.Controls.OfType<TextBox>();
            foreach (var txt in textBoxes)
            {
                txt.BackColor = Color.White;
                if (string.IsNullOrEmpty(txt.Text))
                {
                    txt.Focus();
                    txt.BackColor = Color.Ivory;
                    MessageBox.Show("All fields required");
                    return false;
                }
            }

            if (!ValidateInputPrice())
            {
                return false;
            }
            return true;
        }


        private void ClearAllTestBox()
        {
            var textBoxes = groupBox1.Controls.OfType<TextBox>();
            foreach (var txt in textBoxes)
            {
                txt.Clear();
            }
            BtnFavorite.Checked = false;
        }


        private async Task<Task> LoadData(string Search = "")
        {
            try
            {
                var ResultOfData = string.IsNullOrEmpty(Search)
                    ? await OperationsofProducts.Get_AllProduct(1, 5000000)
                    : await OperationsofProducts.Get_AllProduct_BySearch(Search, 1, 5000000);


                // تأكد من أن الجدول يحتوي على أعمدة
                if (ResultOfData != null && ResultOfData.Columns.Count > 5)
                {
                    // إنشاء عمود جديد
                    string columnName = ResultOfData.Columns[5].ColumnName;
                    var newColumn = new DataColumn(columnName + "_Copy", typeof(string));

                    // إضافة العمود الجديد إلى الجدول
                    ResultOfData.Columns.Add(newColumn);

                    // نسخ البيانات من العمود الأصلي إلى العمود الجديد كقيم نصية
                    foreach (DataRow row in ResultOfData.Rows)
                    {                       
                        if (row[columnName] != DBNull.Value && bool.TryParse(row[columnName].ToString(), out bool value))
                        {
                            row[newColumn.ColumnName] = value ? "Yes" : "No";
                        }
                        else
                        {
                            // التعامل مع الحالات التي يكون فيها العمود فارغًا أو يحتوي على قيمة غير صحيحة
                            row[newColumn.ColumnName] = "Unknown";
                        }
                    }




                    // إزالة العمود القديم إذا لزم الأمر
                    ResultOfData.Columns.Remove(columnName);
                    // إعادة تسمية العمود الجديد إلى نفس اسم العمود القديم
                    newColumn.ColumnName = columnName;
                }


                DGVProducts.DataSource = ResultOfData;
                LbCount.Text = DGVProducts.RowCount.ToString();
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



        private void LbCount_Click(object sender, EventArgs e)
        {

        }

        private async void Frm_ProductsAddDeleteUpdate_Load(object sender, EventArgs e)
        {
            var ResultOfData = await LoadData();
            // we can say  ResultOfData.IsCompleted or the condation belwo
            if (ResultOfData.Status == TaskStatus.RanToCompletion)
            {
                DGVColumnHeaderTextAndWidth();
            }

        }

        private async void BtnDeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVProducts.RowCount > 0)
                {
                    CTB_Products MCTB_Products = new CTB_Products("ctr2");
                    if (MessageBox.Show("Are you sure you want to Delete  " + Environment.NewLine
                        + Environment.NewLine + "product_Id    : " + DGVProducts.CurrentRow.Cells[MCTB_Products.product_Id].Value.ToString()
                        + Environment.NewLine + "product_NameEn   : " + DGVProducts.CurrentRow.Cells[MCTB_Products.product_NameEn].Value.ToString()
                        + Environment.NewLine + "product_NameAr     : " + DGVProducts.CurrentRow.Cells[MCTB_Products.product_NameAr].Value.ToString()
                        , "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        bool Result = await OperationsofProducts.DeleteAsync(DGVProducts.CurrentRow.Cells[0].Value.ToString());
                        if (Result)
                        {
                            Chelp.RegisterUsersActionLogs("Delete prodect", TxtProductId.Text);
                            MessageBox.Show("Data Deleted Successfully ");
                            await LoadData();
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
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private CTB_Products AssignValuesToClass()
        {
            CTB_Products MTB_Products = new CTB_Products
            {
                product_Id = TxtProductId.Text,
                product_NameAr = TxtProductNameAr.Text,
                product_NameEn = TxtProductNameEn.Text,
                product_Price = TxtProductPrice.Text,
                product_Unit = TxtProductUnit.Text,
                product_favorite = BtnFavorite.Checked.ToString(),

            };
            return MTB_Products;
        }

        private async void BtnAddProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDataEntry())
                {

                    if (OperationsofProducts.Check_ProductIdNotDuplicate(TxtProductId.Text))
                    {
                        TxtProductId.Focus();
                        TxtProductId.SelectAll();
                        MessageBox.Show($"Product Id {TxtProductId.Text} already exists");
                        return;
                    }

                    CTB_Products MTB_Products = AssignValuesToClass();

                    var result = await OperationsofProducts.AddNewAsync(MTB_Products);
                    if (result)
                    {
                        Chelp.RegisterUsersActionLogs("Add prodect", TxtProductId.Text);
                        MessageBox.Show("Data Added Successfully");
                        ClearAllTestBox();
                        if (string.IsNullOrEmpty(TxtSearch.Text))
                        {
                            await LoadData();
                        }
                        else
                        {
                            TxtSearch.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sorry There is an error try later and re-open the software again");
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



        private async void BtnEditProduct_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckDataEntry())
                {

                    CTB_Products MTB_Products = AssignValuesToClass();
                    var result = await OperationsofProducts.UpdateAsync(MTB_Products);
                    if (result)
                    {
                        Chelp.RegisterUsersActionLogs("Update prodect", TxtProductId.Text);
                        MessageBox.Show(" Data Updated Successfully ");
                        ClearAllTestBox();
                        EnabledDisableButtons(false);
                        if (string.IsNullOrEmpty(TxtSearch.Text))
                        {
                            await LoadData();
                        }
                        else
                        {
                            TxtSearch.Clear();
                        }

                    }
                    else
                    {
                        MessageBox.Show("No Data Updated Please re run the app");
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

        private async void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            await LoadData(TxtSearch.Text.Trim());
        }


        private void MoveToNextText(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = e.SuppressKeyPress = true;
            }
        }


        private void TxtProductPrice_Validated(object sender, EventArgs e)
        {
            ValidateInputPrice();
        }

        private bool ValidateInputPrice()
        {
            int dotCount = TxtProductPrice.Text.Count(c => c == '.');
            if (TxtProductPrice.Text != "" && dotCount <= 1)
            {
                TxtProductPrice.Text = Convert.ToString(string.Format("{0:###,###,##0.00}", (Convert.ToDouble(TxtProductPrice.Text))));
                TxtProductPrice.SelectionStart = TxtProductPrice.Text.Length;
                TxtProductPrice.SelectionLength = 0;
                return true;
            }
            else
            {
                TxtProductPrice.Focus();
                MessageBox.Show("the dot you add more than one check your price again, please");
                return false;
            }
        }

        public static int CountDots(string text)
        {
            Regex dotRegex = new Regex("\\.");
            MatchCollection matches = dotRegex.Matches(text);
            return matches.Count;
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

        private void EnabledDisableButtons(bool IsEditMode)
        {
            TxtProductId.Enabled = (!IsEditMode);
            BtnEditProduct.Enabled = IsEditMode;

            BtnCancelEdit.Visible = IsEditMode;
            BtnAddProduct.Visible = (!IsEditMode);
            BtnDeleteProduct.Visible = (!IsEditMode);
        }

        private void DGVProducts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CTB_Products MCTB_Products = new CTB_Products("ctr2");
                TxtProductId.Text = DGVProducts.CurrentRow.Cells[MCTB_Products.product_Id].Value.ToString();
                TxtProductNameEn.Text = DGVProducts.CurrentRow.Cells[MCTB_Products.product_NameEn].Value.ToString();
                TxtProductNameAr.Text = DGVProducts.CurrentRow.Cells[MCTB_Products.product_NameAr].Value.ToString();
                TxtProductPrice.Text = DGVProducts.CurrentRow.Cells[MCTB_Products.product_Price].Value.ToString();
                TxtProductUnit.Text = DGVProducts.CurrentRow.Cells[MCTB_Products.product_Unit].Value.ToString();

                // التعامل مع القيم "favorite" التي هي نصوص "Yes" أو "No"
                object favoriteValue = DGVProducts.CurrentRow.Cells[MCTB_Products.product_favorite]?.Value;
                if (favoriteValue != null)
                {
                    string favoriteText = favoriteValue.ToString();
                    BtnFavorite.Checked = favoriteText.Equals("Yes", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    BtnFavorite.Checked = false;
                }


                EnabledDisableButtons(true);
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private async void BtnCancelEdit_Click(object sender, EventArgs e)
        {
            EnabledDisableButtons(false);
            ClearAllTestBox();
            if (string.IsNullOrEmpty(TxtSearch.Text))
            {
                await LoadData();
            }
            else
            {
                TxtSearch.Clear();
            }

        }

        private void Btn_HintAndInfo_Click(object sender, EventArgs e)
        {
            var Result = "";
            Result += $"on product Name should apply the below info  \n\n";
            Result += $"1- Product category + 2 product type + 3 Product information \n\n";
            Result += $"Example \n";
            Result += $"Product category = Evolis or Fargo or Elka or etc \n";
            Result += $"product type = Primacy 2 or HID 5600 or  etc \n";
            Result += $"Product information = duplex 300DPI or single 300DPI or Re- transfer or etc  \n";
            Result += $"---------------------------------------------------------------- \n";
            Result += $"Evolis Primacy 2 duplex 300DPI .... \n";
            Result += $"Fargo HID 5600 duplex 600DPI Re- transfer .... \n";
            Result += $"Evolis Primacy 2 Ribbon YMCKO 300 print/Roll .... \n";

            MessageBox.Show(Result, "Info on how to add new products For Easest Search", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnFavorite_CheckedChanged(object sender, EventArgs e)
        {
            BtnFavorite.ForeColor = BtnFavorite.Checked ? Color.Red : Color.Black;
        }
    }
}
