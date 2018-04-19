using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Collections;
using System.Web.Caching;
using System.Data;

namespace WComm
{
    /// <summary>
    ///	the dictionary of table structure.
    /// </summary>
    [Serializable]
    internal class TableSchema
    {
        /// <summary>
        ///	Cache collection of table structures.
        /// </summary>

        public TableSchema(string tableName, string dataConnectProviders)
        {
            Connector _connector = new Connector(dataConnectProviders);
            this._registryKeyValue = _connector.RegistryKeyValue;
            this._connectionString = _connector.ConnectionString;
            this._providerType = _connector.ProviderType;

            this._name = tableName;

        }


        private string _name;
        private string _registryKeyValue;
        private string _connectionString;
        private string _providerType;

        /// <summary>
        ///	The field list for this table structure.
        /// </summary>
        public List<Field> FieldList
        {
            get
            {
                List<Field> _fieldList = new List<Field>();

                StringBuilder _keyBuilder = new StringBuilder();
                _keyBuilder.Append ("TableFieldDictionary");
                _keyBuilder.Append(_registryKeyValue);
                _keyBuilder.Append(this._name);

                string _key = _keyBuilder.ToString();

                if (HttpRuntime.Cache[_key] != null)
                {
                    _fieldList = HttpRuntime.Cache[_key] as List<Field>;
                }
                else
                {
                    #region generate fieldList

                    DataFactory _dal = null;

                    if (String.IsNullOrEmpty(this._connectionString))
                    {
                        _dal = new DataFactory(this._registryKeyValue, this._providerType);
                    }
                    else
                    {
                        _dal = new DataFactory(this._registryKeyValue, this._providerType, this._connectionString);
                    }

                    DataTable dtColumns = new DataTable();
                    DataTable dtPKeys = new DataTable();
                    DataTable dtFKeys = new DataTable();


                    //get all columns fields
                    string Usp_SQL = "EXEC sp_columns @table_name='" + this._name + "'";
                    dtColumns = _dal.getDataTable(Usp_SQL);
                    Usp_SQL = "EXEC sp_pkeys @table_name ='" + this._name + "'";
                    dtPKeys = _dal.getDataTable(Usp_SQL);
                    Usp_SQL = "EXEC sp_fkeys @fktable_name  ='" + this._name + "'";
                    dtFKeys = _dal.getDataTable(Usp_SQL);

                    foreach (DataRow dr in dtColumns.Rows)
                    {
                        if (dr["TABLE_NAME"].ToString().ToUpper() == this._name.ToUpper())
                        {
                            Field _field = new Field();
                            _field.CHAR_OCTET_LENGTH = dr["CHAR_OCTET_LENGTH"] == System.DBNull.Value ? 0 : int.Parse(dr["CHAR_OCTET_LENGTH"].ToString());
                            _field.COLUMN_DEF = dr["COLUMN_DEF"].ToString();
                            _field.COLUMN_NAME = dr["COLUMN_NAME"].ToString();
                            _field.DATA_TYPE = dr["DATA_TYPE"] == System.DBNull.Value ? 0 : int.Parse(dr["DATA_TYPE"].ToString());
                            _field.NULLABLE = dr["NULLABLE"] == System.DBNull.Value ? 0 : int.Parse(dr["NULLABLE"].ToString());
                            _field.ORDINAL_POSITION = dr["ORDINAL_POSITION"] == System.DBNull.Value ? 0 : int.Parse(dr["ORDINAL_POSITION"].ToString());
                            _field.PRECISION = dr["PRECISION"] == System.DBNull.Value ? 0 : int.Parse(dr["PRECISION"].ToString());
                            _field.SQL_DATA_TYPE = dr["SQL_DATA_TYPE"] == System.DBNull.Value ? 0 : int.Parse(dr["SQL_DATA_TYPE"].ToString());
                            _field.SQL_DATETIME_SUB = dr["SQL_DATETIME_SUB"] == System.DBNull.Value ? 0 : int.Parse(dr["SQL_DATETIME_SUB"].ToString());
                            _field.TABLE_NAME = dr["TABLE_NAME"].ToString();
                            _field.TYPE_NAME = dr["TYPE_NAME"].ToString();
                            _field.LENGTH = dr["LENGTH"] == System.DBNull.Value ? 0 : int.Parse(dr["LENGTH"].ToString());
                            _fieldList.Add(_field);
                        }
                    }

                    foreach (DataRow dr in dtPKeys.Rows)
                    {
                        foreach (Field _field in _fieldList)
                        {
                            if (_field.COLUMN_NAME == dr["COLUMN_NAME"].ToString())
                            {
                                _field.Pkey = true;
                                break;
                            }
                        }
                    }
                    //setup fkey columns
                    foreach (DataRow dr in dtFKeys.Rows)
                    {
                        foreach (Field _field in _fieldList)
                        {
                            if (_field.COLUMN_NAME == dr["FKCOLUMN_NAME"].ToString())
                            {
                                _field.FKey = true;
                                break;
                            }
                        }
                    }
                    //add to cache

                    HttpRuntime.Cache.Add(_key, _fieldList, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                        TimeSpan.FromSeconds(600), System.Web.Caching.CacheItemPriority.Default, null);

                    #endregion
                }

                return _fieldList;
            }
        }

    }
}
