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


namespace WComm
{

    /// <summary>
    ///	The gateway between database and entity.
    /// </summary>
    internal class Gateway
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

        private string _sQL_Insert;
        /// <summary>
        ///	Updated By victor,2009-12-17
        /// @@identity to scope_identity()

        /// </summary>
        public string SQL_Insert
        {
            get
            {
                if (_sQL_Insert == null)
                {
                    if (FullSQL)
                    {
                        #region FullSQL
                        string _result = " Insert Into [" + TableName + "]";
                        string _fields = "";
                        string _values = "";
                        string _params = "";
                        string _paramsFormat = "";

                        bool _hasidentity = false;

                        TableSchema _dictionary = getTableSchema();

                        foreach (Field _field in _dictionary.FieldList)
                        {

                            if (_field.TYPE_NAME == "int identity")
                            {
                                _hasidentity = true;
                                continue;
                            }

                            if (_field.NULLABLE == 0)
                            {
                                if (GetObjectFieldValueByField(_field) == "null")
                                    throw new Exception("The field " + _field.COLUMN_NAME + " can't accept null value.");
                            }


                            _fields = String.Format("{0}[{1}],", _fields, _field.COLUMN_NAME);
                            _values = _values + GetObjectFieldValueByField(_field) + ",";
                            _params = String.Format("{0}@{1},", _params, _field.COLUMN_NAME);
                            if (_field.TYPE_NAME.ToLower().Contains ("varchar"))
                            {
                                _paramsFormat = String.Format("{0}@{1} {2}({3}),", _paramsFormat, _field.COLUMN_NAME, _field.TYPE_NAME, _field.LENGTH);
                            }
                            else 
                            {
                                _paramsFormat = String.Format("{0}@{1} {2},", _paramsFormat, _field.COLUMN_NAME, _field.TYPE_NAME);
                            }
                        }

                        if (_fields.IndexOf(",") > 0) _fields = _fields.Remove(_fields.Length - 1, 1);
                        if (_values.IndexOf(",") > 0) _values = _values.Remove(_values.Length - 1, 1);

                        if (_params.IndexOf(",") > 0) _params = _params.Remove(_params.Length - 1, 1);
                        if (_paramsFormat.IndexOf(",") > 0) _paramsFormat = _paramsFormat.Remove(_paramsFormat.Length - 1, 1);
                        if (_hasidentity == true)
                        {
                            _result = String.Format("{0} ( {1} ) values ( {2} );SELECT scope_Identity() ", _result, _fields, _params);
                        }
                        else
                        {
                            _result = String.Format("{0} ( {1} ) values ( {2} );SELECT 0 ", _result, _fields, _params);

                        }


                        _result = String.Format("EXEC sp_executesql N'{0}',N'{1}',{2}", _result, _paramsFormat, _values);



                        _sQL_Insert = _result;

                        #endregion
                        
                    }
                    else 
                    {
                        #region Old SQL
                        string _result = " Insert Into [" + TableName + "]";
                        string _fields = "";
                        string _values = "";

                        bool _hasidentity = false;

                        TableSchema _dictionary = getTableSchema();

                        foreach (Field _field in _dictionary.FieldList)
                        {

                            if (_field.TYPE_NAME == "int identity")
                            {
                                _hasidentity = true;
                                continue;
                            }

                            if (_field.NULLABLE == 0)
                            {
                                if (GetObjectFieldValueByField(_field) == "null")
                                    throw new Exception("The field " + _field.COLUMN_NAME + " can't accept null value.");
                            }


                            _fields = String.Format("{0}[{1}],", _fields, _field.COLUMN_NAME);
                            _values = _values + GetObjectFieldValueByField(_field) + ",";
                        }

                        if (_fields.IndexOf(",") > 0) _fields = _fields.Remove(_fields.Length - 1, 1);
                        if (_values.IndexOf(",") > 0) _values = _values.Remove(_values.Length - 1, 1);

                        if (_hasidentity == true)
                        {
                            _result = String.Format("{0} ( {1} ) values ( {2} );SELECT scope_Identity()", _result, _fields, _values);
                        }
                        else
                        {
                            _result = String.Format("{0} ( {1} ) values ( {2} );SELECT 0 ", _result, _fields, _values);

                        }


                        _sQL_Insert = _result;
                        #endregion
                    }
                    
                   
                }
                return _sQL_Insert;
            }
        }

        private string _sQL_InsertIDENTITYINSERT;
        /// <summary>
        ///	SQL insert script.
        /// </summary>
        public string SQL_InsertIDENTITYINSERT
        {
            get
            {
                if (_sQL_Insert == null)
                {
                    string _result = " Insert Into [" + TableName + "]";
                    string _fields = "";
                    string _values = "";
                    bool _hasidentity = false;

                    TableSchema _dictionary = getTableSchema();

                    foreach (Field _field in _dictionary.FieldList)
                    {

                        //if (_field.TYPE_NAME == "int identity")
                        //{
                        //    _hasidentity = true;
                        //    continue;
                        //}

                        if (_field.NULLABLE == 0)
                        {
                            if (GetObjectFieldValueByField(_field) == "null")
                                throw new Exception("The field " + _field.COLUMN_NAME + " can't accept null value.");
                        }


                        _fields = _fields + "[" + _field.COLUMN_NAME + "],";
                        _values = _values + GetObjectFieldValueByField(_field) + ",";

                    }

                    if (_fields.IndexOf(",") > 0) _fields = _fields.Remove(_fields.Length - 1, 1);
                    if (_values.IndexOf(",") > 0) _values = _values.Remove(_values.Length - 1, 1);
                    if (_hasidentity == true)
                    {
                        _result = _result + " ( " + _fields + " ) values ( " + _values + " );SELECT @@IDENTITY ";
                    }
                    else
                    {
                        _result = _result + " ( " + _fields + " ) values ( " + _values + " );SELECT 0 ";

                    }
                    _sQL_Insert = _result;
                }
                return _sQL_Insert;
            }
        }

        private string _sQL_Update;
        /// <summary>
        ///	SQL update script.
        /// </summary>
        public string SQL_Update
        {
            get
            {
                if (_sQL_Update == null)
                {

                    if (FullSQL)
                    {
                        #region FullSQL

                        string _result = String.Format(" Update [{0}] with (rowlock) ", TableName);
                        string _sets = "";
                        string _wheres = "";
                        string _values = "";
                        string _paramsFormat = "";

                        TableSchema _dictionary = getTableSchema();
                        foreach (Field _field in _dictionary.FieldList)
                        {
                            
                            if (_field.Pkey == true)
                            {
                                _wheres = String.Format("{0}[{1}]=@{1} and ", _wheres, _field.COLUMN_NAME);

                                _values = _values + GetObjectFieldValueByField(_field) + ",";
                                if (_field.TYPE_NAME.ToLower() == "varchar")
                                {
                                    _paramsFormat = String.Format("{0}@{1} {2}({3}),", _paramsFormat, _field.COLUMN_NAME, _field.TYPE_NAME, _field.LENGTH);
                                }
                                else
                                {
                                    if (_field.TYPE_NAME == "int identity")
                                    {
                                        _paramsFormat = String.Format("{0}@{1} {2},", _paramsFormat, _field.COLUMN_NAME, "int");
                                    }
                                    else
                                    {
                                        _paramsFormat = String.Format("{0}@{1} {2},", _paramsFormat, _field.COLUMN_NAME, _field.TYPE_NAME);
                                    }
                                }
                                
                            }

                            if (_field.NULLABLE == 0)
                            {
                                if (string.IsNullOrEmpty(GetObjectFieldValueByField(_field)))
                                    throw new Exception(String.Format("The field {0} can't accept null value.", _field.COLUMN_NAME));
                            }

                            if (_field.TYPE_NAME == "int identity")
                            {
                                continue;
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

                            if (this.IsFullUpdate == true || _fieldValue != "null")
                            {
                                _sets = String.Format("{0}[{1}]=@{1},", _sets, _field.COLUMN_NAME);
                                if (_field.Pkey == false)
                                {
                                    _values = _values + _fieldValue + ",";
                                    if (_field.TYPE_NAME.ToLower().Contains("varchar"))
                                    {
                                        _paramsFormat = String.Format("{0}@{1} {2}({3}),", _paramsFormat, _field.COLUMN_NAME, _field.TYPE_NAME, _field.LENGTH);
                                    }
                                    else
                                    {
                                        _paramsFormat = String.Format("{0}@{1} {2},", _paramsFormat, _field.COLUMN_NAME, _field.TYPE_NAME);
                                    }
                                }

                            }
                        }

                        if (_wheres.IndexOf(" and ") > 0) _wheres = _wheres.Remove(_wheres.Length - 5, 5);
                        if (_sets.IndexOf(",") > 0) _sets = _sets.Remove(_sets.Length - 1, 1);
                        if (_values.IndexOf(",") > 0) _values = _values.Remove(_values.Length - 1, 1);
                        if (_paramsFormat.IndexOf(",") > 0) _paramsFormat = _paramsFormat.Remove(_paramsFormat.Length - 1, 1);
                        _result = String.Format("{0} Set {1} Where {2}", _result, _sets, _wheres);
                        _result = String.Format("EXEC sp_executesql N'{0}',N'{1}',{2}", _result, _paramsFormat, _values);
                        _sQL_Update = _result;
                        #endregion
                    }
                    else
                    {
                        #region OldSQL

                        string _result = String.Format(" Update [{0}] with (rowlock) ", TableName);
                        string _sets = "";
                        string _wheres = "";

                        TableSchema _dictionary = getTableSchema();
                        foreach (Field _field in _dictionary.FieldList)
                        {
                            if (_field.Pkey == true)
                            {
                                _wheres = String.Format("{0}[{1}]={2} and ", _wheres, _field.COLUMN_NAME, GetObjectFieldValueByField(_field));
                            }

                            if (_field.NULLABLE == 0)
                            {
                                if (string.IsNullOrEmpty(GetObjectFieldValueByField(_field)))
                                    throw new Exception(String.Format("The field {0} can't accept null value.", _field.COLUMN_NAME));
                            }

                            if (_field.TYPE_NAME == "int identity")
                            {
                                continue;
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

                            if (this.IsFullUpdate == true || _fieldValue != "null")
                            {
                                _sets = String.Format("{0}[{1}]={2},", _sets, _field.COLUMN_NAME, _fieldValue);
                            }
                        }

                        if (_wheres.IndexOf(" and ") > 0) _wheres = _wheres.Remove(_wheres.Length - 5, 5);
                        if (_sets.IndexOf(",") > 0) _sets = _sets.Remove(_sets.Length - 1, 1);

                        _result = String.Format("{0} Set {1} Where {2}", _result, _sets, _wheres);
                        _sQL_Update = _result;
                        #endregion
                    }
                }
                return _sQL_Update;
            }
        }



        private string _sQL_Delete;
        /// <summary>
        ///	SQL delete script.
        /// </summary>
        public string SQL_Delete
        {
            get
            {
                if (_sQL_Delete == null)
                {
                    string _result = " Delete [" + TableName + "]  Where ";
                    TableSchema _dictionary = getTableSchema();
                    foreach (Field _field in _dictionary.FieldList)
                    {
                        if (_field.Pkey == true)
                        {
                            _result = _result + "[" + _field.COLUMN_NAME + "]=" + GetObjectFieldValueByField(_field) + " and ";
                        }
                    }
                    if (_result.IndexOf(" and ") > 0) _result = _result.Remove(_result.Length - 5, 5);
                    _sQL_Delete = _result;
                }
                return _sQL_Delete;
            }
        }

        private PropertyItemList _propertyItemList;
        private string _registryKeyValue;
        private Entity _oEntity;

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
        /// <summary>
        ///	The database table name.
        /// </summary>
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


        private bool _isFullUpdate;
        public bool IsFullUpdate
        {
            set
            {
                _isFullUpdate = value;
            }
            get
            {
                return _isFullUpdate;
            }
        }


        #endregion

        /// <summary>
        ///	Constructor of gateway.
        /// </summary>
        /// <param name="registryKeyValue">the registry key value of data connection string.</param>
        /// <param name="objectEntity">Object entity.</param>
        /// <param name="connectionString">the connection string key of data connection string.</param>
        public Gateway(Entity o)
        {
            underGateway(o.DataConnectProviders);
            this._oEntity = o;
        }
        public Gateway(string dataConnectProviders)
        {
            underGateway(dataConnectProviders);
        }
        public Gateway(string dataConnectProviders, Transaction trans)
        {
            underGateway(dataConnectProviders);
            this._trans = trans;
        }
        public Gateway(Entity o, Transaction trans)
        {
            underGateway(o.DataConnectProviders);
            this._oEntity = o;
            this._trans = trans;
        }

        /// <summary>
        ///	For Build Full SQL(Update ,Insert SQL)
        /// </summary>
        private static bool? _fullSQL;
        protected static bool FullSQL
        {
            get
            {
                if (_fullSQL != null)
                {
                    return _fullSQL.Value;
                }
                try
                {
                    if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["FullSQL"]))
                    {
                        _fullSQL = false;
                        return _fullSQL.Value;
                    }
                    else
                    {
                        _fullSQL = bool.Parse(ConfigurationManager.AppSettings["FullSQL"]);
                        return _fullSQL.Value;
                    }
                }
                catch
                {
                    _fullSQL = false;
                    return _fullSQL.Value;
                }
            }
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

        /// <summary>
        ///	Get entity object simple method.<br />
        /// </summary>
        /// <param name="sql">the sql script or stored procedure with parameter.</param>
        /// <returns>Return the object from database.</returns>
        public ReturnValue getEntity(string sql, bool isCache)
        {
            ReturnValue _result = new ReturnValue();


            _result.SQLText = sql;
            _result.Success = true;

            try
            {
                Entity _object = Helper.CreateNewEntityInstanceOf(EntityType);

                BuildPropertyItemList();

                DataFactory _dal = this.getDataFactory();
                _dal.IsCache = isCache;

                DataTable dt = _dal.getDataTable(sql);

                foreach (DataRow dr in dt.Rows)
                {
                    _object = this.BindDataToObject(dr);
                    _object.TableName = _oEntity.TableName;
                }
                _result.Object = _object;
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

        /// <summary>
        ///	Get entity list simple method.<br/>
        /// </summary>
        /// <param name="sql">the sql script or stored procedure with parameter.</param>
        /// <returns>Return a entity list type object.</returns>
        public ReturnValue getEntityList(string sql, bool isCache)
        {
            ReturnValue _result = new ReturnValue();
            _result.SQLText = sql;
            _result.Success = true;

            try
            {
                EntityList _datalist = new EntityList();
                BuildPropertyItemList();

                DataFactory _dal = this.getDataFactory();
                _dal.IsCache = isCache;

                DataTable dt = _dal.getDataTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    Entity _item = BindDataToObject(dr);
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

        /// <summary>
        ///	Get the DataSet object simple method.<br />
        /// </summary>
        /// <param name="sql">the sql script or stored procedure with parameter.</param>
        /// <returns>Return a DataSet object.</returns>
        public ReturnValue getDataSet(string sql, bool isCache)
        {
            ReturnValue _result = new ReturnValue();
            _result.SQLText = sql;
            _result.Success = true;

            try
            {
                DataFactory _dal = getDataFactory();
                _dal.IsCache = isCache;

                DataSet _ds = new DataSet();

                _ds = _dal.getDataSet(sql);
                _result.DataSet = _ds;
            }
            catch (DBException exa)
            {
                _result.Object = null;
                _result.Success = false;
                _result.ErrMessage = exa.ToString() + "---" + _globalErrorHandler.ExceptionToString(exa);
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.Object = null;
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "---" + _globalErrorHandler.ExceptionToString(ex);
            }
            return _result;
        }


        public ReturnValue getDataTable(string sql)
        {
            ReturnValue _result = new ReturnValue();
            _result.SQLText = sql;
            _result.Success = true;

            try
            {
                DataFactory _dal = getDataFactory();

                DataTable _dt = new DataTable();

                _dt = _dal.getDataTable(sql);
                _result.ObjectValue = _dt;
            }
            catch (DBException exa)
            {
                _result.Object = null;
                _result.Success = false;
                _result.ErrMessage = exa.ToString() + "---" + _globalErrorHandler.ExceptionToString(exa);
                _result.Code = exa.ErrorCode;
            }
            catch (Exception ex)
            {
                _result.Object = null;
                _result.Success = false;
                _result.ErrMessage = ex.Message + "--" + ex.InnerException + "--" + ex.StackTrace + "---" + _globalErrorHandler.ExceptionToString(ex);
            }

            return _result;
        }

        /// <summary>
        ///	Execute Sql script simple method.<br />
        /// </summary>
        /// <param name="sql">the sql script or stored procedure with parameter.</param>
        /// <returns>
        ///	The DataFactory error code.
        ///	</returns>
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

        /// <summary>
        ///	Insert into database for this entity object.<br />
        /// </summary>
        /// <returns>The DataFactory error code.</returns>
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

        public ReturnValue SaveIDENTITYINSERT()
        {
            ReturnValue _result = new ReturnValue();
            _result.Success = true;
            _result.Table = this.TableName;


            try
            {
                DataFactory dal = this.getDataFactory();
                _result.SQLText = this.SQL_InsertIDENTITYINSERT;
                DataTable dt = dal.getDataTable(this.SQL_InsertIDENTITYINSERT);
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

        /// <summary>
        ///	Update into database for this entity object.<br />
        /// </summary>
        /// <returns>The DataFactory error code.</returns>
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


        /// <summary>
        ///	Delete into database for this entity object.<br />
        /// </summary>
        /// <returns>The DataFactory error code.</returns>
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


        #region Private function
        /// <summary>
        ///	Initialization DataFactory.
        /// </summary>
        /// <returns>instance of WComm.Framework.DataFactory of transaction.</returns>
        protected DataFactory getDataFactory()
        {
            DataFactory _result;
            if (this._trans == null)
            {
                if (String.IsNullOrEmpty(this._connectionString))
                {
                    _result = new DataFactory(this._registryKeyValue, this._providerType);
                }
                else
                {
                    _result = new DataFactory(this._registryKeyValue, this._providerType, this._connectionString);
                }
            }
            else
            {
                _result = new DataFactory(this._trans);
            }

            if (this._providerType == "PostgreSQL")
            {
                if (this._trans == null)
                {
                    _result = new DataFactoryPostgreSQL(this._registryKeyValue, this._providerType, this._connectionString);
                }
                else
                {
                    _result = new DataFactoryPostgreSQL(this._trans);
                }
            }

            return _result;

        }

        /// <summary>
        ///	Bind data to object.
        /// </summary>
        /// <param name="dr">DataTable</param>
        /// <param name="_object">object</param>
        /// <returns>object</returns>
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
                                            _value =HttpUtility.HtmlDecode(_value);
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

        /// <summary>
        ///	Build property item list.
        /// </summary>
        /// <param name="_object">Object</param>
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

                TableSchema _dictionary = getTableSchema();
                string _where = string.Empty;
                foreach (Field _field in _dictionary.FieldList)
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
                    return "1";
                case false:
                    return "0";
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
        /// <summary>
        ///	Get Object Field Value By Field.
        /// </summary>
        /// <param name="field">Field</param>
        /// <returns>string</returns>
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

        /// <summary>
        ///	Get Object Field Name By Field.
        /// </summary>
        /// <param name="field">Field</param>
        /// <returns>string</returns>
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

        /// <summary>
        ///	Initialization Dictionary.
        /// </summary>
        /// <returns>Dictionary instance.</returns>
        private TableSchema getTableSchema()
        {
            TableSchema _result;
            _result = new TableSchema(this._tableName, this._dataConnectProviders);


            return _result;

        }

        #endregion
    }
}