
using Alkamous.Controller;
using System;
using System.Reflection;
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

     

        private void BtnForgetPassword_Click(object sender, EventArgs e)
        {
            Chelp chelp = new Chelp();
            chelp.ShowForm(new Frm_UserForgetPassword());
        }

        private void BtnUpdateVersion_Click(object sender, EventArgs e)
        {
            // Version version = Assembly.GetEntryAssembly().GetName().Version;
            string version = VersionLabel;


            //MessageBox.Show($"latest update - No Update Available Yet  {Environment.NewLine}" +
            //    $"{version}", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            MessageBox.Show($"latest update - No Update Available Yet", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        public string VersionLabel
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return $"Product Name: {Assembly.GetEntryAssembly().GetName().Name},{Environment.NewLine}" +
                           $"Version: {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return $"Product Name: {Assembly.GetEntryAssembly().GetName().Name},{Environment.NewLine} " +
                           $"Version: {ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision}";
                }
            }
        }
    }
}
