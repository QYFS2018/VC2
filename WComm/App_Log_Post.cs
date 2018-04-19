using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;


namespace WComm
{
	/// <summary>
	///		Application log of post entity.When you post a website 
	///		to get information,the application need save the information 
	///		to database.
	/// </summary>
	[Serializable]
	[BindingClass("App_Log_Post")]
    public class App_Log_Post : Entity
	{
		/// <summary>
		/// 	Constructor of application post log.
		/// </summary>
		public App_Log_Post()
		{
            //this.DataConnectProviders = "Log";
			
		}

		#region Basic Property

		private int? _iD;
		private DateTime? _createdOn;
		private int? _dc;
		private string _requestContent;
		private string _requestCode;
		private string _responseContent;
		private string _responseCode;
		private string _oId;
		private bool? _success;
		private string _notes;
		private int? _programId;
        private string _type;

		/// <summary>
		///		post log identify.
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
		///		dc.
		/// </summary>
		[BindingField("Dc",true)]
		public int? Dc
		{
			set
			{
				_dc=value;
			}
			get
			{
				return _dc;
			}
		}
		/// <summary>
		///		the request content of this post procedure.
		/// </summary>
		[BindingField("RequestContent",true)]
		public string RequestContent
		{
			set
			{
				_requestContent=value;
			}
			get
			{
				return _requestContent;
			}
		}
		/// <summary>
		///		the request code of this post procedure. 
		/// </summary>
		[BindingField("RequestCode",true)]
		public string RequestCode
		{
			set
			{
				_requestCode=value;
			}
			get
			{
				return _requestCode;
			}
		}
		/// <summary>
		///		the response content of this post procedure. 
		/// </summary>
		[BindingField("ResponseContent",true)]
		public string ResponseContent
		{
			set
			{
				_responseContent=value;
			}
			get
			{
				return _responseContent;
			}
		}
		/// <summary>
		///		the response code of this post procedure. 
		/// </summary>
		[BindingField("ResponseCode",true)]
		public string ResponseCode
		{
			set
			{
				_responseCode=value;
			}
			get
			{
				return _responseCode;
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
		///		the flag of if the post send success.
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
                _type = value;
            }
            get
            {
                return _type;
            }
        }

		#endregion 

		#region Extend Property

		#endregion 

	
	}
}
