using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace WComm
{
    public class ConInfo
    {

        #region Save in Cookiee for static var
        private static string _url;
        /// <summary>
        /// save in cookiee
        /// </summary>
        public static string Url
        {
            set
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    _state["URL"] = value;
                }
                else
                {
                    _url = value;
                }
            }
            get
            {
                string _result;
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    _result = _state["URL"];

                }
                else
                {
                    _result = _url;
                }
                return _result;
            }
        }

        private static string _user;
        /// <summary>
        /// save in cookiee
        /// </summary>
        public static string User
        {
            set
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    _state["ConInfoUser"] = value;
                }
                else
                {
                    _user = value;
                }
            }
            get
            {
                string _result;
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    _result = _state["ConInfoUser"];

                }
                else
                {
                    _result = _user;
                }
                return _result;
            }
        }

        private static int _programId;
        /// <summary>
        /// save in cookiee
        /// </summary>
        public static int ProgramId
        {
            set
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    _state["ConInfoProgramId"] = value.ToString ();
                }
                else
                {
                    _programId = value;
                }
            }
            get
            {
                int _result=0;
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    try
                    {
                        _result = int.Parse(_state["ConInfoProgramId"]);
                    }
                    catch { };

                }
                else
                {
                    _result = _programId;
                }
                return _result;
            }
        }

        private static string _connectionString;
        public static string ConnectionString
        {
            set
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    _state["ConInfoConnectionString"] = value.ToString();
                }
                else
                {
                    _connectionString = value;
                }
            }
            get
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    return _state["ConInfoConnectionString"];
                }
                else
                {
                    return _connectionString;
                }
                return _connectionString;
            }
        }
        #endregion

        #region Save in HttpContext for internal using
        private static WComm.Transaction _transcation;
        public static WComm.Transaction Transcation
        {
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items["ErrorTransaction"] = value;
                }
                else
                {
                    _transcation = value;
                }
            }
            get
            {
                WComm.Transaction _result ;

                if (HttpContext.Current != null)
                {
                    _result = HttpContext.Current.Items["ErrorTransaction"] as WComm.Transaction;
                }
                else
                {
                    _result = _transcation;
                }
                return _result;
            }
        }

        private static string _dataConnectProviders;
        public static string DataConnectProviders
        {
            set
            {
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.Items["DataConnectProviders"] = value;
                }
                else
                {
                    _dataConnectProviders = value;
                }
            }
            get
            {
                string _result="";

                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items["DataConnectProviders"] != null)
                    {
                        _result = HttpContext.Current.Items["DataConnectProviders"].ToString();
                    }
                }
                else
                {
                    _result = _dataConnectProviders;
                }
                return _result;
            }
        }

        #endregion
    }
}

