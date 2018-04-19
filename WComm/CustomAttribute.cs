using System;

namespace WComm
{
	/// <summary>
	///		The binging field attribute for markup entity field.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property,Inherited=false,AllowMultiple=false)]
	public class BindingFieldAttribute :Attribute
	{
		/// <summary>
		///		The constructor method to binding a field to datebase table field.
		/// </summary>
		/// <param name="fieldName">database table field name.</param>
		/// <param name="isPersistence">if it is Persistence</param>
		/// <param name="isSimpleObject">if it is a SimpleObject</param>
		public BindingFieldAttribute(string fieldName,bool isPersistence,bool isSimpleObject)
		{
			_fieldName=fieldName;
			_isPersistence=isPersistence;
			_isSimpleObject=isSimpleObject;
		}

        public BindingFieldAttribute(string fieldName, bool isPersistence, bool isSimpleObject, bool isMulContent)
        {
            _fieldName = fieldName;
            _isPersistence = isPersistence;
            _isSimpleObject = isSimpleObject;
            _isMulContent = isMulContent;
        }

		/// <summary>
		///		The constructor method to binding a field to datebase table field.
		/// </summary>
		/// <param name="fieldName">database table field name.</param>
		/// <param name="isPersistence">if it is Persistence</param>
		public BindingFieldAttribute(string fieldName,bool isPersistence)
		{
			_fieldName=fieldName;
			_isPersistence=isPersistence;
		}

		private string _fieldName="";
		private bool _isPersistence=true;
		private bool _isSimpleObject=true;
        private bool _isMulContent = false;

		/// <summary>
		///		database table field name for binding.
		/// </summary>
		public string FieldName
		{
			get
			{
				return _fieldName;
			}
		}
		/// <summary>
		///		if it is persistence.
		/// </summary>
		public bool IsPersistence
		{
			get
			{
				return _isPersistence;
			}
		}
		/// <summary>
		///		if it is a simple object.
		/// </summary>
		public bool IsSimpleObject
		{
			get
			{
				return _isSimpleObject;
			}
		}
        public bool IsMulContent
        {
            get
            {
                return _isMulContent;
            }
        }

	}
	/// <summary>
	///		The allow null attribute for markup entity field.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property,Inherited=false,AllowMultiple=false)]
	public class AllowNullAttribute :Attribute
	{
		/// <summary>
		///		The constructor method to binding a field to datebase table field.
		/// </summary>
		/// <param name="isAllowNull">Is allow null.</param>
		public AllowNullAttribute(bool isAllowNull)
		{
			_isAllowNull=isAllowNull;
		}


		private bool _isAllowNull=true;
		/// <summary>
		///		Is allow null.
		/// </summary>
		public bool IsAllowNull
		{
			get
			{
				return _isAllowNull;
			}
		}
		

	}
	/// <summary>
	///		The NVarchar attribute for markup entity field.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property,Inherited=false,AllowMultiple=false)]
	public class NVarcharAttribute :Attribute
	{
		/// <summary>
		///		The constructor method to binding a field to datebase table field.
		/// </summary>
		/// <param name="isNVarchar">Is NVarchar.</param>
		public NVarcharAttribute(bool isNVarchar)
		{
			_isNVarchar=isNVarchar;
		}


		private bool _isNVarchar=true;
		/// <summary>
		///		Is NVarchar.
		/// </summary>
		public bool IsNVarchar
		{
			get
			{
				return _isNVarchar;
			}
		}
		

	}


    /// <summary>
    ///		The NVarchar attribute for markup entity field.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class XMLBindingFieldAttribute : Attribute
    {
        /// <summary>
        ///		The constructor method to binding a field to datebase table field.
        /// </summary>
        /// <param name="xmlfield">Is NVarchar.</param>
        public XMLBindingFieldAttribute(string xmlfield, string searchOn, string searchPath)
        {
            _xmlfield = xmlfield;
            _searchOn = searchOn;
            _searchPath = searchPath;
        }


        private string _xmlfield ;
        private string _searchOn;
        private string _searchPath;
      
        public string Xmlfield
        {
            get
            {
                return _xmlfield;
            }
        }

        public string SearchOn
        {
            get
            {
                return _searchOn;
            }
        }

        public string SearchPath
        {
            get
            {
                return _searchPath;
            }
        }

    }

	/// <summary>
	///		The binding class attribute for markup entity class.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class,Inherited=true,AllowMultiple=false)]
	public class BindingClassAttribute :Attribute
	{
		/// <summary>
		///		The constructor method to binding a entity class to datebase table.
		/// </summary>
		/// <param name="tableName">database table name for binging.</param>
		public BindingClassAttribute(string tableName)
		{
			_tableName=tableName;
		}


		private string _tableName="";
		/// <summary>
		///		database table name for binging.
		/// </summary>
		public string TableName
		{
			get
			{
				return _tableName;
			}
		}
	}
}
