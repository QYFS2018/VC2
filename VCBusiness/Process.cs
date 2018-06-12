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
        public ReturnValue Run(string ownerCode,string action,string oid)
        {
            ReturnValue _result = new ReturnValue();

            #region get Owner List

            Controler Controler = new Controler();
            _result = Controler.getControler();
            if (_result.Success == false)
            {
                Common.ProcessError(_result, true);

                return _result;
            }

            #endregion

            foreach (Owner owner in Controler.Owners)
            {
                if (ownerCode == "999" || ownerCode == owner.OwnerCode.ToString())
                {
                    #region check and set onwer setting

                    bool enableRun = false;
                    if (owner.Actions.ContainsKey(action.ToUpper()) == true)
                    {
                        enableRun = Convert.ToBoolean(owner.Actions[action.ToUpper()].ToString());
                    }

                    if (enableRun == false)
                    {
                        continue;
                    }

                    Common.OwnerCode = owner.OwnerCode;
                    WComm.ConInfo.Url = owner.RegSubKey;
                    VCBusiness.BaseOrder Order = Common.CreateObject(owner, "Order") as VCBusiness.BaseOrder;
                    Order.Owner = owner;
                    VCBusiness.BaseProduct Product = Common.CreateObject(owner, "Product") as VCBusiness.BaseProduct;
                    Product.Owner = owner;
                    Common.ProcessType = action;

                    #endregion

                    #region Run function

                    if (action.ToUpper() == "OrderDownload".ToUpper())
                    {
                        Common.Log("Start OrderDownload");
                        _result = Order.Download();
                    }

                    if (action.ToUpper() == "UpdateShipment".ToUpper())
                    {
                        Common.Log("Start Update Shipment");
                        _result = Order.UpdateShipment();
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
                        #region ShipConfirmEmail

                        Common.Log("Start ShipConfirmEmail");

                        VCBusiness.Model.TProgram_Email _tProgram_Email = Common.CreateObject(owner, "TProgram_Email") as VCBusiness.Model.TProgram_Email;
                        _result = _tProgram_Email.getEmailTemplate("SHIP_CONFIRMATION");
                        if (_result.Success == false)
                        {
                            return _result;
                        }

                        _tProgram_Email = _result.Object as TProgram_Email;

                        
                        VCBusiness.EmailFactory EmailFactory = Common.CreateObject(owner, "EmailFactory") as VCBusiness.EmailFactory;
                        _result = EmailFactory.GetMailContent(int.Parse(oid), 1, _tProgram_Email);
                        if (_result.Success == false)
                        {
                            return _result;
                        }
                        EmailMessage email = _result.ObjectValue as EmailMessage;

                        _result = EmailFactory.SentEmail(int.Parse(oid), 1, email);
                        if (_result.Success == false)
                        {
                            return _result;
                        }

                        #endregion
                    }



                    if (action.ToUpper() == "ImportDMOrderDetail".ToUpper())
                    {
                        _result = Order.ImportDMOrderDetail(int.Parse(oid));
                    }

                    #endregion

                    if (_result.Success == false)
                    {
                        Common.ProcessError(_result, false);
                    }

                    Common.Log("Finish");
                }
            }

            return _result;
        }

    }
}
