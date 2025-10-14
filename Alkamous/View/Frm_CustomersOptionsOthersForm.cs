using Alkamous.Controller;
using System;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_CustomersOptionsOthersForm : Form
    {
        public Frm_CustomersOptionsOthersForm()
        {
            InitializeComponent();
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

        private void BtnTerms_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_TermsAddDeleteUpdate());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBanks_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_BanksAccountAddUpdateDelete());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnImportExport_Click(object sender, EventArgs e)
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

        private void BtnProductsgroup_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_Products_group());
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
