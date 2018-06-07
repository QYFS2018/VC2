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
        public virtual ReturnValue GetMailContent(int orderId,int releaseID, TProgram_Email mi)
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
            _result = _tOrder_Line_Item.getOrderDetailsByOrderId(orderId, releaseID);
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

        public ReturnValue SentEmail(int orderId,int releaseID, EmailMessage email)
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
            _app_Log_Mail.ReleaseID = releaseID;
            _app_Log_Mail.Success = true; ;
            _app_Log_Mail.Notes = "";
            _app_Log_Mail.Type = "ShippingConfirm";
            _app_Log_Mail.Save();

            #endregion

            return _result;

        }
    }

    public class TecnifibreEmailFactory : EmailFactory
    {
        public override ReturnValue GetMailContent(int orderId, int releaseID, TProgram_Email mi)
        {
            ReturnValue _result = new ReturnValue();


            System.Globalization.NumberFormatInfo nfi = Utilities.CurrentNumberFormat;

            #region getCustomerInfo
            TOrderTF _tOrder = new TOrderTF();
            _result = _tOrder.getOrderById(orderId);
            if (!_result.Success)
            {

                return _result;
            }
            _tOrder = _result.Object as TOrderTF;

            if (_tOrder.SourceId == 19)
            {
                _result.Code = 19;
                return _result;
            }


            TCustomer _tCustomer = new TCustomer();
            _result = _tCustomer.getCustomerById(_tOrder.PWPCustomerId);
            if (!_result.Success)
            {

                return _result;
            }
            _tCustomer = _result.Object as TCustomer;



            TOrder_Line_Item _tOrder_Line_Item = new TOrder_Line_Item();
            _result = _tOrder_Line_Item.getOrderDetailsByOrderId(_tOrder.OrderId, releaseID);
            if (!_result.Success)
            {

                return _result;
            }
            EntityList _list = _result.ObjectList;

            if (_list.Count == 0)
            {
                return _result;
            }

            _tOrder_Line_Item = _list[0] as TOrder_Line_Item;

            TShipMethod _tShipMethod = new TShipMethod();
            _result = _tShipMethod.getShipMethodById(_tOrder_Line_Item.ShipMethodId);
            if (!_result.Success)
            {

                return _result;
            }
            _tShipMethod = _result.Object as TShipMethod;


            TAddress _tBillAddress = new TAddress();
            _result = _tBillAddress.getAddressById(_tOrder.CustomerAddressId);
            if (!_result.Success)
            {

                return _result;
            }
            _tBillAddress = _result.Object as TAddress;



            TAddress _tShipAddress = new TAddress();
            _result = _tBillAddress.getAddressById(_tOrder.ShipToAddressId);
            if (!_result.Success)
            {

                return _result;
            }
            _tShipAddress = _result.Object as TAddress;


            TPaymentArrangement _tPaymentArrangement = new TPaymentArrangement();
            _result = _tPaymentArrangement.getPaymentArrangementList(orderId);
            if (!_result.Success)
            {

                return _result;
            }
            EntityList _paymentList = _result.ObjectList;


            double _paymentApplied = 0;
            double _estimatedAmountDue = 0;

            foreach (TPaymentArrangement _item in _paymentList)
            {
                if (_item.PayMethodId == 4)
                {
                    _estimatedAmountDue += _item.Amount;
                }
                else
                {
                    _paymentApplied += _item.Amount;
                }
            }




            #endregion


            try
            {
                #region setup EmailMessage

                EmailMessage _mail = new EmailMessage();

                string MailContent = HttpUtility.HtmlDecode(mi.FullHtml);

                #region filter the email content
                MailContent = MailContent.Replace("[CustomerName]", _tCustomer.FirstName == null ? "" : _tCustomer.FirstName.ToString());
                MailContent = MailContent.Replace("[OrderDate]", _tOrder.OrderDate.ToString("MM/dd/yyyy"));

                string siteURL = "http://admin.tecnifibreusa.com/";
                MailContent = MailContent.Replace("[WebSite]", siteURL);
                MailContent = MailContent.Replace("[OrderNumber]", _tOrder.AltOrderNum);
                MailContent = MailContent.Replace("[PONumber]", _tOrder.PONumber);
                MailContent = MailContent.Replace("[CustomerAcct]", _tCustomer.AltCustNum);
                MailContent = MailContent.Replace("[ShipMethod]", _tShipMethod.Description);

                MailContent = MailContent.Replace("[BillingAddress]", _tBillAddress.Company + "<br> " + _tBillAddress.Address1 + " " + _tBillAddress.Address2 + "<br>" + _tBillAddress.City + ", " + _tBillAddress.StateCode + " " + _tBillAddress.PostalCode);
                MailContent = MailContent.Replace("[ShippingAddress]", _tShipAddress.Company + "<br> " + _tShipAddress.Address1 + " " + _tShipAddress.Address2 + "<br>" + _tShipAddress.City + ", " + _tShipAddress.StateCode + " " + _tShipAddress.PostalCode);

                MailContent = MailContent.Replace("[SubTotal]", (_tOrder.TotalWholeSaleAmount - _tOrder.CompProductAmount).ToString("C", Utilities.CurrentNumberFormat));
                MailContent = MailContent.Replace("[Tax]", "(" + Utilities.Round(_tOrder.TaxRate * 100, 2).ToString() + "%)" + (_tOrder.TotalTax - _tOrder.CompTax).ToString("C", Utilities.CurrentNumberFormat));
                MailContent = MailContent.Replace("[Shipping]", (_tOrder.TotalShipping - _tOrder.CompShipingCost).ToString("C", Utilities.CurrentNumberFormat));
                MailContent = MailContent.Replace("[Discount]", (_tOrder.TotalDiscountAmount + _tOrder.CompProductAmount).ToString("C", Utilities.CurrentNumberFormat));
                MailContent = MailContent.Replace("[OrderTotal]", _tOrder.TotalOrderAmount.ToString("C", Utilities.CurrentNumberFormat));

                MailContent = MailContent.Replace("[PaymentApplied]", _paymentApplied.ToString("C", Utilities.CurrentNumberFormat));
                MailContent = MailContent.Replace("[EstimatedAmountDue]", _estimatedAmountDue.ToString("C", Utilities.CurrentNumberFormat));

                MailContent = MailContent.Replace("[BFirstName]", _tBillAddress.FirstName);
                MailContent = MailContent.Replace("[BLastName]", _tBillAddress.LastName);
                MailContent = MailContent.Replace("[SFirstName]", _tShipAddress.FirstName);
                MailContent = MailContent.Replace("[SLastName]", _tShipAddress.LastName);


                StringBuilder OrderItemHTML = new StringBuilder();

                foreach (TOrder_Line_Item _item in _list)
                {
                    OrderItemHTML.Append("<tr>");
                    OrderItemHTML.Append("  <td>" + _item.LineNum + "</td>");
                    OrderItemHTML.Append("<td>" + _item.PartNumber + "</td>");
                    OrderItemHTML.Append("<td>" + _item.ProductName + "</td>");
                    OrderItemHTML.Append("<td>" + _item.Quantity + "</td>");
                    if (_item.ShippedDate != null)
                    {
                        OrderItemHTML.Append("<td>" + _item.ShippedDate.Value.ToString("MM/dd/yyyy") + "</td>");
                    }
                    else
                    {
                        OrderItemHTML.Append("<td></td>");
                    }
                    OrderItemHTML.Append("<td>" + _item.TrackingNumber + "</td>");
                    OrderItemHTML.Append("<td>" + _item.Price.ToString("C", Utilities.CurrentNumberFormat) + "</td>");
                    OrderItemHTML.Append("<td>" + (_item.DiscountAmount + _item.ComAmount).ToString("C", Utilities.CurrentNumberFormat) + "</td>");
                    OrderItemHTML.Append("<td>" + ((_item.ActualPrice - _item.ComAmount) / _item.Quantity).ToString("C", Utilities.CurrentNumberFormat) + "</td>");
                    OrderItemHTML.Append("<td>" + (_item.ActualPrice - _item.ComAmount).ToString("C", Utilities.CurrentNumberFormat) + "</td>");
                    OrderItemHTML.Append("</tr>");
                }
                #endregion


                MailContent = MailContent.Replace("[Items]", OrderItemHTML.ToString());


                _mail.HtmlPart = new HtmlAttachment(MailContent);


                _mail.FromAddress = new EmailAddress(mi.RespondTo);
                _mail.Subject = mi.Subject;

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
                    if (string.IsNullOrEmpty(_tCustomer.Email) == true)
                    {
                        _result.Success = false;
                        _result.ErrMessage = "Email To Address is empty";
                        return _result;
                    }
                    else
                    {
                        _mail.AddToAddress(new EmailAddress(_tCustomer.Email));
                    }

                    _result.Table = _tCustomer.Email;
                }


                if (string.IsNullOrEmpty(mi.BccAddress) == false)
                {
                    string[] bcclist = mi.BccAddress.Split(';');
                    foreach (string _item in bcclist)
                    {
                        if (string.IsNullOrEmpty(_item) == false)
                        {
                            _mail.AddBccAddress(new EmailAddress(_item));
                        }
                    }
                }
                if (string.IsNullOrEmpty(mi.CCAddress) == false)
                {
                    string[] bcclist = mi.CCAddress.Split(';');
                    foreach (string _item in bcclist)
                    {
                        if (string.IsNullOrEmpty(_item) == false)
                        {
                            _mail.AddCcAddress(new EmailAddress(_item));
                        }
                    }
                }


                if (string.IsNullOrEmpty(_tCustomer.OrderEmail) == false)
                {
                    _mail.AddCcAddress(new EmailAddress(_tCustomer.OrderEmail));
                }
                if (string.IsNullOrEmpty(_tCustomer.SecondaryEmail) == false)
                {
                    _mail.AddCcAddress(new EmailAddress(_tCustomer.SecondaryEmail));
                }

                if (string.IsNullOrEmpty(_tCustomer.SalesRepEmail) == false)
                {
                    _mail.AddBccAddress(new EmailAddress(_tCustomer.SalesRepEmail));
                }


                _result.ObjectValue = _mail;

                #endregion

            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.ErrMessage = ex.ToString();
            }


            return _result;
        }

        public ReturnValue SentInvoiceEmail(int invoiceId)
        {
            ReturnValue _result = new ReturnValue();

            string MailContent = "";

            string _emailTo = "";

            System.Globalization.NumberFormatInfo nfi = Utilities.CurrentNumberFormat;

            Model.TProgram_EmailTF _tProgram_Email = new TProgram_EmailTF();

            try
            {
                #region generate Email Content

                TCustomer _tCustomer = new TCustomer();
                _result = _tCustomer.getCustomerByInvoiceId(invoiceId);
                if (_result.Success == false)
                {
                    return _result;
                }
                _tCustomer = _result.Object as TCustomer;


                _result = _tProgram_Email.getEmailTemplate("ShipInvoices");
                if (_result.Success == false)
                {
                    return _result;
                }
                _tProgram_Email = _result.Object as Model.TProgram_EmailTF;

                string _mailHtmlContent = System.Web.HttpUtility.HtmlDecode(System.Web.HttpUtility.HtmlDecode(_tProgram_Email.FullHtml));


                MailContent = _mailHtmlContent.Replace("[CustomerName]", _tCustomer.FirstName);

                EmailMessage _mail = new EmailMessage();

                _mail.HtmlPart = new HtmlAttachment(MailContent);
                _mail.FromAddress = new EmailAddress(_tProgram_Email.RespondTo);
                _mail.Subject = _tProgram_Email.Subject;



                #endregion

                #region setup EmailMessage

                if (Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsTestMode"].ToString()) == true)
                {
                    string[] maillist = Common.TestMailTo.Split(';');
                    foreach (string _item in maillist)
                    {
                        _mail.AddToAddress(new EmailAddress(_item));

                    }

                    _emailTo = Common.TestMailTo;
                }
                else
                {
                    if (string.IsNullOrEmpty(_tCustomer.Email) == true)
                    {
                        _result.Success = false;
                        _result.ErrMessage = "Email To Address is empty";
                        return _result;
                    }
                    else
                    {
                        string[] bcclist = _tCustomer.Email.Split(';');
                        foreach (string _item in bcclist)
                        {
                            if (string.IsNullOrEmpty(_item) == false)
                            {
                                _mail.AddToAddress(new EmailAddress(_item));
                            }
                        }
                    }

                    _emailTo = _tCustomer.Email;
                }


                if (string.IsNullOrEmpty(_tProgram_Email.BccAddress) == false)
                {
                    string[] bcclist = _tProgram_Email.BccAddress.Split(';');
                    foreach (string _item in bcclist)
                    {
                        if (string.IsNullOrEmpty(_item) == false)
                        {
                            _mail.AddBccAddress(new EmailAddress(_item));
                        }
                    }
                }
                if (string.IsNullOrEmpty(_tProgram_Email.CCAddress) == false)
                {
                    string[] bcclist = _tProgram_Email.CCAddress.Split(';');
                    foreach (string _item in bcclist)
                    {
                        if (string.IsNullOrEmpty(_item) == false)
                        {
                            _mail.AddCcAddress(new EmailAddress(_item));
                        }
                    }
                }

                if (string.IsNullOrEmpty(_tCustomer.InvoiceEmail) == false)
                {
                    _mail.AddCcAddress(new EmailAddress(_tCustomer.InvoiceEmail));
                }

                if (string.IsNullOrEmpty(_tCustomer.SalesRepEmail) == false)
                {
                    _mail.AddBccAddress(new EmailAddress(_tCustomer.SalesRepEmail));
                }


                #endregion

                #region attached pdf

                string pdffilename = "";


                TecnifibreInvoicePDF TecnifibreInvoicePDF = new TecnifibreInvoicePDF();
                _result = TecnifibreInvoicePDF.PrintInvoice(invoiceId);
                if (_result.Success == false)
                {
                    return _result;
                }

                pdffilename = "Invoice/TFInvoice_" + invoiceId.ToString() + ".pdf";




                FileAttachment fileAttachment = new FileAttachment(new FileInfo(pdffilename));

                fileAttachment.ContentType = "application/pdf";

                _mail.AddMixedAttachment(fileAttachment);

                #endregion

              

            }
            catch (Exception ex)
            {
                _result.Success = false;
                _result.ErrMessage = ex.ToString();
            }


            #region Log App_Log_Mail
            App_Log_Mail _app_Log_Mail = new App_Log_Mail();
            // _app_Log_Mail.ProgramId = this.Owner.DefaultProgram.ProgramId;
            _app_Log_Mail.IsTest = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsTestMode"].ToString());
            _app_Log_Mail.CreatedOn = System.DateTime.Now;
            _app_Log_Mail.AddressFrom = _tProgram_Email.RespondTo;
            _app_Log_Mail.AddressTo = _emailTo;
            _app_Log_Mail.Subject = _tProgram_Email.Subject;
            _app_Log_Mail.Content = MailContent;

            _app_Log_Mail.OId = invoiceId.ToString();
            _app_Log_Mail.Success = _result.Success;
            _app_Log_Mail.Notes = _result.ErrMessage;
            _app_Log_Mail.Type = "ShippingInvoice";
            _app_Log_Mail.Save();

            #endregion


            return _result;
        }

        


    }
}
