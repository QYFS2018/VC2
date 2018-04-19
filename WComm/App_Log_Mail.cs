using System;
using System.Collections;
using System.Data.OleDb;
using System.Data;


namespace WComm
{
	/// <summary>
	///		Application log of mail entity.When you send a mail,the 
	///		application need save the information to database.
	/// </summary>
	[Serializable]
	[BindingClass("App_Log_Mail")]
	public class App_Log_Mail : Entity
	{
		/// <summary>
		///		Constructor of application mail log.
		/// </summary>
		public App_Log_Mail()
		{
            //this.DataConnectProviders = "Log";
		}

		#region Basic Property

		private int? _iD;
		private DateTime? _createdOn;
		private string _addressFrom;
		private string _addressTo;
		private string _addressBcc;
		private string _subject;
		private string _content;
		private string _oId;
		private bool? _success;
		private string _notes;
		private int? _programId;
		private bool? _isTest;
		private string _type;

		/// <summary>
		///		mail log identify.
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
		///		mail address from.
		/// </summary>
		[BindingField("AddressFrom",true)]
		public string AddressFrom
		{
			set
			{
				_addressFrom=value;
			}
			get
			{
				return _addressFrom;
			}
		}
		/// <summary>
		///		mail address to.
		/// </summary>
		[BindingField("AddressTo",true)]
		public string AddressTo
		{
			set
			{
				_addressTo=value;
			}
			get
			{
				return _addressTo;
			}
		}
		/// <summary>
		///		addressbcc.
		/// </summary>
		[BindingField("AddressBcc",true)]
		public string AddressBcc
		{
			set
			{
				_addressBcc=value;
			}
			get
			{
				return _addressBcc;
			}
		}
		/// <summary>
		///		mail subject.
		/// </summary>
		[BindingField("Subject",true)]
		public string Subject
		{
			set
			{
				_subject=value;
			}
			get
			{
				return _subject;
			}
		}
		/// <summary>
		///		mail content.
		/// </summary>
		[BindingField("Content",true)]
		public string Content
		{
			set
			{
				_content=value;
			}
			get
			{
				return _content;
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
		///		the flag of if the mail send success.
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
		///		the program id of this mail.
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
		///		the flag of if the mail is for test.
		/// </summary>
		[BindingField("IsTest",true)]
		public bool? IsTest
		{
			set
			{
				_isTest=value;
			}
			get
			{
				return _isTest;
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
