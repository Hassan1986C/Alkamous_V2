namespace Alkamous.View
{
    partial class Frm_UserForgetPassword
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBoxSaveNewPassword = new System.Windows.Forms.GroupBox();
            this.LbPassLevel = new System.Windows.Forms.Label();
            this.BtnSavePassword = new System.Windows.Forms.Button();
            this.txtNewPass = new System.Windows.Forms.TextBox();
            this.txtConformpass = new System.Windows.Forms.TextBox();
            this.BtnShowPassword = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnConformCode = new System.Windows.Forms.Button();
            this.groupBoxSendCode = new System.Windows.Forms.GroupBox();
            this.WaitingLabel = new System.Windows.Forms.Label();
            this.BtnSendCode = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TxtUserName = new System.Windows.Forms.TextBox();
            this.TxtEmail = new System.Windows.Forms.TextBox();
            this.TxtConformCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxSaveNewPassword.SuspendLayout();
            this.groupBoxSendCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBoxSaveNewPassword);
            this.groupBox1.Controls.Add(this.BtnConformCode);
            this.groupBox1.Controls.Add(this.groupBoxSendCode);
            this.groupBox1.Controls.Add(this.TxtConformCode);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(38, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1043, 639);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // groupBoxSaveNewPassword
            // 
            this.groupBoxSaveNewPassword.Controls.Add(this.LbPassLevel);
            this.groupBoxSaveNewPassword.Controls.Add(this.BtnSavePassword);
            this.groupBoxSaveNewPassword.Controls.Add(this.txtNewPass);
            this.groupBoxSaveNewPassword.Controls.Add(this.txtConformpass);
            this.groupBoxSaveNewPassword.Controls.Add(this.BtnShowPassword);
            this.groupBoxSaveNewPassword.Controls.Add(this.label2);
            this.groupBoxSaveNewPassword.Controls.Add(this.label3);
            this.groupBoxSaveNewPassword.Enabled = false;
            this.groupBoxSaveNewPassword.Location = new System.Drawing.Point(19, 276);
            this.groupBoxSaveNewPassword.Name = "groupBoxSaveNewPassword";
            this.groupBoxSaveNewPassword.Size = new System.Drawing.Size(965, 335);
            this.groupBoxSaveNewPassword.TabIndex = 60;
            this.groupBoxSaveNewPassword.TabStop = false;
            // 
            // LbPassLevel
            // 
            this.LbPassLevel.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbPassLevel.Location = new System.Drawing.Point(175, 28);
            this.LbPassLevel.Name = "LbPassLevel";
            this.LbPassLevel.Size = new System.Drawing.Size(563, 55);
            this.LbPassLevel.TabIndex = 10;
            this.LbPassLevel.Text = "Password Level";
            this.LbPassLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LbPassLevel.Visible = false;
            // 
            // BtnSavePassword
            // 
            this.BtnSavePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(178)))));
            this.BtnSavePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSavePassword.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnSavePassword.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnSavePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSavePassword.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSavePassword.ForeColor = System.Drawing.Color.White;
            this.BtnSavePassword.Location = new System.Drawing.Point(175, 249);
            this.BtnSavePassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSavePassword.Name = "BtnSavePassword";
            this.BtnSavePassword.Size = new System.Drawing.Size(566, 72);
            this.BtnSavePassword.TabIndex = 9;
            this.BtnSavePassword.Text = "Save Changes";
            this.BtnSavePassword.UseVisualStyleBackColor = false;
            this.BtnSavePassword.Click += new System.EventHandler(this.BtnSavePassword_Click);
            // 
            // txtNewPass
            // 
            this.txtNewPass.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewPass.Location = new System.Drawing.Point(175, 122);
            this.txtNewPass.MaxLength = 20;
            this.txtNewPass.Name = "txtNewPass";
            this.txtNewPass.Size = new System.Drawing.Size(566, 40);
            this.txtNewPass.TabIndex = 6;
            this.txtNewPass.UseSystemPasswordChar = true;
            this.txtNewPass.TextChanged += new System.EventHandler(this.txtNewPass_TextChanged);
            // 
            // txtConformpass
            // 
            this.txtConformpass.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConformpass.Location = new System.Drawing.Point(175, 203);
            this.txtConformpass.MaxLength = 20;
            this.txtConformpass.Name = "txtConformpass";
            this.txtConformpass.Size = new System.Drawing.Size(566, 40);
            this.txtConformpass.TabIndex = 7;
            this.txtConformpass.UseSystemPasswordChar = true;
            // 
            // BtnShowPassword
            // 
            this.BtnShowPassword.AutoSize = true;
            this.BtnShowPassword.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnShowPassword.Location = new System.Drawing.Point(559, 89);
            this.BtnShowPassword.Name = "BtnShowPassword";
            this.BtnShowPassword.Size = new System.Drawing.Size(179, 27);
            this.BtnShowPassword.TabIndex = 14;
            this.BtnShowPassword.Text = "Show Password";
            this.BtnShowPassword.UseVisualStyleBackColor = true;
            this.BtnShowPassword.CheckedChanged += new System.EventHandler(this.BtnShowPassword_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(178, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 27);
            this.label2.TabIndex = 12;
            this.label2.Text = "New Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(178, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 27);
            this.label3.TabIndex = 13;
            this.label3.Text = "Confirm New Password";
            // 
            // BtnConformCode
            // 
            this.BtnConformCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(178)))));
            this.BtnConformCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnConformCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnConformCode.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnConformCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnConformCode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConformCode.ForeColor = System.Drawing.Color.White;
            this.BtnConformCode.Location = new System.Drawing.Point(686, 196);
            this.BtnConformCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnConformCode.Name = "BtnConformCode";
            this.BtnConformCode.Size = new System.Drawing.Size(280, 62);
            this.BtnConformCode.TabIndex = 59;
            this.BtnConformCode.Text = "conform code";
            this.BtnConformCode.UseVisualStyleBackColor = false;
            this.BtnConformCode.Click += new System.EventHandler(this.BtnConformCode_Click);
            // 
            // groupBoxSendCode
            // 
            this.groupBoxSendCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBoxSendCode.Controls.Add(this.WaitingLabel);
            this.groupBoxSendCode.Controls.Add(this.BtnSendCode);
            this.groupBoxSendCode.Controls.Add(this.label4);
            this.groupBoxSendCode.Controls.Add(this.label5);
            this.groupBoxSendCode.Controls.Add(this.TxtUserName);
            this.groupBoxSendCode.Controls.Add(this.TxtEmail);
            this.groupBoxSendCode.Location = new System.Drawing.Point(48, 23);
            this.groupBoxSendCode.Name = "groupBoxSendCode";
            this.groupBoxSendCode.Size = new System.Drawing.Size(936, 167);
            this.groupBoxSendCode.TabIndex = 46;
            this.groupBoxSendCode.TabStop = false;
            // 
            // WaitingLabel
            // 
            this.WaitingLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.WaitingLabel.Font = new System.Drawing.Font("Arial", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.WaitingLabel.ForeColor = System.Drawing.Color.Red;
            this.WaitingLabel.Location = new System.Drawing.Point(7, 12);
            this.WaitingLabel.Name = "WaitingLabel";
            this.WaitingLabel.Size = new System.Drawing.Size(922, 150);
            this.WaitingLabel.TabIndex = 59;
            this.WaitingLabel.Text = "Please Wait until send the OTP(code) to your email ...";
            this.WaitingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WaitingLabel.Visible = false;
            // 
            // BtnSendCode
            // 
            this.BtnSendCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(178)))));
            this.BtnSendCode.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSendCode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnSendCode.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnSendCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSendCode.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSendCode.ForeColor = System.Drawing.Color.White;
            this.BtnSendCode.Location = new System.Drawing.Point(638, 18);
            this.BtnSendCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnSendCode.Name = "BtnSendCode";
            this.BtnSendCode.Size = new System.Drawing.Size(280, 127);
            this.BtnSendCode.TabIndex = 47;
            this.BtnSendCode.Text = "Send the Code";
            this.BtnSendCode.UseVisualStyleBackColor = false;
            this.BtnSendCode.Click += new System.EventHandler(this.BtnSendCode_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 23);
            this.label4.TabIndex = 58;
            this.label4.Text = "User Email";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 23);
            this.label5.TabIndex = 56;
            this.label5.Text = "UserName";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtUserName.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserName.Location = new System.Drawing.Point(123, 18);
            this.TxtUserName.Multiline = true;
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(499, 59);
            this.TxtUserName.TabIndex = 1;
            this.TxtUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtEmail
            // 
            this.TxtEmail.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtEmail.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtEmail.Location = new System.Drawing.Point(123, 83);
            this.TxtEmail.Multiline = true;
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(499, 62);
            this.TxtEmail.TabIndex = 2;
            this.TxtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TxtConformCode
            // 
            this.TxtConformCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtConformCode.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtConformCode.Location = new System.Drawing.Point(116, 196);
            this.TxtConformCode.Multiline = true;
            this.TxtConformCode.Name = "TxtConformCode";
            this.TxtConformCode.Size = new System.Drawing.Size(554, 62);
            this.TxtConformCode.TabIndex = 57;
            this.TxtConformCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(57, 216);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 23);
            this.label7.TabIndex = 55;
            this.label7.Text = "Code";
            // 
            // Frm_UserForgetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1153, 759);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_UserForgetPassword";
            this.Text = "Frm_UserForgetPassword";            
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxSaveNewPassword.ResumeLayout(false);
            this.groupBoxSaveNewPassword.PerformLayout();
            this.groupBoxSendCode.ResumeLayout(false);
            this.groupBoxSendCode.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox BtnShowPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label LbPassLevel;
        private System.Windows.Forms.Button BtnSavePassword;
        private System.Windows.Forms.TextBox txtConformpass;
        private System.Windows.Forms.TextBox txtNewPass;
        private System.Windows.Forms.GroupBox groupBoxSendCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TxtConformCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TxtUserName;
        private System.Windows.Forms.TextBox TxtEmail;
        private System.Windows.Forms.Button BtnSendCode;
        private System.Windows.Forms.Button BtnConformCode;
        private System.Windows.Forms.GroupBox groupBoxSaveNewPassword;
        private System.Windows.Forms.Label WaitingLabel;
    }
}