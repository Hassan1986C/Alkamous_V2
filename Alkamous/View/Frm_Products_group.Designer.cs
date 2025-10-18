namespace Alkamous.View
{
    partial class Frm_Products_group
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
            this.groupBoxItems = new System.Windows.Forms.GroupBox();
            this.LbCountProdects = new System.Windows.Forms.Label();
            this.BtnShowAllProdcts = new System.Windows.Forms.Button();
            this.TxtGroupByItem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BtnDeleteRowFromDGVProducts = new System.Windows.Forms.Button();
            this.BtnSaveData = new System.Windows.Forms.Button();
            this.BtnUpMoveRows = new System.Windows.Forms.Button();
            this.BtnDownMoveRows = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).BeginInit();
            this.groupBoxItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGVProducts
            // 
            this.DGVProducts.AllowUserToAddRows = false;
            this.DGVProducts.AllowUserToDeleteRows = false;
            this.DGVProducts.AllowUserToOrderColumns = true;
            this.DGVProducts.AllowUserToResizeRows = false;
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
            this.DGVProducts.Location = new System.Drawing.Point(12, 189);
            this.DGVProducts.MultiSelect = false;
            this.DGVProducts.Name = "DGVProducts";
            this.DGVProducts.ReadOnly = true;
            this.DGVProducts.RowHeadersWidth = 51;
            this.DGVProducts.RowTemplate.Height = 26;
            this.DGVProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProducts.Size = new System.Drawing.Size(1071, 479);
            this.DGVProducts.TabIndex = 46;
            // 
            // groupBoxItems
            // 
            this.groupBoxItems.BackColor = System.Drawing.SystemColors.Control;
            this.groupBoxItems.Controls.Add(this.LbCountProdects);
            this.groupBoxItems.Controls.Add(this.BtnShowAllProdcts);
            this.groupBoxItems.Controls.Add(this.TxtGroupByItem);
            this.groupBoxItems.Controls.Add(this.label3);
            this.groupBoxItems.Location = new System.Drawing.Point(12, 38);
            this.groupBoxItems.Margin = new System.Windows.Forms.Padding(2);
            this.groupBoxItems.Name = "groupBoxItems";
            this.groupBoxItems.Padding = new System.Windows.Forms.Padding(2);
            this.groupBoxItems.Size = new System.Drawing.Size(1071, 146);
            this.groupBoxItems.TabIndex = 50;
            this.groupBoxItems.TabStop = false;
            // 
            // LbCountProdects
            // 
            this.LbCountProdects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.LbCountProdects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LbCountProdects.Font = new System.Drawing.Font("Century Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbCountProdects.ForeColor = System.Drawing.Color.White;
            this.LbCountProdects.Location = new System.Drawing.Point(943, 100);
            this.LbCountProdects.Name = "LbCountProdects";
            this.LbCountProdects.Size = new System.Drawing.Size(123, 41);
            this.LbCountProdects.TabIndex = 65;
            this.LbCountProdects.Text = "0";
            this.LbCountProdects.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnShowAllProdcts
            // 
            this.BtnShowAllProdcts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnShowAllProdcts.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.BtnShowAllProdcts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnShowAllProdcts.FlatAppearance.BorderSize = 0;
            this.BtnShowAllProdcts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.MediumSeaGreen;
            this.BtnShowAllProdcts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnShowAllProdcts.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold);
            this.BtnShowAllProdcts.ForeColor = System.Drawing.Color.White;
            this.BtnShowAllProdcts.Location = new System.Drawing.Point(5, 86);
            this.BtnShowAllProdcts.Name = "BtnShowAllProdcts";
            this.BtnShowAllProdcts.Size = new System.Drawing.Size(295, 55);
            this.BtnShowAllProdcts.TabIndex = 52;
            this.BtnShowAllProdcts.Text = "Add Product to Group";
            this.BtnShowAllProdcts.UseVisualStyleBackColor = false;
            this.BtnShowAllProdcts.Click += new System.EventHandler(this.BtnShowAllProdcts_Click);
            // 
            // TxtGroupByItem
            // 
            this.TxtGroupByItem.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.TxtGroupByItem.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.TxtGroupByItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TxtGroupByItem.Font = new System.Drawing.Font("Tahoma", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGroupByItem.FormattingEnabled = true;
            this.TxtGroupByItem.ItemHeight = 33;
            this.TxtGroupByItem.Location = new System.Drawing.Point(4, 41);
            this.TxtGroupByItem.Margin = new System.Windows.Forms.Padding(2);
            this.TxtGroupByItem.Name = "TxtGroupByItem";
            this.TxtGroupByItem.Size = new System.Drawing.Size(1062, 41);
            this.TxtGroupByItem.TabIndex = 2;
            this.TxtGroupByItem.SelectedIndexChanged += new System.EventHandler(this.TxtGroupByItem_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 19);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "ITEMS GROUP";
            // 
            // BtnDeleteRowFromDGVProducts
            // 
            this.BtnDeleteRowFromDGVProducts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnDeleteRowFromDGVProducts.BackColor = System.Drawing.Color.DarkRed;
            this.BtnDeleteRowFromDGVProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnDeleteRowFromDGVProducts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnDeleteRowFromDGVProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteRowFromDGVProducts.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDeleteRowFromDGVProducts.ForeColor = System.Drawing.Color.White;
            this.BtnDeleteRowFromDGVProducts.Location = new System.Drawing.Point(13, 695);
            this.BtnDeleteRowFromDGVProducts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BtnDeleteRowFromDGVProducts.Name = "BtnDeleteRowFromDGVProducts";
            this.BtnDeleteRowFromDGVProducts.Size = new System.Drawing.Size(246, 55);
            this.BtnDeleteRowFromDGVProducts.TabIndex = 51;
            this.BtnDeleteRowFromDGVProducts.Text = "Delete Row";
            this.BtnDeleteRowFromDGVProducts.UseVisualStyleBackColor = false;
            this.BtnDeleteRowFromDGVProducts.Click += new System.EventHandler(this.BtnDeleteRowFromDGVProducts_Click);
            // 
            // BtnSaveData
            // 
            this.BtnSaveData.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnSaveData.BackColor = System.Drawing.Color.Navy;
            this.BtnSaveData.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSaveData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.BtnSaveData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnSaveData.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold);
            this.BtnSaveData.ForeColor = System.Drawing.Color.White;
            this.BtnSaveData.Location = new System.Drawing.Point(832, 695);
            this.BtnSaveData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.BtnSaveData.Name = "BtnSaveData";
            this.BtnSaveData.Size = new System.Drawing.Size(246, 55);
            this.BtnSaveData.TabIndex = 53;
            this.BtnSaveData.Text = "Save All Data";
            this.BtnSaveData.UseVisualStyleBackColor = false;
            this.BtnSaveData.Click += new System.EventHandler(this.BtnSaveData_Click);
            // 
            // BtnUpMoveRows
            // 
            this.BtnUpMoveRows.BackColor = System.Drawing.Color.DarkGreen;
            this.BtnUpMoveRows.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnUpMoveRows.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.BtnUpMoveRows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnUpMoveRows.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnUpMoveRows.ForeColor = System.Drawing.Color.White;
            this.BtnUpMoveRows.Location = new System.Drawing.Point(333, 708);
            this.BtnUpMoveRows.Name = "BtnUpMoveRows";
            this.BtnUpMoveRows.Size = new System.Drawing.Size(147, 35);
            this.BtnUpMoveRows.TabIndex = 55;
            this.BtnUpMoveRows.Text = "UP";
            this.BtnUpMoveRows.UseVisualStyleBackColor = false;
            this.BtnUpMoveRows.Click += new System.EventHandler(this.BtnUpMoveRows_Click);
            // 
            // BtnDownMoveRows
            // 
            this.BtnDownMoveRows.BackColor = System.Drawing.Color.DarkGreen;
            this.BtnDownMoveRows.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.BtnDownMoveRows.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Black;
            this.BtnDownMoveRows.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDownMoveRows.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDownMoveRows.ForeColor = System.Drawing.Color.White;
            this.BtnDownMoveRows.Location = new System.Drawing.Point(486, 708);
            this.BtnDownMoveRows.Name = "BtnDownMoveRows";
            this.BtnDownMoveRows.Size = new System.Drawing.Size(147, 35);
            this.BtnDownMoveRows.TabIndex = 54;
            this.BtnDownMoveRows.Text = "Down";
            this.BtnDownMoveRows.UseVisualStyleBackColor = false;
            this.BtnDownMoveRows.Click += new System.EventHandler(this.BtnDownMoveRows_Click);
            // 
            // Frm_Products_group
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 853);
            this.Controls.Add(this.BtnUpMoveRows);
            this.Controls.Add(this.BtnDownMoveRows);
            this.Controls.Add(this.BtnSaveData);
            this.Controls.Add(this.BtnDeleteRowFromDGVProducts);
            this.Controls.Add(this.groupBoxItems);
            this.Controls.Add(this.DGVProducts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frm_Products_group";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frm_Products_group";
            this.Load += new System.EventHandler(this.Frm_Products_group_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProducts)).EndInit();
            this.groupBoxItems.ResumeLayout(false);
            this.groupBoxItems.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVProducts;
        private System.Windows.Forms.GroupBox groupBoxItems;
        private System.Windows.Forms.ComboBox TxtGroupByItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnDeleteRowFromDGVProducts;
        private System.Windows.Forms.Button BtnShowAllProdcts;
        private System.Windows.Forms.Button BtnSaveData;
        public System.Windows.Forms.Label LbCountProdects;
        private System.Windows.Forms.Button BtnUpMoveRows;
        private System.Windows.Forms.Button BtnDownMoveRows;
    }
}