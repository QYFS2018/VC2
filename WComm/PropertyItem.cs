using System;
using System.Reflection;
using System.Collections;

namespace WComm
{
    /// <summary>
    ///	Property Item object class.
    /// </summary>
    internal class PropertyItem
    {

        private string _propertyName;
        private string _fieldName;
        private PropertyInfo _propertyInfo;
        private PropertyItemList _subObject;

        /// <summary>
        ///	Property name.
        /// </summary>
        public string PropertName
        {
            set
            {
                _propertyName = value;
            }
            get
            {
                return _propertyName;
            }
        }
        /// <summary>
        ///	Field name.
        /// </summary>
        public string FieldName
        {
            set
            {
                _fieldName = value;
            }
            get
            {
                return _fieldName;
            }
        }
        /// <summary>
        ///	Property Info.
        /// </summary>
        public PropertyInfo PropertyInfo
        {
            set
            {
                _propertyInfo = value;
            }
            get
            {
                return _propertyInfo;
            }
        }

        public PropertyItemList SubObject
        {
            set
            {
                _subObject = value;
            }
            get
            {
                return _subObject;
            }
        }

        private bool _isPersistence;
        private bool _isSimpleObject;
        private bool _isMulLanguage;


        public bool IsPersistence
        {
            set
            {
                _isPersistence = value;
            }
            get
            {
                return _isPersistence;
            }
        }
        public bool IsSimpleObject
        {
            set
            {
                _isSimpleObject = value;
            }
            get
            {
                return _isSimpleObject;
            }
        }
        public bool IsMulLanguage
        {
            set
            {
                _isMulLanguage = value;
            }
            get
            {
                return _isMulLanguage;
            }
        }

        /// <summary>
        ///	Constructor.
        /// </summary>
        public PropertyItem()
        { }

        /// <summary>
        ///	Constructor.
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <param name="fieldName">field name</param>
        /// <param name="propertyInfo">property info</param>
        public PropertyItem(string propertyName, string fieldName, bool isPersistence, bool isSimpleObject, bool isMulLanguage, PropertyInfo propertyInfo)
        {
            this.PropertName = propertyName;
            this.FieldName = fieldName;
            this.PropertyInfo = propertyInfo;
            this.IsMulLanguage = isMulLanguage;
            this.IsPersistence = isPersistence;
            this.IsSimpleObject = isSimpleObject;
        }
    }
    /// <summary>
    ///	Property item collection list.
    /// </summary>
    internal class PropertyItemList : System.Collections.Generic.List<PropertyItem>
    {
        /// <summary>
        ///	Get item by property.
        /// </summary>
        /// <param name="propertyName">property name</param>
        /// <returns>property item</returns>
        public PropertyItem getItemByProperty(string propertyName)
        {
            PropertyItem _result = null;
            foreach (PropertyItem _item in this)
            {
                if (_item.PropertName.Trim().ToUpper() == propertyName.Trim().ToUpper())
                {
                    _result = _item;
                    break;
                }
            }
            return _result;
        }
        /// <summary>
        ///	Get item by field.
        /// </summary>
        /// <param name="fieldName">field name.</param>
        /// <returns>Property item</returns>
        public PropertyItem getItemByField(string fieldName)
        {
            PropertyItem _result = null;
            foreach (PropertyItem _item in this)
            {
                if (_item.FieldName.Trim().ToUpper() == fieldName.Trim().ToUpper())
                {
                    _result = _item;
                    break;
                }
            }
            return _result;
        }
    }
}