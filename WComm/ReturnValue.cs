using System;
using System.Data;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;


namespace WComm
{
    [XmlRoot("ReturnValue")]   
    [Serializable]
	public class ReturnValue
	{
		#region Base Property

		private bool _success;

		/// <summary>
		///	Check if method success.
		/// </summary>
		public bool Success
		{
			get
			{
				return _success;
			}
			set
			{
				_success = value;
			}
		}

		private int _identityId;

		/// <summary>
		///	The identity id from database.
		/// </summary>
		public int IdentityId
		{
			set
			{
				_identityId=value;
			}
			get
			{
				return _identityId;
			}
		}

		private string _errMessage;

		/// <summary>
		///	The error message.
		/// </summary>
		public string ErrMessage
		{
			set
			{
				_errMessage=value;
			}
			get
			{
				return _errMessage;
			}
		}

		
		private string _sQLText;

		/// <summary>
		///	The error message.
		/// </summary>
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

        private Entity _object;
        public Entity Object
        {
            set
            {
                _object = value;
            }
            get
            {
                return _object;
            }
        }

        private EntityList _objectList;
        public EntityList ObjectList
        {
            set
            {
                _objectList = value;
            }
            get
            {
                return _objectList;
            }
        }

        private List<Entity> _entityList;
        public List<Entity> EntityList
        {
            set
            {
                _entityList = value;
            }
            get
            {
                return _entityList;
            }
        }

        private DataSet _dataSet;
        public DataSet DataSet
        {
            set
            {
                _dataSet = value;
            }
            get
            {
                return _dataSet;
            }
        }

        private int _effectRows;
        public int EffectRows
        {
            set
            {
                _effectRows = value;
            }
            get
            {
                return _effectRows;
            }
        }

        private int _code;
        public int Code
        {
            set
            {
                _code = value;
            }
            get
            {
                return _code;
            }
        }

        private string _notes;
        public string Notes
        {
            set
            {
                _notes = value;
            }
            get
            {
                return _notes;
            }
        }

        private string _table;
        public string Table
        {
            set
            {
                _table = value;
            }
            get
            {
                return _table;
            }
        }

        private object _objectValue;
        public object ObjectValue
        {
            set
            {
                _objectValue = value;
            }
            get
            {
                return _objectValue;
            }
        }

        private System.Collections.Generic.List<string> _messageList;
        public System.Collections.Generic.List<string> MessageList
        {
            set
            {
                _messageList = value;
            }
            get
            {
                return _messageList;
            }
        }


        private System.Collections.Generic.Dictionary<string, Entity> _dictionaryList;
        public System.Collections.Generic.Dictionary<string, Entity> DictionaryList
        {
            set
            {
                _dictionaryList = value;
            }
            get
            {
                return _dictionaryList;
            }
        }

     

		#endregion

		/// <summary>
		///	Constructor method.
		/// </summary>
		
        public ReturnValue()
		{
            this.Success = true;
            this.Object = new Entity();
            this.ObjectList = new EntityList();
            this.MessageList = new System.Collections.Generic.List<string>();
            this.DictionaryList = new Dictionary<string, Entity>();
            this.EntityList = new List<Entity>();
		}
	}
}
