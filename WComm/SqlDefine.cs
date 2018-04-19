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
using System.Reflection;
using System.IO;

namespace WComm
{
    public class SqlDefine
    {
        public SqlDefine()
        { }


        private static string getFile(string fileName)
        {
            string _path = "";
            if (System.Configuration.ConfigurationSettings.AppSettings["SQLConfigFromRegistry"] != null)
            {
                string _registryKey=System.Configuration.ConfigurationSettings.AppSettings["SQLConfigFromRegistry"];
                if (_registryKey.ToUpper() != "URL")
                {
                    _path = RegistryAccess.getRegistryKeyValue(_registryKey, "SqlConfigPath");
                }
                else
                {
                    _path = RegistryAccess.getRegistryKeyValue(WComm.Utilities.urlPath, "SqlConfigPath");
                }

                _path = _path + "\\" + fileName + ".Config";
            }
            else
            {
                if (HttpContext.Current != null)
                {
                    _path ="SQL\\" + fileName + ".Config";

                    try
                    {
                        _path = HttpContext.Current.Request.PhysicalApplicationPath + _path;
                    }
                    catch { }

                }
                else
                {
                    string assemblyFilePath = Assembly.GetExecutingAssembly().Location;

                    string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);


                    _path = assemblyDirPath + "\\SQL\\" + fileName + ".Config";
                }
              
            }

            return _path;

        }

        public static string getSQL(string name, string fileName)
        {
            Dictionary<string, string> _list = new Dictionary<string, string>();

            string _cacheKey = "sqldefine" + fileName;
            if (HttpRuntime.Cache[_cacheKey] != null)
            {
                _list = (Dictionary<string, string>)HttpRuntime.Cache[_cacheKey];
            }
            else
            {

                string path = getFile(fileName);
              

                XmlDocument doc = new XmlDocument();

                doc.Load(path);


                XmlNodeList _nl = doc.GetElementsByTagName("SQLDefines").Item(0).ChildNodes;


                foreach (XmlNode _item in _nl)
                {
                    if (!_list.ContainsKey(_item.Attributes["name"].InnerText))
                    {
                        _list.Add(_item.Attributes["name"].InnerText, _item.InnerText);
                    }
                }

                if (HttpContext.Current != null)
                {
                    CacheDependency dependency = new CacheDependency(path);

                    HttpRuntime.Cache.Add(_cacheKey, _list, dependency, System.Web.Caching.Cache.NoAbsoluteExpiration,
                         TimeSpan.FromSeconds(600), System.Web.Caching.CacheItemPriority.Default, null);
                }
            }

            string _result = "";
            try
            {
                _result = _list[name].ToString();
            }
            catch { }

            return _result;

        }

        public static string getSQL(string name)
        {
            return SqlDefine.getSQL(name, "SQL");
        }
    }
}
   

