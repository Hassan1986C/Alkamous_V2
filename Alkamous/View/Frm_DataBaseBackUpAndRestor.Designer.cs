namespace Alkamous.View
{
    partial class Frm_DataBaseBackUpAndRestor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_DataBaseBackUpAndRestor));
            this.label1 = new System.Windows.Forms.Label();
            this.BtnBrows = new System.Windows.Forms.Button();
            this.txtbrows = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnRestorDataBase = new System.Windows.Forms.CheckBox();
            this.BtnBackUpDataBase = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.BtnResetToDefault = new System.Windows.Forms.Button();
            this.BtnSaveConfigurationAndProcess = new System.Windows.Forms.Button();
            this.LbWaitingProcessing = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 31.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(124, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(804, 64);
            this.label1.TabIndex = 1;
            this.label1.Text = "BackUp And Restor DataBase";
            // 
            // BtnBrows
            // 
            this.BtnBrows.BackColor = System.Drawing.Color.DarkRed;
            this.BtnBrows.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBrows.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnBrows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrows.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBrows.ForeColor = System.Drawing.Color.White;
            this.BtnBrows.Location = new System.Drawing.Point(34, 222);
            this.BtnBrows.Name = "BtnBrows";
            this.BtnBrows.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.BtnBrows.Size = new System.Drawing.Size(48, 34);
            this.BtnBrows.TabIndex = 65;
            this.BtnBrows.Text = "...";
            this.BtnBrows.UseVisualStyleBackColor = false;
            this.BtnBrows.Click += new System.EventHandler(this.BtnBrows_Click);
            // 
            // txtbrows
            // 
            this.txtbrows.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbrows.Location = new System.Drawing.Point(88, 222);
            this.txtbrows.Name = "txtbrows";
            this.txtbrows.ReadOnly = true;
            this.txtbrows.Size = new System.Drawing.Size(680, 30);
            this.txtbrows.TabIndex = 63;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.picLoading);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.BtnRestorDataBase);
            this.groupBox3.Controls.Add(this.BtnBrows);
            this.groupBox3.Controls.Add(this.BtnBackUpDataBase);
            this.groupBox3.Controls.Add(this.txtbrows);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox3.Location = new System.Drawing.Point(135, 175);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox3.Size = new System.Drawing.Size(796, 295);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.ForestGreen;
            this.label5.Location = new System.Drawing.Point(138, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(421, 27);
            this.label5.TabIndex = 54;
            this.label5.Text = "select one of the operations below :-";
            // 
            // BtnRestorDataBase
            // 
            this.BtnRestorDataBase.AutoSize = true;
            this.BtnRestorDataBase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnRestorDataBase.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRestorDataBase.Location = new System.Drawing.Point(43, 142);
            this.BtnRestorDataBase.Name = "BtnRestorDataBase";
            this.BtnRestorDataBase.Size = new System.Drawing.Size(190, 27);
            this.BtnRestorDataBase.TabIndex = 28;
            this.BtnRestorDataBase.Text = "Restor DataBase";
            this.BtnRestorDataBase.UseVisualStyleBackColor = true;
            this.BtnRestorDataBase.CheckedChanged += new System.EventHandler(this.BtnRestorDataBase_CheckedChanged);
            this.BtnRestorDataBase.MouseEnter += new System.EventHandler(this.ChangeColoreMouseEnter);
            this.BtnRestorDataBase.MouseLeave += new System.EventHandler(this.ChangeColoreMouseLeave);
            // 
            // BtnBackUpDataBase
            // 
            this.BtnBackUpDataBase.AutoSize = true;
            this.BtnBackUpDataBase.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBackUpDataBase.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBackUpDataBase.Location = new System.Drawing.Point(43, 88);
            this.BtnBackUpDataBase.Name = "BtnBackUpDataBase";
            this.BtnBackUpDataBase.Size = new System.Drawing.Size(207, 27);
            this.BtnBackUpDataBase.TabIndex = 27;
            this.BtnBackUpDataBase.Text = "BackUp DataBase";
            this.BtnBackUpDataBase.UseVisualStyleBackColor = true;
            this.BtnBackUpDataBase.CheckedChanged += new System.EventHandler(this.BtnBackUpDataBase_CheckedChanged);
            this.BtnBackUpDataBase.MouseEnter += new System.EventHandler(this.ChangeColoreMouseEnter);
            this.BtnBackUpDataBase.MouseLeave += new System.EventHandler(this.ChangeColoreMouseLeave);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.BtnResetToDefault);
            this.groupBox4.Controls.Add(this.BtnSaveConfigurationAndProcess);
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox4.Location = new System.Drawing.Point(135, 551);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.groupBox4.Size = new System.Drawing.Size(796, 111);
            this.groupBox4.TabIndex = 68;
            this.groupBox4.TabStop = false;
            // 
            // BtnResetToDefault
            // 
            this.BtnResetToDefault.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnResetToDefault.BackColor = System.Drawing.Color.Green;
            this.BtnResetToDefault.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnResetToDefault.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnResetToDefault.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnResetToDefault.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnResetToDefault.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnResetToDefault.ForeColor = System.Drawing.Color.White;
            this.BtnResetToDefault.Location = new System.Drawing.Point(573, 34);
            this.BtnResetToDefault.Name = "BtnResetToDefault";
            this.BtnResetToDefault.Size = new System.Drawing.Size(201, 62);
            this.BtnResetToDefault.TabIndex = 46;
            this.BtnResetToDefault.Text = "Reset To Default";
            this.BtnResetToDefault.UseVisualStyleBackColor = false;
            this.BtnResetToDefault.Click += new System.EventHandler(this.BtnResetToDefault_Click);
            // 
            // BtnSaveConfigurationAndProcess
            // 
            this.BtnSaveConfigurationAndProcess.BackColor = System.Drawing.Color.Navy;
            this.BtnSaveConfigurationAndProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSaveConfigurationAndProcess.Enabled = false;
            this.BtnSaveConfigurationAndProcess.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnSaveConfigurationAndProcess.FlatAppearance.BorderSize = 0;
            this.BtnSaveConfigurationAndProcess.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnSaveConfigurationAndProcess.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSaveConfigurationAndProcess.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSaveConfigurationAndProcess.ForeColor = System.Drawing.Color.White;
            this.BtnSaveConfigurationAndProcess.Location = new System.Drawing.Point(43, 34);
            this.BtnSaveConfigurationAndProcess.Name = "BtnSaveConfigurationAndProcess";
            this.BtnSaveConfigurationAndProcess.Size = new System.Drawing.Size(294, 62);
            this.BtnSaveConfigurationAndProcess.TabIndex = 7;
            this.BtnSaveConfigurationAndProcess.Text = "Start Processing ...";
            this.BtnSaveConfigurationAndProcess.UseVisualStyleBackColor = false;
            this.BtnSaveConfigurationAndProcess.Click += new System.EventHandler(this.BtnSaveConfigurationAndProcess_Click);
            // 
            // LbWaitingProcessing
            // 
            this.LbWaitingProcessing.AutoSize = true;
            this.LbWaitingProcessing.Font = new System.Drawing.Font("Century Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbWaitingProcessing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LbWaitingProcessing.Location = new System.Drawing.Point(206, 497);
            this.LbWaitingProcessing.Name = "LbWaitingProcessing";
            this.LbWaitingProcessing.Size = new System.Drawing.Size(606, 39);
            this.LbWaitingProcessing.TabIndex = 66;
            this.LbWaitingProcessing.Text = "Please wait until the processing finish";
            this.LbWaitingProcessing.Visible = false;
            // 
            // picLoading
            // 
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(328, 76);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(105, 93);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLoading.TabIndex = 66;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // Frm_DataBaseBackUpAndRestor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 806);
            this.Controls.Add(this.LbWaitingProcessing);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_DataBaseBackUpAndRestor";
            this.Text = "Frm_DataBaseRestor";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnBrows;
        private System.Windows.Forms.TextBox txtbrows;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox BtnRestorDataBase;
        private System.Windows.Forms.CheckBox BtnBackUpDataBase;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button BtnResetToDefault;
        private System.Windows.Forms.Button BtnSaveConfigurationAndProcess;
        private System.Windows.Forms.Label LbWaitingProcessing;
        private System.Windows.Forms.PictureBox picLoading;
    }
}