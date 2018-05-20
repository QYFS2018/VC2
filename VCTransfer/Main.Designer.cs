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
            this.SuspendLayout();
            // 
            // OrderDownload
            // 
            this.OrderDownload.Location = new System.Drawing.Point(51, 102);
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
            this.ShipmentUpdate.Location = new System.Drawing.Point(208, 102);
            this.ShipmentUpdate.Name = "ShipmentUpdate";
            this.ShipmentUpdate.Size = new System.Drawing.Size(128, 57);
            this.ShipmentUpdate.TabIndex = 1;
            this.ShipmentUpdate.Text = "Shipment Update";
            this.ShipmentUpdate.UseVisualStyleBackColor = true;
            this.ShipmentUpdate.Click += new System.EventHandler(this.ShimentUpdate_Click);
            // 
            // InventoryUpdate
            // 
            this.InventoryUpdate.Location = new System.Drawing.Point(208, 181);
            this.InventoryUpdate.Name = "InventoryUpdate";
            this.InventoryUpdate.Size = new System.Drawing.Size(128, 57);
            this.InventoryUpdate.TabIndex = 2;
            this.InventoryUpdate.Text = "Inventory Update";
            this.InventoryUpdate.UseVisualStyleBackColor = true;
            this.InventoryUpdate.Click += new System.EventHandler(this.InventoryUpdate_Click);
            // 
            // ProductDownload
            // 
            this.ProductDownload.Location = new System.Drawing.Point(51, 181);
            this.ProductDownload.Name = "ProductDownload";
            this.ProductDownload.Size = new System.Drawing.Size(128, 57);
            this.ProductDownload.TabIndex = 3;
            this.ProductDownload.Text = "Product Download";
            this.ProductDownload.UseVisualStyleBackColor = true;
            this.ProductDownload.Click += new System.EventHandler(this.ProductDownload_Click);
            // 
            // ShipConfirmEmail
            // 
            this.ShipConfirmEmail.Location = new System.Drawing.Point(358, 265);
            this.ShipConfirmEmail.Name = "ShipConfirmEmail";
            this.ShipConfirmEmail.Size = new System.Drawing.Size(128, 57);
            this.ShipConfirmEmail.TabIndex = 4;
            this.ShipConfirmEmail.Text = "ShipConfirm Email";
            this.ShipConfirmEmail.UseVisualStyleBackColor = true;
            this.ShipConfirmEmail.Click += new System.EventHandler(this.ShipConfirmEmail_Click);
            // 
            // OID
            // 
            this.OID.Location = new System.Drawing.Point(51, 265);
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
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 350);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_Owner);
            this.Controls.Add(this.OID);
            this.Controls.Add(this.ShipConfirmEmail);
            this.Controls.Add(this.ProductDownload);
            this.Controls.Add(this.InventoryUpdate);
            this.Controls.Add(this.ShipmentUpdate);
            this.Controls.Add(this.OrderDownload);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VeraCore Transfer";
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
    }
}

