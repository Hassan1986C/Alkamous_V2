using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_UserForgetPassword : Form
    {
        #region Declare varabls

        private string OTPUserCode = "";
        CTB_Users MTB_Users = new CTB_Users();
        ClsOperationsofUsers ClsOperationsofUsers = new ClsOperationsofUsers(new DataAccessLayer());
        private int CountButton;
        private Timer timer = new Timer
        {
            Interval = 1000,
            Enabled = true
        };

        #endregion
        public Frm_UserForgetPassword()
        {
            InitializeComponent();
        }

        private bool IsUserNameAndEmailValid(string userName)
        {
            // التحقق من أن اسم المستخدم والبريد الإلكتروني غير فارغين
            if (string.IsNullOrEmpty(TxtUserName?.Text.Trim()) || string.IsNullOrEmpty(TxtEmail?.Text.Trim()))
                return false;

            // جلب معلومات المستخدم بناءً على اسم المستخدم المدخل
            MTB_Users = ClsOperationsofUsers.Get_AllBySearch(userName);

            // التحقق من أن MTB_Users ليست null قبل مقارنة البريد الإلكتروني
            if (MTB_Users != null && MTB_Users.UserEmail.Trim() == TxtEmail.Text.Trim())
                return true;

            return false;
        }

        private bool CheckpasswordRequirement()
        {
            // Helper function to show a message box and set focus
            void ShowMessageAndFocus(Control control, string message)
            {
                control.Focus();
                MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (string.IsNullOrWhiteSpace(TxtUserName.Text))
            {
                ShowMessageAndFocus(TxtUserName, "Insert the current UserName");
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

        public string generateID()
        {
            // use the first 16 of 32 degit
            return Guid.NewGuid().ToString("N").Substring(0, 16);
        }


        private async void BtnSendCode_Click(object sender, EventArgs e)
        {
            try
            {
                // عرض رسالة انتظار وتعطيل الزر


                if (IsUserNameAndEmailValid(TxtUserName.Text.Trim()))
                {
                    BtnSendCode.Enabled = false;
                    WaitingLabel.Visible = true;
                    Cursor = Cursors.WaitCursor;

                    OTPUserCode = generateID();

                    // تحقق من النتيجة بعد إرسال البريد
                    bool result = await ClsSendUserEmail.SendEmailAsync(TxtUserName.Text.Trim(), TxtEmail.Text.Trim(), OTPUserCode);

                    if (result)
                    {
                        CountButton = 60;
                        timer.Tick += Timer_Tick;
                        MessageBox.Show("Email sent successfully! Please note that this email is for one-time use only and should not be shared with anyone.",
                            "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while sending email", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("please check username and Email", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
            finally
            {
                // إخفاء رسالة الانتظار وإعادة تفعيل الزر

                Cursor = Cursors.Default;

                WaitingLabel.Visible = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CountButton--;
            if (CountButton == 0)
            {
                BtnSendCode.Text = "Send the Code ";
                BtnSendCode.Enabled = true;
                timer.Tick -= Timer_Tick;
            }
            else
            {
                BtnSendCode.Text = "Send the Code " + CountButton.ToString();
            }

        }

        private void BtnConformCode_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(TxtConformCode.Text.Trim())) return;

            if (OTPUserCode == TxtConformCode.Text.Trim())
            {
                WaitingLabel.Visible = true;
                WaitingLabel.Text = "Congratulations, you can now create a new password. \r\n please don't forget it again!!";
                groupBoxSaveNewPassword.Enabled = true;
            }
        }

        private async void BtnSavePassword_Click(object sender, EventArgs e)
        {
            if (CheckpasswordRequirement())
            {
                BtnSavePassword.Enabled = false;
                var (Password, UserKey, UserIV) = Chelp.EncryptedPassword(txtNewPass.Text.Trim());

                MTB_Users.UserName = TxtUserName.Text.Trim();
                MTB_Users.UserPassword = Password;
                MTB_Users.UserAESKey = UserKey;
                MTB_Users.UserAESIV = UserIV;


                if (await ClsOperationsofUsers.UpdateAsync(MTB_Users))
                {
                    var Myip = Dns.GetHostAddresses(Dns.GetHostName()).Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).FirstOrDefault();
                    Chelp.RegisterUsersActionLogs("Forget Password", $"Change new Password By {Environment.MachineName} IPv4 Address: {Myip}");
                    MessageBox.Show("Password changed successfully the application will restart to login with a new password", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Restart();
                }
                else
                {
                    MessageBox.Show($"problem try later");
                }
            }
            BtnSavePassword.Enabled = true;
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
        private void txtNewPass_TextChanged(object sender, EventArgs e)
        {
            var (Massge, BackColor) = CheckStrength(txtNewPass.Text);
            LbPassLevel.Text = Massge;
            LbPassLevel.BackColor = BackColor;
        }

        
    }
}
