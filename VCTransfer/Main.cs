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

            VCBusiness.Process Process = new VCBusiness.Process();

            _result = Process.Run(action,OID.Text .Trim ());
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
    }
}
