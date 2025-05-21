using Alkamous.Controller;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Alkamous.Model;

namespace Alkamous.View
{
    public partial class Frm_UserChangePassword : Form
    {
        Model.CTB_Users MTB_Users = new Model.CTB_Users();
        ClsOperationsofUsers ClsOperationsofUsers = new ClsOperationsofUsers(new DataAccessLayer());

        public Frm_UserChangePassword()
        {
            InitializeComponent();
        }



        private (string Message, Color BackColor) CheckStrength(string password)
        {
            try
            {
                LbPassLevel.Visible = !string.IsNullOrEmpty(txtNewPass.Text.Trim());

                int score = 0;

                if (password.Length >= 5) score++;

                if (Regex.Match(password, "[0-9]").Success) score++;

                if (Regex.Match(password, "[A-Z]").Success) score++;

                if (Regex.Match(password, "[a-z]").Success) score++;

                if (Regex.Match(password, "[!,@,#,$,%,^,&,*,?,_,~,-,£,(,)]").Success) score++;

                switch (score)
                {
                    case var s when s >= 5:

                        return ("Very Password Strong", Color.LightGreen);

                    case 4:

                        return ("Password Strong", Color.LightGreen);

                    case 3:

                        return ("Password Medium", Color.LightYellow);

                    case 2:

                        return ("Password Weak", Color.LightYellow);
                    default:

                        return ("Very Password Weak", Color.Red);
                }
            }
            catch (Exception)
            {
                return ("", Color.White);
            }

        }

        private bool CheckOldpassword()
        {
            if (!(string.IsNullOrEmpty(txtOldPass.Text.Trim()) && string.IsNullOrEmpty(txtNewPass.Text.Trim())))
            {
                MTB_Users = ClsOperationsofUsers.Get_AllBySearch(Frm_Main.FrmMain.LBUserName.Text);

                if (MTB_Users != null)
                {
                    string result = ClsAESEncryption.AESDecrypt(MTB_Users.UserPassword, MTB_Users.UserAESKey, MTB_Users.UserAESIV);

                    return result.Equals(txtOldPass.Text.Trim());

                }
                else
                {
                    return false;

                }
            }
            return false;
        }


        private async void BtnSavePassword_Click(object sender, EventArgs e)
        {
           
            if (!CheckpasswordRequirement()) return;
                

            if (CheckOldpassword())
            {                        // To Change Password and Save in Properties
                bool MSresult = MessageBox.Show("Do you want to save a new password  ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                
                if (MSresult)
                {
                 
                    var (Password, UserKey, UserIV) = Chelp.EncryptedPassword(txtNewPass.Text.Trim());

                    MTB_Users.UserName = Frm_Main.FrmMain.LBUserName.Text;
                    MTB_Users.UserPassword = Password;
                    MTB_Users.UserAESKey = UserKey;
                    MTB_Users.UserAESIV = UserIV;
                   

                    if (await ClsOperationsofUsers.UpdateAsync(MTB_Users))
                    {
                        AutoLogin(BtnAutoLogin.Checked);
                        Chelp.RegisterUsersActionLogs("Change Password", "Change Password");
                        MessageBox.Show("Password changed successfully the application will restart to login with a new password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Application.Restart();
                    }
                    else
                    {
                        MessageBox.Show($"problem try later");
                    }

                }

            }
            else
            {
                MessageBox.Show("Your current password is incorrect", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtOldPass.Focus();
                txtOldPass.SelectAll();
            }


        }

        private void AutoLogin(bool IsAutoLogin)
        {
            // check if auto login checked
            Properties.Settings.Default.IsLogin = IsAutoLogin;
            Properties.Settings.Default.UserName = IsAutoLogin ? Frm_Main.FrmMain.LBUserName.Text : "";
            Properties.Settings.Default.Save();
        }

        private bool CheckpasswordRequirement()
        {
            // Helper function to show a message box and set focus
            void ShowMessageAndFocus(Control control, string message)
            {
                control.Focus();
                MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (string.IsNullOrWhiteSpace(txtOldPass.Text))
            {
                ShowMessageAndFocus(txtOldPass, "Insert the current password");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPass.Text))
            {
                ShowMessageAndFocus(txtNewPass, "Insert the new password");
                return false;
            }

            if (txtNewPass.TextLength < 4)
            {
                ShowMessageAndFocus(txtNewPass, "The new password is too short");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtConformpass.Text))
            {
                ShowMessageAndFocus(txtConformpass, "Please confirm the new password");
                return false;
            }

            if (txtNewPass.Text != txtConformpass.Text)
            {
                MessageBox.Show("Confirm password does not match", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }


        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {

            var (Massge, BackColor) = CheckStrength(txtNewPass.Text);
            LbPassLevel.Text = Massge;
            LbPassLevel.BackColor = BackColor;

        }

        private void BtnShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnShowPassword.Checked == true)
            {
                txtNewPass.UseSystemPasswordChar = false;
                txtConformpass.UseSystemPasswordChar = false;
            }
            else
            {
                txtNewPass.UseSystemPasswordChar = true;
                txtConformpass.UseSystemPasswordChar = true;

            }
        }

        
    }
}
