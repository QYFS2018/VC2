using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Net.Mail;
namespace WComm
{
    public class GlobalErrorHandler
    {

        private string _strViewstate = string.Empty;

        private const string _strViewstateKey = "__VIEWSTATE";


        public GlobalErrorHandler()
        {
        }

        public string ExceptionToString(Exception ex)
        {
            StringBuilder sb = new StringBuilder(4096);
            try
            {

                sb.Append(SysInfoToString());
                sb.Append(Environment.NewLine);
                sb.Append(ex.GetType().FullName);
                sb.Append(Environment.NewLine);
                sb.Append(EnhancedStackTrace(new StackTrace(ex, true)));
                sb.Append(Environment.NewLine);
                sb.Append(GetASPSettings());
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);
            return sb.ToString();
        } 

        private  string StackFrameToString(StackFrame sf)
        {
            StringBuilder sb = new StringBuilder(4096);
            int intParam;
            MemberInfo mi = sf.GetMethod();
            sb.Append(" ");
            sb.Append(mi.DeclaringType.Namespace);
            sb.Append(".");
            sb.Append(mi.DeclaringType.Name);
            sb.Append(".");
            sb.Append(mi.Name);
            sb.Append("(");
            intParam = 0;
            foreach (ParameterInfo param in sf.GetMethod().GetParameters())
            {
                intParam += 1;
                if (intParam > 1)
                {
                    sb.Append(", ");
                }
                sb.Append(param.Name);
                sb.Append(" As ");
                sb.Append(param.ParameterType.Name);
            }
            sb.Append(")");
            sb.Append(Environment.NewLine);
            sb.Append(" ");
            if (sf.GetFileName() == null || sf.GetFileName().Length == 0)
            {
                sb.Append("(unknown file)");
                sb.Append(": N ");
                sb.Append(string.Format("{0:#00000}", sf.GetNativeOffset()));
            }
            else
            {
                sb.Append(System.IO.Path.GetFileName(sf.GetFileName()));
                sb.Append(": line ");
                sb.Append(string.Format("{0:#0000}", sf.GetFileLineNumber()));
                sb.Append(", col ");
                sb.Append(string.Format("{0:#00}", sf.GetFileColumnNumber()));
                if (sf.GetILOffset() != StackFrame.OFFSET_UNKNOWN)
                {
                    sb.Append(", IL ");
                    sb.Append(string.Format("{0:#0000}", sf.GetILOffset()));
                }
            }
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }

        private string EnhancedStackTrace(StackTrace st)
        {
            StringBuilder sb = new StringBuilder(4096);

            sb.Append(Environment.NewLine);
            sb.Append("---- Stack Trace ----");
            sb.Append(Environment.NewLine);

            for (int intFrame = 0; intFrame <= st.FrameCount - 1; intFrame++)
            {
                StackFrame sf = st.GetFrame(intFrame);
                MemberInfo mi = sf.GetMethod();
                {
                    sb.Append(StackFrameToString(sf));
                }
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        private string GetASPSettings()
        {
            string strSuppressKeyPattern = "^ALL_HTTP|^ALL_RAW|VSDEBUGGER";

            StringBuilder sb = new StringBuilder(4096);

            sb.Append("---- ASP.NET Collections ----");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(HttpVarsToString(HttpContext.Current.Request.QueryString, "QueryString"));
            sb.Append(HttpVarsToString(HttpContext.Current.Request.Form, "Form"));
            sb.Append(HttpVarsToString(HttpContext.Current.Request.Cookies));
            sb.Append(HttpVarsToString(HttpContext.Current.Session));
            sb.Append(HttpVarsToString(HttpContext.Current.Cache));
            sb.Append(HttpVarsToString(HttpContext.Current.Application));
            sb.Append(HttpVarsToString(HttpContext.Current.Request.ServerVariables, "ServerVariables", true, strSuppressKeyPattern));

            return sb.ToString();
        }

        public string HttpVarsToString(HttpCookieCollection c)
        {
            if (c.Count == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder(4096);
            sb.Append("Cookies");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            foreach (string strKey in c)
            {
                if (strKey == "WComm.Core.State" || strKey .StartsWith ("WComm.Core.FrameWork.")==true)
                {
                    NameValueCollection _stateCookieCollection = HttpContext.Current.Request.Cookies[strKey].Values;
                    foreach (string strKey1 in _stateCookieCollection)
                    {
                        AppendLine(sb, strKey + "." + strKey1 + ":", Utilities.UnFransferChar(_stateCookieCollection[strKey1]));
                    }
                }
                else
                {
                    AppendLine(sb, strKey + ":", Utilities.UnFransferChar(c[strKey].Value));
                }
            }

            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        private string HttpVarsToString(HttpApplicationState a)
        {
            if (a.Count == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder(4096);
            sb.Append("Application");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            foreach (string strKey in a)
            {
                AppendLine(sb, strKey, a[strKey]);
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        private string HttpVarsToString(System.Web.Caching.Cache c)
        {
            if (c.Count == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder(4096);
            sb.Append("Cache");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            foreach (DictionaryEntry de in c)
            {
                AppendLine(sb, Convert.ToString(de.Key), de.Value);
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        private string HttpVarsToString(System.Web.SessionState.HttpSessionState s)
        {
            if (s == null || s.Count == 0)
                return string.Empty;

            StringBuilder sb = new StringBuilder(4096);
            sb.Append("Session");
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            foreach (string strKey in s)
            {
                AppendLine(sb, strKey, s[strKey]);
            }
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        private string HttpVarsToString(System.Collections.Specialized.NameValueCollection nvc, string strTitle)
        {
            return HttpVarsToString(nvc, strTitle, false);
        }

        private string HttpVarsToString(System.Collections.Specialized.NameValueCollection nvc, string strTitle, bool blnSuppressEmpty)
        {
            return HttpVarsToString(nvc, strTitle, blnSuppressEmpty, string.Empty);
        }

        private string HttpVarsToString(System.Collections.Specialized.NameValueCollection nvc, string strTitle, bool blnSuppressEmpty, string strSuppressKeyPattern)
        {
            if (!nvc.HasKeys())
                return string.Empty;

            StringBuilder sb = new StringBuilder(4096);
            sb.Append(strTitle);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);

            bool blnDisplay;
            foreach (string strKey in nvc)
            {
                blnDisplay = true;
                if (blnSuppressEmpty)
                    blnDisplay = nvc[strKey] != string.Empty;

                if (strKey == _strViewstateKey)
                {
                    _strViewstate = nvc[strKey];
                    blnDisplay = false;
                }

                if (blnDisplay && strSuppressKeyPattern != string.Empty)
                    blnDisplay = !Regex.IsMatch(strKey, strSuppressKeyPattern);

                if (blnDisplay)
                    AppendLine(sb, strKey, nvc[strKey]);
            }

            sb.Append(Environment.NewLine);

            return sb.ToString();
        }

        private string SysInfoToString()
        {
            StringBuilder sb = new StringBuilder(4096);
            sb.Append("Date and Time: ");
            sb.Append(DateTime.Now);
            sb.Append(Environment.NewLine);
            sb.Append("Machine Name: ");
            try
            {
                sb.Append(Environment.MachineName);
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);
            sb.Append("Process User: ");
            sb.Append(ProcessIdentity());
            sb.Append(Environment.NewLine);
            sb.Append("Remote User: ");
            sb.Append(HttpContext.Current.Request.ServerVariables["REMOTE_USER"]);
            sb.Append(Environment.NewLine);
            sb.Append("Remote Address: ");
            sb.Append(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            sb.Append(Environment.NewLine);
            sb.Append("Remote Host: ");
            sb.Append(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
            sb.Append(Environment.NewLine);
            sb.Append("URL: ");
            sb.Append(WebCurrentUrl());
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append("NET Runtime version: ");
            sb.Append(System.Environment.Version.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("Application Domain: ");
            try
            {
                sb.Append(System.AppDomain.CurrentDomain.FriendlyName);
            }
            catch (Exception e)
            {
                sb.Append(e.Message);
            }
            sb.Append(Environment.NewLine);

            sb.Append(EnhancedStackTrace(new StackTrace(true)));

            return sb.ToString();
        }

        private string ProcessIdentity()
        {
            string strTemp = CurrentWindowsIdentity();
            if (strTemp == string.Empty)
            {
                return CurrentEnvironmentIdentity();
            }
            return strTemp;
        }
        private string CurrentWindowsIdentity()
        {
            try
            {
                return System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private string CurrentEnvironmentIdentity()
        {
            try
            {
                return System.Environment.UserDomainName + @"\" + System.Environment.UserName;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        } 
        private string WebCurrentUrl()
        {
            string strUrl;
            strUrl = "http://" + HttpContext.Current.Request.ServerVariables["server_name"];
            if (HttpContext.Current.Request.ServerVariables["server_port"] != "80")
            {
                strUrl += ":" + HttpContext.Current.Request.ServerVariables["server_port"];
            }
            strUrl += HttpContext.Current.Request.ServerVariables["url"];
            if (HttpContext.Current.Request.ServerVariables["query_string"].Length > 0)
            {
                strUrl += "?" + HttpContext.Current.Request.ServerVariables["query_string"];
            }
            return strUrl;
        }
        private string AppendLine(StringBuilder sb, string Key, object Value)
        {
            string strValue;
            if (Value == null)
            {
                strValue = "(Nothing)";
            }
            else
            {
                try
                {
                    strValue = Value.ToString();
                }
                catch (Exception)
                {
                    strValue = "(" + Value.GetType().ToString() + ")";
                }
            }
            return AppendLine(sb, Key, strValue);
        }
        private string AppendLine(StringBuilder sb, string Key, string strValue)
        {
            sb.Append(string.Format(" {0, -30}{1}", Key, strValue));
            sb.Append(Environment.NewLine);

            return sb.ToString();
        }	

    }
}
