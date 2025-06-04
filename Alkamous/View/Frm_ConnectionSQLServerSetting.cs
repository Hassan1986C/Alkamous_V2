using Alkamous.Controller;
using Alkamous.Model;
using System;
using System.Windows.Forms;

namespace Alkamous.View
{
    public partial class Frm_ConnectionSQLServerSetting : Form
    {
        private readonly DataAccessLayer DLA = new DataAccessLayer();
        public Frm_ConnectionSQLServerSetting()
        {
            InitializeComponent();
        }

        private void BtnSaveConfiguration_Click(object sender, EventArgs e)
        {
            //Properties.Settings.Default.ServerName = txtServerName.Text;
            //Properties.Settings.Default.Database = txtDataBase.Text;
            //Properties.Settings.Default.Userid = txtUserName.Text;
            //Properties.Settings.Default.password = txtPassword.Text;
            //Properties.Settings.Default.ConnectTimeout = txtConnectTimeout.Text;
            //Properties.Settings.Default.Save();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                if (DLA.Is_SQLServer_connection_setting_successful(txtServerName.Text, txtDataBase.Text, txtPassword.Text, txtUserName.Text, txtConnectTimeout.Text))
                {
                    if (MessageBox.Show("Do you want to Save Configuration Setting", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Properties.Settings.Default.ServerName = txtServerName.Text;
                        Properties.Settings.Default.Database = txtDataBase.Text;
                        Properties.Settings.Default.Userid = txtUserName.Text;
                        Properties.Settings.Default.password = txtPassword.Text;
                        Properties.Settings.Default.ConnectTimeout = txtConnectTimeout.Text;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("The data has been saved successfully. The program will be restarted");
                        Application.Restart();
                    }
                }
                else
                {
                    MessageBox.Show("failed to connect to the database check the name of database", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                var Btn = sender as Button;
                Chelp.WriteErrorLog(Name + " => " + Btn.Name.ToString() + " => " + ex.Message);
                MessageBox.Show(ex.Message);
            }finally 
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void Frm_ConnectionSQLServerSetting_Load(object sender, EventArgs e)
        {
            txtServerName.Text = Properties.Settings.Default.ServerName;
            txtDataBase.Text = Properties.Settings.Default.Database;
            txtConnectTimeout.Text = Properties.Settings.Default.ConnectTimeout;
        }
    }
}
