using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Configuration;

namespace WComm
{
    public class XmlHelper
    {
        private XmlHelper()
        {

        }
        private static readonly object _SyncLock = new object();

        public static XmlHelper GetInstance()
        {
            XmlHelper catchedHelper = null;
            lock (_SyncLock)
            {
                catchedHelper = HttpRuntime.Cache.Get("XmlHelper") as XmlHelper;
                if (catchedHelper == null)
                {
                    catchedHelper = new XmlHelper();
                    HttpRuntime.Cache.Insert("XmlHelper", catchedHelper, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);

                }

            }
            return catchedHelper;

        }

        public static void InitXmlHelper()
        {
            XmlHelper catchedHelper = null;
            lock (_SyncLock)
            {
                catchedHelper = HttpRuntime.Cache.Get("XmlHelper") as XmlHelper;
                if (catchedHelper != null)
                {

                    HttpRuntime.Cache.Remove("XmlHelper");
                    //HttpRuntime.Cache.Insert("", null, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60), CacheItemPriority.Default, null);

                }

            }
        }

        private Dictionary<string, string> m_Cache = new Dictionary<string, string>();
        public string GetContent(string langCode, string skinCode, string xml)
        {
            string result = String.Empty;
            string key = String.Format("{0} {1} {2}", langCode, skinCode, xml);
            try
            {
                if (m_Cache.ContainsKey(key))
                {
                    return m_Cache[key];
                }
                xml = xml.Insert(0, "<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
                StringReader sr = new StringReader(xml);
                XPathDocument _doc = new XPathDocument(sr);
                XPathNavigator _navi = _doc.CreateNavigator();
                result = TryGetFromQuery(_navi, string.Format("//Language[@code='{0}'][@skincode='{1}']", langCode, skinCode));


                if (string.IsNullOrEmpty(result))
                {
                    result = TryGetFromQuery(_navi, string.Format("//Language[@code='{0}'][@skincode='{1}']", ConfigurationManager.AppSettings["DefaultLanguageCode"], ConfigurationManager.AppSettings["DefaultSkinCode"]));

                }


                if (string.IsNullOrEmpty(result))
                {
                    result = TryGetFromQuery(_navi, string.Format("//Language[@code='{0}']", langCode));

                }

            }
            catch (Exception ex)
            {

            }

            if (string.IsNullOrEmpty(result))
            {
                lock (_SyncLock)
                {
                    m_Cache[key] = string.Empty;
                }
            }
            else
            {
                lock (_SyncLock)
                {
                    m_Cache[key] = result;
                }
            }
            return m_Cache[key];

        }

        private string TryGetFromQuery(XPathNavigator navigator, string query)
        {
            string result = string.Empty;
            XPathNodeIterator _iter = navigator.Select(query);
            if (_iter.Count > 0)
            {
                while (_iter.MoveNext())
                {
                    return _iter.Current.InnerXml;
                }
            }
            return result;
        }
    }
}
