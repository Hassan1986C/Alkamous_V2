using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Alkamous.Controller;

namespace Alkamous.View
{
    public partial class Frm_DataBaseBackUpAndRestor : Form
    {
        readonly ClsDataBaseBackUpAndRestor DataBaseBackUpAndRestor = new ClsDataBaseBackUpAndRestor();
        public Frm_DataBaseBackUpAndRestor()
        {
            InitializeComponent();
        }

        private void ResetToDefault()
        {
            txtbrows.Text = "";

            BtnBackUpDataBase.Checked = false;
            BtnRestorDataBase.Checked = false;

            BtnBackUpDataBase.Enabled = true;
            BtnRestorDataBase.Enabled = true;

            BtnSaveConfigurationAndProcess.Enabled = false;

        }

        private void ChangeColoreMouseEnter(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.ForeColor = Color.Red;
            }
        }

        private void ChangeColoreMouseLeave(object sender, EventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                checkBox.ForeColor = Color.Black;
            }
        }

        private void BtnResetToDefault_Click(object sender, EventArgs e)
        {
            ResetToDefault();
        }

        private async void BtnSaveConfigurationAndProcess_Click(object sender, EventArgs e)
        {

            if (BtnBackUpDataBase.Checked)
            {
                if (MessageBox.Show($"Backup of the database will be created.\r\n Please ensure that no critical operations are being performed at this time. ?", "Message of Backup", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Initializeprocessing(true);

                    string filename = txtbrows.Text + "\\" + Properties.Settings.Default.Database + DateTime.Now.ToString(" yyyy-MM-dd --HH-mm-ss") + ".bak";

                    var result = await DataBaseBackUpAndRestor.DataBaseBackUp(filename);
                    if (result)
                    {
                        MessageBox.Show("Backup successful " + filename, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Backup failed " + filename, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Initializeprocessing(false);

                }
            }

            if (BtnRestorDataBase.Checked)
            {
                if (MessageBox.Show($"Warning: If the Backup copy is not updated to the latest version,\r\n any data entered after the backup process may be lost during restoration.\r\n Please ensure that you have the most recent backup before proceeding.", "Message of Restoration", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Initializeprocessing(true);

                    string filename = txtbrows.Text;
                    var result = await DataBaseBackUpAndRestor.DataBaseRestore(filename);
                    if (result)
                    {
                        MessageBox.Show("Restoration successful " + filename, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Restoration failed " + filename, "Info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Initializeprocessing(false);

                }
            }


        }

        private void Initializeprocessing(bool IsProcessStarted)
        {
            BtnSaveConfigurationAndProcess.Enabled = false;
            picLoading.Visible = IsProcessStarted;
            LbWaitingProcessing.Visible = IsProcessStarted;
            Cursor = IsProcessStarted ? Cursors.WaitCursor : Cursors.Default;
            if (!IsProcessStarted) ResetToDefault();
        }

        private void BtnBrows_Click(object sender, EventArgs e)
        {
            if ((BtnBackUpDataBase.Checked == false) & (BtnRestorDataBase.Checked == false))
            {
                MessageBox.Show("please select one of the options above");
                return;
            }
            if (BtnBackUpDataBase.Checked)
            {
                using (var folderBrowser = new FolderBrowserDialog
                {
                    Description = "Choose the path to export the BackUp File"
                })
                {
                    if (folderBrowser.ShowDialog() == DialogResult.OK)
                    {
                        string rootDirectory = Path.GetPathRoot(folderBrowser.SelectedPath);
                        if (string.Equals(rootDirectory, @"C:\", StringComparison.OrdinalIgnoreCase))
                        {
                            MessageBox.Show("Please select another directory not located on the C drive.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        };

                        txtbrows.Text = folderBrowser.SelectedPath;
                        BtnSaveConfigurationAndProcess.Enabled = true;
                    }
                }
            }
            else
            {
                using (var _openFileDialog = new OpenFileDialog
                {
                    Filter = "files (*.dat)|*.bak",
                    Title = "Select Restore file"
                })
                {
                    if (_openFileDialog.ShowDialog() != DialogResult.OK)
                        return;

                    string filePath = _openFileDialog.FileName;
                    txtbrows.Text = filePath;
                    BtnSaveConfigurationAndProcess.Enabled = true;
                }
            }
        }
       
        private void BtnBackUpDataBase_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnBackUpDataBase.Checked)
            {
                CheckedIsSelected(true);
            }
        }

        private void BtnRestorDataBase_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnRestorDataBase.Checked)
            {
                CheckedIsSelected(false);
            }
        }
        private void CheckedIsSelected(bool IsBackUpDataBase)
        {
            txtbrows.Text = "";

            BtnBackUpDataBase.Checked = IsBackUpDataBase;
            BtnRestorDataBase.Checked = !(IsBackUpDataBase);

            BtnBackUpDataBase.Enabled = !(IsBackUpDataBase);
            BtnRestorDataBase.Enabled = (IsBackUpDataBase);

            BtnSaveConfigurationAndProcess.Enabled = false;
        }
    }
}
