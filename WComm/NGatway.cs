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
    internal class NGateway
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
        private NEntity _oEntity;

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


     

        #endregion

        /// <summary>
        ///	Constructor of gateway.
        /// </summary>
        /// <param name="registryKeyValue">the registry key value of data connection string.</param>
        /// <param name="objectEntity">Object entity.</param>
        /// <param name="connectionString">the connection string key of data connection string.</param>
        public NGateway(NEntity o)
        {
            underGateway(o.DataConnectProviders);
            this._oEntity = o;
        }
        public NGateway(string dataConnectProviders)
        {
            underGateway(dataConnectProviders);
        }
        public NGateway(string dataConnectProviders, Transaction trans)
        {
            underGateway(dataConnectProviders);
            this._trans = trans;
        }
        public NGateway(NEntity o, Transaction trans)
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
                    NEntity _item = this._oEntity.BindDataToNEntity(dr);
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

               // NEntity _item = Activator.CreateInstance(EntityType) as NEntity;

                NEntity _item =  Helper.CreateNewEntityInstanceOf(EntityType) as NEntity;
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
        public ReturnValue Save(string sql)
        {
            ReturnValue _result = new ReturnValue();
            _result.Success = true;
            _result.Table = this.TableName;


            try
            {
                DataFactory dal = this.getDataFactory();
                _result.SQLText = sql;
                DataTable dt = dal.getDataTable(sql);
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

        protected DataFactory getDataFactory()
        {
            DataFactory _result;

            if (WComm.CommonLogic.Connect() == false)
            {
                return new DataFactory();
            }

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
        private void underGateway(string dataConnectProviders)
        {
            Connector _connector = new Connector(dataConnectProviders);
            this._registryKeyValue = _connector.RegistryKeyValue;
            this._connectionString = _connector.ConnectionString;
            this._providerType = _connector.ProviderType;
            this._dataConnectProviders = dataConnectProviders;

            ConInfo.DataConnectProviders = dataConnectProviders;
        }
    }
}
