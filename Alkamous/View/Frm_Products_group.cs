using Alkamous.Controller;
using Alkamous.InterfaceForAllClass;
using Alkamous.Model;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Alkamous.Model.CTB_Products;

namespace Alkamous.View
{
    public partial class Frm_Products_group : Form
    {

        private readonly ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts(new DataAccessLayer());
        private readonly CTB_Products MCTB_Products = new CTB_Products(CTB_Products.ProductFieldNaming.Plain);

        public static DataTable dtProducts = new DataTable();


        private static Frm_Products_group frmProductsgroup;
        public static string Invoice_NumberToGetData { get; set; }
       

        // this methode to make a new form same as orgnal from
        public static Frm_Products_group FrmProductsgroup
        {
            get { return frmProductsgroup; }
        }

        public Frm_Products_group()
        {
            InitializeComponent();
            frmProductsgroup = this;
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
            Model.CTB_Products MCTB_Products = new Model.CTB_Products(ProductFieldNaming.Plain);


            DGVProducts.RowHeadersVisible = false;

            using (dtProducts = new DataTable())
            {
                dtProducts.Columns.Add(MCTB_Products.product_Id);
                dtProducts.Columns.Add(MCTB_Products.product_NameEn);
                dtProducts.Columns.Add(MCTB_Products.product_NameAr);
                dtProducts.Columns.Add("Invoice_QTY");
                dtProducts.Columns.Add(MCTB_Products.product_Unit);
                dtProducts.Columns.Add(MCTB_Products.product_Price);
                dtProducts.Columns.Add("Invoice_Amount");

                DGVProducts.DataSource = dtProducts;
            }


            DGVProducts.AutoGenerateColumns = false;
            DGVProducts.Columns.Clear();

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
                Name = "Invoice_QTY",
                DataPropertyName = "Invoice_QTY",
                HeaderText = "Invoice_QTY",
                Width = 20,
                Visible = false
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
                Name = MCTB_Products.product_Price,
                DataPropertyName = MCTB_Products.product_Price,
                HeaderText = "Price",
                Width = 20
            });



            DGVProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Invoice_Amount",
                DataPropertyName = "Invoice_Amount",
                HeaderText = "Invoice_Amount",
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

        private void BtnShowAllProdcts_Click(object sender, EventArgs e)
        {
            try
            {

                Frm_Products frm = new Frm_Products();

                Frm_Products.WhoSendOrder = "Group";
                Frm_Products.isMainQuotation = "Group";

                frm.ShowDialog();


            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void Frm_Products_group_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadEnumProdectsModules();
        }

        private async void LoadDataGroupByItem(string Search)
        {

            dtProducts.Clear();

            dtProducts = await OperationsofProducts.Get_product_GroupBy_BySearch(Search);

            DGVProducts.DataSource = dtProducts;
            LbCountProdects.Text = dtProducts.Rows.Count.ToString();
            ReColoreDGV(DGVProducts);

        }


        public async void LoadEnumProdectsModules()
        {
            // Load data from database (await the task)
            DataTable dtDep = await OperationsofProducts.Get_product_item_Group();

            // Add a default "Select Printer Type" row at the top
            DataRow newRow = dtDep.NewRow();
            newRow["product_Group_Name"] = "Select Printer Type";
            newRow["product_item_Group_AutoNum"] = "0";
            dtDep.Rows.InsertAt(newRow, 0);

            // Bind to ComboBox
            TxtGroupByItem.DataSource = dtDep;
            TxtGroupByItem.DisplayMember = "product_Group_Name";
            TxtGroupByItem.ValueMember = "product_item_Group_AutoNum";

            // Optionally select nothing at first
            TxtGroupByItem.SelectedIndex = 0;
        }

      
        private void BtnDeleteRowFromDGVProducts_Click(object sender, EventArgs e)
        {
            DeleteRowFromDGVBySender(DGVProducts, dtProducts);
            LbCountProdects.Text = DGVProducts.RowCount.ToString();
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
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private async Task SaveData()
        {
            try
            {
               
                string product_GroupBy_Id = Convert.ToString(TxtGroupByItem.SelectedValue.ToString());
                string product_GroupBy_Name = TxtGroupByItem.Text?.Trim();

                string message = $"Do you want to save the product group '{product_GroupBy_Name}'?";

                if (MessageBox.Show(message, "Confirm Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    await OperationsofProducts.Delete_product_GroupBy(product_GroupBy_Id);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        string productId = row["product_Id"]?.ToString();

                        bool result = await OperationsofProducts.Add_product_GroupByAsync(product_GroupBy_Id, productId);
                    }

                    Cursor.Current = Cursors.Default;


                    MessageBox.Show("Data has been added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //TxtGroupByItem.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private async void BtnSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. استخدام الحارس (Guard Clauses) لتبسيط الكود وتقليل التداخل (Nesting)
                if (TxtGroupByItem.SelectedIndex <= 0)
                {
                    MessageBox.Show("Please select a group to add the products to.", "Selection Required",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DGVProducts.Rows.Count == 0)
                {
                    MessageBox.Show("There are no products to save.", "Empty Data",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. تعطيل الزر لمنع النقرات المتعددة (Race Conditions)
                BtnSaveData.Enabled = false;

                // 3. تنفيذ عملية الحفظ
                await SaveData();
               
            }
            catch (Exception ex)
            {
                // 4. معالجة الأخطاء (Error Handling)
                MessageBox.Show($"An error occurred while saving: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // 5. إعادة تفعيل الزر في كل الأحوال
                BtnSaveData.Enabled = true;
            }
        }

        private void TxtGroupByItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TxtGroupByItem.SelectedIndex == 0)
            {
                dtProducts.Clear();
                LbCountProdects.Text ="0"; 
                return;

            }           

            if (TxtGroupByItem.SelectedValue != null)
            {
                LoadDataGroupByItem(TxtGroupByItem.SelectedValue.ToString());
            }
        }



        #region Move Rows Up And Down
        // we use Action Delegate as lambda
        private void BtnUpMoveRows_Click(object sender, EventArgs e)
        {
            Chelp.ExecuteSafely(() => Chelp.MoveRow(DGVProducts, dtProducts, Chelp.MoveDirection.Up));

        }

        private void BtnDownMoveRows_Click(object sender, EventArgs e)
        {
            Chelp.ExecuteSafely(() => Chelp.MoveRow(DGVProducts, dtProducts, Chelp.MoveDirection.Down));
        }

        #endregion

        private void BtnAddToGroup_Click(object sender, EventArgs e)
        {
            Frm_Products_group_AddDeleteUpdate frm = new Frm_Products_group_AddDeleteUpdate();
            frm.ShowDialog();
            LoadEnumProdectsModules();
        }
    }
}
