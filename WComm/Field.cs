using System;

namespace WComm
{
	/// <summary>
	/// The field info for table structure.
	/// </summary>
	[Serializable]
    internal class Field
	{
		/// <summary>
		///	Constructor of field.
		/// </summary>
		public Field()
		{
		}


		private string _cOLUMN_NAME;
		private int _dATA_TYPE;
		private string _tYPE_NAME;
		private int _pRECISION;
		private int _lENGTH;
		private int _nULLABLE;
		private string _cOLUMN_DEF;
		private int _sQL_DATA_TYPE;
		private int _sQL_DATETIME_SUB;
		private int _cHAR_OCTET_LENGTH;
		private int _oRDINAL_POSITION;
		private string _tABLE_NAME;
        private string _dEFAULT;
        private bool _uNIQUE;
        

		/// <summary>
		///	Column name.
		/// </summary>
		[BindingField("COLUMN_NAME",true)]
		public string COLUMN_NAME
		{
			set
			{
				_cOLUMN_NAME=value;
			}
			get
			{
				return _cOLUMN_NAME;
			}
		}
		/// <summary>
		///	data type.
		/// </summary>
		[BindingField("DATA_TYPE",true)]
		public int DATA_TYPE
		{
			set
			{
				_dATA_TYPE=value;
			}
			get
			{
				return _dATA_TYPE;
			}
		}
		/// <summary>
		///	type name.
		/// </summary>
		[BindingField("TYPE_NAME",true)]
		public string TYPE_NAME
		{
			set
			{
				_tYPE_NAME=value;
			}
			get
			{
				return _tYPE_NAME;
			}
		}
		/// <summary>
		///	precision.
		/// </summary>
		[BindingField("PRECISION",true)]
		public int PRECISION
		{
			set
			{
				_pRECISION=value;
			}
			get
			{
				return _pRECISION;
			}
		}
		/// <summary>
		///	field length.
		/// </summary>
		[BindingField("LENGTH",true)]
		public int LENGTH
		{
			set
			{
				_lENGTH=value;
			}
			get
			{
				return _lENGTH;
			}
		}
		/// <summary>
		///	null able.
		/// </summary>
		[BindingField("NULLABLE",true)]
		public int NULLABLE
		{
			set
			{
				_nULLABLE=value;
			}
			get
			{
				return _nULLABLE;
			}
		}
		/// <summary>
		///	column define.
		/// </summary>
		[BindingField("COLUMN_DEF",true)]
		public string COLUMN_DEF
		{
			set
			{
				_cOLUMN_DEF=value;
			}
			get
			{
				return _cOLUMN_DEF;
			}
		}
		/// <summary>
		///	sql data type.
		/// </summary>
		[BindingField("SQL_DATA_TYPE",true)]
		public int SQL_DATA_TYPE
		{
			set
			{
				_sQL_DATA_TYPE=value;
			}
			get
			{
				return _sQL_DATA_TYPE;
			}
		}
		/// <summary>
		///	sql datetime subject.
		/// </summary>
		[BindingField("SQL_DATETIME_SUB",true)]
		public int SQL_DATETIME_SUB
		{
			set
			{
				_sQL_DATETIME_SUB=value;
			}
			get
			{
				return _sQL_DATETIME_SUB;
			}
		}
		/// <summary>
		///	char octet lenght.
		/// </summary>
		[BindingField("CHAR_OCTET_LENGTH",true)]
		public int CHAR_OCTET_LENGTH
		{
			set
			{
				_cHAR_OCTET_LENGTH=value;
			}
			get
			{
				return _cHAR_OCTET_LENGTH;
			}
		}
		/// <summary>
		///	ordinal postition.
		/// </summary>
		[BindingField("ORDINAL_POSITION",true)]
		public int ORDINAL_POSITION
		{
			set
			{
				_oRDINAL_POSITION=value;
			}
			get
			{
				return _oRDINAL_POSITION;
			}
		}
		/// <summary>
		///	table name.
		/// </summary>
		[BindingField("TABLE_NAME",true)]
		public string TABLE_NAME
		{
			set
			{
				_tABLE_NAME=value;
			}
			get
			{
				return _tABLE_NAME;
			}
		}
        [BindingField("DEFAULT", true)]
        public string DEFAULT
        {
            set
            {
                _dEFAULT = value;
            }
            get
            {
                return _dEFAULT;
            }
        }
        [BindingField("UNIQUE", true)]
        public bool UNIQUE
        {
            set
            {
                _uNIQUE = value;
            }
            get
            {
                return _uNIQUE;
            }
        }


		private bool _pkey;
		private bool _fkey;
		private string _value;
		/// <summary>
		///	Is primary key.
		/// </summary>
		[BindingField("Pkey",true)]
		public bool Pkey
		{
			set
			{
				_pkey=value;
			}
			get
			{
				return _pkey;
			}
		}

		/// <summary>
		///	Is Fkey.
		/// </summary>
		[BindingField("FKey",true)]
		public bool FKey
		{
			set
			{
				_fkey=value;
			}
			get
			{
				return _fkey;
			}
		}

		/// <summary>
		///	value.
		/// </summary>
		[BindingField("Value",true)]
		public string Value
		{
			set
			{
				_value=value;
			}
			get
			{
				return _value;
			}
		}

	}
}
