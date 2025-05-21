using Alkamous.Controller;
using Alkamous.Model;
using Microsoft.Office.Interop.Word;
using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_Login : Form
    {
        #region Declare variables
        Model.CTB_Users MTB_Users = new Model.CTB_Users();
        private ClsOperationsofUsers ClsOperationsofUsers = new ClsOperationsofUsers(new DataAccessLayer());
        #endregion

        public Frm_Login()
        {
            InitializeComponent();
        }

        #region Login Function 
        /// <summary>
        /// Login Function
        /// </summary>
        private void LOGIN(string UserName, string Password)
        {

            if (!((string.IsNullOrEmpty(UserName)) && (string.IsNullOrEmpty(Password))))
            {
                MTB_Users = ClsOperationsofUsers.Get_AllBySearch(UserName);
                if (MTB_Users != null)
                {
                    string result = ClsAESEncryption.AESDecrypt(MTB_Users.UserPassword, MTB_Users.UserAESKey, MTB_Users.UserAESIV);

                    if (result.Equals(Password))
                    {
                        foreach (Control btn in Frm_Main.FrmMain.panelLesft.Controls)
                        {
                            if (btn is Button || btn is PictureBox)
                            {
                                btn.Enabled = true;
                            }
                        }


                        if (BtnAutoLogin.Checked)
                        {
                            Properties.Settings.Default.IsLogin = true;
                            Properties.Settings.Default.UserName = UserName;
                            Properties.Settings.Default.Save();
                        }

                        Frm_Main.FrmMain.LBUserName.Text = UserName;
                        Frm_Main.FrmMain.BtnLoginLogout.Text = "Logout";

                        var Myip = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
                        Chelp.RegisterUsersActionLogs($"Login ", $"Login By {Environment.MachineName} IPv4 Address: {Myip}");
                        Chelp chelp = new Chelp();
                        chelp.ShowForm(new Frm_WlcomeScreen());

                        //  MessageBox.Show("ok Login IN");
                    }
                    else
                    {
                        MessageBox.Show("Check UserName Or PassWord Not Much");
                    }

                }
                else
                {
                    MessageBox.Show("Check UserName Or PassWord Not Much !!!");

                }
            }
        }
        #endregion

       

        #region BtnLogin to call Login Function
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            LOGIN(TxtUserName.Text.Trim(), TxtPassword.Text.Trim());
        }
        #endregion

        #region BtnShowPassword
        private void BtnShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnShowPassword.Checked == true)
            {
                TxtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                TxtPassword.UseSystemPasswordChar = true;
            }
        }
        #endregion

        #region BtnKeyDown to check textbox and call function Login
        private void BtnLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                if (string.IsNullOrEmpty(TxtUserName.Text.Trim()))
                {
                    TxtUserName.Focus();
                }
                else if (string.IsNullOrEmpty(TxtPassword.Text.Trim()))
                {
                    TxtPassword.Focus();
                }
                else
                {
                    LOGIN(TxtUserName.Text.Trim(), TxtPassword.Text.Trim());
                }
            }
        }
        #endregion

        private void BtnForgetPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Chelp chelp = new Chelp();
            chelp.ShowForm(new Frm_UserForgetPassword());

        }
    }
}
