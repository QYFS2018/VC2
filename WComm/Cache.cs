using System;
using System.Web.UI;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Caching;

namespace WComm
{
    public class WCommCache
    {
        private CacheDependency _dependencies;
        private TimeSpan _slidingExpiration;

        public WCommCache()
        {
        }

        public WCommCache(CacheDependency dependencies, TimeSpan slidingExpiration)
        {
            _dependencies = dependencies;
            _slidingExpiration = slidingExpiration;
        }

        public object this[string key]
        {
            get
            {
                if (HttpRuntime.Cache[key] != null)
                {
                    return HttpRuntime.Cache[key];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                HttpRuntime.Cache.Insert(key, value, _dependencies, System.Web.Caching.Cache.NoAbsoluteExpiration, _slidingExpiration);
            }
        }

        public void Remove(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }
    }
}
