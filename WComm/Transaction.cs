using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Web;
using System.Web.Caching;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Transactions;
using System.IO;
using Npgsql;

namespace WComm
{
    public class Transaction
    {
        private DbConnection dbConnect;
        internal  DbConnection DbConnect
        {
            get
            {
                return dbConnect;
            }
        }


        //private CommittableTransaction transaction;

        private System.Data.Common .DbTransaction  _transaction;
        internal System.Data.Common.DbTransaction ITransaction
        {
            get
            {
                return _transaction;
            }
        }
        private bool _inTrans = false;

        public Transaction(string dataConnectProviders)
        {
            createTransaction(dataConnectProviders);
            ConInfo.Transcation = this;

        }

        public Transaction()
        {
            createTransaction(null);
            ConInfo.Transcation = this;
        }

        private void createTransaction(string dataConnectProviders)
        {
            Connector _connector = new Connector(dataConnectProviders);

            if (String.IsNullOrEmpty(_connector.ConnectionString))
            {
                _connector.ConnectionString = "zstoreConnectionString";
            }
            string conString = "";
            if (_connector.RegistryKeyValue.ToLower() != "config" && _connector.RegistryKeyValue.ToUpper() != "CONNECTIONVAR")
            {
                conString = RegistryAccess.getRegistryKeyValue(_connector.RegistryKeyValue, _connector.ConnectionString);
            }
            else
            {
                conString = _connector.ConnectionString;
            }
            try
            {
                if (_connector.ProviderType == "PostgreSQL")
                {
                    NpgsqlConnection conn = new NpgsqlConnection(_connector.ConnectionString);
                    conn.Open();

                    dbConnect = conn;

                    Npgsql.NpgsqlTransaction _trans = conn.BeginTransaction();

                    _transaction = _trans;

                    _inTrans = true;
                }
                else
                {
                    DbProviderFactory dbFactory = DbProviderFactories.GetFactory(_connector.ProviderType);
                    dbConnect = dbFactory.CreateConnection();
                    dbConnect.ConnectionString = conString;
                    dbConnect.Open();


                    _transaction = dbConnect.BeginTransaction();
                    //transaction = new CommittableTransaction();
                    //dbConnect.EnlistTransaction(transaction);
                    _inTrans = true;
                }
            }
            catch (Exception ex)
            {
                this.SentEmail(conString, ex.ToString());
            }
        }
      
        public ReturnValue CommitTransaction()
        {
            ReturnValue _result = new ReturnValue();

            if (dbConnect == null || !_inTrans)
            {
                throw new DBException("No open transaction exists.", new Exception(""));
            }

            try
            {
                _transaction.Commit();
                _transaction = null;
                _inTrans = false;

                ConInfo.Transcation = null;
            }
            catch (Exception ex)
            {
                RollbackTransaction();

                string detail = "Exception occurred while trying to commit transaction.\n"
                    + "See inner exception information for details.";
                this.SentEmail(detail, ex.ToString());

                _result.Code = 1021;
                _result.Success = false;
                _result.ErrMessage = "Exception occurred while trying to commit transaction.\n"
                    + "See inner exception information for details.------" + ex.ToString();
            }
            finally
            {
                if (dbConnect != null && dbConnect.State != ConnectionState.Closed)
                    dbConnect.Close();
            }


            return _result;
        }

        public void RollbackTransaction()
        {
            if (dbConnect == null)
            {
                return;
            }
            else if (dbConnect.State != System.Data.ConnectionState.Open || _transaction == null)
            {
                return;
            }

            try
            {
                _transaction.Rollback();
                _transaction = null;
                _inTrans = false;

                ConInfo.Transcation = null;
            }
            catch (Exception ex)
            {
                string detail = "Exception occurred while trying to rollback transaction.\n"
                    + "See inner exception information for details.";
                this.SentEmail(detail, ex.ToString());
            }
            finally
            {
                if (dbConnect != null && dbConnect.State != ConnectionState.Closed)
                    dbConnect.Close();
            }
        }

        public bool InTransaction
        {
            get
            {
                if (_transaction == null)
                    return false;
                else
                    return _inTrans;
            }
        }


        private void SentEmail(string conString,string notes)
        {
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


            _mail.Subject = "Exception Detected [SQL Server Connection Error !!!]" + WComm.Utilities.urlPath;


            string _notes = "";

          
            string _url = "This is not Website.";
            try
            {
                _url = WComm.Utilities.urlPath;
            }
            catch { }



            _notes = _notes + "Server:" + System.Environment.MachineName + "\r\n";
            _notes = _notes + "URL:" + _url + "\r\n";
            _notes = _notes + "DATE:" + System.DateTime.Now.ToString() + "\r\n";
            _notes = _notes += "DETAILS:" + "\r\n";
            _notes = _notes + notes + "\r\n\r\n";
            _notes = _notes += "This is a CONFIDENTIAL email. Do not reply to this email.";


            _mail.BodyText = _notes;
            string _mailresult = _mail.Send();

            string path = "App_Log_Mail_Result.log";
            string _message = "Date:" + System.DateTime.Now.ToString() + "\r\n" +
                             "Address From:" + _mail.Address_From + "\r\n" +
                             "Address To:" + _mail.Address_To + "\r\n" +
                             "Address Bcc:" + _mail.Bcc + "\r\n" +
                             "Subject:" + _mail.Subject + "\r\n" +
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
