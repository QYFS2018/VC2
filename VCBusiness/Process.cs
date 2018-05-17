using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WComm;
using VCBusiness.Model;
using DotNetOpenMail;

namespace VCBusiness
{
    public class Process
    {
        public ReturnValue Run(string action,string oid)
        {
            ReturnValue _result = new ReturnValue();

            VCBusiness.Order Order = new Order();
            VCBusiness.Product Product = new Product();

            Common.ProcessType = action;

            if (action.ToUpper() == "OrderDownload".ToUpper())
            {
                Common.Log("Start Order Download");
                _result = Order.OrderDownload();
            }

            if (action.ToUpper() == "UpdateShipment".ToUpper())
            {
                Common.Log("Start Update Shipment");
                _result = Order.ShipmentUpdate();
            }

            if (action.ToUpper() == "ProductDownload".ToUpper())
            {
                Common.Log("Start Product Download");
                _result = Product.ProductDownload();
            }

            if (action.ToUpper() == "UpdateInventoryStatus".ToUpper())
            {
                Common.Log("Start UpdateInventoryStatus");
                _result = Product.UpdateInventoryStatus();
            }

            if (action.ToUpper() == "ShipConfirmEmail".ToUpper())
            {
                Common.Log("Start ShipConfirmEmail");

                TProgram_Email _tProgram_Email = new TProgram_Email();
                _result = _tProgram_Email.getEmailTemplate("SHIP_CONFIRMATION");
                if (_result.Success == false)
                {
                    return _result;
                }

                _tProgram_Email = _result.Object as TProgram_Email;

                EmailFactory EmailFactory = new VCBusiness.EmailFactory();
                _result = EmailFactory.GetMailContent(int.Parse(oid),1, _tProgram_Email);
                if (_result.Success == false)
                {
                    return _result;
                }
                EmailMessage email = _result.ObjectValue as EmailMessage;

                _result = EmailFactory.SentEmail(int.Parse(oid),1, email);
                if (_result.Success == false)
                {
                    return _result;
                }
            }

            if (_result.Success == false)
            {
                Common.ProcessError(_result, false);
            }

            Common.Log("Finish");


            return _result;
        }

    }
}
