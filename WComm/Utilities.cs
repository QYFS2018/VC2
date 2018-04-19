using System;
using System.Web.UI;
using System.Configuration;
using System.Web;
using System.Text;
using System.Xml;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Text;


namespace WComm
{
    /// <summary>
    ///	Utilities class.
    /// </summary>
    public sealed class Utilities
    {
        /// <summary>
        ///	Constructor method.
        /// </summary>
        public Utilities()
        {

        }


        #region Validation

        /// <summary>
        ///	Check the string if is integer.
        /// </summary>
        /// <param name="str">string to check.</param>
        /// <returns>result.</returns>
        public static bool IsInteger(string str)
        {
            return RegexMatch(str, @"^-?\d+$");
        }
        /// <summary>
        ///	Check the string if is double.
        /// </summary>
        /// <param name="str">string to check.</param>
        /// <returns>result.</returns>
        public static bool IsDouble(string str)
        {
            return RegexMatch(str, @"^(-?\d+)(\.\d+)?$");
        }
        /// <summary>
        ///		Check the string if is datetime.
        /// </summary>
        /// <param name="str">string to check.</param>
        /// <returns>result.</returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(str);
            }
            catch
            {
                if (str.Trim().Replace("-", "/") == "02/29" || str.Trim().Replace("-", "/") == "2/29")
                {
                    return true;
                }

                return false;
            }
            return true;
        }
        /// <summary>
        ///	RegexMatch
        /// </summary>
        /// <param name="str">check string</param>
        /// <param name="regex">Regular Expressions.</param>
        /// <returns>result.</returns>
        private static bool RegexMatch(string str, string regex)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            System.Text.RegularExpressions.Regex _regex =
                new System.Text.RegularExpressions.Regex(regex);
            return _regex.IsMatch(str);
        }
        public static bool isPostalCode(string countryISO2, string postalCode)
        {
            bool returnValue = false;

            if (countryISO2 == "US")	//United states
            {
                string patrn = "^[0-9]{5}$|^[0-9]{5}[-]{1}[0-9]{4}$";
                System.Text.RegularExpressions.Regex regEx = new System.Text.RegularExpressions.Regex(patrn);
                returnValue = regEx.IsMatch(postalCode);
            }
            else
            {
                returnValue = true;
            }

            return returnValue;
        }

        /// <summary>
        /// Determines if the email address was entered in the proper format.
        /// </summary>
        /// <param name="address">The email address to be processed.</param>
        /// <returns>True/False</returns>
        public static bool isEmailAddress(string address)
        {
            //string expr = @"^[a-z0-9A-Z_\-\.]+[@]{1}[a-z0-9A-Z_\-\.]+[\.]{1}[a-z0-9A-Z_\-\.]+$";
            string expr = @"((^[a-zA-Z0-9]+[a-z0-9A-Z_\-\.]*[a-z0-9A-Z_\-]+)|(^[a-zA-Z0-9]+))[@]{1}[a-zA-Z0-9]+[a-z0-9A-Z_\-\.]*[\.]{1}[a-z0-9A-Z_\-\.]*[a-zA-Z0-9]+$";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(expr);

            bool eval = reg.IsMatch(address);
            if (eval)
            {
                expr = @"\.{2,}";
                reg = new System.Text.RegularExpressions.Regex(expr);
                eval = !reg.IsMatch(address);
            }
            return eval;
        }

        /// <summary>
        /// Determines if the phone number was entered in the proper format.
        /// </summary>
        /// <param name="phone">The phone number to be processed.</param>
        /// <returns>True/False</returns>
        public static bool isPhoneNumber(string phone)
        {
            bool eval = false;
            string Phone = string.IsNullOrEmpty(phone) ? "" : phone.Replace(" ", "");
            //Phone number (Supports (713)743-1000 | 713-743-1000 | 713.743.1000) | 7137431000 | 7431000 | 743-1000
            string expr = @"^[1]?(\([0-9]{3}\)?[0-9]{3}-[0-9]{4})$|^([0-9]{3}-?[0-9]{3}-[0-9]{4})$|^([0-9]{3}\.?[0-9]{3}\.[0-9]{4})$|^[1]?[0-9]{10}$";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(expr);

            if (phone != null)
            {
                Phone = Phone.Trim();
                eval = reg.IsMatch(Phone);
            }

            return eval;
        }

        public static bool isInteger(string str)
        {
            bool retVal = false;

            if (str == null || str == "")
            {
                //Blank do nothing
            }
            else
            {
                string expr = "^[0-9]+$";
                System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(expr);
                retVal = reg.IsMatch(str);
            }

            return retVal;
        }

        public static bool IsDecimal(string str)
        {
            
            bool result = false;
            if (!string.IsNullOrEmpty(str))
            {
                System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^[0-9]+\.?[0-9]{0,2}$");
                result = r.IsMatch(str);
            }

            return result;
          
        }
        public static bool isSocialSecurityNumber(string ssn)
        {
            bool returnValue = false;
            //Social security pattern
            //Matches: xxx-xx-xxxx or xxxxxxxxx where 'x' is a number.
            string patrn = "(^[0-9]{3}[-]{1}[0-9]{2}[-]{1}[0-9]{4}$)|(^[0-9]{9}$)";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(patrn);
            returnValue = reg.IsMatch(ssn);

            return returnValue;
        }

        public static Decimal ConvertToDecimal(Double val)
        {
            return Decimal.Parse(val.ToString());
        }

        public static Double ConvertToDouble(Decimal val)
        {
            return Double.Parse(val.ToString());
        }


        /// <summary>
        /// Verifies the user's age is greater than 18
        /// </summary>
        /// <param name="dob">The date of birth to be processed.</param>
        /// <returns>True/False</returns>
        public static bool verifyAge(DateTime birthdate)
        {
            bool returnValue = false;
            //verify at least 18
            if (System.DateTime.Today.Year - birthdate.Year >= 18)
            {
                if (System.DateTime.Today.Year - birthdate.Year == 18)
                {
                    if (birthdate.Month - System.DateTime.Today.Month <= 0)
                    {
                        if (birthdate.Month - System.DateTime.Today.Month == 0)
                        {
                            if (birthdate.Day - System.DateTime.Today.Day <= 0)
                            {
                                returnValue = true;
                            }
                        }
                        else
                        {
                            returnValue = true;
                        }
                    }
                }
                else
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }
        public static bool Mod10Validation(string cardNum)
        {
            bool returnValue = false;
            int mod = 0;

            if (cardNum == null || cardNum == "")
            {
                //There is no value being passed in - avoid errors.
            }
            else
            {
                cardNum = Utilities.CleanCC(cardNum);
                //Step 1 remove any non-numerical characters
                if (cardNum == null || cardNum == "")
                {
                    //There is no value being passed in - avoid errors.
                }
                else
                {
                    //Step 2 do the Mod 10 stuff on the card.
                    for (int i = cardNum.Length - 1; i >= 0; i--)
                    {
                        int curDigit = System.Int32.Parse(cardNum.Substring(i, 1));
                        //If it is even
                        //NOTE:  The length is based on a zero index - the algorythm is based on a 1 index.
                        //	the i+1 accounts for the difference in bases.
                        if ((cardNum.Length - (i + 1)) % 2 == 1)
                        {
                            curDigit *= 2;
                            if (curDigit >= 10)
                            {
                                curDigit = (curDigit % 10) + 1;
                            }
                        }
                        mod += curDigit;
                    }

                    if ((mod % 10) == 0 && mod != 0)
                    {
                        returnValue = true;
                    }
                }
            }
            return returnValue;
        }
        public static string CleanCC(string cardNum)
        {
            string patrn = @"[^0-9]";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(patrn);
            cardNum = reg.Replace(cardNum, "");

            return cardNum;
        }

        public static double Round(double value, int digits)
        {
            int sign = Math.Sign(value);
            double scale = Math.Pow(10.0, digits);
            double round = Math.Floor(Math.Abs(value) * scale + 0.5);
            return (sign * round / scale);
        }

        /// <summary>
        /// Mathematically rounds the specified value.
        /// </summary>
        /// <param name="value">The value to be processed.</param>
        /// <param name="digits">The number of digits to round to.</param>
        /// <returns>The value rounded to the number of digits.</returns>
        public static float Round(float value, int digits)
        {
            int sign = Math.Sign(value);
            float scale = System.Convert.ToSingle(Math.Pow(10.0, digits));
            float round = System.Convert.ToSingle(Math.Floor(Math.Abs(value) * scale + 0.5));
            return (float)(sign * round / scale);
        }

        /// <summary>
        /// Mathematically rounds the specified value.
        /// </summary>
        /// <param name="value">The value to be processed.</param>
        /// <param name="digits">The number of digits to round to.</param>
        /// <returns>The value rounded to the number of digits.</returns>
        public static decimal Round(decimal value, int digits)
        {
            int sign = Math.Sign(value);
            double scale = Math.Pow(10.0, digits);
            decimal round = (decimal)Math.Floor(Math.Abs((double)value) * scale + 0.5);
            return (decimal)(sign * (double)round / scale);
        }

        #endregion

        public static System.Globalization.NumberFormatInfo CurrentNumberFormat
        {
            get
            {
                System.Globalization.NumberFormatInfo nfi = new System.Globalization.CultureInfo("en-US", false).NumberFormat;
                nfi.NumberDecimalDigits = 2;
                return nfi;
            }
        }

        private static bool EncryptCookie
        {
            get
            {
                bool _encryptCookie = true;

                if (System.Configuration.ConfigurationSettings.AppSettings["EncryptCookie"] != null)
                {
                    try
                    {
                        _encryptCookie = Convert.ToBoolean(System.Configuration.ConfigurationSettings.AppSettings["EncryptCookie"]);
                    }
                    catch { }
                }

                return _encryptCookie;

            }
        }

        /// <summary>
        ///	Fransfer Char.
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>new string.</returns>
        public static string FransferChar(string s)
        {
            if (s == null)
            {
                return s;
            }
            string _encrpts = s;
            if (Utilities.EncryptCookie == true)
            {
                _encrpts = Encrypt.EncryptData(EncrptKey, s);
            }

            _encrpts = _encrpts.Replace("<", "*L*E*F*T*").Replace(">", "*R*I*G*H*T*").Replace("\\", "*F*E*E*T*").Replace(";", "*S*E*M*I*C*O*L*O*N*").Replace("&", "*A*N*D*");

            //if (HttpContext.Current != null)
            //{
            //    _encrpts = HttpContext.Current.Server.HtmlEncode(_encrpts);
            //}

            return _encrpts;

        }
        /// <summary>
        ///	UnFransferChar
        /// </summary>
        /// <param name="s">string.</param>
        /// <returns>new string.</returns>
        public static string UnFransferChar(string s)
        {
            string _sfordecrpt;
            //if (HttpContext.Current != null)
            //{
            //    _sfordecrpt = HttpContext.Current.Server.HtmlDecode (s);
            //}
            _sfordecrpt = s.Replace("*L*E*F*T*", "<").Replace("*R*I*G*H*T*", ">").Replace("*F*E*E*T*", "\\").Replace("*S*E*M*I*C*O*L*O*N*", ";").Replace("*A*N*D*", "&");

            if (Utilities.EncryptCookie == true)
            {
                _sfordecrpt = Encrypt.DecryptData(EncrptKey, _sfordecrpt);
            }

            return _sfordecrpt;
        }

        private static string EncrptKey
        {
            get
            {
                return "WCOMM";
            }
        }

        public static string TransErrorMessage(int errorKey)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(HttpContext.Current.Server.MapPath("View.Config"));
            string _result = "";
            string pageName = WComm.Utilities.getCurPage;

            //get current page
            XmlNodeList _pagenl = doc.GetElementsByTagName("WComm.Core").Item(0).SelectNodes("ErrorMessage").Item(0).ChildNodes;
            XmlNode _pagend = null;
            foreach (XmlNode _xn in _pagenl)
            {
                if (_xn.Attributes["name"].InnerXml == pageName)
                {
                    _pagend = _xn;
                    break;
                }
            }

            //search view in this page
            if (_pagend != null)
            {
                XmlNodeList _nl = _pagend.ChildNodes;

                foreach (XmlNode _xn in _nl)
                {
                    if (_xn.Attributes["Id"].InnerXml == errorKey.ToString())
                    {
                        _result = _xn.InnerXml.ToString();
                        break;
                    }
                }
            }
            return _result;
        }

        public static void SetTextForDropDownList(DropDownList _ddl, string textValue)
        {
            foreach (ListItem _item in _ddl.Items)
            {
                if (_item.Text.ToUpper().Trim() == textValue.ToUpper().Trim())
                {
                    _item.Selected = true;
                    break;
                }
            }
        }

        public static string checkAVSOutput(int result)
        {
            StringBuilder err = new StringBuilder();
            switch (result)
            {
                case 31:
                    //Exact match
                    break;
                case 10:
                    //err = "Address line contains more than one address.  Please indicate one address.";
                    err.Append("The Street Address provided cannot be verified.  It appears to contain more than one delivery address (i.e. a range of suite or apartment numbers).  Please correct the entry and try again.");
                    err.Append("<li>Supply specific Apartment or Suite information on the second Street Address line.</li>");
                    err.Append("<li>Make sure to include spaces where appropriate.</li>");
                    break;
                case 11:
                    err.Append("The city, state, or zipcode is invalid.");
                    break;
                case 12:
                    err.Append("The state is invalid.");
                    break;
                case 13:
                    err.Append("The city entered is invalid.");
                    break;
                case 21:
                    //err = "The address entered was not found.";
                    err.Append("The Street Address provided cannot be verified.  Please correct the entry and try again.");
                    err.Append("<li>Supply specific Apartment or Suite information on the second Street Address line.</li>");
                    err.Append("<li>Make sure to include spaces where appropriate.</li>");
                    break;
                case 22:
                    //err = "Please check and correct the address field.  There were multiple addresses found.";
                    err.Append("The Street Address provided cannot be verified.  More than one address matches your input.  Please correct the entry and try again.");
                    err.Append("<li>Supply specific Apartment or Suite information on the second Street Address line.</li>");
                    err.Append("<li>Make sure to include spaces where appropriate.</li>");
                    break;
                case 32:
                    //Commented 121703 RMS As Per Coleman and Ahsan instructions: the 'Default' match should go through...
                    //err = "Please provide more specific address information.";
                    break;
                case -1:
                    err.Append("DATABASE FILE MISSING OR CORRUPTED");
                    break;
                case -2:
                    err.Append("COULD NOT OPEN CONFIGURATION DATA FILES");
                    break;
                case -3:
                    err.Append("DATABASE EXPIRED");
                    break;
                case -4:
                    err.Append("INCOMPLETE STREET ADDRESS");
                    break;
                case -5:
                    err.Append("SEMAPHORE LOCK WILL NOT RELEASE");
                    break;
                case -999:
                case -998:
                case -997:
                    //Application error
                    err.Append("The Address entered is invalid");
                    break;
            }
            return err.ToString();
        }

        #region System Key
        private static string _currentLangCode = "en";
        private static string _currentSkinCode = "Default";
        private static int _currentProgramId = 0;
        private static int _currentUserId = 0;

        public static string CurrentLangCode
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();

                    string _result = "";
                    if (String.IsNullOrEmpty(_state["CurrentLangCode"]) == true)
                    {
                        _result = System.Configuration.ConfigurationManager.AppSettings["DefaultLanguageCode"];
                    }
                    else
                    {
                        _result = _state["CurrentLangCode"].ToString();
                    }
                    return _result;
                }
                else
                {
                    return _currentLangCode;
                }
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();

                    _state["CurrentLangCode"] = value;
                }
                else
                {
                    _currentLangCode = value;
                }
            }
        }
        public static string CurrentSkinCode
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();

                    string _result = "";
                    if (String.IsNullOrEmpty(_state["CurrentSkinCode"]) == true)
                    {
                        _result = System.Configuration.ConfigurationManager.AppSettings["DefaultSkinCode"];
                    }
                    else
                    {
                        _result = _state["CurrentSkinCode"].ToString();
                    }
                    return _result;
                }
                else
                {
                    return _currentSkinCode;
                }
            }
            set
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();

                    _state["CurrentSkinCode"] = value;
                }
                else
                {
                    _currentSkinCode = value;
                }
            }
        }
        public static int CurrentProgramId
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    if (String.IsNullOrEmpty(_state["ProgramId"]) == false)
                    {
                        _currentProgramId = int.Parse(_state["ProgramId"].ToString());
                    }

                }

                return _currentProgramId;
            }
            set
            {
                _currentProgramId = value;
            }
        }
        public static int CurrentUserId
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    FrameWorkState _state = new FrameWorkState();
                    if (String.IsNullOrEmpty(_state["UserId"]) == false)
                    {
                        _currentUserId = int.Parse(_state["UserId"].ToString());
                    }

                }

                return _currentUserId;
            }
            set
            {
                _currentUserId = value;
            }
        }
        public static string CreateAddressKey(string line1, string line2, string zip)
        {
            StringBuilder key = new StringBuilder();



            string numericPatrn = "[^0-9]";
            string textPatrn = "[^A-Z]";

            //Make sure that these can be worked with
            if (line1 == null) { line1 = ""; }
            if (line2 == null) { line2 = ""; }
            if (zip == null) { zip = ""; }

            System.Text.RegularExpressions.Regex numericReg = new System.Text.RegularExpressions.Regex(numericPatrn);
            System.Text.RegularExpressions.Regex textReg = new System.Text.RegularExpressions.Regex(textPatrn);

            string line1Num = numericReg.Replace(line1, "");
            key.Append(line1Num.Substring(0, (line1Num.Length > 8 ? 8 : line1Num.Length)));

            string line2Num = numericReg.Replace(line2, "");
            key.Append(line2Num.Substring(0, (line2Num.Length > 8 ? 8 : line2Num.Length)));


            string line1Char = textReg.Replace(line1.ToUpper(), "");
            key.Append(line1Char.Substring(0, (line1Char.Length > 8 ? 8 : line1Char.Length)));

            //string zipNum = numericReg.Replace(zip, "");
            key.Append(zip.Substring(0, (zip.Length > 5 ? 5 : zip.Length)));

            return key.ToString();
        }

        public static bool TraceLog
        {
            get
            {
                bool _result = false;
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["TraceLog"]) == false)
                {
                    _result = Convert.ToBoolean(ConfigurationManager.AppSettings["TraceLog"]);
                }
                return _result;
            }
        }

        public static bool TracePageLog
        {
            get
            {
                bool _result = false;
                if (string.IsNullOrEmpty(ConfigurationManager.AppSettings["TracePageLog"]) == false)
                {
                    _result = Convert.ToBoolean(ConfigurationManager.AppSettings["TracePageLog"]);
                }
                return _result;
            }
        }

        #endregion

        #region get Page Info
        /// <summary>
        /// 
        /// </summary>
        public static int getPageSize
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string getRootPath
        {
            get
            {
                string url = HttpContext.Current.Request.RawUrl;
                if (!url.EndsWith("/"))
                {
                    url = url.Substring(0, url.LastIndexOf("/") + 1);
                }

                return url;
            }
        }

        public static string getUrlByRootPath(string PageName)
        {
            return getRootPath + PageName;
        }

        /// <summary>
        /// 
        /// </summary>
        public static string getCurPage
        {
            get
            {
                int pos = HttpContext.Current.Request.ServerVariables["URL"].LastIndexOf("/");
                string page = HttpContext.Current.Request.ServerVariables["URL"].Substring(pos + 1);
                return page;
            }
        }


        /// </summary>
        public static string getCurPageModule
        {
            get
            {
                return getCurPage.Substring(0, WComm.Utilities.getCurPage.IndexOf('.')).ToLower ();
            }
        }

        public static string getReferencePage
        {
            get
            {
                string _url;
                //if (HttpContext.Current.Request.UrlReferrer == null)
                //{
                // _url = HttpContext.Current.Request.RawUrl;
                //}
                //else
                //{
                _url = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                //}
                int pos = _url.LastIndexOf("/");
                string page = _url.Substring(pos + 1);
                return page;
            }
        }

        public static string getRawUrlPage
        {
            get
            {
                string _url;
                //if (HttpContext.Current.Request.UrlReferrer == null)
                //{
                _url = HttpContext.Current.Request.RawUrl;
                //}
                //else
                //{
                //    _url = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                //}
                int pos = _url.LastIndexOf("/");
                string page = _url.Substring(pos + 1);
                return page;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string urlPath
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                string siteUrl = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
                string siteFolder = HttpContext.Current.Request.ServerVariables["PATH_INFO"];
                int prevSeperator = siteFolder.LastIndexOf("/");
                siteFolder = siteFolder.Substring(0, prevSeperator);
                siteUrl += siteFolder;

                if (siteUrl.LastIndexOf("/") == siteUrl.Length - 1)
                {
                    siteUrl = siteUrl.Substring(0, siteUrl.Length - 1);
                }
                return siteUrl;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string ServerSite
        {
            get
            {
                return ConfigurationManager.AppSettings["ServerSite"].ToString();
            }
        }

        #endregion


        /// <summary>
        /// Removes any non numeric characters from the credit card number.
        /// </summary>
        /// <param name="cardNum">The credit card number to be processed.</param>
        /// <returns>The credit card number without non numeric characters.</returns>
        public static string cleanCC(string cardNum)
        {
            string patrn = @"[^0-9]";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(patrn);
            cardNum = reg.Replace(cardNum, "");

            return cardNum;
        }

        /// <summary>
        /// Generates HTML code to display the standard button in the application.
        /// </summary>
        /// <param name="name">The name of the button to be created.</param>
        /// <param name="onImage">The image of the button to be displayed when the
        /// mouse pointer is hovering over the button.</param>
        /// <param name="offImage">The image of the button to be displayed when the
        /// mouse pointer is NOT hovering over the button.</param>
        /// <param name="action">A set of javascript code to be executed when the
        /// user clicks on the button.</param>
        /// <param name="altText">The text to be displayed if the browser cant display
        /// the image or cant find the image.</param>
        /// <returns>The HTML code of the button.</returns>
        public static string createButton(string name, string onImage, string offImage, string action, string altText)
        {

            string button = string.Empty;
            if (name == "PlaceOrder")
            {
                button = "<div name=myplaceorder id=myplaceorder><a href=\"javascript:" + action + "\" id=" + name + "><img alt=\"" + altText
                    + "\" border=\"0\" src=\"" + offImage
                    + "\" onmouseover=\"swapImage(" + name + ", '" + onImage + "');"
                    + "\" onmouseout=\"swapImage(" + name + ", '" + offImage + "');"
                    + "\" name=\"" + name + "\" /></a></div>";
            }
            else
            {
                button = "<a href=\"javascript:" + action + "\" id=" + name + "><img alt=\"" + altText
                    + "\" border=\"0\" src=\"" + offImage
                    + "\" onmouseover=\"swapImage(" + name + ", '" + onImage + "');"
                    + "\" onmouseout=\"swapImage(" + name + ", '" + offImage + "');"
                    + "\" name=\"" + name + "\" /></a>";
            }

            return button;
        }


        public static Coordinate getCoordinateFromGoogle(string address)
        {
            string url = string.Format(ConfigurationManager.AppSettings["GoogleAPIURL"], address, "csv", ConfigurationManager.AppSettings["GoogleAPIKey"]);

            Coordinate _result = new Coordinate();

            try
            {

                WebClient wc = new WebClient();
                Stream s = wc.OpenRead(url);
                StreamReader sr = new StreamReader(s, Encoding.UTF8);
                string result = sr.ReadToEnd();

                string[] tmpArray = result.Split(',');
                string latitude = tmpArray[2];
                string longitude = tmpArray[3];


                _result.Latitude = double.Parse(latitude);
                _result.Longitude = double.Parse(longitude);
            }
            catch (Exception ex)
            {

            }


            return _result;

        }


    }

    public class Coordinate
    {
        public double Longitude;
        public double Latitude;
    }

    public class ShowMessage
    {
        public ShowMessage()
        {
        }

        /// <summary>
        /// show Alter messagebox
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        public static void showAlter(string message, string title)
        {

            HttpContext _context = HttpContext.Current;

            _context.Response.Write("<Script language=javascript>alert('" +
                message.Replace("\n", "\\n").Replace("\r", "").Replace("'", @"\'")
                + "');</Script>");
        }

        /// <summary>
        /// return a string to show comfrim messagebox
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string getConfirmString(string message)
        {
            string _msg = "";
            _msg = "return window.confirm('" +
                message.Replace(@"\n", @"\\n").Replace(@"\r", @"\\r").Replace(@"\t", @"\\t").
                Replace("'", @"\'") + "');";
            //			}
            return _msg;

        }
        /// <summary>
        /// redirect current page to another page named PageName
        /// </summary>
        /// <param name="PageName"></param>
        public static void RedirectPage(string PageName)
        {
            HttpContext.Current.Response.Write("<Script language=javascript>location.href='" + PageName + "';</Script>");
        }

        //public static void Opennewwindow(WebControl Opener, string PagePath)
        //{
        //    string Clientscript;
        //    Clientscript = "window.open('" + PagePath + "','','height=650,width=600,scrollbars=yes,left=200,top=40,resizable=yes')";
        //    Opener.Attributes.Add("Onclick", Clientscript);
        //}


    }
}
