namespace VCTransfer
{
    partial class Main
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.OrderDownload = new System.Windows.Forms.Button();
            this.ShipmentUpdate = new System.Windows.Forms.Button();
            this.InventoryUpdate = new System.Windows.Forms.Button();
            this.ProductDownload = new System.Windows.Forms.Button();
            this.ShipConfirmEmail = new System.Windows.Forms.Button();
            this.OID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Owner = new System.Windows.Forms.ComboBox();
            this.ImportDMOrderDetail = new System.Windows.Forms.Button();
            this.GenerateInvoicePDF = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.InvoiceEmail = new System.Windows.Forms.Button();
            this.TFDueInvoices = new System.Windows.Forms.Button();
            this.TFPastDue = new System.Windows.Forms.Button();
            this.TFWishList = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // OrderDownload
            // 
            this.OrderDownload.Location = new System.Drawing.Point(67, 24);
            this.OrderDownload.Name = "OrderDownload";
            this.OrderDownload.Size = new System.Drawing.Size(128, 57);
            this.OrderDownload.TabIndex = 0;
            this.OrderDownload.Text = "Order Download";
            this.OrderDownload.UseVisualStyleBackColor = true;
            this.OrderDownload.Click += new System.EventHandler(this.OrderDownload_Click);
            // 
            // ShipmentUpdate
            // 
            this.ShipmentUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ShipmentUpdate.Location = new System.Drawing.Point(224, 24);
            this.ShipmentUpdate.Name = "ShipmentUpdate";
            this.ShipmentUpdate.Size = new System.Drawing.Size(128, 57);
            this.ShipmentUpdate.TabIndex = 1;
            this.ShipmentUpdate.Text = "Shipment Update";
            this.ShipmentUpdate.UseVisualStyleBackColor = true;
            this.ShipmentUpdate.Click += new System.EventHandler(this.ShimentUpdate_Click);
            // 
            // InventoryUpdate
            // 
            this.InventoryUpdate.Location = new System.Drawing.Point(224, 103);
            this.InventoryUpdate.Name = "InventoryUpdate";
            this.InventoryUpdate.Size = new System.Drawing.Size(128, 57);
            this.InventoryUpdate.TabIndex = 2;
            this.InventoryUpdate.Text = "Inventory Update";
            this.InventoryUpdate.UseVisualStyleBackColor = true;
            this.InventoryUpdate.Click += new System.EventHandler(this.InventoryUpdate_Click);
            // 
            // ProductDownload
            // 
            this.ProductDownload.Location = new System.Drawing.Point(67, 103);
            this.ProductDownload.Name = "ProductDownload";
            this.ProductDownload.Size = new System.Drawing.Size(128, 57);
            this.ProductDownload.TabIndex = 3;
            this.ProductDownload.Text = "Product Download";
            this.ProductDownload.UseVisualStyleBackColor = true;
            this.ProductDownload.Click += new System.EventHandler(this.ProductDownload_Click);
            // 
            // ShipConfirmEmail
            // 
            this.ShipConfirmEmail.Location = new System.Drawing.Point(290, 47);
            this.ShipConfirmEmail.Name = "ShipConfirmEmail";
            this.ShipConfirmEmail.Size = new System.Drawing.Size(128, 57);
            this.ShipConfirmEmail.TabIndex = 4;
            this.ShipConfirmEmail.Text = "ShipConfirm Email";
            this.ShipConfirmEmail.UseVisualStyleBackColor = true;
            this.ShipConfirmEmail.Click += new System.EventHandler(this.ShipConfirmEmail_Click);
            // 
            // OID
            // 
            this.OID.Location = new System.Drawing.Point(22, 15);
            this.OID.Name = "OID";
            this.OID.Size = new System.Drawing.Size(285, 21);
            this.OID.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "Owner :";
            // 
            // cb_Owner
            // 
            this.cb_Owner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_Owner.FormattingEnabled = true;
            this.cb_Owner.Location = new System.Drawing.Point(107, 21);
            this.cb_Owner.Name = "cb_Owner";
            this.cb_Owner.Size = new System.Drawing.Size(405, 20);
            this.cb_Owner.TabIndex = 10;
            // 
            // ImportDMOrderDetail
            // 
            this.ImportDMOrderDetail.Location = new System.Drawing.Point(156, 47);
            this.ImportDMOrderDetail.Name = "ImportDMOrderDetail";
            this.ImportDMOrderDetail.Size = new System.Drawing.Size(128, 57);
            this.ImportDMOrderDetail.TabIndex = 12;
            this.ImportDMOrderDetail.Text = "ImportDMOrderDetail";
            this.ImportDMOrderDetail.UseVisualStyleBackColor = true;
            this.ImportDMOrderDetail.Click += new System.EventHandler(this.ImportDMOrderDetail_Click);
            // 
            // GenerateInvoicePDF
            // 
            this.GenerateInvoicePDF.Location = new System.Drawing.Point(22, 47);
            this.GenerateInvoicePDF.Name = "GenerateInvoicePDF";
            this.GenerateInvoicePDF.Size = new System.Drawing.Size(128, 57);
            this.GenerateInvoicePDF.TabIndex = 13;
            this.GenerateInvoicePDF.Text = "Generate Invoice PDF";
            this.GenerateInvoicePDF.UseVisualStyleBackColor = true;
            this.GenerateInvoicePDF.Click += new System.EventHandler(this.GenerateInvoicePDF_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(47, 60);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(516, 339);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.InventoryUpdate);
            this.tabPage1.Controls.Add(this.OrderDownload);
            this.tabPage1.Controls.Add(this.ShipmentUpdate);
            this.tabPage1.Controls.Add(this.ProductDownload);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(460, 204);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Common";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TFWishList);
            this.tabPage2.Controls.Add(this.TFPastDue);
            this.tabPage2.Controls.Add(this.TFDueInvoices);
            this.tabPage2.Controls.Add(this.InvoiceEmail);
            this.tabPage2.Controls.Add(this.OID);
            this.tabPage2.Controls.Add(this.ImportDMOrderDetail);
            this.tabPage2.Controls.Add(this.GenerateInvoicePDF);
            this.tabPage2.Controls.Add(this.ShipConfirmEmail);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(508, 313);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Other";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // InvoiceEmail
            // 
            this.InvoiceEmail.Location = new System.Drawing.Point(22, 110);
            this.InvoiceEmail.Name = "InvoiceEmail";
            this.InvoiceEmail.Size = new System.Drawing.Size(128, 57);
            this.InvoiceEmail.TabIndex = 14;
            this.InvoiceEmail.Text = "Invoice Email";
            this.InvoiceEmail.UseVisualStyleBackColor = true;
            this.InvoiceEmail.Click += new System.EventHandler(this.InvoiceEmail_Click);
            // 
            // TFDueInvoices
            // 
            this.TFDueInvoices.Location = new System.Drawing.Point(22, 173);
            this.TFDueInvoices.Name = "TFDueInvoices";
            this.TFDueInvoices.Size = new System.Drawing.Size(128, 57);
            this.TFDueInvoices.TabIndex = 15;
            this.TFDueInvoices.Text = "TFDueInvoices";
            this.TFDueInvoices.UseVisualStyleBackColor = true;
            this.TFDueInvoices.Click += new System.EventHandler(this.TFDueInvoices_Click);
            // 
            // TFPastDue
            // 
            this.TFPastDue.Location = new System.Drawing.Point(156, 173);
            this.TFPastDue.Name = "TFPastDue";
            this.TFPastDue.Size = new System.Drawing.Size(128, 57);
            this.TFPastDue.TabIndex = 16;
            this.TFPastDue.Text = "TFPastDue";
            this.TFPastDue.UseVisualStyleBackColor = true;
            this.TFPastDue.Click += new System.EventHandler(this.TFPastDue_Click);
            // 
            // TFWishList
            // 
            this.TFWishList.Location = new System.Drawing.Point(290, 173);
            this.TFWishList.Name = "TFWishList";
            this.TFWishList.Size = new System.Drawing.Size(128, 57);
            this.TFWishList.TabIndex = 17;
            this.TFWishList.Text = "TFWishList";
            this.TFWishList.UseVisualStyleBackColor = true;
            this.TFWishList.Click += new System.EventHandler(this.TFWishList_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 445);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_Owner);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VeraCore Transfer";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OrderDownload;
        private System.Windows.Forms.Button ShipmentUpdate;
        private System.Windows.Forms.Button InventoryUpdate;
        private System.Windows.Forms.Button ProductDownload;
        private System.Windows.Forms.Button ShipConfirmEmail;
        private System.Windows.Forms.TextBox OID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_Owner;
        private System.Windows.Forms.Button ImportDMOrderDetail;
        private System.Windows.Forms.Button GenerateInvoicePDF;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button InvoiceEmail;
        private System.Windows.Forms.Button TFWishList;
        private System.Windows.Forms.Button TFPastDue;
        private System.Windows.Forms.Button TFDueInvoices;
    }
}

