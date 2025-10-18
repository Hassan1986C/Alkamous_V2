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
        int DataHaveBeenloaded = 0;


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

        }

        private void LoadEnumProdectsModules()
        {
            if (DataHaveBeenloaded == 0)
            {
                // تحميل أسماء الـ enum إلى القائمة المنسدلة
                var result = Enum.GetValues(typeof(ClsOperationsofProducts.ProdectModels));

                TxtGroupByItem.Items.Add("Select Printer Type");

                for (int i = 0; i < result.Length; i++)
                {
                    TxtGroupByItem.Items.Add(result.GetValue(i).ToString());
                }

                DataHaveBeenloaded++;
                Cursor.Current = Cursors.Default;
                TxtGroupByItem.SelectedIndex = 0;
            }
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
                // Check if there is data to save
                if (DGVProducts.RowCount == 0)
                {
                    MessageBox.Show("No data to save.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string product_GroupBy_Name = TxtGroupByItem.Text?.Trim();

                string message = $"Do you want to save the product group '{product_GroupBy_Name}'?";

                if (MessageBox.Show(message, "Confirm Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    await OperationsofProducts.Delete_product_GroupBy(product_GroupBy_Name);

                    foreach (DataRow row in dtProducts.Rows)
                    {
                        string productId = row["product_Id"]?.ToString()?.Trim();

                        bool result = await OperationsofProducts.Add_product_GroupByAsync(product_GroupBy_Name, productId);
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
            if (TxtGroupByItem.SelectedIndex == 0) return;
            await SaveData();
        }

        private void TxtGroupByItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TxtGroupByItem.SelectedIndex == 0)
            {
                dtProducts.Clear();

                return;

            }
            LoadDataGroupByItem(TxtGroupByItem.Text);
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

    }
}
