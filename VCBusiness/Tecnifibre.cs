using System;
using System.Collections.Generic;
using System.Text;
using WComm;
using DotNetOpenMail;
using System.IO;
using VCBusiness.Model;

namespace VCBusiness
{
    public class Tecnifibre
    {
        public ReturnValue DueInvoices()
        {
            ReturnValue _result = new ReturnValue();

            #region get TProgram_Email

            TProgram_EmailTF _tProgram_Email = new TProgram_EmailTF();
            _result = _tProgram_Email.getEmailTemplate("DueInvoices");
            if (_result.Success == false)
            {

                Common.Log("getProgram_InfoList---ER \r\n" + _result.ErrMessage);

                Common.SentAlterEmail(1, _result.ErrMessage);

                return _result;
            }
            else
            {
                Common.Log("getProgram_InfoList---OK");
            }


            _tProgram_Email = _result.Object as TProgram_EmailTF;

            #endregion


            #region get Invoices

            TInvoice _ttInvoice = new TInvoice();
            _result = _ttInvoice.getDueInvoiceList();
            if (_result.Success == false)
            {

                Common.Log("getDueInvoiceList---ER \r\n" + _result.ErrMessage);

                Common.SentAlterEmail(1, _result.ErrMessage);

                return _result;
            }
            else
            {
                Common.Log("getDueInvoiceList---OK");
            }


            #endregion

            EntityList _list = _result.ObjectList;

            int _successfulRecord = 0;
            int _failedRecord = 0;
            string _errorNotes = "";

            #region sent email

            foreach (TInvoice _item in _list)
            {
                Common.Log("Invoice : " + _item.InvoiceId.ToString() + "-Start");

                string _content = System.Web.HttpUtility.HtmlDecode(_tProgram_Email.FullHtml);

                if (string.IsNullOrWhiteSpace(_content) == true)
                {
                    Common.Log("Invoice : " + _item.InvoiceId.ToString() + "-Skip");

                    continue;
                }

                TProgram_EmailTF _email = _tProgram_Email.Clone() as TProgram_EmailTF;



                DateTime dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime dt3 = new DateTime(_item.DueDate.Year, _item.DueDate.Month, _item.DueDate.Day);

                int _aging = dt1.Subtract(dt3).Days;

                _content = _content.Replace("[CustomerName]", _item.CustomerName)
                .Replace("[Aging]", (_aging * -1).ToString())
                .Replace("[InvoiceId]", _item.InvoiceId.ToString())
                .Replace("[DueDate]", _item.DueDate.ToString("MM/dd/yyyy"));

                _email.ToAddress = _item.Email;
                _email.FullHtml = _content;
                _email.CCAddress = _email.CCAddress + ";" + _item.InvoiceEmail;
                _email.BccAddress = _email.BccAddress + ";" + _item.SalesRepEmail;

                #region attached pdf


                TecnifibreInvoicePDF TecnifibreInvoicePDF = new TecnifibreInvoicePDF();
                _result = TecnifibreInvoicePDF.PrintInvoice(_item.InvoiceId);
                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "Invoice : " + _item.InvoiceId.ToString() + "- TFInvoicePDFGenerator ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- TFInvoicePDFGenerator ---ER \r\n" + _result.ErrMessage);

                    continue;
                }



                string pdffilename = "Invoice/TFInvoice_" + _item.InvoiceId.ToString() + ".pdf";

                FileAttachment fileAttachment = new FileAttachment(new FileInfo(pdffilename));

                fileAttachment.ContentType = "application/pdf";



                #endregion


                _result = SentEmail(_email, EmailFormat.Html, _item.InvoiceId, fileAttachment);

                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---ER \r\n" + _result.ErrMessage);

                    continue;
                }


                _item.DueInvoicesEmailOn = System.DateTime.Now;
                _result = _item.Update();
                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---ER \r\n" + _result.ErrMessage);

                    continue;
                }


                _successfulRecord++;
                Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---OK");


            }

            #endregion


            if (_failedRecord != 0)
            {
                Common.SentAlterEmail(_failedRecord, _errorNotes);
            }


            return _result;
        }

        public ReturnValue PastDue()
        {
            ReturnValue _result = new ReturnValue();

            #region get TProgram_Email

            TProgram_EmailTF _tProgram_Email = new TProgram_EmailTF();
            _result = _tProgram_Email.getEmailTemplate("10DaysPastDue");
            if (_result.Success == false)
            {

                Common.Log("getProgram_InfoList---ER \r\n" + _result.ErrMessage);

                Common.SentAlterEmail(1, _result.ErrMessage);

                return _result;
            }
            else
            {
                Common.Log("getProgram_InfoList---OK");
            }


            _tProgram_Email = _result.Object as TProgram_EmailTF;

            #endregion


            #region get Invoices

            TInvoice _ttInvoice = new TInvoice();
            _result = _ttInvoice.getPastDueInvoiceList();
            if (_result.Success == false)
            {

                Common.Log("getPastDueInvoiceList---ER \r\n" + _result.ErrMessage);

                Common.SentAlterEmail(1, _result.ErrMessage);

                return _result;
            }
            else
            {
                Common.Log("getPastDueInvoiceList---OK");
            }


            #endregion

            EntityList _list = _result.ObjectList;

            int _successfulRecord = 0;
            int _failedRecord = 0;
            string _errorNotes = "";

            #region sent email

            foreach (TInvoice _item in _list)
            {
                string _content = System.Web.HttpUtility.HtmlDecode(_tProgram_Email.FullHtml);

                TProgram_Email _email = _tProgram_Email.Clone() as TProgram_Email;



                DateTime dt1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                DateTime dt3 = new DateTime(_item.DueDate.Year, _item.DueDate.Month, _item.DueDate.Day);
                int _aging = dt1.Subtract(dt3).Days;

                _content = _content.Replace("[customeraccount]", _item.CustomerAccount)
                .Replace("[OrderDate]", _item.OrderDate.ToString("MM/dd/yyyy"))
                .Replace("[aging]", _aging.ToString())
                .Replace("[invoiceNo]", _item.InvoiceId.ToString())
                .Replace("[duedate]", _item.DueDate.ToString("MM/dd/yyyy"));

                _email.ToAddress = _item.Email;
                _email.FullHtml = _content;
                _email.CCAddress = _email.CCAddress + ";" + _item.InvoiceEmail;
                _email.BccAddress = _email.BccAddress + ";" + _item.SalesRepEmail;


                #region attached pdf

                TecnifibreInvoicePDF TecnifibreInvoicePDF = new TecnifibreInvoicePDF();
                _result = TecnifibreInvoicePDF.PrintInvoice(_item.InvoiceId);
                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "Invoice : " + _item.InvoiceId.ToString() + "- TFInvoicePDFGenerator ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- TFInvoicePDFGenerator ---ER \r\n" + _result.ErrMessage);

                    continue;
                }



                string pdffilename = "Invoice/TFInvoice_" + _item.InvoiceId.ToString() + ".pdf";

                FileAttachment fileAttachment = new FileAttachment(new FileInfo(pdffilename));

                fileAttachment.ContentType = "application/pdf";



                #endregion



                _result = SentEmail(_email, EmailFormat.Html, _item.InvoiceId, fileAttachment);

                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "Invoice : " + _item.InvoiceId.ToString() + "- PastDue ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- PastDue ---ER \r\n" + _result.ErrMessage);

                    continue;
                }



                _item.PastDueEmailOn = System.DateTime.Now;
                _result = _item.Update();
                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---ER \r\n" + _result.ErrMessage);

                    continue;
                }


                _successfulRecord++;
                Common.Log("Invoice : " + _item.InvoiceId.ToString() + "- DueInvoices ---OK");


            }

            #endregion


            if (_failedRecord != 0)
            {
                Common.SentAlterEmail(_failedRecord, _errorNotes);
            }


            return _result;
        }

        public ReturnValue CancelHDOrder()
        {
            ReturnValue _result = new ReturnValue();

            #region get orders

            TInvoice _ttInvoice = new TInvoice();
            _result = _ttInvoice.getHDOrderList();
            if (_result.Success == false)
            {

                Common.Log("getHDOrderList---ER \r\n" + _result.ErrMessage);

                Common.SentAlterEmail(1, _result.ErrMessage);

                return _result;
            }
            else
            {
                Common.Log("getHDOrderList---OK");
            }

            EntityList _list = _result.ObjectList;

            int _successfulRecord = 0;
            int _failedRecord = 0;
            string _errorNotes = "";


            foreach (TOrder _item in _list)
            {
                Transaction _transaction = new Transaction();

                _item.StatusCode = "CN";

                _result = _item.Update(_transaction);
                if (_result.Success == false)
                {
                    _transaction.RollbackTransaction();
                    _failedRecord++;
                    _errorNotes = _errorNotes + "Order : " + _item.OrderId.ToString() + "- Cancel ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("Order : " + _item.OrderId.ToString() + "- Cancel ---ER \r\n" + _result.ErrMessage);

                    continue;
                }



                _transaction.CommitTransaction();
                _successfulRecord++;

                Common.Log("Order : " + _item.OrderId.ToString() + "- Cancel ---OK");
            }

            #endregion

            if (_failedRecord != 0)
            {
                Common.SentAlterEmail(_failedRecord, _errorNotes);
            }


            return _result;
        }

        public ReturnValue WishList()
        {
            ReturnValue _result = new ReturnValue();

            #region get TProgram_Email

            TProgram_EmailTF _tProgram_Email = new TProgram_EmailTF();
            _result = _tProgram_Email.getEmailTemplate("WishList");
            if (_result.Success == false)
            {

                Common.Log("getProgram_InfoList---ER \r\n" + _result.ErrMessage);

                Common.SentAlterEmail(1, _result.ErrMessage);

                return _result;
            }
            else
            {
                Common.Log("getProgram_InfoList---OK");
            }


            _tProgram_Email = _result.Object as TProgram_EmailTF;

            #endregion


            #region get WishList

            TWishList _tWishList = new TWishList();
            _result = _tWishList.getWishList();
            if (_result.Success == false)
            {

                Common.Log("getWishList---ER \r\n" + _result.ErrMessage);

                Common.SentAlterEmail(1, _result.ErrMessage);

                return _result;
            }
            else
            {
                Common.Log("getWishList---OK");
            }


            #endregion

            EntityList _list = _result.ObjectList;

            int _successfulRecord = 0;
            int _failedRecord = 0;
            string _errorNotes = "";

            #region sent email

            foreach (TWishList _item in _list)
            {
                string _content = System.Web.HttpUtility.HtmlDecode(_tProgram_Email.FullHtml);

                TProgram_Email _email = _tProgram_Email.Clone() as TProgram_Email;


                _content = _content.Replace("[CustomerName]", _item.FirstName + " " + _item.LastName);


                _email.ToAddress = _item.Email;

                _email.CCAddress = _email.CCAddress + ";" + _item.OrderEmail + ";" + _item.SecondaryEmail;
                _email.BccAddress = _email.BccAddress + ";" + _item.SalesRepEmail;





                TWishList_Line_Item _tWishList_Line_Item = new TWishList_Line_Item();
                _result = _tWishList_Line_Item.getWishListLineItemByWishListId(_item.Id);
                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "WishList : " + _item.Id.ToString() + "-getWishListLineItemByWishListId  ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("WishList : " + _item.Id.ToString() + "- getWishListLineItemByWishListId ---ER \r\n" + _result.ErrMessage);

                    continue;
                }
                EntityList _lineItem = _result.ObjectList;

                StringBuilder _sb = new StringBuilder();

                foreach (TWishList_Line_Item _line in _lineItem)
                {
                    _sb.Append("<tr><td style=\"border-right:1px solid #000000;  border-bottom:1px solid #000000;\">" + _line.PartNumber + "</td>" +
                        "<td style=\" border-bottom:1px solid #000000;\">" + _line.Name + "</td></tr>");
                }

                _content = _content.Replace("[ProductList]", _sb.ToString());


                _email.FullHtml = _content;

                _result = SentEmail(_email, EmailFormat.Html, _item.Id, null);

                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "WishList : " + _item.Id.ToString() + "-  ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("WishList : " + _item.Id.ToString() + "-  ---ER \r\n" + _result.ErrMessage);

                    continue;
                }



                _result = _item.updateMailInformedOnByWishListId(_item.Id);
                if (_result.Success == false)
                {
                    _failedRecord++;
                    _errorNotes = _errorNotes + "WishList : " + _item.Id.ToString() + "-  ---ER \r\n" + _result.ErrMessage + "\r\n";

                    Common.Log("WishList : " + _item.Id.ToString() + "-  ---ER \r\n" + _result.ErrMessage);

                    continue;
                }


                _successfulRecord++;
                Common.Log("WishList : " + _item.Id.ToString() + "- DueInvoices ---OK");


            }

            #endregion


            if (_failedRecord != 0)
            {
                Common.SentAlterEmail(_failedRecord, _errorNotes);
            }


            return _result;
        }

        public enum EmailFormat
        {
            Html, Text, Both
        }

        public static ReturnValue SentEmail(TProgram_Email tProgram_Email, EmailFormat emailFormat, int oId, FileAttachment fileAttachment)
        {
            ReturnValue _result = new ReturnValue();

            bool IsTFTestMode = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsTFTestMode"].ToString());

            if (tProgram_Email.ContentStatusId != 1)
            {
                return _result;
            }

            EmailMessage _mail = new EmailMessage();

            if (string.IsNullOrEmpty(tProgram_Email.RespondToName) == true)
            {
                _mail.FromAddress = new EmailAddress(tProgram_Email.RespondTo);
            }
            else
            {
                _mail.FromAddress = new EmailAddress(tProgram_Email.RespondTo, tProgram_Email.RespondToName);
            }


            _mail.Subject = tProgram_Email.Subject;

            if (emailFormat == EmailFormat.Html || emailFormat == EmailFormat.Both)
            {
                _mail.HtmlPart = new HtmlAttachment(tProgram_Email.FullHtml);
            }
            if (emailFormat == EmailFormat.Text || emailFormat == EmailFormat.Both)
            {
                _mail.TextPart = new TextAttachment(tProgram_Email.FullText);
            }


            string _emailto = tProgram_Email.ToAddress;

            if (IsTFTestMode == true)
            {
                if (string.IsNullOrEmpty(Common.TestMailTo) == false)
                {
                    string[] ccList = Common.TestMailTo.Split(';');
                    foreach (string ccAddress in ccList)
                    {
                        _mail.AddToAddress(new EmailAddress(ccAddress));
                    }

                    _emailto = Common.TestMailTo;
                }


            }
            else
            {

                if (string.IsNullOrEmpty(tProgram_Email.CCAddress) == false)
                {
                    string[] ccList = tProgram_Email.CCAddress.Split(';');
                    foreach (string ccAddress in ccList)
                    {
                        if (string.IsNullOrEmpty(ccAddress.Trim()) == false)
                        {
                            _mail.AddCcAddress(new EmailAddress(ccAddress));
                        }
                    }
                }


                if (string.IsNullOrEmpty(tProgram_Email.BccAddress) == false)
                {
                    string[] BccList = tProgram_Email.BccAddress.Split(';');
                    foreach (string bccAddress in BccList)
                    {
                        if (string.IsNullOrEmpty(bccAddress.Trim()) == false)
                        {
                            _mail.AddBccAddress(new EmailAddress(bccAddress));
                        }
                    }
                }
                if (string.IsNullOrEmpty(tProgram_Email.ToAddress) == false)
                {
                    string[] ToAddressList = tProgram_Email.ToAddress.Split(';');
                    foreach (string toAddress in ToAddressList)
                    {
                        if (string.IsNullOrEmpty(toAddress.Trim()) == false)
                        {
                            _mail.AddToAddress(new EmailAddress(toAddress));
                        }
                    }


                }
            }

            if (fileAttachment != null)
            {
                _mail.AddMixedAttachment(fileAttachment);
            }

            App_Log_Mail _mailLog = new App_Log_Mail();

            try
            {
                SmtpServer server = new SmtpServer(System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"].ToString(), 25);

                DotNetOpenMail.SmtpAuth.SmtpAuthToken SmtpAuthToken =
                      new DotNetOpenMail.SmtpAuth.SmtpAuthToken(System.Configuration.ConfigurationSettings.AppSettings["SMTPUserName"].ToString(),
                      System.Configuration.ConfigurationSettings.AppSettings["SMTPPassword"].ToString());
                server.SmtpAuthToken = SmtpAuthToken;

                bool _success = _mail.Send(server);
            
                _mailLog.Success = _success;
                _mailLog.Notes = "";
            }
            catch (Exception ex)
            {
                _mailLog.Success = false;
                _mailLog.Notes = ex.ToString();
            }
            finally
            {

                _mailLog.AddressBcc = "AddressBcc:" + (string.IsNullOrEmpty(tProgram_Email.BccAddress) ? "" : tProgram_Email.BccAddress) + " CCAddress:" + (string.IsNullOrEmpty(tProgram_Email.CCAddress) ? "" : tProgram_Email.CCAddress);
                _mailLog.AddressFrom = tProgram_Email.RespondTo;
                _mailLog.AddressTo = _emailto;


                if (emailFormat == EmailFormat.Html || emailFormat == EmailFormat.Both)
                {
                    _mailLog.Content = tProgram_Email.FullHtml;
                }
                if (emailFormat == EmailFormat.Text || emailFormat == EmailFormat.Both)
                {
                    _mailLog.Content = tProgram_Email.FullText;
                }

                _mailLog.CreatedOn = System.DateTime.Now;
                _mailLog.Subject = tProgram_Email.Subject;
                _mailLog.Type = tProgram_Email.EmailTypeId.ToString();
                _mailLog.OId = oId.ToString();
                _mailLog.IsTest = IsTFTestMode;
                _mailLog.Save();
            }

            _result.Success = _mailLog.Success.Value;
            _result.ErrMessage = _mailLog.Notes;

            if (string.IsNullOrEmpty(_mailLog.AddressTo) == true)
            {
                _result.Success = true;
            }

            return _result;
        }

    }



}
