using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;
using System.Web;
using System.Configuration;
using System.IO;

namespace WComm
{
	/// <summary>
	///	Application log of handle database.When you post a website 
	///	to get information,the application need save the information 
	///	to database.
	/// </summary>
	[Serializable]
	[BindingClass("App_Log_SQL")]
    public class App_Log_SQL : Entity
	{
		/// <summary>
		///	Constructor of application sql log.
		/// </summary>
		public App_Log_SQL()
		{
            //this.DataConnectProviders = "Log";
			
		}

		#region Basic Property

		private int? _iD;
		private DateTime? _createdOn;
		private string _source;
		private string _sQLText;
		private int? _priority;
		private string _oId;
		private bool? _success;
		private string _notes;
		private int? _programId;
		private string _type;
        private string _tableName;
        private string _sQLType;
        private string _url;
        private string _sessionId;

		/// <summary>
		///		sql log identify.
		/// </summary>
		[BindingField("ID",true)]
		public int? ID
		{
			set
			{
				_iD=value;
			}
			get
			{
				return _iD;
			}
		}
		/// <summary>
		///		the create date of this log.
		/// </summary>
		[BindingField("CreatedOn",true)]
		public DateTime? CreatedOn
		{
			set
			{
				_createdOn=value;
			}
			get
			{
				return _createdOn;
			}
		}
		/// <summary>
		///		the http handler of this handle.
		/// </summary>
		[BindingField("Source",true)]
		public string Source
		{
			set
			{
				_source=value;
			}
			get
			{
				return _source;
			}
		}
		/// <summary>
		///		the sql text of this log
		/// </summary>
		[BindingField("SQLText",true)]
		public string SQLText
		{
			set
			{
				_sQLText=value;
			}
			get
			{
				return _sQLText;
			}
		}
		/// <summary>
		///		Priority.
		/// </summary>
		[BindingField("Priority",true)]
		public int? Priority
		{
			set
			{
				_priority=value;
			}
			get
			{
				return _priority;
			}
		}
		/// <summary>
		///		reference id(such as order id).
		/// </summary>
		[BindingField("OId",true)]
		public string OId
		{
			set
			{
				_oId=value;
			}
			get
			{
				return _oId;
			}
		}
		/// <summary>
		///		the flag of if the handle success.
		/// </summary>
		[BindingField("Success",true)]
		public bool? Success
		{
			set
			{
				_success=value;
			}
			get
			{
				return _success;
			}
		}
		/// <summary>
		///		the extend note of this app log.
		/// </summary>
		[BindingField("Notes",true)]
		public string Notes
		{
			set
			{
				_notes=value;
			}
			get
			{
				return _notes;
			}
		}
		/// <summary>
		///		the program id of this post.
		/// </summary>
		[BindingField("ProgramId",true)]
		public int? ProgramId
		{
			set
			{
				_programId=value;
			}
			get
			{
				return _programId;
			}
		}
		/// <summary>
		///		application log type.
		/// </summary>
        [BindingField("Type", true)]
		public string Type
		{
			set
			{
				_type=value;
			}
			get
			{
				return _type;
			}
		}

        /// <summary>
        ///     The table name.
        /// </summary>
        [BindingField("TableName", true)]
        public string TableName
        {
            set
            {
                _tableName = value;
            }
            get
            {
                return _tableName;
            }
        }

        /// <summary>
        ///     The sql type.
        /// </summary>
        [BindingField("SQLType", true)]
        public string SQLType
        {
            set
            {
                _sQLType = value;
            }
            get
            {
                return _sQLType;
            }
        }
        [BindingField("URL", true)]
        public string URL
        {
            set
            {
                _url = value;
            }
            get
            {
                string url = "This is not Website.";
                try
                {
                    url = WComm.Utilities.urlPath+"\\"+WComm .Utilities .getCurPage ;
                }
                catch { }
                return url;
            }
        }
        [BindingField("SessionId", true)]
        public string SessionId
        {
            set
            {
                _url = value;
            }
            get
            {
                string url = "This is not Website.";
                try
                {
                    url = HttpContext.Current.Session.SessionID;
                }
                catch { }
                return url;
            }
        }
        [BindingField("Server", true)]
        public string Server
        {
            get
            {
                return Environment.MachineName; 
            }
        }
		#endregion 

		#region Extend Property

        public string Subject;
        public string ALERTTitle;

        private int _ErrorCode;
        public int ErrorCode
        {
            set
            {
                _ErrorCode = value;
            }
            get
            {
                return _ErrorCode;
            }
        }

        private int _userId;
        [BindingField("UserId", true)]
        public int UserId
        {
            set
            {
                _userId = value;
            }
            get
            {
                return _userId;
            }
        }

		#endregion 

		/// <summary>
		///	invoke the method will save the application log to database.
		///	<code>
		///	App_Log_SQL appLogSql = new App_Log_SQL();
		///	appLogSql.CreatedOn = System.DateTime.Now;
		///	appLogSql.SaveLog(Common.RegistryKeyValue);
		///	</code>
		/// </summary>
		/// <param name="registryKeyValue">the registry key value of data connection string.</param>
        public ReturnValue SaveLog()
        {
            ReturnValue _result = new ReturnValue();

            this.CreatedOn = System.DateTime.Now;

            if (Notes != null)
            {
                if (Notes.IndexOf("Invalid viewstate") > -1 ||
             Notes.IndexOf("Path 'OPTIONS' is forbidden") > -1 ||
             Notes.IndexOf("Path 'PUT' is forbidden") > -1 ||
             Notes.IndexOf("www.scanalert.com/bot.jsp") > -1 ||
             Notes.IndexOf("Path 'PROPFIND' is forbidden") > -1 ||
               Notes.IndexOf("Failed to load viewstate") > -1 ||
            Notes == "0" ||
              Notes.IndexOf("does not exist.----   at System.Web.UI.Util.CheckVirtualFileExists") > -1 ||

            this.URL.IndexOf("style.aspx") > -1 ||
             this.URL.IndexOf("ScriptResource.axd") > -1 ||
            this.URL.IndexOf("WebResource.axd") > -1 
                    )
                {
                    return _result;
                }
            }


            try
            {
                Gateway gateway = new Gateway(this);
                _result = gateway.Save();
                this.ID = _result.IdentityId;
            }
            catch { }

            LogEmail.Send(this);

            return _result;

            //if (System.Configuration.ConfigurationManager.AppSettings["ErrorPage"] != null)
            //{
            //    try
            //    {
            //        HttpContext.Current.Response.Redirect(System.Configuration.ConfigurationManager.AppSettings["ErrorPage"].ToString());
            //    }
            //    catch (Exception ex)
            //    {
            //        string aa = ex.Message;
            //    }
            //}
        }

        public ReturnValue  SaveLogWithoutEmail()
        {
            ReturnValue _result = new ReturnValue();

            this.CreatedOn = System.DateTime.Now;

            if (Notes != null)
            {
                if (Notes.IndexOf("Invalid viewstate") > -1 ||
            Notes.IndexOf("Path 'OPTIONS' is forbidden") > -1 ||
            Notes.IndexOf("Path 'PUT' is forbidden") > -1 ||
            Notes.IndexOf("www.scanalert.com/bot.jsp") > -1 ||
            Notes.IndexOf("Path 'PROPFIND' is forbidden") > -1 ||
              Notes.IndexOf("Failed to load viewstate") > -1 ||
            Notes == "0" ||
            Notes .IndexOf ("does not exist.----   at System.Web.UI.Util.CheckVirtualFileExists")>-1 ||

            this.URL.IndexOf("style.aspx") > -1 ||
             this.URL.IndexOf("ScriptResource.axd") > -1 ||
            this.URL.IndexOf("WebResource.axd") > -1 
                    )
                {
                    return _result;
                }
            }


            try
            {
                Gateway gateway = new Gateway(this);
                _result=gateway.Save();
                this.ID = _result.IdentityId;
            }
            catch { }

            return _result;

        }

		/// <summary>
		///	get the web configuration settings of "AlwaySaveLogSql".
		/// </summary>
		/// <returns>WComm.Framework.SaveLogType instance of return.</returns>
		internal static SaveLogType getSaveLogConfig()
		{
            if (ConfigurationManager.AppSettings["AlwaysSaveLogSql"] != null)
			{
                switch (ConfigurationManager.AppSettings["AlwaysSaveLogSql"].ToUpper())
				{
					case "NONE":
					{
						return SaveLogType.None;
					}
					case "ALWAYS":
					{
						return SaveLogType.Always;
					}
					case "ERROR":
					{
						return SaveLogType.Error;
					}
				}
			}
            return SaveLogType.Error;
		}
	}

    public enum SaveLogType
    {
        /// <summary>
        ///	Don't auto save sql log.
        /// </summary>
        None,
        /// <summary>
        ///	Alway auto save sql log.
        /// </summary>
        Always,
        /// <summary>
        ///	When sql excute error,then auto save sql log.
        /// </summary>
        Error
    }
}
