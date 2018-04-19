using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection ;
using System.Text ;
using System.Web.Mail ;
using System.Runtime.Serialization;
using System.Data.OleDb ;
using System.IO;
using System.Xml;
using System.Web.Caching;
using System.Transactions;



namespace WComm
{
    /// <summary>
    ///	The database business object.
    ///	You need inherit this class in your business entity object class.
    /// </summary>
    //public class Entity : ICloneable
    [Serializable]
    public class Entity :  ICloneable
    {
        private string _dataConnectProviders;
        public string DataConnectProviders
        {
            set
            {
                _dataConnectProviders = value;
            }
            get
            {
                return _dataConnectProviders;
            }
        }

        private string _tableName;
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

        public DBOperationType DBOperationType = DBOperationType.Update;

        /// <summary>
        ///	Default construct method.
        /// </summary>
        public Entity()
        {

        }

        /// <summary>
        ///	Validate this entity of business logic.
        /// </summary>
        /// <returns>If it is valid.</returns>
        protected virtual ReturnValue Validate()
        {
            ReturnValue result = new ReturnValue();
            result.Success = true;
            return result;
        }


        /// <summary>
        ///	Get entity object simple method.
        ///	<code>
        ///	Product product = new Product();
        ///	product = product.getEntity("select * from product where ProductId=1");
        ///	</code>
        /// </summary>
        /// <param name="sql">the sql script or stored procedure with parameter.</param>
        /// <returns>Return the object from database.</returns>
        public virtual ReturnValue getEntity(string sql, bool isCache)
        {
            Gateway _gateway = new Gateway(this);
            ReturnValue result = _gateway.getEntity(sql, isCache);
            SaveLog(result, _gateway.TableName, "getEntity");

            return result;
        }
        public virtual ReturnValue getEntity(string sql)
        {
            return getEntity(sql, false);
        }
        public virtual ReturnValue getEntity(string sql, Transaction trans)
        {
            Gateway _gateway = new Gateway(this, trans);
            ReturnValue result = _gateway.getEntity(sql, false);
            SaveLog(result, _gateway.TableName, "getEntity");
            return result;
        }

        /// <summary>
        ///	Get entity list simple method.<br/>
        ///	<code>	
        ///	Product product = new Product();
        ///	EntityList entityList = product.getEntityList("select * from product");
        ///	</code>
        /// </summary>
        /// <param name="sql">the sql script or stored procedure with parameter.</param>
        /// <returns>Return a entity list type object.</returns>
        public virtual ReturnValue getEntityList(string sql, bool isCache)
        {
            Gateway _gateway = new Gateway(this);
            ReturnValue result = _gateway.getEntityList(sql, isCache);
            SaveLog(result, _gateway.TableName, "getEntityList");

            return result;
        }
        public virtual ReturnValue getEntityList(string sql)
        {
            return getEntityList(sql, false);
        }
        public virtual ReturnValue getEntityList(string sql, Transaction trans)
        {
            Gateway _gateway = new Gateway(this, trans);
            ReturnValue result = _gateway.getEntityList(sql, false);
            SaveLog(result, _gateway.TableName, "getEntityList");

            return result;
        }

        /// <summary>
        ///	Get the DataSet object simple method.<br />
        ///	<code>DataSet ds = Entity.getDataSet("select * from product");</code>
        /// </summary>
        /// <param name="RegistryKeyValue">The registry Key Value of database connection string.</param>
        /// <param name="sql">the sql script or stored procedure with parameter.</param>
        /// <returns>Return a DataSet object.</returns>
        public virtual ReturnValue getDataSet(string sql, bool isCache)
        {
            Gateway _gateway = new Gateway(_dataConnectProviders);
            ReturnValue result = _gateway.getDataSet(sql, isCache);

            SaveLog(result, "", "getDataSet");

            return result;
        }
        public virtual ReturnValue getDataSet(string sql)
        {
            return getDataSet(sql, false);
        }
        public virtual ReturnValue getDataSet(string sql, Transaction trans)
        {
            Gateway _gateway = new Gateway(_dataConnectProviders, trans);
            ReturnValue result = _gateway.getDataSet(sql, false);

            SaveLog(result, "", "getDataSet");

            return result;
        }

        public virtual ReturnValue getDataTable(string sql, Transaction trans)
        {
            Gateway _gateway = new Gateway(_dataConnectProviders, trans);
            ReturnValue result = _gateway.getDataTable(sql);
            return result;
        }


        public virtual ReturnValue getDataTable(string sql)
        {
            Gateway _gateway = new Gateway(_dataConnectProviders);
            ReturnValue result = _gateway.getDataTable(sql);
            return result;
        }

        public virtual ReturnValue ExecSql(string sql)
        {
            WComm.Transaction _transcation = null;
            //if (string.IsNullOrEmpty(this.DataConnectProviders) == false)
            //{
            //    _transcation = new WComm.Transaction(this.DataConnectProviders);
            //}
            //else
            //{
            //    _transcation = new WComm.Transaction();
            //}
            ReturnValue _result = new ReturnValue();
            _result = this.ExecSql(sql,_transcation);
            //if (_result.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return _result;
            //}

            //ReturnValue commitRV = _transcation.CommitTransaction();
            //if (commitRV.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return commitRV;
            //}

            return _result;
        }
        public virtual ReturnValue ExecSql(string sql, Transaction trans)
        {
            Gateway _gateway = new Gateway(this, trans);
            return underExecSql(sql, _gateway);

        }
        private ReturnValue underExecSql(string sql, Gateway gateway)
        {
            ReturnValue result = gateway.ExecSql(sql);

            if (result.Success == false)
            {
                SaveLog(result, gateway.TableName, "ExecSql");
            }
            else
            {
                result = this.SaveTraceLog(result, gateway.TableName, "ExecSql", gateway.Trans);
            }

            return result;
        }



        public virtual ReturnValue SaveIDENTITYINSERT(Transaction trans)
        {

            ReturnValue _result = new ReturnValue();
            Gateway _gateway = new Gateway(this, trans);

            _result = _gateway.ExecSql("SET IDENTITY_INSERT [" + _gateway.TableName + "] ON");
            if (_result.Success == false)
            {
                return _result;
            }
            _result = _gateway.SaveIDENTITYINSERT();
            if (_result.Success == false)
            {
                return _result;
            }
            _result = _gateway.ExecSql("SET IDENTITY_INSERT [" + _gateway.TableName + "] OFF");
            if (_result.Success == false)
            {
                return _result;
            }
            return _result;
        }


        public virtual ReturnValue Save()
        {
            WComm.Transaction _transcation = null;
            //if (string.IsNullOrEmpty(this.DataConnectProviders) == false)
            //{
            //    _transcation = new WComm.Transaction(this.DataConnectProviders);
            //}
            //else
            //{
            //    _transcation = new WComm.Transaction();
            //}

            ReturnValue _result = new ReturnValue();
            _result = this.Save(_transcation);
            //if (_result.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return _result;
            //}

            //ReturnValue commitRV = _transcation.CommitTransaction();
            //if (commitRV.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return commitRV;
            //}

            return _result;

        }
        public virtual ReturnValue Save(Transaction trans)
        {
            Gateway _gateway = new Gateway(this, trans);
            return underSave(_gateway);
        }
        private ReturnValue underSave(Gateway gateway)
        {
            ReturnValue result = Validate();
            if (!result.Success)
            {
                return result;
            }

            result = gateway.Save();

            #region App_Log_SQL
            if (gateway.TableName.ToUpper() != "App_Log_SQL".ToUpper())
            {
                if (result.Success == false)
                {
                    SaveLog(result, gateway.TableName, "Save");
                }
                else
                {
                    result = this.SaveTraceLog(result, gateway.TableName, "Save", gateway.Trans);
                }

            }
            else
            {
                if (result.Success == false)
                {
                    string path = "App_Log_Falied_Result.log";
                    string _message = "Date:" + System.DateTime.Now.ToString() + "\r\n" +
                                "Table Name:" + gateway.TableName + "\r\n" +
                                "SQL:" + result.SQLText + "\r\n" +
                                "Result:" + result.ErrMessage + "\r\n\r\n";

                    if (System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] == null)
                    {
                        try
                        {
                            path = HttpContext.Current.Request.PhysicalApplicationPath + path;
                        }
                        catch { }

                    }
                    else
                    {
                        path = System.Configuration.ConfigurationManager.AppSettings["LogFilePath"] + path;
                    }

                    try
                    {

                        if (!File.Exists(path))
                        {
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine(_message);
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = File.AppendText(path))
                            {
                                sw.WriteLine(_message);
                            }
                        }
                    }
                    catch { }

                    LogEmail.Send(this as App_Log_SQL);
                }
            }
            #endregion

            return result;
        }


        public virtual ReturnValue Update()
        {
            WComm.Transaction _transcation = null;
            //if (string.IsNullOrEmpty(this.DataConnectProviders) == false)
            //{
            //    _transcation = new WComm.Transaction(this.DataConnectProviders);
            //}
            //else
            //{
            //    _transcation = new WComm.Transaction();
            //}

            ReturnValue _result = new ReturnValue();
            _result = this.Update (_transcation);
            //if (_result.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return _result;
            //}

            //ReturnValue commitRV = _transcation.CommitTransaction();
            //if (commitRV.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return commitRV;
            //}

            return _result;
        }
        public virtual ReturnValue Update(Transaction trans)
        {
            Gateway _gateway = new Gateway(this, trans);
            _gateway.IsFullUpdate = true;
            return underUpdate(_gateway, true);
        }
        public virtual ReturnValue UpdateNotNull()
        {
            WComm.Transaction _transcation = null;
            //if (string.IsNullOrEmpty(this.DataConnectProviders) == false)
            //{
            //    _transcation = new WComm.Transaction(this.DataConnectProviders);
            //}
            //else
            //{
            //    _transcation = new WComm.Transaction();
            //}

            ReturnValue _result = new ReturnValue();
            _result = this.UpdateNotNull(_transcation);
            //if (_result.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return _result;
            //}

            //ReturnValue commitRV = _transcation.CommitTransaction();
            //if (commitRV.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return commitRV;
            //}

            return _result;

        }
        public virtual ReturnValue UpdateNotNull(Transaction trans)
        {
            Gateway _gateway = new Gateway(this, trans);
            _gateway.IsFullUpdate = false;
            return underUpdate(_gateway, false);
        }
        private ReturnValue underUpdate(Gateway gateway, bool IsValidate)
        {
            ReturnValue result;
            if (IsValidate)
            {
                result = Validate();
                if (!result.Success)
                {
                    return result;
                }
            }
            result = gateway.Update();

            if (result.Success == false)
            {
                SaveLog(result, gateway.TableName, "Update");
            }
            else
            {
                result = this.SaveTraceLog(result, gateway.TableName, "Update", gateway.Trans);
            }



            return result;
        }


        public virtual ReturnValue Delete()
        {
            WComm.Transaction _transcation = null;
            //if (string.IsNullOrEmpty(this.DataConnectProviders) == false)
            //{
            //    _transcation = new WComm.Transaction(this.DataConnectProviders);
            //}
            //else
            //{
            //    _transcation = new WComm.Transaction();
            //}

            ReturnValue _result = new ReturnValue();
            _result=this.Delete(_transcation);
            //if (_result.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return _result;
            //}

            //ReturnValue commitRV = _transcation.CommitTransaction();
            //if (commitRV.Success == false)
            //{
            //    _transcation.RollbackTransaction();
            //    return commitRV;
            //}

            return _result;
        }
        public virtual ReturnValue Delete(Transaction trans)
        {
            Gateway _gateway = new Gateway(this, trans);
            return underDelete(_gateway);
        }
        private ReturnValue underDelete(Gateway gateway)
        {
            ReturnValue result = gateway.Delete();

            if (result.Success == false)
            {
                SaveLog(result, gateway.TableName, "Delete");
            }
            else
            {
                result = this.SaveTraceLog(result, gateway.TableName, "Delete", gateway.Trans);
            }

            return result;
        }

        public object Clone()
        {
            object result = Activator.CreateInstance(GetType());
            PropertyInfo[] properties = this.GetType().GetProperties();
            for (int i = 0; i < properties.Length; i++)
            {
                object obj = properties[i].GetValue(this, null);
                properties[i].SetValue(result, obj, null);
            }
            return result;
        }


        /// <summary>
        ///	Compare to another new entity.
        ///	check the difference of two entity.
        ///	<code>
        ///	Product product1 = new Product();
        ///	product1.ProductId = 1;
        ///	Product product2 = new Product();
        ///	product2.ProductId = 2;
        ///	ArrayList list = product1.Compare(Product2);
        ///	</code>
        /// </summary>
        /// <param name="newEntity">The new entity instance for compare.</param>
        /// <returns>A arrylist contains WComm.Framework.EntityDifference instance between the two entity.</returns>
        public ArrayList Compare(Entity newEntity)
        {
            return CompareEntity(this, newEntity);
        }

        /// <summary>
        ///	Compare to another new entity.
        ///	check the difference of two entity.
        ///	<code>
        ///	Product product1 = new Product();
        ///	product1.ProductId = 1;
        ///	Product product2 = new Product();
        ///	product2.ProductId = 2;
        ///	ArrayList list = Entity.CompareEntity(product1,product2);
        ///	</code>
        /// </summary>
        /// <param name="OriginalEntity">The original entity instance.</param>
        /// <param name="NewEntity">The new entity instance.</param>
        /// <returns>A arrylist contains WComm.Framework.EntityDifference instance between the two entity.</returns>
        public static ArrayList CompareEntity(Entity OriginalEntity, Entity NewEntity)
        {
            ArrayList result = new ArrayList();
            PropertyInfo[] originalProperties = OriginalEntity.GetType().GetProperties();
            PropertyInfo[] newProperties = NewEntity.GetType().GetProperties();
            for (int i = 0; i < originalProperties.Length; i++)
            {
                object originalValue = originalProperties[i].GetValue(OriginalEntity, null);
                object newValue = newProperties[i].GetValue(NewEntity, null);
                if (!object.Equals(originalValue, newValue))
                {
                    EntityDifference entityDifference = new EntityDifference(originalProperties[i].Name, originalValue, newValue);
                    result.Add(entityDifference);
                }
            }
            return result;
        }
        /// <summary>
        ///	save App_Log_SQL to database for transaction.
        /// </summary>
        /// <param name="returnvalue">the result of WComm.Framework.ReturnValue.</param>
        /// <param name="dal">The DataFactory instance of transaction.</param>
        protected void SaveLog(ReturnValue returnvalue, string TableName, string SQLType)
        {
            SaveLogType saveLogType = App_Log_SQL.getSaveLogConfig();
            switch (saveLogType)
            {
                case SaveLogType.None:
                    {
                        break;
                    }
                case SaveLogType.Always:
                    {
                        App_Log_SQL appLogSQL = new App_Log_SQL();

                        appLogSQL.DataConnectProviders = this.DataConnectProviders;
                        appLogSQL.CreatedOn = System.DateTime.Now;
                        appLogSQL.SQLText = returnvalue.SQLText;
                        appLogSQL.Success = returnvalue.Success;
                        appLogSQL.Notes = returnvalue.ErrMessage;
                        appLogSQL.TableName = TableName;
                        appLogSQL.SQLType = SQLType;
                        try
                        {
                            if (WComm.Utilities.isInteger(WComm.Utilities.CurrentProgramId.ToString()))
                            {
                                appLogSQL.ProgramId = WComm.Utilities.CurrentProgramId;
                            }
                            if (WComm.Utilities.isInteger(WComm.Utilities.CurrentUserId.ToString()))
                            {
                                appLogSQL.UserId = WComm.Utilities.CurrentUserId;
                            }
                        }
                        catch { }
                        appLogSQL.Save();

                        break;
                    }
                case SaveLogType.Error:
                    {
                        if (returnvalue.Success == false)
                        {
                            App_Log_SQL appLogSQL = new App_Log_SQL();
                            appLogSQL.DataConnectProviders = this.DataConnectProviders;
                            appLogSQL.CreatedOn = System.DateTime.Now;
                            appLogSQL.SQLText = returnvalue.SQLText;
                            appLogSQL.Success = returnvalue.Success;
                            appLogSQL.Notes = returnvalue.ErrMessage;
                            appLogSQL.TableName = TableName;
                            appLogSQL.SQLType = SQLType;
                            try
                            {
                                if (WComm.Utilities.isInteger(WComm.Utilities.CurrentProgramId.ToString()))
                                {
                                    appLogSQL.ProgramId = WComm.Utilities.CurrentProgramId;
                                }
                                if (WComm.Utilities.isInteger(WComm.Utilities.CurrentUserId.ToString()))
                                {
                                    appLogSQL.UserId = WComm.Utilities.CurrentUserId;
                                }
                            }
                            catch { }

                            if (returnvalue.ErrMessage != null && returnvalue.ErrMessage.ToUpper().IndexOf("TIMEOUT") > 0)
                            {
                                appLogSQL.Type = "TIMEOUT";
                            }

                            if (returnvalue.ErrMessage != null && returnvalue.ErrMessage.ToUpper().IndexOf("DEADLOCKED") > 0)
                            {
                                appLogSQL.Type = "DEADLOCKED";
                            }



                            using (TransactionScope ts_appLogSQL = new TransactionScope(TransactionScopeOption.Suppress))
                            {
                                appLogSQL.Save();
                                ts_appLogSQL.Complete();
                            }

                            LogEmail.Send(appLogSQL);

                        }
                        break;
                    }
            }

        }

        private ReturnValue SaveTraceLog(ReturnValue returnvalue, string TableName, string SQLType, Transaction trans)
        {
            ReturnValue _result = returnvalue;

            if (TableName.ToUpper() != "App_Admin_Log".ToUpper())
            {
                if (Utilities.TraceLog == true && returnvalue.Success == true)
                {
                    App_Admin_Log _app_Admin_Log = new App_Admin_Log();
                    _app_Admin_Log.CreatedOn = System.DateTime.Now;
                    _app_Admin_Log.SQLText = returnvalue.SQLText;
                    _app_Admin_Log.ProgramId = Utilities.CurrentProgramId;
                    _app_Admin_Log.TableName = TableName;
                    _app_Admin_Log.SQLType = SQLType;
                    _app_Admin_Log.UserId = Utilities.CurrentUserId;

                    _app_Admin_Log.IPAddress = HttpContext.Current.Request.UserHostAddress;
                    _app_Admin_Log.URL = WComm.Utilities.urlPath;
                    _app_Admin_Log.SessionId = HttpContext.Current.Session.SessionID;

                    _result = _app_Admin_Log.Save(trans);
                    _result.IdentityId = returnvalue.IdentityId;
                    //Added By Kevin 2008.6.26
                    _result.EffectRows = returnvalue.EffectRows;
                    //Added By Kevin 2008.6.26

                    SaveLog(_result, "App_Admin_Log", "Save");
                }

            }
            return _result;

        }

    }

    /// <summary>
    ///	The entity collection list.
    /// </summary>
    [Serializable ]
    public class EntityList : System.Collections.Generic .List<Entity>
    {
        /// <summary>
        ///	Sort this entity list.
        /// </summary>
        /// <param name="sortfield">Sort field.</param>
        /// <param name="isAscending">Is asceding.</param>
        public void Sort(string sortfield, bool isAscending)
        {
            if (this.Count != 0 && string.IsNullOrEmpty(sortfield) == false && string.IsNullOrEmpty(sortfield) == false)
            {
                base.Sort(new ListComparer(sortfield));
                if (!isAscending) base.Reverse();

               
            }
        }

        public EntityList Select(string selectField, string value)
        {
            EntityList entityList = new EntityList();
            foreach (Entity entity in this)
            {
                if (entity.GetType().GetProperty(selectField) != null)
                {
                    Type t = entity.GetType();
                    object o = t.GetProperty(selectField).GetValue(entity, null);
                    if (o == null)
                    {
                        if (value.ToString() == "null")
                        {
                            entityList.Add(entity);
                        }
                    }
                    else
                    {
                        if (o.ToString().ToLower() == value.ToLower())
                        {
                            entityList.Add(entity);
                        }
                    }
                }
            }
            return entityList;
        }

        public EntityList Search(string[] selectFields, string[] values)
        {
            EntityList entityList = new EntityList();
            foreach (Entity entity in this)
            {
                bool IsFound = false;
                for (int i = 0; i < selectFields.Length; i++)
                {
                    string selectField = selectFields[i];
                    string value = values[i];
                    if (entity.GetType().GetProperty(selectField) != null)
                    {
                        Type t = entity.GetType();
                        object o = t.GetProperty(selectField).GetValue(entity, null);
                        if (o == null)
                        {
                            if (value.ToString() == "null")
                            {
                                if (IsFound == false)
                                {
                                    entityList.Add(entity);
                                    IsFound = true;
                                }
                            }
                        }
                        else
                        {
                            if (o.ToString().ToLower().Contains(value.ToLower()))
                            {
                                if (IsFound == false)
                                {
                                    entityList.Add(entity);
                                    IsFound = true;
                                }
                            }
                        }
                    }
                }
            }
            return entityList;
        }

        public EntityList Filter(string filterField, string value)
        {
            EntityList entityList = new EntityList();
            foreach (Entity entity in this)
            {
                if (entity.GetType().GetProperty(filterField) != null)
                {
                    Type t = entity.GetType();
                    object o = t.GetProperty(filterField).GetValue(entity, null);
                    if (o == null)
                    {
                        if (value.ToString() == "null")
                        {
                            entityList.Add(entity);
                        }
                    }
                    else
                    {
                        if (o.ToString().ToLower().IndexOf(value.ToLower()) > -1)
                        {
                            entityList.Add(entity);
                        }
                    }
                }
            }
            return entityList;
        }

        public EntityList Clone() 
        {
            EntityList _result = new EntityList();
            foreach(Entity obj in this)
            {
                _result.Add(obj.Clone() as Entity);
            }
            return _result;
        }

        public ArrayList CloneToArrayList()
        {
            ArrayList _result = new ArrayList();
            foreach (Entity obj in this)
            {
                _result.Add(obj.Clone());
            }
            return _result;
        }

    }
    public enum DBOperationType
    {
        Update, Delete, Save
    }

    
}
