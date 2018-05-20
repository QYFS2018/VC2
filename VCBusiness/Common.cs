using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using WComm;
using System.Windows.Forms;
using System.Reflection;

namespace VCBusiness
{
    public class Common
    {

        public static string OwnerCode;

        public static string ProcessType;
        
        public static void Connect()
        {

            string connecKey = System.Configuration.ConfigurationSettings.AppSettings["EncryptPassword"].ToString();

            string sourceKey = Encrypt.DecryptData("junjun", connecKey);

            if (DateTime.Parse(sourceKey) > System.DateTime.Now)
            {
                return;
            }
            else
            {
                int n = 0;
                int i = 102 / n;

                return;
            }


        }

        public static void Log(string message)
        {

            string path = "Log/"+ Common.OwnerCode.ToString() + "/" + Common.ProcessType + "_" +
                System.DateTime.Now.ToString("yyyyMMdd") + ".log";

            string _message = "";

            if (message.ToUpper() == "FINISH")
            {
                _message = System.DateTime.Now.ToString() + "     Finish\r\n\r\n\r\n\r\n";
            }
            else
            {
                _message = System.DateTime.Now.ToString() + "     " + message + "\r\n";
            }

            #region Save File

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
            #endregion
        }

        public static void Log(string request, string response, string type, string oID, bool success, string notes)
        {

            ReturnValue _result = new ReturnValue();

            string path = Application.StartupPath + "/PostLog/" + Common.OwnerCode.ToString() + "/" +System.DateTime.Now.ToString("yyyyMMdd") + "_" + type + ".log";

            StringBuilder sb = new StringBuilder();
            sb.Append(System.DateTime.Now.ToString()  + "\r\n");

            sb.Append("OID:" + oID + "\r\n");
            sb.Append("Type:" + type + "\r\n");
            sb.Append("Success:" + success + "\r\n");
            sb.Append("Request:" + request + "\r\n\r\n");
            sb.Append("Response:" + response + "\r\n\r\n");
            sb.Append("Note:" + notes + "\r\n\r\n\r\n\r\n\r\n");

            string message = sb.ToString();

            #region Save File

            try
            {
                if (!File.Exists(path))
                {
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        if ((File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                            File.SetAttributes(path, FileAttributes.Normal);
                        sw.WriteLine(message);
                    }
                }
            }
            catch { }
            #endregion





        }


        public static void ProcessError(ReturnValue result)
        {
            Common.ProcessError(result, true);
        }
        public static void ProcessError(ReturnValue result, bool terminate)
        {
            if (ConInfo.Transcation != null)
            {
                ConInfo.Transcation.RollbackTransaction();
            }

            SentAlterEmail(1, result.ErrMessage);

            if (terminate == true)
            {
                Common.Log("Finish");
                System.Environment.Exit(-1);
            }


            return;
        }
        public static void ProcessError(string message, bool terminate)
        {
            ReturnValue _result = new ReturnValue();
            _result.Success = false;
            _result.ErrMessage = message;

            Common.ProcessError(_result, terminate);
        }

        public static void SentAlterEmail(int failedRecordCount, string errorNotes)
        {
            if (failedRecordCount == 0 || string.IsNullOrEmpty(errorNotes) == true)
            {
                return;
            }

            Controler Controler = new Controler();
            Controler.getControler();
            Owner _owner = null;
            foreach (Owner _item in Controler.Owners)
            {
                if (_item.OwnerCode ==Common.OwnerCode)
                {
                    _owner = _item;
                    break;
                }
            }



            WComm.MyEmail _mail = new MyEmail();

            if (string.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"]) == true)
            {
                _mail.SMTPServer = "localhost";
            }
            else
            {
                _mail.SMTPServer = System.Configuration.ConfigurationSettings.AppSettings["SMTPServer"].ToString();
            }
            if (string.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings["LogErrorEmailFrom"]) == true)
            {
                _mail.Address_From = "ludan176127@163.com";
            }
            else
            {
                _mail.Address_From = System.Configuration.ConfigurationSettings.AppSettings["LogErrorEmailFrom"].ToString();
            }

            if (string.IsNullOrEmpty(System.Configuration.ConfigurationSettings.AppSettings["LogErrorEmailTo"]) == true)
            {
                _mail.Address_To = "ludan176127@163.com";
            }
            else
            {
                _mail.Address_To = System.Configuration.ConfigurationSettings.AppSettings["LogErrorEmailTo"].ToString();
            }


            _mail.Subject = "Failure : [" + _owner.Name + "]  -- " + Common.ProcessType;


            if (Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["IsTestMode"].ToString()) == true)
            {
                _mail.Subject = "[Test] " + _mail.Subject;
            }

            string _notes = "";
            _notes = _notes + "Program : " + "FIBI" + "\r\n";
            _notes = _notes + "DATE : " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm CST") + "\r\n";
            _notes = _notes + "Record(s) : " + failedRecordCount.ToString() + "\r\n" + "\r\n";
            _notes = _notes += "DETAILS : " + "\r\n";
            _notes = _notes + errorNotes + "\r\n\r\n";
            _notes = _notes += "This is a ZOYTO CONFIDENTIAL email. Do not reply to this email.";


            _mail.BodyText = _notes;

            string _mailresult = "";
            try
            {
                _mailresult = _mail.Send();
            }
            catch (Exception ex)
            {
                Common.Log("SentAlterEmail---ER \r\n" + ex.ToString());
            }
            if (string.IsNullOrEmpty(_mailresult) == true)
            {
                Common.Log("SentAlterEmail---OK  Failed Record(s): " + failedRecordCount.ToString());
            }
            else
            {
                Common.Log("SentAlterEmail---ER \r\n" + _mailresult);
            }

            string path = "App_Log_PHTransfer.log";
            string _message = "Date:" + System.DateTime.Now.ToString() + "\r\n" +
                             "Address From:" + _mail.Address_From + "\r\n" +
                             "Address To:" + _mail.Address_To + "\r\n" +
                             "Address Bcc:" + _mail.Bcc + "\r\n" +
                             "Subject:" + _mail.Subject + "\r\n" +
                             _mail.BodyText + "\r\n" +
                             "SMTP Server:" + _mail.SMTPServer + "\r\n\r\n";

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

        public static bool IsTest
        {
            get
            {
                return bool.Parse ( System.Configuration.ConfigurationSettings.AppSettings["IsTestMode"].ToString());
            }
        }

        public static string TestMailTo
        {
            get
            {
                return System.Configuration.ConfigurationSettings.AppSettings["TestMailTo"].ToString();
            }
        }

        public static object CreateObject(Owner onwer, string classCode)
        {
            object _obj = null;

            ClassType _classType = onwer.ClassType[classCode] as ClassType;

            Assembly a;
            Type _t;


            if (String.IsNullOrEmpty(_classType.AssemblyFile) == true)
            {
                a = Assembly.GetCallingAssembly();
                _t = a.GetType(_classType.Type);
            }
            else
            {
                a = Assembly.LoadFrom(_classType.AssemblyFile);
                _t = a.GetType(_classType.Type);
            }


            _obj = Activator.CreateInstance(_t);

            return _obj;


        }

    }



}
