namespace Alkamous.View
{
    partial class Frm_Products
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVProducts = new System.Windows.Forms.DataGridView();
            this.LbCount = new System.Windows.Forms.Label();
            this.TxtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnMultiSelectItem = new System.Windows.Forms.CheckBox();
            this.BtnFavorite = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVProducts
            // 
            this.DGVProducts.AllowUserToAddRows = false;
            this.DGVProducts.AllowUserToDeleteRows = false;
            this.DGVProducts.AllowUserToOrderColumns = true;
            this.DGVProducts.AllowUserToResizeRows = false;
            this.DGVProducts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DGVProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.DGVProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProducts.Location = new System.Drawing.Point(12, 83);
            this.DGVProducts.MultiSelect = false;
            this.DGVProducts.Name = "DGVProducts";
            this.DGVProducts.ReadOnly = true;
            this.DGVProducts.RowHeadersWidth = 51;
            this.DGVProducts.RowTemplate.Height = 26;
            this.DGVProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProducts.Size = new System.Drawing.Size(1058, 711);
            this.DGVProducts.TabIndex = 1;
            this.DGVProducts.DoubleClick += new System.EventHandler(this.DGVProducts_DoubleClick);
            this.DGVProducts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVProducts_KeyDown);
            // 
            // LbCount
            // 
            this.LbCount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LbCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LbCount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LbCount.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbCount.ForeColor = System.Drawing.Color.White;
            this.LbCount.Location = new System.Drawing.Point(930, 36);
            this.LbCount.Name = "LbCount";
            this.LbCount.Size = new System.Drawing.Size(140, 41);
            this.LbCount.TabIndex = 4;
            this.LbCount.Text = "0";
            this.LbCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TxtSearch
            // 
            this.TxtSearch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtSearch.BackColor = System.Drawing.Color.White;
            this.TxtSearch.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSearch.ForeColor = System.Drawing.Color.Black;
            this.TxtSearch.Location = new System.Drawing.Point(352, 36);
            this.TxtSearch.MaxLength = 40;
            this.TxtSearch.Name = "TxtSearch";
            this.TxtSearch.Size = new System.Drawing.Size(569, 41);
            this.TxtSearch.TabIndex = 3;
            this.TxtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TxtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.TxtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(631, 19);
            this.label1.TabIndex = 5;
            this.label1.Text = "Search By product_Id -  product_NameAR - product_NameEN - product_Unit";
            // 
            // BtnMultiSelectItem
            // 
            this.BtnMultiSelectItem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnMultiSelectItem.AutoSize = true;
            this.BtnMultiSelectItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnMultiSelectItem.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMultiSelectItem.Location = new System.Drawing.Point(171, 42);
            this.BtnMultiSelectItem.Name = "BtnMultiSelectItem";
            this.BtnMultiSelectItem.Size = new System.Drawing.Size(167, 31);
            this.BtnMultiSelectItem.TabIndex = 28;
            this.BtnMultiSelectItem.Text = "multi-select";
            this.BtnMultiSelectItem.UseVisualStyleBackColor = true;
            this.BtnMultiSelectItem.CheckedChanged += new System.EventHandler(this.BtnMultiSelectItem_CheckedChanged);
            // 
            // BtnFavorite
            // 
            this.BtnFavorite.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnFavorite.AutoSize = true;
            this.BtnFavorite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnFavorite.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFavorite.Location = new System.Drawing.Point(21, 42);
            this.BtnFavorite.Name = "BtnFavorite";
            this.BtnFavorite.Size = new System.Drawing.Size(118, 31);
            this.BtnFavorite.TabIndex = 29;
            this.BtnFavorite.Text = "favorite";
            this.BtnFavorite.UseVisualStyleBackColor = true;
            this.BtnFavorite.CheckedChanged += new System.EventHandler(this.BtnFavorite_CheckedChanged);
            // 
            // Frm_Products
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 806);
            this.Controls.Add(this.BtnFavorite);
            this.Controls.Add(this.BtnMultiSelectItem);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LbCount);
            this.Controls.Add(this.TxtSearch);
            this.Controls.Add(this.DGVProducts);
            this.Name = "Frm_Products";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Products & Items";
            this.Load += new System.EventHandler(this.Frm_Products_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVProducts;
        public System.Windows.Forms.Label LbCount;
        private System.Windows.Forms.TextBox TxtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox BtnMultiSelectItem;
        private System.Windows.Forms.CheckBox BtnFavorite;
    }
}