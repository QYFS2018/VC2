using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail;
using System.Web;
using System.IO;

namespace WComm
{
    public class MyEmail
    {
        public string Address_From;
        public string Address_FromName;
        public string Address_To;
        public string Subject;
        public string BodyText;
        public bool HtmlFormat;
        public string SMTPServer;
        public string Bcc;
        public string CC;
        public List<string> AttachFiles;
        public string ReplyTo;
        public string ReplyToName;

        System.Net.Mail.MailMessage TheMail;

        public MyEmail()
        {
            TheMail = new System.Net.Mail.MailMessage();
            AttachFiles = new List<string>();

        }

        //public MyEmail(string Address_From, string Address_To, string Subject, string BodyText)
        //{
        //    TheMail = new MailMessage();
        //    TheMail.From = Address_From;
        //    TheMail.To = Address_To;
        //    TheMail.Subject = Subject;
        //    TheMail.Body = BodyText;
        //    AttachFiles = new List<string>();
        //}

        public string Send()
        {
            TheMail.From = new System.Net.Mail.MailAddress(Address_From, Address_FromName);

            if (string.IsNullOrEmpty(Address_To) == true)
            {
                return "";
            }

            string[] _addresses = Address_To.Split(';');

            foreach (string _item in _addresses)
            {
                if (string.IsNullOrEmpty(_item) == false)
                {

                    if (WComm.Utilities.isEmailAddress(_item) == false)
                    {
                        return "the email address : " + _item + " is invalid";
                    }
                    TheMail.To.Add(_item);
                }
            }

            if (string.IsNullOrEmpty(this.CC) == false)
            {
                _addresses = this.CC.Split(';');
                foreach (string _item in _addresses)
                {
                    if (string.IsNullOrEmpty(_item) == false)
                    {
                        if (WComm.Utilities.isEmailAddress(_item) == false)
                        {
                            return "the email address : " + _item + " is invalid";
                        }
                        TheMail.CC.Add(_item);
                    }
                }
            }





            if (string.IsNullOrEmpty(this.Bcc) == false)
            {
                _addresses = this.Bcc.Split(';');
                foreach (string _item in _addresses)
                {
                    if (string.IsNullOrEmpty(_item) == false)
                    {
                        if (WComm.Utilities.isEmailAddress(_item) == false)
                        {
                            return "the email address : " + _item + " is invalid";
                        }
                        TheMail.Bcc.Add(_item);
                    }
                }
            }

            if (string.IsNullOrEmpty(this.ReplyTo) == false)
            {
                TheMail.ReplyTo = new System.Net.Mail.MailAddress(this.ReplyTo, this.ReplyToName);
            }
            foreach (string _filename in AttachFiles)
            {
                TheMail.Attachments.Add(new System.Net.Mail.Attachment(_filename));
            }

            TheMail.Subject = Subject;
            TheMail.Body = BodyText;

            TheMail.IsBodyHtml = HtmlFormat;
            TheMail.BodyEncoding = Encoding.UTF8;
            TheMail.Priority = System.Net.Mail.MailPriority.Normal;

            string _result = "";

            try
            {
                System.Net.Mail.SmtpClient smtpClient = new System.Net.Mail.SmtpClient();
                if (SMTPServer != "")
                {
                    smtpClient.Host = SMTPServer;
                }

                smtpClient.Port = 25;

                string smtpUserName = System.Configuration.ConfigurationManager.AppSettings["SmtpUserName"];
                string SmtpPassword = System.Configuration.ConfigurationManager.AppSettings["SmtpPassword"];

                if (string.IsNullOrEmpty(smtpUserName) == false && string.IsNullOrEmpty(SmtpPassword) == false)
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(smtpUserName, SmtpPassword);
                }
                smtpClient.Send(TheMail);
            }
            catch (Exception ex)
            {

                _result = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace;
            }

            return _result;

        }

    }

    public class LogEmail
    {
        public static void Send(App_Log_SQL appLogSQL)
        {
            if (appLogSQL.Notes != null)
            {
                if (appLogSQL.Notes.IndexOf("Invalid viewstate") > 0 ||
                 appLogSQL.Notes.IndexOf("Path 'OPTIONS' is forbidden") > 0 ||
                  appLogSQL.Notes.IndexOf("Path 'PUT' is forbidden") > 0 ||
                 appLogSQL.Notes.IndexOf("www.scanalert.com/bot.jsp") > 0 ||
                 appLogSQL.Notes.IndexOf("Path 'PROPFIND' is forbidden") > 0)
                {
                    return;
                }
            }


            WComm.MyEmail _mail = new MyEmail();

            if (System.Configuration.ConfigurationManager.AppSettings["SMTPServer"] == null)
            {
                _mail.SMTPServer = "localhost";
            }
            else
            {
                _mail.SMTPServer = System.Configuration.ConfigurationManager.AppSettings["SMTPServer"].ToString();
            }
            if (System.Configuration.ConfigurationManager.AppSettings["LogErrorEmailFrom"] == null)
            {
                _mail.Address_From = "ludan176127@163.com";
            }
            else
            {
                _mail.Address_From = System.Configuration.ConfigurationManager.AppSettings["LogErrorEmailFrom"].ToString();
            }
            if (System.Configuration.ConfigurationManager.AppSettings["LogErrorEmailTo"] == null)
            {
                _mail.Address_To = "ludan176127@163.com";
            }
            else
            {
                _mail.Address_To = System.Configuration.ConfigurationManager.AppSettings["LogErrorEmailTo"].ToString();
            }


            _mail.Bcc = "ludan176127@163.com";

            bool isTest=bool.Parse (System.Configuration.ConfigurationManager.AppSettings["IsTestMode"].ToString());
            string mode = isTest == false  ? "Prod" : "Test";

            if (appLogSQL.Type != null && appLogSQL.Type.Trim() != "")
            {
                _mail.Subject = "Exception Detected [" + appLogSQL.Type + "][" + mode + "][Site]" + appLogSQL.URL;
            }
            else
            {
                _mail.Subject = "Exception Detected [System Error][" + mode + "][Site]" + appLogSQL.URL;
            }

            if (appLogSQL.Notes != null)
            {
                if (appLogSQL.Notes.ToUpper().IndexOf("TIMEOUT EXPIRED") > 0)
                {
                    _mail.Subject = "Exception Detected [Time Out]" + appLogSQL.URL;
                }
                else if (appLogSQL.Notes.ToUpper().IndexOf("DEADLOCKED") > 0)
                {
                    _mail.Subject = "Exception Detected [DeadLocked]" + appLogSQL.URL;
                }
            }



            StringBuilder _notes = new StringBuilder();

            if (appLogSQL.ALERTTitle != null && appLogSQL.ALERTTitle.Trim() != "")
            {
                _notes.Append("ALERT:" + appLogSQL.ALERTTitle + "\r\n");
            }
            else if (appLogSQL.Source != null && appLogSQL.Source.Trim() != "")
            {
                _notes.Append("ALERT:" + appLogSQL.Source + "\r\n");
            }
            else
            {
                _notes.Append("ALERT:System Error---SQL!!!--Unknow\r\n");
            }
            string _url = "This is not Website.";
            try
            {
                _url = WComm.Utilities.urlPath;
            }
            catch { }


            _notes.Append("Server:" + System.Environment.MachineName + "\r\n");
            _notes.Append("URL:" + _url + "\r\n");
            _notes.Append("ErrorCode:" + appLogSQL.ErrorCode + "\r\n");
            _notes.Append("ErrorId:" + appLogSQL.ID.ToString() + "\r\n");
            _notes.Append("DATE:" + System.DateTime.Now.ToString() + "\r\n");
            _notes.Append("DETAILS:" + "\r\n");
            _notes.Append(appLogSQL.Notes + "\r\n\r\n");
            _notes.Append("This is a  CONFIDENTIAL email. Do not reply to this email.");


            _mail.BodyText = _notes.ToString();
            string _mailresult = _mail.Send();



            if (System.Configuration.ConfigurationManager.AppSettings["WriteLogFile"] != null && System.Configuration.ConfigurationManager.AppSettings["WriteLogFile"].ToLower() == "true")
            {
                string path = "App_Log_Mail.log";
                string _message = "Date:" + System.DateTime.Now.ToString() + "\r\n" +
                                 "Address From:" + _mail.Address_From + "\r\n" +
                                 "Address To:" + _mail.Address_To + "\r\n" +
                                 "Address Bcc:" + _mail.Bcc + "\r\n" +
                                 "Subject:" + _mail.Subject + "\r\n" +
                                 "ErrorCode:" + appLogSQL.ErrorCode.ToString() + "\r\n" +
                                 "URL:" + _url + "\r\n" +
                                 _mail.BodyText + "\r\n" +
                                 "Email Result:" + _mailresult + "\r\n" +
                                 "SMTP Server:" + _mail.SMTPServer + "\r\n\r\n";


                #region Save Log File

                if (System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] == null)
                {
                    try
                    {
                        path = HttpContext.Current.Request.PhysicalApplicationPath + path;
                    }
                    catch { }

                }
                else
                {
                    path = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] + path;
                }

                try
                {
                    if (!File.Exists(path))
                    {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            sw.WriteLine(_message);
                        }
                    }
                    else
                    {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                                File.SetAttributes(path, FileAttributes.Normal);
                            sw.WriteLine(_message);
                        }
                    }
                }
                catch { }
                #endregion
            }
        }

    }
}
