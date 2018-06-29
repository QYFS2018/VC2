using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WComm;
using VCBusiness;


namespace VCTransfer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            ReturnValue _result = new ReturnValue();

            Controler Controler = new Controler();
            _result = Controler.getControler();

            Controler.Owners.Sort(Controler.OwnerComparison);

            foreach (Owner _item in Controler.Owners)
            {
                this.cb_Owner.Items.Add(_item.OwnerCode.ToString() + "-" + _item.Name);
            }

            this.cb_Owner.SelectedIndex = 0;
        }

        private void OrderDownload_Click(object sender, EventArgs e)
        {
            if (!confirm("Order Download")) return;

            Process("OrderDownload");
        }

        private void ShimentUpdate_Click(object sender, EventArgs e)
        {
            if (!confirm("Shipment Update")) return;

            Process("UpdateShipment");
        }

        private void InventoryUpdate_Click(object sender, EventArgs e)
        {
            if (!confirm("Inventory Update")) return;

            Process("UpdateInventoryStatus");
        }

        private bool confirm(string function)
        {
            string message = "Do you want to run " + function + "?";
            string caption = "Confirm";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;

            result = MessageBox.Show(this, message, caption, buttons);

            return result == DialogResult.Yes;
        }

        private void Process(string action)
        {
            if (System.Environment.MachineName.ToLower() != System.Configuration.ConfigurationSettings.AppSettings["MachineName"].ToString().ToLower())
            {
                MessageBox.Show("Can't run on " + System.Environment.MachineName);

                return;
            }
     
            ReturnValue _result = new ReturnValue();

            string[] _cbValue = this.cb_Owner.SelectedItem.ToString().Split('-');

            string _ownerCode = _cbValue[0].ToString();

            VCBusiness.Process Process = new VCBusiness.Process();

            _result = Process.Run(_ownerCode, action, OID.Text.Trim());
            if (_result.Success == false)
            {
                MessageBox.Show(_result.ErrMessage);
            }
            else
            {
                MessageBox.Show(action + " Completed!");
            }
        }

        private void ProductDownload_Click(object sender, EventArgs e)
        {
            if (!confirm("Product Download")) return;

            Process("ProductDownload");
        }

        private void ShipConfirmEmail_Click(object sender, EventArgs e)
        {
            if (!confirm("ShipConfirm Email")) return;

            Process("ShipConfirmEmail");
        }

        private void ImportDMOrderDetail_Click(object sender, EventArgs e)
        {
            if (!confirm("ImportDMOrderDetail")) return;

            Process("ImportDMOrderDetail");
        }

        private void GenerateInvoicePDF_Click(object sender, EventArgs e)
        {
            if (!confirm("GenerateInvoicePDF")) return;

            Process("GenerateInvoicePDF");
        }

        private void InvoiceEmail_Click(object sender, EventArgs e)
        {
            if (!confirm("InvoiceEmail")) return;

            Process("InvoiceEmail");
        }
    }
}
