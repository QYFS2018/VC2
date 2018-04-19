using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Text;
using System.Web.Mail;
using System.Runtime.Serialization;
using System.Data.OleDb;
using System.IO;
using System.Xml;
using System.Web.Caching;
using System.Data.Common;
using System.Web.Configuration;
using System.Transactions;
using Npgsql;

namespace WComm
{
    internal class PGateway 
    {
        #region Property

        ReflectionHelper _Helper;
        public ReflectionHelper Helper
        {
            get
            {
                if (_Helper == null)
                {
                    _Helper = ReflectionHelper.GetInstance();
                }
                return _Helper;
            }
        }



        private PropertyItemList _propertyItemList;
        private string _registryKeyValue;
        private PEntity _oEntity;

        private Type _EntityType;
        private Type EntityType
        {
            get
            {
                if (_EntityType == null && _oEntity != null)
                    _EntityType = _oEntity.GetType();
                return _EntityType;
            }
        }

        private string _connectionString;
        private string _providerType;
        private string _dataConnectProviders;
        private Transaction _trans;
        public Transaction Trans
        {
            get
            {
                return _trans;
            }
        }

        private GlobalErrorHandler _globalErrorHandler = new GlobalErrorHandler();

        private string _tableName;
        public string TableName
        {
            get
            {
                if (_tableName == null)
                {
                    string _result = string.Empty;

                    foreach (BindingClassAttribute attr in Helper.GetBindingClassAttributeOnEntity(EntityType))
                    {
                        _result = attr.TableName;
                        _oEntity.TableName = attr.TableName;
                        break;
                    }
                    if (_result == "")
                    {
                        if (String.IsNullOrEmpty(this._oEntity.TableName) == false)
                        {
                            _tableName = this._oEntity.TableName;
                        }
                        else
                        {
                            throw new Exception("Haven't define the class customer attribute.");
                        }
                    }
                    else
                    {
                        _tableName = _result;
                    }
                }
                return _tableName;
            }
        }

        private string _sQL_Insert;
        public string SQL_Insert
        {
            get
            {
                if (_sQL_Insert == null)
                {
                    string _result = " Insert Into [" + TableName + "]";
                    string _fields = "";
                    string _values = "";

                    string _pID = "";
                  
                    List<Field> _fieldList = this.getFields();

                    #region build insert query 

                    foreach (Field _field in _fieldList)
                    {
                        if (_field.NULLABLE == 0 && _field.DEFAULT.IndexOf("nextval") == -1)
                        {
                            if (GetObjectFieldValueByField(_field) == "null")
                                throw new Exception("The field " + _field.COLUMN_NAME + " can't accept null value.");
                        }


                        _fields = String.Format("{0}\"{1}\",", _fields, _field.COLUMN_NAME);


                        if (_field.DEFAULT.IndexOf("nextval") > -1)
                        {
                            DataFactory _dal = this.getDataFactory();
                            string sql = String.Format("select " + _field.DEFAULT);

                            DataTable dt = _dal.getDataTable(sql);
                            _values = _values + dt.Rows[0][0].ToString() + ",";

                            if (_field.Pkey == true)
                            {
                                _pID = dt.Rows[0][0].ToString();
                            }
                        }
                        else
                        {
                            if (_field.COLUMN_NAME == "weight")
                            {
                            }
                            _values = _values + GetObjectFieldValueByField(_field) + ",";
                        }
                    }

                    if (_fields.IndexOf(",") > 0) _fields = _fields.Remove(_fields.Length - 1, 1);
                    if (_values.IndexOf(",") > 0) _values = _values.Remove(_values.Length - 1, 1);


                    if (string.IsNullOrEmpty(_pID) == false)
                    {
                        _result = String.Format("Insert into {0} ( {1} ) values ( {2} );Select "+_pID+" as Id ", TableName, _fields, _values);
                    }
                    else
                    {
                        _result = String.Format("Insert into {0} ( {1} ) values ( {2} );Select 0 as Id ", TableName, _fields, _values);
                    }

                    _sQL_Insert = _result;

                    #endregion

                }
                return _sQL_Insert;
            }
        }

        private string _sQL_Update;
        public string SQL_Update
        {
            get
            {
                if (_sQL_Update == null)
                {
                    string _result = String.Format(" Update \"{0}\" ", TableName);
                    string _sets = "";
                    string _wheres = "";

                    List<Field> _fieldList = this.getFields();

                    foreach (Field _field in _fieldList)
                    {
                        if (_field.Pkey == true)
                        {
                            _wheres = String.Format("{0}\"{1}\"={2}", _wheres, _field.COLUMN_NAME, GetObjectFieldValueByField(_field));
                            continue;
                        }

                        if (_field.NULLABLE == 0)
                        {
                            if (string.IsNullOrEmpty(GetObjectFieldValueByField(_field)))
                                throw new Exception(String.Format("The field {0} can't accept null value.", _field.COLUMN_NAME));
                        }


                        bool hasProperty = false;

                        PropertyInfo[] objectPropertiesArray = Helper.GetPropertiesForEntity(EntityType);

                        foreach (PropertyInfo objProperty in objectPropertiesArray)
                        {
                            foreach (BindingFieldAttribute attr in Helper.GetBindingFieldAtttributesOnProperty(EntityType, objProperty))
                            {
                                if (attr.IsSimpleObject == true && String.Compare(_field.COLUMN_NAME, attr.FieldName, StringComparison.InvariantCultureIgnoreCase) == 0)
                                {
                                    hasProperty = true;
                                }
                            }
                        }

                        if (!hasProperty)
                        {
                            continue;
                        }

                        string _fieldValue = GetObjectFieldValueByField(_field);

                        _sets = String.Format("{0}\"{1}\"={2},", _sets, _field.COLUMN_NAME, _fieldValue);

                        if (_fieldValue != "null")
                        {
                           
                        }
                    }
                  
                    if (_sets.IndexOf(",") > 0) _sets = _sets.Remove(_sets.Length - 1, 1);

                    _result = String.Format("{0} Set {1} Where {2}", _result, _sets, _wheres);
                    _sQL_Update = _result;

                }

                return _sQL_Update;
            }
        }

        private string _sQL_Delete;
        public string SQL_Delete
        {
            get
            {
                if (_sQL_Delete == null)
                {
                    string _result = " Delete from \"" + TableName + "\"  Where ";


                    List<Field> _fieldList = this.getFields();

                    foreach (Field _field in _fieldList)
                    {
                        if (_field.Pkey == true)
                        {
                            _result = _result + "\"" + _field.COLUMN_NAME + "\"=" + GetObjectFieldValueByField(_field);
                        }
                    }
                    _sQL_Delete = _result;
                }
                return _sQL_Delete;
            }
        }

        #endregion

        public PGateway(PEntity o)
        {
            underGateway(o.DataConnectProviders);
            this._oEntity = o;
        }
        public PGateway(string dataConnectProviders)
        {
            underGateway(dataConnectProviders);
        }
        public PGateway(string dataConnectProviders, Transaction trans)
        {
            underGateway(dataConnectProviders);
            this._trans = trans;
        }
        public PGateway(PEntity o, Transaction trans)
        {
            underGateway(o.DataConnectProviders);
            this._oEntity = o;
            this._trans = trans;
        }


        public ReturnValue getEntityList(string sql)
        {
            ReturnValue _result = new ReturnValue();
            _result.SQLText = sql;
            _result.Success = true;

            try
            {
                EntityList _datalist = new EntityList();
                DataFactory _dal = this.getDataFactory();

                DataTable dt = _dal.getDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    PEntity _item = this._oEntity.BindDataToNEntity(dr);
                    _datalist.Add(_item);
                }
                _result.ObjectList = _datalist;
            }
            catch (DBException exa)
            {
                _result.ObjectList = new EntityList();
                _result.Success = false;
                _result.ErrMessage = exa.ToString() + "---" + _globalErrorHandler.ExceptionToString(exa);
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.ObjectList = new EntityList();
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "---" + _globalErrorHandler.ExceptionToString(ex);
            }
            return _result;
        }
        public ReturnValue getEntity(string sql)
        {
            ReturnValue _result = new ReturnValue();


            _result.SQLText = sql;
            _result.Success = true;

            try
            {

                DataFactory _dal = this.getDataFactory();
                DataTable dt = _dal.getDataTable(sql);

                // PEntity _item = Activator.CreateInstance(EntityType) as PEntity;

                PEntity _item = Helper.CreateNewEntityInstanceOf(EntityType) as PEntity;
                _item.TableName = _oEntity.TableName;

                if (dt.Rows.Count > 0)
                {
                    _item = this._oEntity.BindDataToNEntity(dt.Rows[dt.Rows.Count - 1]);

                }

                _result.Object = _item;
            }
            catch (DBException exa)
            {
                _result.Object = Helper.CreateNewEntityInstanceOf(EntityType);
                _result.Success = false;
                _result.ErrMessage = exa.ToString();
                _result.ErrMessage = exa.ToString() + "---" + _globalErrorHandler.ExceptionToString(exa) + "---" + sql;
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.Object = Helper.CreateNewEntityInstanceOf(EntityType);
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "--" + _globalErrorHandler.ExceptionToString(ex) + "---" + sql;
            }
            return _result;
        }

        public ReturnValue ExecSql(string sql)
        {
            ReturnValue _result = new ReturnValue();
            _result.SQLText = sql;
            _result.Success = true;

            try
            {
                DataFactory _dal = getDataFactory();
                int EffectRows = _dal.execSql(sql);
                _result.EffectRows = EffectRows;
            }
            catch (DBException exa)
            {
                _result.EffectRows = 0;
                _result.Success = false;
                _result.ErrMessage = exa.ToString() + "---" + _globalErrorHandler.ExceptionToString(exa);
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.EffectRows = 0;
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "---" + _globalErrorHandler.ExceptionToString(ex);
            }
            return _result;
        }
        public ReturnValue Save()
        {
            ReturnValue _result = new ReturnValue();
            _result.Success = true;
            _result.Table = this.TableName;


            try
            {
                DataFactory dal = this.getDataFactory();
                _result.SQLText = this.SQL_Insert;
                DataTable dt = dal.getDataTable(this.SQL_Insert);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0] != null)
                    {
                        _result.IdentityId = int.Parse(dr[0].ToString());
                    }
                }
            }
            catch (DBException exa)
            {
                _result.IdentityId = 0;
                _result.Success = false;
                _result.ErrMessage = exa.ToString() + "---" + _globalErrorHandler.ExceptionToString(exa);
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.IdentityId = 0;
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "---" + _globalErrorHandler.ExceptionToString(ex);

            }


            return _result;
        }
        public ReturnValue Update()
        {
            ReturnValue _result = new ReturnValue();
            _result.Success = true;
            _result.Table = this.TableName;


            try
            {
                DataFactory dal = this.getDataFactory();
                _result.SQLText = this.SQL_Update;
                int EffectRows = dal.execSql(SQL_Update);
                _result.EffectRows = EffectRows;
            }
            catch (DBException exa)
            {
                _result.IdentityId = 0;
                _result.Success = false;
                _result.ErrMessage = exa.ToString() + _globalErrorHandler.ExceptionToString(exa);
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.IdentityId = 0;
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + _globalErrorHandler.ExceptionToString(ex);


            }
            return _result;
        }
        public ReturnValue Delete()
        {

            ReturnValue _result = new ReturnValue();
            _result.Success = true;
            _result.Table = this.TableName;


            try
            {
                DataFactory dal = this.getDataFactory();
                _result.SQLText = this.SQL_Delete;
                int EffectRows = dal.execSql(SQL_Delete);
                _result.EffectRows = EffectRows;
            }
            catch (DBException exa)
            {
                _result.IdentityId = 0;
                _result.Success = false;
                _result.ErrMessage = exa.ToString() + _globalErrorHandler.ExceptionToString(exa);
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.IdentityId = 0;
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + _globalErrorHandler.ExceptionToString(ex);


            }
            return _result;
        }


        protected DataFactory getDataFactory()
        {
            DataFactory _result;

            if (WComm.CommonLogic.Connect() == false)
            {
                return new DataFactory();
            }

            if (this._trans == null)
            {
                _result = new DataFactoryPostgreSQL(this._registryKeyValue, this._providerType, this._connectionString);
            }
            else
            {
                _result = new DataFactoryPostgreSQL(this._trans);
            }


            return _result;

        }
        private void underGateway(string dataConnectProviders)
        {
            Connector _connector = new Connector(dataConnectProviders);
            this._registryKeyValue = _connector.RegistryKeyValue;
            this._connectionString = _connector.ConnectionString;
            this._providerType = _connector.ProviderType;
            this._dataConnectProviders = dataConnectProviders;

            ConInfo.DataConnectProviders = dataConnectProviders;
        }

   
        private Entity BindDataToObject(DataRow dr)
        {
            Entity _result = Helper.CreateNewEntityInstanceOf(EntityType);

            foreach (PropertyItem _item in _propertyItemList)
            {
                if (_item.SubObject == null)
                {
                    foreach (DataColumn dc in dr.Table.Columns)
                    {
                        if (String.Compare(dc.ColumnName, _item.FieldName, StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            if (!dr.IsNull(_item.FieldName))
                            {
                                string _value = dr[_item.FieldName].ToString();

                                if (_item.IsMulLanguage == true)
                                {
                                    _value = Process.GetContentFromXML(Utilities.CurrentLangCode, Utilities.CurrentSkinCode, _value);
                                    if (HttpContext.Current != null)
                                    {
                                        _value = HttpContext.Current.Server.HtmlDecode(_value);
                                    }
                                    _value = HttpUtility.HtmlDecode(_value);
                                }
                                Process.SetPropertyValue(_result, _item.PropertyInfo, _value);
                            }
                        }
                    }
                }
                else
                {
                    object _obj = _item.PropertyInfo.GetValue(_result, null);
                    if (_obj != null)
                    {
                        foreach (PropertyItem _oitem in _item.SubObject)
                        {
                            foreach (DataColumn dc in dr.Table.Columns)
                            {
                                if (String.Compare(dc.ColumnName, _oitem.FieldName, StringComparison.InvariantCultureIgnoreCase) == 0)
                                {
                                    if (!dr.IsNull(_oitem.FieldName))
                                    {
                                        string _value = dr[_oitem.FieldName].ToString();

                                        if (_oitem.IsMulLanguage == true)
                                        {
                                            _value = Process.GetContentFromXML(Utilities.CurrentLangCode, Utilities.CurrentSkinCode, _value);
                                            if (HttpContext.Current != null)
                                            {
                                                _value = HttpContext.Current.Server.HtmlDecode(_value);
                                            }
                                            _value = HttpUtility.HtmlDecode(_value);
                                        }

                                        Process.SetPropertyValue(_obj, _oitem.PropertyInfo, _value);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return _result;
        }
        private void BuildPropertyItemList()
        {
            _propertyItemList = new PropertyItemList();

            foreach (PropertyInfo propertyInfo in Helper.GetPropertiesForEntity(EntityType))
            {
                foreach (BindingFieldAttribute attr in Helper.GetBindingFieldAtttributesOnProperty(EntityType, propertyInfo))
                {
                    if (attr.IsSimpleObject == true)
                    {
                        _propertyItemList.Add(new PropertyItem(propertyInfo.Name, attr.FieldName, attr.IsPersistence, attr.IsSimpleObject, attr.IsMulContent, propertyInfo));
                    }
                    else
                    {
                        if (propertyInfo.GetValue(_oEntity, null) != null)
                        {
                            PropertyItem _item = new PropertyItem(propertyInfo.Name, attr.FieldName, attr.IsPersistence, attr.IsSimpleObject, attr.IsMulContent, propertyInfo);
                            _item.SubObject = new PropertyItemList();

                            foreach (PropertyInfo omi in propertyInfo.GetValue(_oEntity, null).GetType().GetProperties())
                            {
                                foreach (BindingFieldAttribute oattr in omi.GetCustomAttributes(typeof(BindingFieldAttribute), false))
                                {
                                    _item.SubObject.Add(new PropertyItem(omi.Name, attr.FieldName + oattr.FieldName, oattr.IsPersistence, oattr.IsSimpleObject, oattr.IsMulContent, omi));
                                }
                            }
                            _propertyItemList.Add(_item);
                        }

                    }
                }
            }
            return;
        }
        private void HtmlEncodeStringPropertyValue(PropertyInfo objProperty, object propertyValue)
        {
            string val = propertyValue.ToString();
            val = HttpUtility.HtmlEncode(val);
            objProperty.SetValue(_oEntity, val, null);
        }
        private string GenerateXmlContent(PropertyInfo objProperty, BindingFieldAttribute attr, out bool shouldReturn)
        {
            shouldReturn = false;
            if (attr.IsMulContent == true)
            {
                object propertyValue = objProperty.GetValue(_oEntity, null);
                if (objProperty.PropertyType == Helper.StringType)
                {
                    if (propertyValue != null)
                    {
                        HtmlEncodeStringPropertyValue(objProperty, propertyValue);
                        propertyValue = HttpUtility.HtmlEncode(propertyValue.ToString()); ;
                    }
                }

                string _originalXml = string.Empty;

                List<Field> _fieldList = this.getFields();
                string _where = string.Empty;
                foreach (Field _field in _fieldList)
                {
                    if (_field.Pkey == true)
                    {
                        _where = String.Format("[{0}]={1}", _field.COLUMN_NAME, GetObjectFieldValueByField(_field));
                    }
                }
                string _sql = String.Format("Select Top 1 [{0}] From [{1}] with (nolock) Where {2}", attr.FieldName, this.TableName, _where);

                DataFactory _dal = this.getDataFactory();
                DataTable dt = _dal.getDataTable(_sql);

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0] != null)
                    {
                        _originalXml = dr[0].ToString();
                    }
                }
                string _xml = string.Empty;
                if (propertyValue != null)
                {
                    _xml = propertyValue.ToString();
                }

                _xml = Process.GenerateContentXML(Utilities.CurrentLangCode, Utilities.CurrentSkinCode, _originalXml, _xml);
                shouldReturn = true;
                return String.Format("N'{0}'", _xml);
            }
            return String.Empty;
        }
        private static string GetStringForNullableBoolValue(object propertyValue)
        {
            bool? result = propertyValue as Nullable<bool>;
            switch (result)
            {
                case null:
                    return "null";
                case true:
                    return "true";
                case false:
                    return "false";
            }
            return String.Empty;
        }
        private static string GetStringValue(object result)
        {
            if (result == null)
            {
                return "null";
            }
            else
            {
                return result.ToString();
            }
        }
        private string GetObjectFieldValueByField(Field field)
        {
            string stringResult = "null";
            Type entityType = EntityType;

            foreach (PropertyInfo objProperty in Helper.GetPropertiesForEntity(entityType))
            {
                foreach (BindingFieldAttribute attr in Helper.GetBindingFieldAtttributesOnProperty(entityType, objProperty))
                {
                    if (attr.IsSimpleObject == true && String.Compare(field.COLUMN_NAME, attr.FieldName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        bool shouldReturn;
                        stringResult = GenerateXmlContent(objProperty, attr, out shouldReturn);
                        if (shouldReturn)
                            return stringResult;

                        object propertyValue = objProperty.GetValue(_oEntity, null);

                        if (objProperty.PropertyType == Helper.StringType || objProperty.PropertyType == Helper.GuidType)
                        {
                            if (propertyValue != null && propertyValue.ToString() != string.Empty)
                            {
                                stringResult = String.Format("N'{0}'", propertyValue.ToString().Replace("'", "''"));
                            }
                            else
                            {
                                //stringResult = "''";
                                stringResult = "null"; //change by jack
                            }
                        }
                        else
                        {
                            if (objProperty.PropertyType == Helper.BoolNullableType || objProperty.PropertyType == Helper.BoolType)
                            {
                                stringResult = GetStringForNullableBoolValue(propertyValue);
                            }
                            else
                            {
                                if (objProperty.PropertyType == Helper.NullableDateTimeType || objProperty.PropertyType == Helper.DateTimeType)
                                {
                                    //DateTime? result = propertyValue as Nullable<DateTime>;
                                    //if (result.HasValue && (result.Value.Year != 1 && result.Value.Month != 1 && result.Value.Day != 1))
                                    //{
                                    //    stringResult = String.Format("'{0:yyyy-MM-dd HH:mm:ss}'", result.Value);
                                    //}
                                    //else
                                    //{
                                    //    //stringResult = "''";
                                    //    stringResult = "null"; //change by jack
                                    //}

                                    DateTime? result = propertyValue as Nullable<DateTime>;
                                    if (result.HasValue)
                                    {
                                        if (result.Value.Year == 1 && result.Value.Month == 1 && result.Value.Day == 1)
                                        {
                                            stringResult = "null";
                                        }
                                        else
                                        {
                                            stringResult = String.Format("'{0:yyyy-MM-dd HH:mm:ss}'", result.Value);
                                        }

                                    }
                                    else
                                    {
                                        stringResult = "null";
                                    }
                                }
                                else
                                {

                                    if (objProperty.PropertyType == Helper.NullableIntType || objProperty.PropertyType == Helper.IntType)
                                    {
                                        stringResult = GetStringValue(propertyValue as Nullable<int>);
                                    }
                                    else
                                    {
                                        if (objProperty.PropertyType == Helper.NullableLongType || objProperty.PropertyType == Helper.LongType)
                                        {
                                            stringResult = GetStringValue(propertyValue as Nullable<long>);
                                        }
                                        else if (objProperty.PropertyType == Helper.NullableDecimalType || objProperty.PropertyType == Helper.DecimalType)
                                            {
                                                stringResult = GetStringValue(propertyValue as Nullable<decimal>);
                                            }
                                            else
                                        {
                                            if (objProperty.PropertyType == Helper.NullableDoubleType || objProperty.PropertyType == Helper.DoubleType)
                                            {
                                                stringResult = GetStringValue(propertyValue as Nullable<double>);
                                                if (stringResult != "null" && stringResult.IndexOf('.') == -1)
                                                {
                                                    stringResult += ".00";
                                                }
                                            }
                                            else
                                            {
                                                stringResult = propertyValue.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return stringResult;
        }
        private string GetObjectFieldNameByField(Field field)
        {
            PropertyInfo[] objectPropertiesArray =
                EntityType.GetProperties();

            foreach (PropertyInfo objProperty in objectPropertiesArray)
            {
                foreach (BindingFieldAttribute attr in objProperty.GetCustomAttributes(typeof(BindingFieldAttribute), false))
                {
                    if (attr.IsSimpleObject == true && field.COLUMN_NAME.ToUpper() == attr.FieldName.ToUpper())
                    {
                        return objProperty.Name;
                    }
                }
            }
            return "";
        }

        private List<Field> getFields()
        {
            List<Field> _fieldList = new List<Field>();

            StringBuilder _keyBuilder = new StringBuilder();
            _keyBuilder.Append("TableFieldDictionary");
            _keyBuilder.Append(_registryKeyValue);
            _keyBuilder.Append(this.TableName);

            string _key = _keyBuilder.ToString();

            if (HttpRuntime.Cache[_key] != null)
            {
                _fieldList = HttpRuntime.Cache[_key] as List<Field>;
            }
            else
            {
                DataFactory _dal = this.getDataFactory();

                #region get primary key

                string sql = String.Format("select pg_constraint.conname as pk_name,pg_attribute.attname as colname,pg_type.typname as typename from " +
    "pg_constraint  inner join pg_class on pg_constraint.conrelid = pg_class.oid " +
    "inner join pg_attribute on pg_attribute.attrelid = pg_class.oid and  pg_attribute.attnum = pg_constraint.conkey[1] " +
    "inner join pg_type on pg_type.oid = pg_attribute.atttypid where pg_class.relname = '{0}' and pg_constraint.contype='p'", TableName);

                DataTable dt = _dal.getDataTable(sql);

                string pKeyField = "";
                if (dt.Rows.Count != 0)
                {
                    pKeyField = dt.Rows[0]["colname"].ToString();
                }
                #endregion

                #region get table fields

                sql = String.Format("select table_schema,table_name,  column_name, data_type,  column_default,  is_nullable,character_maximum_length from information_schema.columns" +
    " where table_name = '{0}'", TableName);


                dt = _dal.getDataTable(sql);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Field _field = new Field();
                    _field.COLUMN_NAME = dt.Rows[i]["column_name"].ToString();
                    _field.TYPE_NAME = dt.Rows[i]["data_type"].ToString();
                    _field.NULLABLE = dt.Rows[i]["is_nullable"].ToString() == "NO" ? 0 : 1;
                    _field.DEFAULT = dt.Rows[i]["column_default"].ToString();

                    if (string.IsNullOrEmpty(dt.Rows[i]["character_maximum_length"].ToString()) == false)
                    {
                        _field.LENGTH = int.Parse(dt.Rows[i]["character_maximum_length"].ToString());
                    }

                    if (_field.COLUMN_NAME == pKeyField)
                    {
                        _field.Pkey = true;
                    }

                    _fieldList.Add(_field);
                }

                #endregion

                #region get UNIQUE fiels

                sql = sql = String.Format("SELECT pg_attribute.attname FROM pg_class inner join pg_constraint  on pg_constraint.conrelid = pg_class.oid " +
    " inner join pg_attribute on pg_attribute.attrelid=pg_constraint.conindid where   pg_class.relname = 'Test' and contype='u'", TableName);
                dt = _dal.getDataTable(sql);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    foreach (Field _item in _fieldList)
                    {
                        if (_item.COLUMN_NAME == dt.Rows[i]["attname"].ToString())
                        {
                            _item.UNIQUE = true;
                        }
                    }
                }

                #endregion

                HttpRuntime.Cache.Add(_key, _fieldList, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                 TimeSpan.FromSeconds(600), System.Web.Caching.CacheItemPriority.Default, null);
            }

            return _fieldList;
        }

    }
}
