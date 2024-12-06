using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_Main : Form
    {
        #region declare variable

        Chelp chelp = new Chelp();
        private static Frm_Main frmMain;

        public static Frm_Main FrmMain  // this methode to make a new form same as orgnal from
        {
            get { return frmMain; }
        }

        #endregion

        public Frm_Main()
        {
            InitializeComponent();
            frmMain = this;

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

        private void BtnNewQuotation_Click(object sender, EventArgs e)
        {
            try
            {
                chelp.ShowForm(new Frm_CustomersOptionsForm());
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnConnection_Click(object sender, EventArgs e)
        {
            try
            {
                chelp.ShowForm(new Frm_UsersOptionsForm());
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void AutoLogin()
        {
            // check if auto login checked
            Properties.Settings.Default.IsLogin = false;
            Properties.Settings.Default.UserName = "";
            Properties.Settings.Default.Save();

            foreach (Control btn in panelLesft.Controls)
            {
                if (btn is Button || btn is PictureBox)
                {
                    btn.Enabled = false;
                }
            }            
            BtnConnection.Enabled = true;
            
        }

        private void IsLogin()
        {
            AutoLogin();
            BtnLoginLogout.Text = "Login";
            chelp.ShowForm(new Frm_Login());
        }

        private void InitializeTimer()
        {
            var timer = new Timer
            {
                Interval = 1000,
                Enabled = true
            };
            timer.Tick += Timer_Tick;
            timer.Start();

            LbServerName.Text = Properties.Settings.Default.ServerName;
            LBData.Text = $"Date  : {DateTime.Now.ToString("dd/MM/yyyy")}";

        }

        private void EnableUserInterfaceandUserSession()
        {
            LBUserName.Text = string.Empty;

            if (Properties.Settings.Default.IsLogin)
            {
                foreach (Control btn in panelLesft.Controls)
                {
                    if (btn is Button)
                    {
                        btn.Enabled = true;
                    }
                }

                PicLogo.Enabled = true;
                LBUserName.Text = Properties.Settings.Default.UserName;
                BtnLoginLogout.Text = "Logout";

                var Myip = Dns.GetHostAddresses(Dns.GetHostName())
                    .Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
                Chelp.RegisterUsersActionLogs($"Auto Login ", $"Login By {Environment.MachineName} IPv4 Address: {Myip}");

                Chelp chelp = new Chelp();
                chelp.ShowForm(new Frm_WlcomeScreen());
            }
            else
            {
                IsLogin();

            }
        }
        private async void Frm_Main_Load(object sender, EventArgs e)
        {

            DataAccessLayer DLA = new DataAccessLayer();
            InitializeTimer();

            chelp.ShowForm(new FrmLoading());

            var IsconnectionSuccessful = await Task.Run(() => { return DLA.Is_SQLServer_connection_setting_successful(); });

            if (!IsconnectionSuccessful)
            {
                MessageBox.Show("failed to connect to the database check the name of database and the server", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

                chelp.ShowForm(new Frm_ConnectionSQLServerSetting());
            }
            else
            {
                EnableUserInterfaceandUserSession();

            }
        }


        private void Timer_Tick(object sender, EventArgs e)
        {
            LBTime.Text = $"Time :  {DateTime.Now.ToString("hh:mm:ss tt")}";

        }

        private void PicLogo_Click(object sender, EventArgs e)
        {

            try
            {
                if (LBUserName.Text != string.Empty)
                {

                    chelp.ShowForm(new Frm_WlcomeScreen());
                }
                else
                {
                    IsLogin();
                }

            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }



        private void Frm_Main_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (MessageBox.Show("Do you want to Exit ?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void BtnManage_Click(object sender, EventArgs e)
        {
            try
            {

                chelp.ShowForm(new Frm_CustomersOptionsOthersForm());
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnCustomerInfo_Click(object sender, EventArgs e)
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

        private void BtnLoginLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            IsLogin();
        }
    }
}
