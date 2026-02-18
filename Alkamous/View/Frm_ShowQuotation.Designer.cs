namespace Alkamous.View
{
    partial class Frm_ShowQuotation
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
            this.DGVProducts = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.TxtCustomer_Name = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.TxtDiscount = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.TxtCustomer_Company = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtCustomer_Invoice = new System.Windows.Forms.TextBox();
            this.DGVProductsConsumable = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductsConsumable)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVProducts
            // 
            this.DGVProducts.AllowUserToAddRows = false;
            this.DGVProducts.AllowUserToDeleteRows = false;
            this.DGVProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProducts.Location = new System.Drawing.Point(12, 105);
            this.DGVProducts.MultiSelect = false;
            this.DGVProducts.Name = "DGVProducts";
            this.DGVProducts.ReadOnly = true;
            this.DGVProducts.RowHeadersWidth = 51;
            this.DGVProducts.RowTemplate.Height = 26;
            this.DGVProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProducts.Size = new System.Drawing.Size(1283, 246);
            this.DGVProducts.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.TxtCustomer_Name);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.TxtDiscount);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.TxtCustomer_Company);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.TxtCustomer_Invoice);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1283, 87);
            this.panel1.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(621, 13);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 17);
            this.label7.TabIndex = 26;
            this.label7.Text = "Name";
            // 
            // TxtCustomer_Name
            // 
            this.TxtCustomer_Name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtCustomer_Name.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCustomer_Name.Location = new System.Drawing.Point(624, 41);
            this.TxtCustomer_Name.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.TxtCustomer_Name.MaxLength = 50;
            this.TxtCustomer_Name.Name = "TxtCustomer_Name";
            this.TxtCustomer_Name.ReadOnly = true;
            this.TxtCustomer_Name.Size = new System.Drawing.Size(369, 34);
            this.TxtCustomer_Name.TabIndex = 24;
            this.TxtCustomer_Name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1010, 13);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(62, 17);
            this.label14.TabIndex = 27;
            this.label14.Text = "Discount";
            // 
            // TxtDiscount
            // 
            this.TxtDiscount.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtDiscount.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDiscount.Location = new System.Drawing.Point(1013, 41);
            this.TxtDiscount.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.TxtDiscount.MaxLength = 8;
            this.TxtDiscount.Name = "TxtDiscount";
            this.TxtDiscount.ReadOnly = true;
            this.TxtDiscount.Size = new System.Drawing.Size(198, 34);
            this.TxtDiscount.TabIndex = 25;
            this.TxtDiscount.Text = "0";
            this.TxtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(229, 16);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "Company Name";
            // 
            // TxtCustomer_Company
            // 
            this.TxtCustomer_Company.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtCustomer_Company.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCustomer_Company.Location = new System.Drawing.Point(232, 41);
            this.TxtCustomer_Company.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.TxtCustomer_Company.MaxLength = 50;
            this.TxtCustomer_Company.Name = "TxtCustomer_Company";
            this.TxtCustomer_Company.ReadOnly = true;
            this.TxtCustomer_Company.Size = new System.Drawing.Size(372, 34);
            this.TxtCustomer_Company.TabIndex = 19;
            this.TxtCustomer_Company.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 13);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 22;
            this.label1.Text = "QUOTATION NO.";
            // 
            // TxtCustomer_Invoice
            // 
            this.TxtCustomer_Invoice.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TxtCustomer_Invoice.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCustomer_Invoice.Location = new System.Drawing.Point(20, 41);
            this.TxtCustomer_Invoice.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            this.TxtCustomer_Invoice.MaxLength = 15;
            this.TxtCustomer_Invoice.Name = "TxtCustomer_Invoice";
            this.TxtCustomer_Invoice.ReadOnly = true;
            this.TxtCustomer_Invoice.Size = new System.Drawing.Size(192, 34);
            this.TxtCustomer_Invoice.TabIndex = 20;
            this.TxtCustomer_Invoice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // DGVProductsConsumable
            // 
            this.DGVProductsConsumable.AllowUserToAddRows = false;
            this.DGVProductsConsumable.AllowUserToDeleteRows = false;
            this.DGVProductsConsumable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVProductsConsumable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVProductsConsumable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProductsConsumable.Location = new System.Drawing.Point(12, 379);
            this.DGVProductsConsumable.MultiSelect = false;
            this.DGVProductsConsumable.Name = "DGVProductsConsumable";
            this.DGVProductsConsumable.RowHeadersWidth = 51;
            this.DGVProductsConsumable.RowTemplate.Height = 26;
            this.DGVProductsConsumable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProductsConsumable.Size = new System.Drawing.Size(1283, 283);
            this.DGVProductsConsumable.TabIndex = 7;
            // 
            // Frm_ShowQuotation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 674);
            this.Controls.Add(this.DGVProductsConsumable);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DGVProducts);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "Frm_ShowQuotation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick view";
            this.Load += new System.EventHandler(this.Frm_ShowQuotation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductsConsumable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView DGVProducts;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.TextBox TxtCustomer_Company;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtCustomer_Invoice;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.TextBox TxtCustomer_Name;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TxtDiscount;
        public System.Windows.Forms.DataGridView DGVProductsConsumable;
    }
}