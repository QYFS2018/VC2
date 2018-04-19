using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using System.Web;
using System.Web.Caching;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;

namespace WComm
{
    internal class SQLCacheDependencyItem
    {
        public SQLCacheDependencyItem()
        {}

        private string _name;
        private string _tables;
        private string _database;


        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Table
        {
            get
            {
                return _tables;
            }
            set
            {
                _tables = value;
            }
        }
        public string Database
        {
            get
            {
                return _database;
            }
            set
            {
                _database = value;
            }
        }
    }


    internal class SQLCacheDependency
    {
        public SQLCacheDependency() { }


        public static AggregateCacheDependency GetSQLDependency(string sqlName)
        {
            Dictionary<string, SQLCacheDependencyItem>  _sQLCacheDependencyConfig = SQLCacheDependency.getSQLCacheDependency();

            SQLCacheDependencyItem _sQLCacheDependencyItem=_sQLCacheDependencyConfig[sqlName];


            AggregateCacheDependency dependency = new AggregateCacheDependency();

            char[] configurationSeparator = new char[] { ',' };

            string[] tables = _sQLCacheDependencyItem.Table .Split(configurationSeparator);

            foreach (string tableName in tables)
            {
                DbProviderFactory dbFactory = DbProviderFactories.GetFactory("System.Data.SqlClient");
                DbConnection dbConnect = dbFactory.CreateConnection();
                dbConnect.ConnectionString = "Data Source=DAN-D600\\DAN2005;Initial Catalog=Sample20;Persist Security Info=True;User ID=sa;Password=999";
                dbConnect.Open();
                DbCommand dbCommand = dbConnect.CreateCommand();
                dbCommand.CommandText = "Select [name] from dbo." + tableName;


                dependency.Add(new SqlCacheDependency((System.Data.SqlClient.SqlCommand)dbCommand));
            }
            return dependency;
        }


        private static Dictionary<string, SQLCacheDependencyItem> getSQLCacheDependency()
        {
            Dictionary<string, SQLCacheDependencyItem> _result = new Dictionary<string, SQLCacheDependencyItem>();
            string path = "CacheDependence.Config";
            try
            {
                path = HttpContext.Current.Request.PhysicalApplicationPath + path;
            }
            catch { }

            XmlDocument doc = new XmlDocument();

            doc.Load(path);

            


            XmlNodeList _nl = doc.GetElementsByTagName("sqlCacheDependency").Item(0).ChildNodes;
            foreach (XmlNode _item in _nl)
            {
                SQLCacheDependencyItem _sQLCacheDependencyItem = new SQLCacheDependencyItem();
                _sQLCacheDependencyItem.Name = _item.Attributes["name"].Value .ToString();
                _sQLCacheDependencyItem.Table = _item.Attributes["tables"].Value .ToString();
                _sQLCacheDependencyItem.Database = _item.Attributes["Database"].Value .ToString();

                _result.Add(_sQLCacheDependencyItem.Name, _sQLCacheDependencyItem);
            }

            return _result;

        }
    }


}
