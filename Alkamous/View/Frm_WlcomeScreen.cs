using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Drawing;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_WlcomeScreen : Form
    {
        Chelp chelp = new Chelp();
     
        ClsOperationsofCustomerInfo OperationsofCustomerInfo = new ClsOperationsofCustomerInfo();
        ClsOperationsofProducts OperationsofProducts = new ClsOperationsofProducts();
        ClsOperationsofCustomers OperationsofCustomers = new ClsOperationsofCustomers();
        ClsOperationsofUserLogFile OperationsofUserLogFile = new ClsOperationsofUserLogFile();
        public Frm_WlcomeScreen()
        {
            InitializeComponent();
        }

        private void Frm_WlcomeScreen_Load(object sender, EventArgs e)
        {
            LbWelcome.Text = $"Welcome Mr. {Frm_Main.FrmMain.LBUserName.Text}";
            LoadCountOfItems();

        }

      
        private void LoadCountOfItems()
        {
            // need to check and put try cath for searever 

            var CustomerInfoCount = OperationsofCustomerInfo.Get_CountCustomerInfo();
            lblCountTotalClients.Text = CustomerInfoCount.ToString();

            var ProductsCount = OperationsofProducts.Get_CountProduct();
            lblCountTotalProducts.Text = ProductsCount.ToString();

            var CustomerCount = OperationsofCustomers.Get_CountCustomer();
            lblCountTotalQuotations.Text = CustomerCount.ToString();

            var UsersLogCount = OperationsofUserLogFile.Get_CountUsersLog();
            lblCountTotalUsers.Text = UsersLogCount.ToString();

        }



        private void ChangeColoreMouseEnter(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.Black;
            }
        }

        private void ChangeColoreMouseLeave(object sender, EventArgs e)
        {
            if (sender is Label label)
            {
                label.ForeColor = Color.White;
            }
        }

        private void lbTotalQuotations_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_Customers());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                // MessageBox.Show(ex.Message);
            }
        }

        private void lbTotalClients_Click(object sender, EventArgs e)
        {
            try
            {               
                chelp.ShowForm(new Frm_CustomerInfo());
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void lbTotalUsers_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_UsersLog());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void lbTotalProducts_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_ProductsAddDeleteUpdate());
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
