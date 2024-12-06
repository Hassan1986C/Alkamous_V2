using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_UserAddUpdateDelete : Form
    {
        private CTB_Users MTB_Users = new CTB_Users();
        private readonly ClsOperationsofUsers ClsOperationsofUsers = new ClsOperationsofUsers();

        public Frm_UserAddUpdateDelete()
        {
            InitializeComponent();
        }

        private void Frm_UserAddUpdateDelete_Load(object sender, EventArgs e)
        {
            DGVUsers.Columns.Add("User_AutoNum", "ID");
            DGVUsers.Columns.Add("UserName", "UserName");
            DGVUsers.Columns.Add("UserEmail", "UserEmail");
            LoadData();

        }        

        public static bool IsValidEmail(string email)
        {
            string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(email);
        }


        private async void BtnAddNewUser_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValidEmail(TxtEmail.Text.Trim()))
                {
                    string passwordLoginAsAdmin = "Admin" + DateTime.Now.ToString("hhMMdd");

                    if (passwordLoginAsAdmin == txtLoginAsAdmin.Text.Trim())
                    {
                        var (Password, UserKey, UserIV) = Chelp.EncryptedPassword(TxtPassword.Text.Trim());

                        MTB_Users.UserName = TxtUserName.Text.Trim();
                        MTB_Users.UserPassword = Password;
                        MTB_Users.UserAESKey = UserKey;
                        MTB_Users.UserAESIV = UserIV;
                        MTB_Users.UserEmail = TxtEmail.Text.Trim();

                        if (await ClsOperationsofUsers.AddNewAsync(MTB_Users))
                        {
                            MessageBox.Show($"the UserName is = {MTB_Users.UserName} Add ");
                            TxtUserName.Text = string.Empty;
                            TxtPassword.Text = string.Empty;
                            txtLoginAsAdmin.Text = string.Empty;
                            TxtEmail.Text = string.Empty;
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show($"problem Try later");
                        }
                    }
                    else
                    {
                        MessageBox.Show("To Add User Need a Code", "Message ahmd");
                    }
                }
                else
                {
                    MessageBox.Show("check your email", "Message ahmd");
                }
            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }


        }

        private async void BtnDeleteUser_Click(object sender, EventArgs e)
        {

            try
            {
                string passwordLoginAsAdmin = "Admin" + DateTime.Now.ToString("hhMMdd");

                if (passwordLoginAsAdmin == txtLoginAsAdmin.Text.Trim())
                {
                    if (DGVUsers.RowCount > 0 && DGVUsers.CurrentRow != null)
                    {

                        if (MessageBox.Show("Are you sure you want to Delete  " + Environment.NewLine
                            + Environment.NewLine + "User    : " + DGVUsers.CurrentRow.Cells[1].Value.ToString()

                            , "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            Cursor.Current = Cursors.WaitCursor;

                            MTB_Users.UserName = DGVUsers.CurrentRow.Cells[1].Value.ToString();
                            bool Result =await ClsOperationsofUsers.DeleteAsync(MTB_Users.UserName);


                            if (Result)
                            {
                                Chelp.RegisterUsersActionLogs("Delete User", DGVUsers.CurrentRow.Cells[1].Value.ToString());
                                MessageBox.Show("Data Deleted Successfully ");
                                LoadData();

                            }
                            else
                            {
                                MessageBox.Show("Sorry, there is a mistake !!");
                            }
                            Cursor.Current = Cursors.Default;
                        }

                    }
                }
                else
                {
                    MessageBox.Show("To Delete User Need a Code", "Message ahmd");
                }

            }
            catch (Exception ex)
            {
                string MethodNames = System.Reflection.MethodBase.GetCurrentMethod().Name.ToString();
                Chelp.WriteErrorLog(Name + " => " + MethodNames + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData()
        {

            DGVUsers.Rows.Clear();
            var userList = ClsOperationsofUsers.Get_ALL();
            if (userList != null)
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    DGVUsers.Rows.Add(userList[i].User_AutoNum, userList[i].UserName, userList[i].UserEmail);
                }
            }

        }
    }
}
