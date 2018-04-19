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

namespace WComm
{

    internal class ClassInfo
    {
        private string _classType;
        private string _assemblyFile;


        public string ClassType
        {
            set
            {
                _classType = value;
            }
            get
            {
                return _classType;
            }
        }
        public string AssemblyFile
        {
            set
            {
                _assemblyFile = value;
            }
            get
            {
                return _assemblyFile;
            }
        }

        public ClassInfo()
        {
        }

        public static ClassInfo getClassInfo(string name)
        {
            System.Collections.Generic.Dictionary<string, ClassInfo> _arraylist = new System.Collections.Generic.Dictionary<string, ClassInfo>();


            if (HttpRuntime.Cache["AssemblyClassInfo"] == null)
            {

                FrameWorkState state = new FrameWorkState();

                XmlDocument doc = new XmlDocument();

                string _path = HttpContext.Current.Server.MapPath("~/Assembly.config");

                if (string.IsNullOrEmpty(state["AssemblyPath"]) == false)
                {
                    string _skinPath = HttpContext.Current.Server.MapPath("~/"+state["AssemblyPath"] + "\\Assembly.config");

                    if (System.IO.File.Exists(_skinPath) == true)
                    {
                        _path = _skinPath;
                    }
                }

                doc.Load(_path);

                XmlNodeList _pagenl = doc.GetElementsByTagName("ObjectProviders").Item(0).SelectNodes("Providers").Item(0).ChildNodes;
                XmlNode _pagend = null;
                foreach (XmlNode _xn in _pagenl)
                {
                    ClassInfo _obj = new ClassInfo();
                    _obj.AssemblyFile = _xn.Attributes["AssemblyFile"].Value.ToString();
                    _obj.ClassType = _xn.Attributes["ClassType"].Value.ToString();
                    string _className = _xn.Attributes["name"].Value.ToString().Trim().ToUpper();
                    _arraylist.Add(_className, _obj);
                }

                CacheDependency dep = new CacheDependency(_path);

                HttpRuntime.Cache.Add("AssemblyClassInfo", _arraylist, dep, System.Web.Caching.Cache.NoAbsoluteExpiration,
                      TimeSpan.FromSeconds(600), System.Web.Caching.CacheItemPriority.Default, null);
            }
            else
            {
                _arraylist = HttpRuntime.Cache["AssemblyClassInfo"] as System.Collections.Generic.Dictionary<string, ClassInfo>;
            }

            ClassInfo _result = _arraylist[name.Trim().ToUpper()];

            return _result;

        }
    }

    public class ObjectFactory
    {
      

        public static object Create(string name, Object[] args)
        {
            object _obj = null;
            ClassInfo _aInfo = ClassInfo.getClassInfo(name);
            Assembly a;
            Type _t;


            StringBuilder _keyBuilder = new StringBuilder();
            _keyBuilder.Append("Assembly");
            _keyBuilder.Append(_aInfo.AssemblyFile);

            string _key = _keyBuilder.ToString();

            if (HttpRuntime.Cache[_key] != null)
            {
                a = HttpRuntime.Cache[_key] as Assembly;
                _t = a.GetType(_aInfo.ClassType);
            }
            else
            {

                if (String.IsNullOrEmpty(_aInfo.AssemblyFile) == true)
                {
                    a = Assembly.GetCallingAssembly();
                    _t = a.GetType(name);
                }
                else
                {

                    string _path = _aInfo.AssemblyFile;
                    try
                    {
                        _path = HttpContext.Current.Request.PhysicalApplicationPath + "bin\\" + _path;
                    }
                    catch { }
                    a = Assembly.LoadFrom(_path);
                    _t = a.GetType(_aInfo.ClassType);
                }

                HttpRuntime.Cache.Add(_key, a, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                         TimeSpan.FromSeconds(600), System.Web.Caching.CacheItemPriority.Default, null);


            }


            _obj = Activator.CreateInstance(_t, args);

            return _obj;


        }
        public static object Create(string name)
        {
            return ObjectFactory.Create(name, null);

        }

        public static Type getType(string name)
        {
            object _obj = null;
            ClassInfo _aInfo = ClassInfo.getClassInfo(name);
            Assembly _a;
            Type _t;


            StringBuilder _keyBuilder = new StringBuilder();
            _keyBuilder.Append("Assembly");
            _keyBuilder.Append(_aInfo.AssemblyFile);

            string _key = _keyBuilder.ToString();

            if (HttpRuntime.Cache[_key] != null)
            {
                _a = HttpRuntime.Cache[_key] as Assembly;
                _t = _a.GetType(_aInfo.ClassType);
            }
            else
            {
                if (String.IsNullOrEmpty(_aInfo.AssemblyFile) == true)
                {
                    _a = Assembly.GetCallingAssembly();
                    _t = _a.GetType(name);
                }
                else
                {

                    string _path = _aInfo.AssemblyFile;
                    try
                    {
                        _path = HttpContext.Current.Request.PhysicalApplicationPath + "bin\\" + _path;
                    }
                    catch { }
                    _a = Assembly.LoadFrom(_path);
                    _t = _a.GetType(_aInfo.ClassType);
                }


                HttpRuntime.Cache.Add(_key, _a, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                         TimeSpan.FromSeconds(600), System.Web.Caching.CacheItemPriority.Default, null);


            }


            return _t;


        }
    }
}
