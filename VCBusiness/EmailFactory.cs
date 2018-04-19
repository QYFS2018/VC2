using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WComm;
using VCBusiness.Model;
using DotNetOpenMail;
using System.IO;
using System.Web;

namespace VCBusiness
{
    public class EmailFactory
    {
        public ReturnValue GetMailContent(int orderId, TProgram_Email mi)
        {
            ReturnValue _result = new ReturnValue();

            System.Globalization.NumberFormatInfo nfi = Utilities.CurrentNumberFormat;

            #region getOrderInfo and Check

            TOrder _tOrder = new TOrder();
            _result = _tOrder.getOrderById(orderId);
            if (_result.Success == false)
            {
                return _result;
            }

            _tOrder = _result.Object as TOrder;


            if (_tOrder.OrderId == 0)
            {

                _result.Success = false;
                _result.ErrMessage = "the order doesn't exists --getOrderById";

                return _result;
            }



            TOrder_Line_Item _tOrder_Line_Item = new TOrder_Line_Item();
            _result = _tOrder_Line_Item.getOrderDetailsByOrderId(orderId);
            if (_result.Success == false)
            {
                return _result;
            }

            EntityList  _orderDetails = _result.ObjectList;


            if (_orderDetails.Count == 0)
            {

                _result.Success = false;
                _result.ErrMessage = "the order doesn't exists---getOrdersDetail";

                return _result;
            }


            #endregion

            try
            {
                #region generate Email Content

                EmailMessage _mail = new EmailMessage();

                #region Order Summary

                string MailContent = HttpUtility.HtmlDecode(mi.FullHtml);

                MailContent = MailContent
                    .Replace("{orderid}", _tOrder.AltOrderNum)
                    .Replace("{firstname}", _tOrder.FirstName)
                 .Replace("{shipmethod}", _tOrder.ShipMethodName)
                  .Replace("{ShipCarrier}", _tOrder.ShipCarrier)
                  .Replace("{CustomerName}", _tOrder.FirstName + " " + _tOrder.LastName)
                  .Replace("{trackingnumber}", _tOrder.TrackingNumber)
                    .Replace("{ShipDate}", _tOrder.ShippedDate.Value.ToString("MM/dd/yyyy"));

                #endregion

                TOrder_Line_Item _shipTo = _orderDetails[0] as TOrder_Line_Item;

                string shipinfo = _shipTo.ShipToAddress + "<br>" + _shipTo.ShipToCity + "," + _shipTo.ShipToState + " " + _shipTo.ShipToZip;

                MailContent = MailContent
                 .Replace("{shipmentnfo}", shipinfo);


                #region Shipping Detail

                string RepeatTemp = "";

                if (MailContent.IndexOf("{orderLine}") > 0)
                {
                    RepeatTemp = " <table  width =\"100%\" cellpadding=\"1\"><tr><td>SKU</td><td>Name</td><td>QTY</td></tr>";

                    foreach (TOrder_Line_Item _item in _orderDetails)
                    {
                        RepeatTemp = RepeatTemp + " <tr><td >" + _item.PartNumber + "</td><td>" + _item.ProductName +
                            "</td><td>" + _item.Quantity.ToString() + "</td></tr>";
                    }

                    RepeatTemp = RepeatTemp + "</table>";
                }

                MailContent = MailContent.Replace("{orderLine}", RepeatTemp);

                #endregion




                #endregion

                _mail.HtmlPart = new HtmlAttachment(MailContent);

                #region setup EmailMessage

                _mail.FromAddress = new EmailAddress(mi.RespondTo);
                _mail.Subject = mi.Subject.Replace("[Orderid]", _tOrder.AltOrderNum);

                if (Common.IsTest == true)
                {
                    string[] maillist = Common.TestMailTo.Split(';');
                    foreach (string _item in maillist)
                    {
                        _mail.AddToAddress(new EmailAddress(_item));

                    }

                    _result.Table = Common.TestMailTo;
                }
                else
                {
                    if (string.IsNullOrEmpty(_tOrder.Email) == true)
                    {
                        _result.Success = false;
                        _result.ErrMessage = "Email To Address is empty";
                        return _result;
                    }
                    else
                    {
                        string[] bcclist = _tOrder.Email.Split(';');
                        foreach (string _item in bcclist)
                        {
                            _mail.AddToAddress(new EmailAddress(_item));
                        }
                    }
                }

                if (string.IsNullOrEmpty(mi.BccAddress) == false)
                {
                    string[] bcclist = mi.BccAddress.Split(';');
                    foreach (string _item in bcclist)
                    {
                        _mail.AddBccAddress(new EmailAddress(_item));
                    }
                }
                if (string.IsNullOrEmpty(mi.CCAddress) == false)
                {
                    string[] bcclist = mi.CCAddress.Split(';');
                    foreach (string _item in bcclist)
                    {
                        _mail.AddCcAddress(new EmailAddress(_item));
                    }
                }

                #endregion

                _result.ObjectValue = _mail;
            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.ErrMessage = ex.ToString();
            }

         

            return _result;
        }

        public ReturnValue SentEmail(int orderId, EmailMessage email)
        {
            ReturnValue _result = new ReturnValue();

            App_Log_Mail _app_Log_Mail = new App_Log_Mail();

            try
            {
                SmtpServer server = new SmtpServer(System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"].ToString(), 25);

                DotNetOpenMail.SmtpAuth.SmtpAuthToken SmtpAuthToken =
                      new DotNetOpenMail.SmtpAuth.SmtpAuthToken(System.Configuration.ConfigurationSettings.AppSettings["SMTPUserName"].ToString(),
                      System.Configuration.ConfigurationSettings.AppSettings["SMTPPassword"].ToString());
                server.SmtpAuthToken = SmtpAuthToken;

                email.Send(server);
            }
            catch (Exception ex)
            {
                _app_Log_Mail.Success = false;
                _app_Log_Mail.Notes = ex.ToString();

                _result.Success = false;
                _result.ErrMessage = _app_Log_Mail.Notes;

                return _result;
            }


            #region Log App_Log_Mail

            _app_Log_Mail.IsTest = Common.IsTest;
            _app_Log_Mail.CreatedOn = System.DateTime.Now;
            if (email.FromAddress != null)
            {
                _app_Log_Mail.AddressFrom = email.FromAddress.Email;
            }

            foreach (EmailAddress item in email.ToAddresses)
            {
                _app_Log_Mail.AddressTo = _app_Log_Mail.AddressTo + item.Email + ";";
            }
            
            _app_Log_Mail.Subject = email.Subject;


            if (email.TextPart != null)
            {
                _app_Log_Mail.Content = email.TextPart.Contents;
            }
            if (email.HtmlPart != null)
            {
                _app_Log_Mail.Content = email.HtmlPart.Contents;
            }
            _app_Log_Mail.OId = orderId.ToString();
            _app_Log_Mail.Success = true; ;
            _app_Log_Mail.Notes = "";
            _app_Log_Mail.Type = "ShippingConfirm";
            _app_Log_Mail.Save();

            #endregion

            return _result;

        }

    
    
    }
}
