namespace Alkamous.View
{
    partial class Frm_Products_group_AddDeleteUpdate
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtAutoNumber = new System.Windows.Forms.TextBox();
            this.BtnDelete = new System.Windows.Forms.Button();
            this.TxtproductGroupName = new System.Windows.Forms.TextBox();
            this.BtnCancelEdit = new System.Windows.Forms.Button();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.BtnEdit = new System.Windows.Forms.Button();
            this.LbCount = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.DGVProducts = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.TxtAutoNumber);
            this.groupBox1.Controls.Add(this.BtnDelete);
            this.groupBox1.Controls.Add(this.TxtproductGroupName);
            this.groupBox1.Controls.Add(this.BtnCancelEdit);
            this.groupBox1.Controls.Add(this.BtnAdd);
            this.groupBox1.Controls.Add(this.BtnEdit);
            this.groupBox1.Location = new System.Drawing.Point(45, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(900, 171);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            // 
            // TxtAutoNumber
            // 
            this.TxtAutoNumber.Location = new System.Drawing.Point(23, 73);
            this.TxtAutoNumber.Name = "TxtAutoNumber";
            this.TxtAutoNumber.Size = new System.Drawing.Size(161, 24);
            this.TxtAutoNumber.TabIndex = 56;
            this.TxtAutoNumber.Visible = false;
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.DarkRed;
            this.BtnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDelete.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.ForeColor = System.Drawing.Color.White;
            this.BtnDelete.Location = new System.Drawing.Point(479, 109);
            this.BtnDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.Size = new System.Drawing.Size(214, 43);
            this.BtnDelete.TabIndex = 53;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.UseVisualStyleBackColor = false;
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // TxtproductGroupName
            // 
            this.TxtproductGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtproductGroupName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtproductGroupName.Location = new System.Drawing.Point(7, 17);
            this.TxtproductGroupName.Margin = new System.Windows.Forms.Padding(10);
            this.TxtproductGroupName.Multiline = true;
            this.TxtproductGroupName.Name = "TxtproductGroupName";
            this.TxtproductGroupName.Size = new System.Drawing.Size(869, 43);
            this.TxtproductGroupName.TabIndex = 0;
            // 
            // BtnCancelEdit
            // 
            this.BtnCancelEdit.BackColor = System.Drawing.Color.Green;
            this.BtnCancelEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnCancelEdit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnCancelEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelEdit.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelEdit.ForeColor = System.Drawing.Color.White;
            this.BtnCancelEdit.Location = new System.Drawing.Point(479, 109);
            this.BtnCancelEdit.Name = "BtnCancelEdit";
            this.BtnCancelEdit.Size = new System.Drawing.Size(214, 43);
            this.BtnCancelEdit.TabIndex = 52;
            this.BtnCancelEdit.Text = "Cancel Edit";
            this.BtnCancelEdit.UseVisualStyleBackColor = false;
            this.BtnCancelEdit.Visible = false;
            this.BtnCancelEdit.Click += new System.EventHandler(this.BtnCancelEdit_Click);
            // 
            // BtnAdd
            // 
            this.BtnAdd.BackColor = System.Drawing.Color.MidnightBlue;
            this.BtnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAdd.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAdd.ForeColor = System.Drawing.Color.White;
            this.BtnAdd.Location = new System.Drawing.Point(23, 111);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(214, 43);
            this.BtnAdd.TabIndex = 50;
            this.BtnAdd.Text = "Add";
            this.BtnAdd.UseVisualStyleBackColor = false;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(77)))), ((int)(((byte)(178)))));
            this.BtnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnEdit.Enabled = false;
            this.BtnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnEdit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.MenuHighlight;
            this.BtnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnEdit.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnEdit.ForeColor = System.Drawing.Color.White;
            this.BtnEdit.Location = new System.Drawing.Point(251, 111);
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(214, 43);
            this.BtnEdit.TabIndex = 51;
            this.BtnEdit.Text = "Edit";
            this.BtnEdit.UseVisualStyleBackColor = false;
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // LbCount
            // 
            this.LbCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LbCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LbCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LbCount.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbCount.ForeColor = System.Drawing.Color.White;
            this.LbCount.Location = new System.Drawing.Point(828, 214);
            this.LbCount.Name = "LbCount";
            this.LbCount.Size = new System.Drawing.Size(117, 41);
            this.LbCount.TabIndex = 49;
            this.LbCount.Text = "0";
            this.LbCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtSearch.BackColor = System.Drawing.Color.White;
            this.TxtSearch.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.ForeColor = System.Drawing.Color.Black;
            this.TxtSearch.Location = new System.Drawing.Point(45, 214);
            this.TxtSearch.MaxLength = 40;
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(777, 41);
            this.TxtSearch.TabIndex = 47;
            this.TxtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // DGVProducts
            // 
            this.DGVProducts.AllowUserToAddRows = false;
            this.DGVProducts.AllowUserToDeleteRows = false;
            this.DGVProducts.AllowUserToOrderColumns = true;
            this.DGVProducts.AllowUserToResizeRows = false;
            this.DGVProducts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProducts.Location = new System.Drawing.Point(45, 271);
            this.DGVProducts.MultiSelect = false;
            this.DGVProducts.Name = "DGVProducts";
            this.DGVProducts.ReadOnly = true;
            this.DGVProducts.RowHeadersWidth = 51;
            this.DGVProducts.RowTemplate.Height = 26;
            this.DGVProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProducts.Size = new System.Drawing.Size(900, 356);
            this.DGVProducts.TabIndex = 48;
            this.DGVProducts.DoubleClick += new System.EventHandler(this.DGVProducts_DoubleClick);
            // 
            // Frm_Products_group_AddDeleteUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 653);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.LbCount);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.DGVProducts);
            this.Name = "Frm_Products_group_AddDeleteUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Products_group_AddDeleteUpdate";
            this.Load += new System.EventHandler(this.Frm_Products_group_AddDeleteUpdate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TxtproductGroupName;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.Button BtnCancelEdit;
        private System.Windows.Forms.Button BtnEdit;
        public System.Windows.Forms.Label LbCount;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.DataGridView DGVProducts;
        private System.Windows.Forms.Button BtnDelete;
        private System.Windows.Forms.TextBox TxtAutoNumber;
    }
}