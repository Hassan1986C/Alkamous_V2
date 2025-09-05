using Alkamous.Controller;
using System;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_CustomersOptionsForm : Form
    {
        public Frm_CustomersOptionsForm()
        {
            InitializeComponent();
        }

        private void BtnCustomersUpdateInvoices_Click(object sender, EventArgs e)
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

        private void BtnCustomersAddNewInvoices_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                Frm_CustomersAddNewInvoices.Invoice_NumberToGetData = "AddNewInvoices";
                Frm_CustomersAddNewInvoices.Is_Edit_Invoices_FormOpen = false;
                chelp.ShowForm(new Frm_CustomersAddNewInvoices());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnProducts_Click(object sender, EventArgs e)
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
