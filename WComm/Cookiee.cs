using System;
using System.Web;
using System.Configuration;


namespace WComm
{
    /// <summary>
    ///	Easily to use Cookiee to keep state for base page or base user control.
    ///	The difference of state is this will save more things.
    ///	it can only save string object.
    /// </summary>
    public class Cookiee
    {
        private static string CookieExpiresMins = ConfigurationSettings.AppSettings["CookieExpiresMins"];

        /// <summary>
        ///	the method to save state of cookiee.
        ///	<code>
        ///	Cookiee.saveState("ProductId","21");
        ///	</code>
        /// </summary>
        /// <param name="key">the key string for save.</param>
        /// <param name="value">the value to save.</param>
        public static void saveState(string key, string value)
        {
            string _value = Utilities.FransferChar(value);

            HttpCookie MyCookie = HttpContext.Current.Request.Cookies["WComm.Core." + key];


            //add path setting by ksharp 2012.1.8
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["CookiePath"]))
            {
                MyCookie.Path = ConfigurationSettings.AppSettings["CookiePath"];
            }
            //end

            if (MyCookie != null)
            {
                MyCookie.Value = HttpContext.Current.Server.HtmlEncode(_value);
                if (string.IsNullOrEmpty(CookieExpiresMins) == false)
                {
                    MyCookie.Expires = DateTime.Now.AddMinutes(int.Parse(CookieExpiresMins));
                }
                HttpContext.Current.Response.Cookies.Add(MyCookie);

            }
            else
            {
                MyCookie = new HttpCookie("WComm.Core." + key);

                MyCookie.Value = HttpContext.Current.Server.HtmlEncode(_value);
                if (string.IsNullOrEmpty(CookieExpiresMins) == false)
                {
                    MyCookie.Expires = DateTime.Now.AddMinutes(int.Parse(CookieExpiresMins));
                }
                HttpContext.Current.Response.Cookies.Add(MyCookie);
            }
            HttpContext.Current.Items[key] = _value;
        }


        /// <summary>
        ///	the method to get state of cookiee.
        ///	<code>
        ///	string ProductId = Cookiee.getState("ProductId");
        ///	</code>
        /// </summary>
        /// <param name="key">the key string for get.</param>
        /// <returns>the value to get.</returns>
        public static string getState(string key)
        {

            if (HttpContext.Current.Items[key] != null)
            {
                return Utilities.UnFransferChar(HttpContext.Current.Items[key].ToString());

            }
            HttpCookie MyCookie;




            if (HttpContext.Current.Request.Cookies["WComm.Core." + key] != null)
            {
                MyCookie = HttpContext.Current.Request.Cookies["WComm.Core." + key];
                //add path setting by ksharp 2012.1.8
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["CookiePath"]))
                {
                    MyCookie.Path = ConfigurationSettings.AppSettings["CookiePath"];
                }
                //end
                return Utilities.UnFransferChar(HttpContext.Current.Server.HtmlDecode(MyCookie.Value));
            }
            else
                return "";
        }


        /// <summary>
        ///	To get the value.
        /// </summary>
        public string this[string key]
        {
            get
            {
                return Cookiee.getState(key);
            }
            set
            {
                Cookiee.saveState(key, value);
            }
        }

    }

    public class FrameWorkState
    {
        private string _classKey;
        private static string CookieExpiresMins = ConfigurationSettings.AppSettings["CookieExpiresMins"];

        public FrameWorkState(string classKey)
        {
            this._classKey = classKey;
        }

        public FrameWorkState()
        {
            this._classKey = "Default";

        }

        /// <summary>
        ///	the method to save state.
        ///	<code>
        ///	State.saveState("ProductId","21");
        ///	</code>
        /// </summary>
        /// <param name="key">the key string for save.</param>
        /// <param name="value">the value to save.</param>
        public void saveState(string key, string value)
        {
            //  string _value = Utilities.FransferChar(value);
            string _value = value;

            HttpCookie MyCookie;



            if (HttpContext.Current.Request.Cookies["WComm.Core.FrameWork." + _classKey] != null)
            {
                MyCookie = HttpContext.Current.Request.Cookies["WComm.Core.FrameWork." + _classKey];

                //add path setting by ksharp 2012.1.8
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["CookiePath"]))
                {
                    MyCookie.Path = ConfigurationSettings.AppSettings["CookiePath"];
                }
                //end

                SaveCookieString(ref  MyCookie, key, value);

                if (string.IsNullOrEmpty(CookieExpiresMins) == false)
                {
                    MyCookie.Expires = DateTime.Now.AddMinutes(int.Parse(CookieExpiresMins));
                }
                HttpContext.Current.Response.Cookies.Add(MyCookie);

            }
            else
            {
                MyCookie = new HttpCookie("WComm.Core.FrameWork." + _classKey);

                //add path setting by ksharp 2012.1.8
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["CookiePath"]))
                {
                    MyCookie.Path = ConfigurationSettings.AppSettings["CookiePath"];
                }
                //end

                SaveCookieString(ref  MyCookie, key, value);

                if (string.IsNullOrEmpty(CookieExpiresMins) == false)
                {
                    MyCookie.Expires = DateTime.Now.AddMinutes(int.Parse(CookieExpiresMins));
                }
                HttpContext.Current.Response.Cookies.Add(MyCookie);
            }

            HttpContext.Current.Items[key] = Utilities.FransferChar(_value);
        }

        private void SaveCookieString(ref HttpCookie MyCookie, string key, string value)
        {
            //add path setting by ksharp 2012.1.8
            if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["CookiePath"]))
            {
                MyCookie.Path = ConfigurationSettings.AppSettings["CookiePath"];
            }
            //end

            string strMyCookie = "";

            if (!string.IsNullOrEmpty(MyCookie.Value))
            {
                strMyCookie = Utilities.UnFransferChar(MyCookie.Value);
            }

            key = "&" + key + "=";
            if (strMyCookie.IndexOf(key) > -1)
            {
                string TempKey = strMyCookie.Substring(strMyCookie.IndexOf(key));
                if (TempKey.IndexOf("&", 1) > -1)
                {
                    TempKey = TempKey.Substring(0, TempKey.IndexOf("&", 1));
                }
                if (string.IsNullOrEmpty(value))
                    value = "";
                strMyCookie = strMyCookie.Replace(TempKey, key + value.Replace("=", "<dengyuhao>").Replace("&", "<andhao>"));
            }
            else
            {
                if (string.IsNullOrEmpty(value))
                    value = "";
                strMyCookie = strMyCookie + key + value.Replace("=", "<dengyuhao>").Replace("&", "<andhao>");
            }


            MyCookie.Value = Utilities.FransferChar(strMyCookie);
        }

        /// <summary>
        ///	the method to get state.
        ///	<code>
        ///	string ProductId = State.getState("ProductId");
        ///	</code>
        /// </summary>
        /// <param name="key">the key string to get.</param>
        /// <returns>the value to get.</returns>
        public string getState(string key)
        {
            if (HttpContext.Current.Items[key] != null)
            {
                return Utilities.UnFransferChar(HttpContext.Current.Items[key].ToString());

            }
            HttpCookie MyCookie;



            if (HttpContext.Current.Request.Cookies["WComm.Core.FrameWork." + _classKey] != null)
            {

                MyCookie = HttpContext.Current.Request.Cookies["WComm.Core.FrameWork." + _classKey];

                //add path setting by ksharp 2012.1.8
                if (!string.IsNullOrEmpty(ConfigurationSettings.AppSettings["CookiePath"]))
                {
                    MyCookie.Path = ConfigurationSettings.AppSettings["CookiePath"];
                }
                //end



                string strMyCookie = Utilities.UnFransferChar(MyCookie.Value);

                key = "&" + key + "=";
                if (strMyCookie.IndexOf(key) > -1)
                {
                    strMyCookie = strMyCookie.Substring(strMyCookie.IndexOf(key) + key.Length);
                    if (strMyCookie.IndexOf("&") > -1)
                    {
                        strMyCookie = strMyCookie.Substring(0, strMyCookie.IndexOf("&"));
                    }
                    strMyCookie = strMyCookie.Replace("<dengyuhao>", "=").Replace("<andhao>", "&");
                    return strMyCookie;
                }
                else
                {
                    return "";
                }

                /*if (MyCookie[key] != null)
                {
                    string _value = MyCookie[key].ToString();
                    MyCookie.Value = Utilities.FransferChar(MyCookie.Value);
                    return _value;
                }
                else
                {
                    MyCookie.Value = Utilities.FransferChar(MyCookie.Value);
                    return "";
                }*/
            }
            else
                return "";
        }


        /// <summary>
        ///	To get the value.
        /// </summary>
        public string this[string key]
        {
            get
            {
                return getState(key);
            }
            set
            {
                saveState(key, value);
            }
        }

    }
}


