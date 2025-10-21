using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_Products_group_AddDeleteUpdate : Form
    {

        private readonly ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts(new DataAccessLayer());


        public static DataTable dtProducts = new DataTable();

        public Frm_Products_group_AddDeleteUpdate()
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

        private async void BtnAdd_Click(object sender, EventArgs e)
        {
            await SaveData();
        }

        private async Task SaveData()
        {
            try
            {

                if (CheckDataEntry())
                {
                    string product_item_Group_Name = TxtproductGroupName.Text?.Trim();

                    string message = $"Do you want to save the product group '{product_item_Group_Name}'?";

                    if (MessageBox.Show(message, "Confirm Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;

                        await OperationsofProducts.Add_product_Item_GroupAsync(product_item_Group_Name);


                        Cursor.Current = Cursors.Default;


                        MessageBox.Show("Data has been added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearAllTestBox();
                        loadData();
                    }
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

        private void CheckWhoSendOrder(bool sender)
        {

            BtnEdit.Enabled = sender;
            BtnCancelEdit.Visible = sender;
            BtnAdd.Visible = (!sender);
            BtnDelete.Visible = (!sender);
        }
        private async void BtnEdit_Click(object sender, EventArgs e)
        {
            if (CheckDataEntry())
            {
                var result = await OperationsofProducts.Edit_product_Item_GroupAsync(TxtAutoNumber.Text, TxtproductGroupName.Text.Trim());
                if (result)
                {
                    Chelp.RegisterUsersActionLogs("Update Terms", TxtproductGroupName.Text.Trim());
                    MessageBox.Show(" Data Updated Successfully ");
                    CheckWhoSendOrder(false);
                    ClearAllTestBox();
                    loadData();
                }
                else
                {
                    MessageBox.Show("No");
                }
            }
        }

        private bool CheckDataEntry()
        {
            if (string.IsNullOrWhiteSpace(TxtproductGroupName.Text))
            {
                MessageBox.Show("Please enter the product group name.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtproductGroupName.Focus();
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
            TxtSearch.Clear();
            TxtAutoNumber.Clear();
        }
        private void BtnCancelEdit_Click(object sender, EventArgs e)
        {
            CheckWhoSendOrder(false);
            ClearAllTestBox();
            loadData();
        }

        private async void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (DGVProducts.RowCount > 0)
                {

                    if (MessageBox.Show("Are you sure you want to Delete  " + Environment.NewLine

                        , "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        bool Result = await OperationsofProducts.Delete_product_Item_GroupAsync(DGVProducts.CurrentRow.Cells[0].Value.ToString());
                        await OperationsofProducts.Delete_product_GroupBy(DGVProducts.CurrentRow.Cells[0].Value.ToString());
                        if (Result)
                        {
                            Chelp.RegisterUsersActionLogs("Delete Group", TxtproductGroupName.Text.Trim());
                            MessageBox.Show("Data Deleted Successfully ");
                            loadData();
                            ClearAllTestBox();
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

        private void Frm_Products_group_AddDeleteUpdate_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private async void loadData()
        {
            dtProducts.Clear();

            dtProducts = await OperationsofProducts.Get_product_item_Group();

            DGVProducts.DataSource = dtProducts;
            LbCount.Text = dtProducts.Rows.Count.ToString();
        }

        private void DGVProducts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                CheckWhoSendOrder(true);
                TxtAutoNumber.Text = DGVProducts.CurrentRow.Cells[0].Value.ToString();
                TxtproductGroupName.Text = DGVProducts.CurrentRow.Cells[1].Value.ToString();

            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
    }
}
