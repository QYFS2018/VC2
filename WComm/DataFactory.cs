using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Web;
using System.Web.Caching;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Transactions;


namespace WComm
{
    internal class DataFactory
    {
        protected string _conString;
        protected string _providerType;
        protected Transaction _trans;
        private bool _isCache;
        public bool IsCache
        {
            set
            {
                _isCache = value;
            }
            get
            {
                return _isCache;
            }
        }

        protected static string DataSetCommandTimeout = System.Configuration.ConfigurationSettings.AppSettings["DataSetCommandTimeout"];
        protected static string CommandTimeout = System.Configuration.ConfigurationSettings.AppSettings["CommandTimeout"];

        public DataFactory(){}

        public DataFactory(string url, string providerType)
        {
            this._conString = RegistryAccess.getRegistryKeyValue(url, "zstoreConnectionString");
            this._providerType = providerType;
        }
        public DataFactory(Transaction strans)
        {
            this._trans = strans;

        }
        public DataFactory(string url, string providerType, string keyValue)
        {
            if (url.ToLower() != "config" && url.ToUpper() != "CONNECTIONVAR")
            {
                this._conString = RegistryAccess.getRegistryKeyValue(url, keyValue);
            }
            else
            {
                this._conString = keyValue;
            }

            this._providerType = providerType;
        }

        public virtual DataSet getDataSet(string sql)
        {
            if (this._isCache == true)
            {
                if (HttpRuntime.Cache[sql] != null)
                {
                    return (DataSet)HttpRuntime.Cache[sql];
                }
            }

            DataSet ds = new DataSet();
            DbDataAdapter adpt = null;
            SqlCacheDependency dependency = null;

            try
            {
                adpt = getDataAdapter(sql);

                if (DataSetCommandTimeout != null)
                {
                    adpt.SelectCommand.CommandTimeout = int.Parse(DataSetCommandTimeout);
                }


                if (this._isCache == true)
                {
                    string type = adpt.SelectCommand.GetType().ToString();
                    if (type == "System.Data.SqlClient.SqlCommand")
                    {
                        SqlDependency.Start(_conString);

                        dependency = new SqlCacheDependency((System.Data.SqlClient.SqlCommand)adpt.SelectCommand);
                    }
                }

                adpt.Fill(ds);

                if (this._isCache == true)
                {
                    HttpRuntime.Cache.Add(sql, ds, dependency, System.Web.Caching.Cache.NoAbsoluteExpiration,
                      TimeSpan.FromSeconds(60), System.Web.Caching.CacheItemPriority.Default, null);

                }
            }
            catch (SqlException exa)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, exa);
            }
            catch (Exception ex)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, ex);
            }
            finally
            {
                if (adpt != null && adpt.SelectCommand != null && adpt.SelectCommand.Connection != null && adpt.SelectCommand.Connection.State != ConnectionState.Closed)
                    adpt.SelectCommand.Connection.Close();
            }

            return ds;
        }

        public virtual DataTable getDataTable(string sql)
        {
            if (this.IsCache == true)
            {
                if (HttpRuntime.Cache[sql] != null)
                {
                    return (DataTable)HttpRuntime.Cache[sql];
                }
            }

            DataTable dt = new DataTable();
            DbCommand dbCommand = null;
            SqlCacheDependency dependency = null;

            try
            {
                dbCommand = getCommand(sql);


                if (this.IsCache == true)
                {
                    string type = dbCommand.GetType().ToString();
                    if (type == "System.Data.SqlClient.SqlCommand")
                    {
                        SqlDependency.Start(_conString);

                        dependency = new SqlCacheDependency((System.Data.SqlClient.SqlCommand)dbCommand);
                    }
                }

                DbDataReader rdr;

                rdr = dbCommand.ExecuteReader();
                dt.Load(rdr);
                rdr.Close();

                if (this.IsCache == true)
                {
                    HttpRuntime.Cache.Add(sql, dt, dependency, System.Web.Caching.Cache.NoAbsoluteExpiration,
                      TimeSpan.FromSeconds(60), System.Web.Caching.CacheItemPriority.Default, null);

                }



            }
            catch (SqlException exa)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, exa);
            }
            catch (Exception ex)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, ex);
            }
            finally
            {
                if (dbCommand != null && dbCommand.Connection != null && dbCommand.Connection.State != ConnectionState.Closed && this._trans == null)
                    dbCommand.Connection.Close();
            }
            return dt;
        }

        public virtual int execSql(string sql)
        {
            int retVal = 0;
            DbCommand dbCommand = null;

            try
            {
                dbCommand = getCommand(sql);

                //Execute the statement and return the # of rows effected.
                //	NOTE: this may be innaccurate.
                retVal = dbCommand.ExecuteNonQuery();
            }
            catch (SqlException exa)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, exa);
            }
            catch (Exception ex)
            {
                //Execption thrown, provide debugging information as to where it 
                //	happened and what was going on when it happened.
                string detail = "Exception occurred in DataAccess.execSql\n"
                    + "Could not execute the following query:\n" + sql + "\n"
                    + "See inner exception information for details.";
                //Pass the exception up to be handled closer to the interface.
                throw new DBException(detail, ex);
            }
            finally
            {
                if (dbCommand != null && dbCommand != null && dbCommand.Connection != null && dbCommand.Connection.State != ConnectionState.Closed && this._trans == null)
                    dbCommand.Connection.Close();
            }

            //Do not rely on this as being correct.
            return retVal;	//Rows effected - may not be accurate depending on the statemnt
        }

        private DbCommand getCommand(string sql)
        {
            DbConnection dbConnect;
            DbCommand dbCommand;

            if (this._trans == null)
            {
                DbProviderFactory dbFactory = DbProviderFactories.GetFactory(this._providerType);
                dbCommand = dbFactory.CreateCommand();
                dbCommand.CommandText = sql;

                int _commandTimeout = 20;

                if (CommandTimeout != null)
                {
                    _commandTimeout = int.Parse(CommandTimeout);
                }

                dbCommand.CommandTimeout = _commandTimeout;

                dbConnect = dbFactory.CreateConnection();
                dbConnect.ConnectionString = _conString;
                dbConnect.Open();
            }
            else
            {
                dbConnect = this._trans.DbConnect;
                dbCommand = dbConnect.CreateCommand();
                dbCommand.Transaction = this._trans.ITransaction;
                dbCommand.CommandText = sql;
                int _commandTimeout = 20;

                if (CommandTimeout != null)
                {
                    _commandTimeout = int.Parse(CommandTimeout);
                }

                dbCommand.CommandTimeout = _commandTimeout;
            }
            dbCommand.Connection = dbConnect;

            return dbCommand;
        }

        private DbDataAdapter getDataAdapter(string sql)
        {
            DbProviderFactory dbFactory = DbProviderFactories.GetFactory(this._providerType);
            DbDataAdapter dbAdapter = dbFactory.CreateDataAdapter();

            dbAdapter.SelectCommand = getCommand(sql);
            return dbAdapter;
        }

    }


    /// <summary>
    /// Database exception class.  This creates a special class that inherits from 
    /// System.Exception and holds information about the exception and additional
    /// information from the DataAccess class as to what, when, and other information
    /// that will be handy in debugging.
    /// </summary>
    internal class DBException : System.Exception
    {
        /// <summary>
        /// DBException constructor:  Must be created with additional details
        /// and an underlying exception.
        /// </summary>
        /// <param name="details">Class specific information describing events leanding up to the exception.</param>
        /// <param name="innerException">System exception thrown.</param>
        public DBException(string details, Exception innerException)
        {
            //Details of the exception
            _details = details;
            //System exeception thrown
            _inner = innerException;
            //When the exception was thrown.
            _occurred = System.DateTime.Now;

            //			int intCount = ((System.Data.OleDb.OleDbException)_inner).Errors.Count-1;
            //			ErrorCode=((System.Data.OleDb.OleDbException)_inner).Errors[intCount].NativeError;
            //			if(ErrorCode == 50000)
            //			{
            //				ErrorCode = int.Parse(((System.Data.OleDb.OleDbException)_inner).Errors[intCount].Message);
            //			}
            ErrorCode = -1;
        }

        public DBException(string details, SqlException innerException)
        {
            //Details of the exception
            _details = details;
            //System exeception thrown
            _inner = innerException;
            //When the exception was thrown.
            _occurred = System.DateTime.Now;

            //			int intCount = ((System.Data.OleDb.OleDbException)_inner).Errors.Count-1;
            //			ErrorCode=((System.Data.OleDb.OleDbException)_inner).Errors[intCount].NativeError;
            //			if(ErrorCode == 50000)
            //			{
            //				ErrorCode = int.Parse(((System.Data.OleDb.OleDbException)_inner).Errors[intCount].Message);
            //			}
            ErrorCode = innerException.Number;
        }

        public DBException(string details, Npgsql.NpgsqlException innerException)
        {
            //Details of the exception
            _details = details;
            //System exeception thrown
            _inner = innerException;
            //When the exception was thrown.
            _occurred = System.DateTime.Now;

            //			int intCount = ((System.Data.OleDb.OleDbException)_inner).Errors.Count-1;
            //			ErrorCode=((System.Data.OleDb.OleDbException)_inner).Errors[intCount].NativeError;
            //			if(ErrorCode == 50000)
            //			{
            //				ErrorCode = int.Parse(((System.Data.OleDb.OleDbException)_inner).Errors[intCount].Message);
            //			}
            ErrorCode =int.Parse( innerException.Code);
        }

        /// <summary>
        /// Override of the ToString method.
        /// </summary>
        /// <returns>Compiled information on the exception that was thrown.</returns>
        public override string ToString()
        {
            string retValue = "Exception occurred at: " + _occurred.ToString()
                + "\nDetails: " + _details + "\nInner Exception: " + _inner.ToString();
            return retValue;
        }
        /// <summary>Private data member holding the information about the system exception that was thrown.</summary>
        private Exception _inner;
        /// <summary>Private data member that holds additional information about what happened in the class before the exception was thrown.</summary>
        private string _details;
        /// <summary>Private data member that holds information as to when the exception was thrown.</summary>
        private System.DateTime _occurred;
        public int ErrorCode;
    }
}