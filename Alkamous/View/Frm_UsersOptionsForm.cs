
using Alkamous.Controller;
using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_UsersOptionsForm : Form
    {
        public Frm_UsersOptionsForm()
        {
            InitializeComponent();

        }

        private void BtnConnectionSQLServerSetting_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();

                chelp.ShowForm(new Frm_ConnectionSQLServerSetting());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnUserAddUpdateDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_UserAddUpdateDelete());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUserChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_UserChangePassword());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUsersLog_Click(object sender, EventArgs e)
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

        private void BtnExportPathWord_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_UserExportPathMsWord());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }

        }

        private void BtnFixWordTempletefiles_Click(object sender, EventArgs e)
        {
            Chelp.FixWordTempletefiles();           
        }

       
        private void BtnBackUpAndRestorData_Click(object sender, EventArgs e)
        {
            try
            {
                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_DataBaseBackUpAndRestor());
            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }





        private void BtnArrangeProducts_Click(object sender, EventArgs e)
        {
            MessageBox.Show("No thing Yet", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void BtnForgetPassword_Click(object sender, EventArgs e)
        {
            Chelp chelp = new Chelp();
            chelp.ShowForm(new Frm_UserForgetPassword());
        }
    }
}
