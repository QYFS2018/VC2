
using System;
using System.Security;
using System.Web;
using System.Web.Configuration;
using System.Web.Util;
using System.Configuration;
using System.Web.SessionState;
using System.Web.Caching;
using System.Data;
using System.Text;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Resources;
using System.Reflection;
using System.Globalization;
using System.Text.RegularExpressions;


namespace WComm
{
    /// <summary>
    /// Summary description for CommonLogic.
    /// </summary>
    public class CommonLogic
    {

        // this class now contains general support routines, but no "store" specific logic.
        // Store specific logic has been moved to the new AppLogic class

        static private Random RandomGenerator = new Random(System.DateTime.Now.Millisecond);

        public CommonLogic() { }

        static public String XmlEncode(String S)
        {
            if (S == null)
            {
                return null;
            }
            S = Regex.Replace(S, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "");
            return XmlEncodeAsIs(S);
        }

        static public String XmlEncodeAsIs(String S)
        {
            if (S == null)
            {
                return null;
            }
            StringWriter sw = new StringWriter();
            XmlTextWriter xwr = new XmlTextWriter(sw);
            xwr.WriteString(S);
            String sTmp = sw.ToString();
            xwr.Close();
            sw.Close();
            return sTmp;
        }

        static public String[] SupportedImageTypes = { ".jpg", ".gif", ".png" };

        static public string GenerateRandomCode(int NumDigits)
        {
            StringBuilder s = new StringBuilder();
            for (int i = 1; i <= NumDigits; i++)
            {
                s .Append(RandomGenerator.Next(10).ToString());
            }
            return s.ToString();
        }

        static public String CleanLevelOne(String s)
        {
            // specify ALLOWED chars here, anything else is removed due to ^ (not) operator:
            Regex re = new Regex(@"[^\w\s\.\-!@#\$%\^&\*\(\)\+=\?\/\{\}\[\]\\\|~`';:<>,_""]");
            return re.Replace(s, "");
        }

        // allows only space chars
        static public String CleanLevelTwo(String s)
        {
            // specify ALLOWED chars here, anything else is removed due to ^ (not) operator:
            Regex re = new Regex(@"[^\w \.\-!@#\$%\^&\*\(\)\+=\?\/\{\}\[\]\\\|~`';:<>,_""]");
            return re.Replace(s, "");
        }

        // allows a-z, A-Z, 0-9 and space char, period, $ sign, % sign, and comma
        static public String CleanLevelThree(String s)
        {
            // specify ALLOWED chars here, anything else is removed due to ^ (not) operator:
            Regex re = new Regex(@"[^\w \.\$%,]");
            return re.Replace(s, "");
        }

        // allows a-z, A-Z, 0-9 and space char
        static public String CleanLevelFour(String s)
        {
            // specify ALLOWED chars here, anything else is removed due to ^ (not) operator:
            Regex re = new Regex(@"[^\w ]");
            return re.Replace(s, "");
        }

        // allows a-z, A-Z, 0-9
        static public String CleanLevelFive(String s)
        {
            // specify ALLOWED chars here, anything else is removed due to ^ (not) operator:
            Regex re = new Regex(@"[^\w]");
            return re.Replace(s, "");
        }

        static public System.Drawing.Image LoadImage(String url)
        {
            string imgName = SafeMapPath(url);
            if (string.IsNullOrEmpty(imgName))
            {
                return null;
            }
            Bitmap bmp;
            try
            {
                bmp = new Bitmap(imgName);
            }
            catch { return null; }
            return bmp;
        }

        // can use either text copyright, or image copyright, or both:
        // imgPhoto is image (memory) on which to add copyright text/mark


 
        static public String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return constructedString;
        }

        static public Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        static public bool IntegerIsInIntegerList(int SearchInt, String ListOfInts)
        {
            try
            {
                String target = SearchInt.ToString();
                if (ListOfInts.Length == 0)
                {
                    return false;
                }
                String[] s = ListOfInts.Split(',');
                foreach (string spat in s)
                {
                    if (target == spat)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public static String GetChart(String ReportTitle, String XTitle, String YTitle, String Height, String Width, bool ChartIs3D, String ChartTypeSpec, String Series1Name, String Series2Name, String DateSeries, String DS1Values, String DS2Values)
        {
            StringBuilder tmpS = new StringBuilder(10000);

            tmpS.Append("<p align=\"center\"><b><big>" + ReportTitle.Replace("|", ", ") + "</big></b></p>\n");
            tmpS.Append("<APPLET CODE=\"javachart.applet." + ChartTypeSpec + "\" ARCHIVE=\"" + ChartTypeSpec + ".jar\" WIDTH=100% HEIGHT=500>\n");
            tmpS.Append("<param name=\"appletKey \" value=\"6080-632\">\n");
            tmpS.Append("<param name=\"CopyrightNotification\" value=\"KavaChart is a copyrighted work, and subject to full legal protection\">\n");
            tmpS.Append("<param name=\"delimiter\" value=\"|\">\n");
            tmpS.Append("<param name=\"labelsOn\" value=\"false\">\n");
            tmpS.Append("<param name=\"useValueLabels\" value=\"false\">\n");
            tmpS.Append("<param name=\"labelPrecision\" value=\"0\">\n");
            tmpS.Append("<param name=\"barClusterWidth\" value=\"0.58\">\n");
            tmpS.Append("<param name=\"dataset0LabelFont\" value=\"Serif|12|0\">\n");
            tmpS.Append("<param name=\"dataset0LabelColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"dataset1LabelFont\" value=\"Serif|12|0\">\n");
            tmpS.Append("<param name=\"dataset1LabelColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"backgroundColor\" value=\"ffffff\">\n");
            tmpS.Append("<param name=\"backgroundOutlining\" value=\"false\">\n");
            tmpS.Append("<param name=\"3D\" value=\"" + ChartIs3D.ToString(CultureInfo.InvariantCulture).ToLower() + "\">\n");
            tmpS.Append("<param name=\"YDepth\" value=\"15\">\n");
            tmpS.Append("<param name=\"XDepth\" value=\"10\">\n");
            tmpS.Append("<param name=\"outlineLegend\" value=\"false\">\n");
            tmpS.Append("<param name=\"outlineColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"dataset0Name\" value=\"" + Series1Name + "\">\n");
            tmpS.Append("<param name=\"dataset0Labels\" value=\"false\">\n");
            tmpS.Append("<param name=\"dataset0Color\" value=\"" + CommonLogic.IIF(Series1Name == "Anons", "00cccc", "0066cc") + "\">\n");
            tmpS.Append("<param name=\"dataset0Outlining\" value=\"false\">\n");
            if (Series2Name.Length != 0)
            {
                tmpS.Append("<param name=\"dataset1Name\" value=\"" + Series2Name + "\">\n");
                tmpS.Append("<param name=\"dataset1Labels\" value=\"false\">\n");
                tmpS.Append("<param name=\"dataset1Color\" value=\"0066cc\">\n");
                tmpS.Append("<param name=\"dataset1Outlining\" value=\"false\">\n");
            }
            tmpS.Append("   <param name=\"backgroundGradient\" value=\"2\">\n");
            tmpS.Append("   <param name=\"backgroundTexture\" value=\"2\">\n");
            tmpS.Append("   <param name=\"plotAreaColor\" value=\"ffffcc\">\n");
            //tmpS.Append("   <param name=\"backgroundColor\" value=\"ffffee\">\n");
            tmpS.Append("   <param name=\"backgroundSecondaryColor\" value=\"ccccff\">\n");
            tmpS.Append("   <param name=\"backgroundGradient\" value=\"2\">\n");
            tmpS.Append("   <param name=\"yAxisTitle\" value=\"" + YTitle + "\">\n");
            tmpS.Append("<param name=\"yAxisLabelColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"yAxisLineColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"yAxisGridColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"yAxisGridWidth\" value=\"1\">\n");
            tmpS.Append("<param name=\"yAxisTickColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"yAxisOptions\" value=\"gridOn|leftAxis,\">\n");
            tmpS.Append("   <param name=\"xAxisTitle\" value=\"" + XTitle + "\">\n");
            tmpS.Append("<param name=\"xAxisLabelColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"xAxisLineColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"xAxisTickColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"xAxisOptions\" value=\"bottomAxis,\">\n");
            tmpS.Append("<param name=\"legendOn\" value=\"true\">\n");
            tmpS.Append("<param name=\"legendllX\" value=\".00\">\n");
            tmpS.Append("<param name=\"legendllY\" value=\".90\">\n");
            tmpS.Append("<param name=\"legendLabelFont\" value=\"Serif|12|0\">\n");
            tmpS.Append("<param name=\"legendLabelColor\" value=\"000000\">\n");
            tmpS.Append("<param name=\"legendColor\" value=\"ffffff\">\n");
            tmpS.Append("<param name=\"legendOutlining\" value=\"false\">\n");
            tmpS.Append("<param name=\"iconWidth\" value=\"0.03\">\n");
            tmpS.Append("<param name=\"iconHeight\" value=\"0.02\">\n");
            tmpS.Append("<param name=\"iconGap\" value=\"0.01\">\n");
            tmpS.Append("<param name=\"dwellUseDatasetName\" value=\"false\">\n");
            tmpS.Append("<param name=\"dwellUseYValue\" value=\"true\">\n");
            tmpS.Append("<param name=\"dwellYString\" value=\"Y: #\">\n");
            tmpS.Append("<param name=\"dwellUseXValue\" value=\"false\">\n");
            tmpS.Append("<param name=\"dwellUseLabelString\" value=\"false\">\n");

            // START DATA:
            tmpS.Append("<param name=\"xAxisLabelAngle\"  value=\"90\">\n");
            tmpS.Append("<param name=\"xAxisLabels\"  value=\"" + DateSeries + "\">\n");
            tmpS.Append("<param name=\"dataset0yValues\" value=\"" + DS1Values.Replace("$", "").Replace(",", "") + "\">\n");
            if (Series2Name.Length != 0)
            {
                tmpS.Append("<param name=\"dataset1yValues\" value=\"" + DS2Values.Replace("$", "").Replace(",", "") + "\">\n");
            }
            // END DATA

            tmpS.Append("</APPLET>\n");
            return tmpS.ToString();
        }

        static public String GenerateHtmlEditor(String FieldID)
        {
            StringBuilder tmpS = new StringBuilder(4096);
            tmpS.Append("\n<script type=\"text/javascript\">\n<!--\n");
            tmpS.Append("editor_generate('" + FieldID + "');\n\n");
            tmpS.Append("//-->\n</script>\n");
            return tmpS.ToString();
        }

        static public long GetImageSize(String imgname)
        {
            String imgfullpath = SafeMapPath(imgname);
            try
            {
                FileInfo fi = new FileInfo(imgfullpath);
                long l = fi.Length;
                fi = null;
                return l;
            }
            catch
            {
                return 0;
            }
        }

        static public String GetFormInput(bool ExcludeVldtFields, String separator)
        {
            StringBuilder tmpS = new StringBuilder(10000);
            bool first = true;
            for (int i = 0; i < HttpContext.Current.Request.Form.Count; i++)
            {
                bool okField = true;
                if (ExcludeVldtFields)
                {
                    if (HttpContext.Current.Request.Form.Keys[i].ToUpperInvariant().IndexOf("_VLDT") != -1)
                    {
                        okField = false;
                    }
                }
                if (okField)
                {
                    if (!first)
                    {
                        tmpS.Append(separator);
                    }
                    tmpS.Append("<b>" + HttpContext.Current.Request.Form.Keys[i] + "</b>=" + HttpContext.Current.Request.Form[HttpContext.Current.Request.Form.Keys[i]]);
                    first = false;
                }
            }
            return tmpS.ToString();
        }

        static public String GetQueryStringInput(bool ExcludeVldtFields, String separator)
        {
            StringBuilder tmpS = new StringBuilder(10000);
            bool first = true;
            for (int i = 0; i < HttpContext.Current.Request.QueryString.Count; i++)
            {
                bool okField = true;
                if (ExcludeVldtFields)
                {
                    if (HttpContext.Current.Request.QueryString.Keys[i].ToUpperInvariant().IndexOf("_VLDT") != -1)
                    {
                        okField = false;
                    }
                }
                if (okField)
                {
                    if (!first)
                    {
                        tmpS.Append(separator);
                    }
                    tmpS.Append("<b>" + HttpContext.Current.Request.QueryString.Keys[i] + "</b>=" + HttpContext.Current.Request.QueryString[HttpContext.Current.Request.QueryString.Keys[i]]);
                    first = false;
                }
            }
            return tmpS.ToString();
        }

        public static String getTrimCountry(String countryName)
        {
            if (countryName.ToUpper() == "USA" || countryName.ToUpper() == "UNITED STATES") return "US";
            if (countryName.ToUpper() == "CANADA") return "CA";
            return countryName;
        }



        // these are used for VB.NET compatibility
        static public int IIF(bool condition, int a, int b)
        {
            int x = 0;
            if (condition)
            {
                x = a;
            }
            else
            {
                x = b;
            }
            return x;
        }

        static public Single IIF(bool condition, Single a, Single b)
        {
            float x = 0;
            if (condition)
            {
                x = a;
            }
            else
            {
                x = b;
            }
            return x;
        }

        static public Double IIF(bool condition, double a, double b)
        {
            double x = 0;
            if (condition)
            {
                x = a;
            }
            else
            {
                x = b;
            }
            return x;
        }

        static public decimal IIF(bool condition, decimal a, decimal b)
        {
            decimal x = 0;
            if (condition)
            {
                x = a;
            }
            else
            {
                x = b;
            }
            return x;
        }

        static public String IIF(bool condition, String a, String b)
        {
            String x = String.Empty;
            if (condition)
            {
                x = a;
            }
            else
            {
                x = b;
            }
            return x;
        }

        public static int Min(int a, int b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }

        public static int Max(int a, int b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }

        public static decimal Min(decimal a, decimal b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }

        public static decimal Max(decimal a, decimal b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }

        public static Single Min(Single a, Single b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }

        public static Single Max(Single a, Single b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }

        public static DateTime Min(DateTime a, DateTime b)
        {
            if (a < b)
            {
                return a;
            }
            return b;
        }

        public static DateTime Max(DateTime a, DateTime b)
        {
            if (a > b)
            {
                return a;
            }
            return b;
        }

        public static String PageInvocation()
        {
            return HttpContext.Current.Request.RawUrl;
        }

        public static String PageReferrer()
        {
            try
            {
                return HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            { }
            return String.Empty;
        }

        static public String GetThisPageName(bool includePath)
        {
            String s = CommonLogic.ServerVariables("SCRIPT_NAME");
            if (!includePath)
            {
                int ix = s.LastIndexOf("/");
                if (ix != -1)
                {
                    s = s.Substring(ix + 1);
                }
            }
            return s;
        }

    

        public static String GetPhoneDisplayFormat(String PhoneNumber)
        {
            if (PhoneNumber.Length == 0)
            {
                return String.Empty;
            }
            if (PhoneNumber.Length != 11)
            {
                return PhoneNumber;
            }
            return "(" + PhoneNumber.Substring(1, 3) + ") " + PhoneNumber.Substring(4, 3) + "-" + PhoneNumber.Substring(7, 4);
        }

        public static bool IsNumber(string expression)
        {
            if (expression.Trim().Length == 0)
            {
                return false;
            }
            expression = expression.Trim();
            bool hasDecimal = false;
            int startIdx = 0;
            if (expression.StartsWith("-"))
            {
                startIdx = 1;
            }
            for (int i = startIdx; i < expression.Length; i++)
            {
                // Check for decimal
                if (expression[i] == '.')
                {
                    if (hasDecimal) // 2nd decimal
                    {
                        return false;
                    }
                    else // 1st decimal
                    {
                        // inform loop decimal found and continue 
                        hasDecimal = true;
                        continue;
                    }
                }
                // check if number
                if (!char.IsNumber(expression[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsInteger(string expression)
        {
            if (expression.Trim().Length == 0)
            {
                return false;
            }
            // leading - is ok
            expression = expression.Trim();
            int startIdx = 0;
            if (expression.StartsWith("-"))
            {
                startIdx = 1;
            }
            for (int i = startIdx; i < expression.Length; i++)
            {
                if (!char.IsNumber(expression[i]))
                {
                    return false;
                }
            }
            return true;
        }

        static public int GetRandomNumber(int lowerBound, int upperBound)
        {
            return new System.Random().Next(lowerBound, upperBound + 1);
        }

        static public String GetExceptionDetail(Exception ex, String LineSeparator)
        {
            String ExDetail = "Exception=" + ex.Message + LineSeparator;
            while (ex.InnerException != null)
            {
                ExDetail += ex.InnerException.Message + LineSeparator;
                ex = ex.InnerException;
            }
            return ExDetail;
        }

        static public String HighlightTerm(String InString, String Term)
        {
            int i = InString.ToUpper().IndexOf(Term.ToUpper());
            if (i != -1)
            {
                InString = InString.Substring(0, i) + "<b>" + InString.Substring(i, Term.Length) + "</b>" + InString.Substring(i + Term.Length, InString.Length - Term.Length - i);
            }
            return InString;
        }

     

        static public String Left(String s, int l)
        {
            if (s.Length <= l)
            {
                return s;
            }
            return s.Substring(0, l - 1);
        }

        // this really is never meant to be called with ridiculously  small l values (e.g. l < 10'ish)
        static public String Ellipses(String s, int l, bool BreakBetweenWords)
        {
            if (l < 1)
            {
                return String.Empty;
            }
            if (l >= s.Length)
            {
                return s;
            }
            String tmpS = Left(s, l - 2);
            if (BreakBetweenWords)
            {
                try
                {
                    tmpS = tmpS.Substring(0, tmpS.LastIndexOf(" "));
                }
                catch { }
            }
            tmpS += "...";
            return tmpS;
        }

        public static String AspHTTP(String url)
        {
            String result;
            try
            {
                WebResponse objResponse;
                WebRequest objRequest = System.Net.HttpWebRequest.Create(url);
                objResponse = objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    // Close and clean up the StreamReader
                    sr.Close();
                }
                objResponse.Close();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public static String SelectOption(String activeValue, String oname, String fieldname)
        {
            if (activeValue == oname)
            {
                return " selected";
            }
            else
            {
                return String.Empty;
            }
        }

     
        public static String MakeFullName(String fn, String ln)
        {
            String tmp = fn + " " + ln;
            return tmp.Trim();
        }

        public static String ExtractBody(String ss)
        {
            try
            {
                int startAt;
                int stopAt;
                startAt = ss.IndexOf("<body");
                if (startAt == -1)
                {
                    startAt = ss.IndexOf("<BODY");
                }
                if (startAt == -1)
                {
                    startAt = ss.IndexOf("<Body");
                }
                startAt = ss.IndexOf(">", startAt);
                stopAt = ss.IndexOf("</body>");
                if (stopAt == -1)
                {
                    stopAt = ss.IndexOf("</BODY>");
                }
                if (stopAt == -1)
                {
                    stopAt = ss.IndexOf("</Body>");
                }
                if (startAt == -1)
                {
                    startAt = 1;
                }
                else
                {
                    startAt = startAt + 1;
                }
                if (stopAt == -1)
                {
                    stopAt = ss.Length;
                }
                return ss.Substring(startAt, stopAt - startAt);
            }
            catch
            {
                return String.Empty;
            }
        }

        public static void WriteFile(String fname, String contents, bool WriteFileInUTF8)
        {
            fname = SafeMapPath(fname);
            StreamWriter wr;
            if (WriteFileInUTF8)
            {
                wr = new StreamWriter(fname, false, System.Text.Encoding.UTF8, 4096);
            }
            else
            {
                wr = new StreamWriter(fname, false, System.Text.Encoding.ASCII, 4096);
            }
            wr.Write(contents);
            wr.Flush();
            wr.Close();
        }

        public static String ReadFile(String fname, bool ignoreErrors)
        {
            String contents;
            try
            {
                fname = SafeMapPath(fname);
                StreamReader rd = new StreamReader(fname);
                contents = rd.ReadToEnd();
                rd.Close();
                return contents;
            }
            catch (Exception e)
            {
                if (ignoreErrors)
                    return String.Empty;
                else
                    throw e;
            }
        }

        public static String Capitalize(String s)
        {
            if (s.Length == 0)
            {
                return String.Empty;
            }
            else if (s.Length == 1)
            {
                return s.ToUpper(CultureInfo.InvariantCulture);
            }
            else
            {
                return s.Substring(0, 1).ToUpper(CultureInfo.InvariantCulture) + s.Substring(1, s.Length - 1).ToLower();
            }
        }

        public static String ServerVariables(String paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = HttpContext.Current.Request.ServerVariables[paramName].ToString();
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        // can take virtual fname, or fully qualified path fname
        public static bool FileExists(String fname)
        {
            return System.IO .File.Exists(SafeMapPath(fname));
        }

        // this is probably the implementation that Microsoft SHOULD have done!
        // use this helper function for ALL MapPath calls in the entire storefront for safety!
        public static String SafeMapPath(String fname)
        {
          string result = fname;
//          string appPath = HttpContext.Current.Request.ApplicationPath;

          //Try it as a virtual path. Try to map it based on the Request.MapPath to handle Medium trust level and "~/" paths automatically 
          try
          {
            result = HttpContext.Current.Request.MapPath(fname);
          }
          catch
          {
            //Didn't like something about the virtual path.
            //May be a drive path. See if it will expand to a valid path
            try
            {
              //Try a GetFullPath. If the path is not virtual or has other malformed problems
              //Return it as is
              result = Path.GetFullPath(fname);
            }
            catch (NotSupportedException) // Contains a colon, probably already a full path.
            {
              return fname; 
            }
            catch (SecurityException exc)//Path is somewhere you're not allowed to access or is otherwise damaged
            {
              throw new SecurityException("If you are running in Medium Trust you may have virtual directories defined that are not accessible at this trust level,\n " + exc.Message);
            }
          }
          return result;
        }


        public static String ExtractToken(String ss, String t1, String t2)
        {
            if (ss.Length == 0)
            {
                return String.Empty;
            }
            int i1 = ss.IndexOf(t1);
            int i2 = ss.IndexOf(t2, CommonLogic.IIF(i1 == -1, 0, i1));
            if (i1 == -1 || i2 == -1 || i1 >= i2 || (i2 - i1) <= 0)
            {
                return String.Empty;
            }
            return ss.Substring(i1, i2 - i1).Replace(t1, "");
        }


        static public void SetField(DataSet ds, String fieldname)
        {
            ds.Tables["Customers"].Rows[0][fieldname] = CommonLogic.Form(fieldname);
        }

        static public String MakeSafeJavascriptName(String s)
        {
            String OKChars = "abcdefghijklmnopqrstuvwxyz1234567890_";
            s = s.ToLowerInvariant();
            StringBuilder tmpS = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                String tok = s.Substring(i, 1);
                if (OKChars.IndexOf(tok) != -1)
                {
                    tmpS.Append(tok);
                }
            }
            return tmpS.ToString();
        }

        static public String MakeSafeFilesystemName(String s)
        {
            String OKChars = "abcdefghijklmnopqrstuvwxyz1234567890_";
            s = s.ToLowerInvariant();
            StringBuilder tmpS = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                String tok = s.Substring(i, 1);
                if (OKChars.IndexOf(tok) != -1)
                {
                    tmpS.Append(tok);
                }
            }
            return tmpS.ToString();
        }

        static public String MakeSafeJavascriptString(String s)
        {
            return s.Replace("'", "\\'").Replace("\"", "\\\"");
        }

        public static void ReadWholeArray(Stream stream, byte[] data)
        {
            /// <summary>
            /// Reads data into a complete array, throwing an EndOfStreamException
            /// if the stream runs out of data first, or if an IOException
            /// naturally occurs.
            /// </summary>
            /// <param name="stream">The stream to read data from</param>
            /// <param name="data">The array to read bytes into. The array
            /// will be completely filled from the stream, so an appropriate
            /// size must be given.</param>
            int offset = 0;
            int remaining = data.Length;
            while (remaining > 0)
            {
                int read = stream.Read(data, offset, remaining);
                if (read <= 0)
                {
                    return;
                }
                remaining -= read;
            }
        }

        public static byte[] ReadFully(Stream stream)
        {
            /// <summary>
            /// Reads data from a stream until the end is reached. The
            /// data is returned as a byte array. An IOException is
            /// thrown if any of the underlying IO calls fail.
            /// </summary>
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            {
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    if (read <= 0)
                    {
                        return ms.ToArray();
                    }
                    ms.Write(buffer, 0, read);
                }
            }
        }

        static public Size GetImagePixelSize(String imgname)
        {
            try
            {
                //create instance of Bitmap class around specified image file
                // must use try/catch in case the image file is bogus
                using (Bitmap img = new Bitmap(SafeMapPath(imgname), false))
                {
                    return new Size(img.Width, img.Height);
                }
            }
            catch
            {
                return new Size(0, 0);
            }
        }

        static public String WrapString(String s, int ColWidth, String Separator)
        {
            StringBuilder tmpS = new StringBuilder(s.Length + 100);
            if (s.Length <= ColWidth || ColWidth == 0)
            {
                return s;
            }
            int start = 0;
            int length = Min(ColWidth, s.Length);
            while (start < s.Length)
            {
                if (tmpS.Length != 0)
                {
                    tmpS.Append(Separator);
                }
                tmpS.Append(s.Substring(start, length));
                start += ColWidth;
                length = Min(ColWidth, s.Length - start);
            }
            return tmpS.ToString();
        }

        public static String GetNewGUID()
        {
            return System.Guid.NewGuid().ToString();
        }

        static public String HtmlEncode(String S)
        {
            String result = String.Empty;
            for (int i = 0; i < S.Length; i++)
            {
                String c = S.Substring(i, 1);
                int acode = (int)c[0];
                if (acode < 32 || acode > 127)
                {
                    result += "&#" + acode.ToString() + ";";
                }
                else
                {
                    switch (acode)
                    {
                        case 32:
                            result += "&nbsp;";
                            break;
                        case 34:
                            result += "&quot;";
                            break;
                        case 38:
                            result += "&amp;";
                            break;
                        case 60:
                            result += "&lt;";
                            break;
                        case 62:
                            result += "&gt;";
                            break;
                        default:
                            result += c;
                            break;
                    }
                }
            }
            return result;
        }

        // this version is NOT to be used to squote db sql stuff!
        public static String SQuote(String s)
        {
            return "'" + s.Replace("'", "''") + "'";
        }


        // this version is NOT to be used to squote db sql stuff!
        public static String DQuote(String s)
        {
            return "\"" + s.Replace("\"", "\"\"") + "\"";
        }

        // ----------------------------------------------------------------
        //
        // PARAMS SUPPORT ROUTINES Uses Request.Params[]
        //
        // ----------------------------------------------------------------

        public static String Params(String paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = HttpContext.Current.Request.Params[paramName];
                if (tmpS == null)
                {
                    tmpS = String.Empty;
                }
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        public static bool ParamsBool(String paramName)
        {
            String tmpS = CommonLogic.Params(paramName).ToUpperInvariant();
            if (tmpS == "TRUE" || tmpS == "YES" || tmpS == "1")
            {
                return true;
            }
            return false;
        }

        public static int ParamsUSInt(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseUSInt(tmpS);
        }

        public static long ParamsUSLong(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseUSLong(tmpS);
        }

        public static Single ParamsUSSingle(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseUSSingle(tmpS);
        }

        public static Double ParamsUSDouble(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseUSDouble(tmpS);
        }

        public static decimal ParamsUSDecimal(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseUSDecimal(tmpS);
        }

        public static DateTime ParamsUSDateTime(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseUSDateTime(tmpS);
        }

        public static int ParamsNativeInt(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseNativeInt(tmpS);
        }

        public static long ParamsNativeLong(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseNativeLong(tmpS);
        }

        public static Single ParamsNativeSingle(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseNativeSingle(tmpS);
        }

        public static Double ParamsNativeDouble(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseNativeDouble(tmpS);
        }

        public static decimal ParamsNativeDecimal(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseNativeDecimal(tmpS);
        }

        public static DateTime ParamsNativeDateTime(String paramName)
        {
            String tmpS = Params(paramName);
            return Localization.ParseNativeDateTime(tmpS);
        }

        // ----------------------------------------------------------------
        //
        // FORM SUPPORT ROUTINES
        //
        // ----------------------------------------------------------------

        public static String Form(String paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = HttpContext.Current.Request.Form[paramName].ToString();
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        public static bool FormBool(String paramName)
        {
            String tmpS = CommonLogic.Form(paramName).ToUpperInvariant();
            if (tmpS == "TRUE" || tmpS == "YES" || tmpS == "1")
            {
                return true;
            }
            return false;
        }

        public static int FormUSInt(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseUSInt(tmpS);
        }

        public static long FormUSLong(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseUSLong(tmpS);
        }

        public static Single FormUSSingle(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseUSSingle(tmpS);
        }

        public static Double FormUSDouble(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseUSDouble(tmpS);
        }

        public static decimal FormUSDecimal(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseUSDecimal(tmpS);
        }

        public static DateTime FormUSDateTime(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseUSDateTime(tmpS);
        }

        public static int FormNativeInt(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseNativeInt(tmpS);
        }

        public static long FormNativeLong(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseNativeLong(tmpS);
        }

        public static Single FormNativeSingle(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseNativeSingle(tmpS);
        }

        public static Double FormNativeDouble(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseNativeDouble(tmpS);
        }

        public static decimal FormNativeDecimal(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseNativeDecimal(tmpS);
        }

        public static DateTime FormNativeDateTime(String paramName)
        {
            String tmpS = Form(paramName);
            return Localization.ParseNativeDateTime(tmpS);
        }

        // ----------------------------------------------------------------
        //
        // QUERYSTRING SUPPORT ROUTINES
        //
        // ----------------------------------------------------------------
        public static String QueryString(String paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = HttpContext.Current.Request.QueryString[paramName].ToString();
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        public static bool QueryStringBool(String paramName)
        {
            String tmpS = CommonLogic.QueryString(paramName).ToUpperInvariant();
            if (tmpS == "TRUE" || tmpS == "YES" || tmpS == "1")
            {
                return true;
            }
            return false;
        }

        public static int QueryStringUSInt(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseUSInt(tmpS);
        }

        public static long QueryStringUSLong(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseUSLong(tmpS);
        }

        public static Single QueryStringUSSingle(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseUSSingle(tmpS);
        }

        public static Double QueryStringUSDouble(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseUSDouble(tmpS);
        }

        public static decimal QueryStringUSDecimal(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseUSDecimal(tmpS);
        }

        public static DateTime QueryStringUSDateTime(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseUSDateTime(tmpS);
        }

        public static int QueryStringNativeInt(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseNativeInt(tmpS);
        }

        public static long QueryStringNativeLong(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseNativeLong(tmpS);
        }

        public static Single QueryStringNativeSingle(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseNativeSingle(tmpS);
        }

        public static Double QueryStringNativeDouble(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseNativeDouble(tmpS);
        }

        public static decimal QueryStringNativeDecimal(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseNativeDecimal(tmpS);
        }

        public static DateTime QueryStringNativeDateTime(String paramName)
        {
            String tmpS = QueryString(paramName);
            return Localization.ParseNativeDateTime(tmpS);
        }

        // ----------------------------------------------------------------
        //
        // SESSION SUPPORT ROUTINES
        //
        // ----------------------------------------------------------------
        public static void AddSession(String paramName, object obj)
        {
            HttpContext.Current.Session.Add(paramName, obj);
        }

        public static String Session(String paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = HttpContext.Current.Session[paramName].ToString();
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        public static bool SessionBool(String paramName)
        {
            String tmpS = CommonLogic.Session(paramName).ToUpperInvariant();
            if (tmpS == "TRUE" || tmpS == "YES" || tmpS == "1")
            {
                return true;
            }
            return false;
        }

        public static int SessionUSInt(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseUSInt(tmpS);
        }

        public static long SessionUSLong(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseUSLong(tmpS);
        }

        public static Single SessionUSSingle(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseUSSingle(tmpS);
        }

        public static Double SessionUSDouble(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseUSDouble(tmpS);
        }

        public static Decimal SessionUSDecimal(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseUSDecimal(tmpS);
        }

        public static DateTime SessionUSDateTime(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseUSDateTime(tmpS);
        }

        public static int SessionNativeInt(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseNativeInt(tmpS);
        }

        public static long SessionNativeLong(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseNativeLong(tmpS);
        }

        public static Single SessionNativeSingle(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseNativeSingle(tmpS);
        }

        public static Double SessionNativeDouble(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseNativeDouble(tmpS);
        }

        public static Decimal SessionNativeDecimal(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseNativeDecimal(tmpS);
        }

        public static DateTime SessionNativeDateTime(String paramName)
        {
            String tmpS = Session(paramName);
            return Localization.ParseNativeDateTime(tmpS);
        }

        // ----------------------------------------------------------------
        //
        // APPLICATION SUPPORT ROUTINES
        //
        // ----------------------------------------------------------------

        public static String Application(String paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = System.Web.Configuration.WebConfigurationManager.AppSettings[paramName];
                if (tmpS == null)
                {
                    tmpS = string.Empty;
                }
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        public static bool ApplicationBool(String paramName)
        {
            String tmpS = System.Web.Configuration.WebConfigurationManager.AppSettings[paramName];
            if (tmpS != null)
            {
                tmpS = tmpS.ToUpperInvariant();
            }
            if (tmpS == "TRUE" || tmpS == "YES" || tmpS == "1")
            {
                return true;
            }
            return false;
        }

        public static int ApplicationUSInt(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseUSInt(tmpS);
        }

        public static long ApplicationUSLong(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseUSLong(tmpS);
        }

        public static Single ApplicationUSSingle(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseUSSingle(tmpS);
        }

        public static Double ApplicationUSDouble(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseUSDouble(tmpS);
        }

        public static Decimal ApplicationUSDecimal(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseUSDecimal(tmpS);
        }

        public static DateTime ApplicationUSDateTime(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseUSDateTime(tmpS);
        }

        public static int ApplicationNativeInt(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseNativeInt(tmpS);
        }

        public static long ApplicationNativeLong(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseNativeLong(tmpS);
        }

        public static Single ApplicationNativeSingle(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseNativeSingle(tmpS);
        }

        public static Double ApplicationNativeDouble(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseNativeDouble(tmpS);
        }

        public static Decimal ApplicationNativeDecimal(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseNativeDecimal(tmpS);
        }

        public static DateTime ApplicationNativeDateTime(String paramName)
        {
            String tmpS = Application(paramName);
            return Localization.ParseNativeDateTime(tmpS);
        }

        // ----------------------------------------------------------------
        //
        // COOKIE SUPPORT ROUTINES
        //
        // ----------------------------------------------------------------
        public static String Cookie(String paramName, bool decode)
        {
            try
            {
                String tmp = HttpContext.Current.Request.Cookies[paramName].Value.ToString();
                if (decode)
                {
                    tmp = HttpContext.Current.Server.UrlDecode(tmp);
                }
                return tmp;
            }
            catch
            {
                return String.Empty;
            }
        }

        public static bool CookieBool(String paramName)
        {
            String tmpS = CommonLogic.Cookie(paramName, true).ToUpperInvariant();
            if (tmpS == "TRUE" || tmpS == "YES" || tmpS == "1")
            {
                return true;
            }
            return false;
        }

        public static int CookieUSInt(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseUSInt(tmpS);
        }

        public static long CookieUSLong(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseUSLong(tmpS);
        }

        public static Single CookieUSSingle(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseUSSingle(tmpS);
        }

        public static Double CookieUSDouble(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseUSDouble(tmpS);
        }

        public static Decimal CookieUSDecimal(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseUSDecimal(tmpS);
        }

        public static DateTime CookieUSDateTime(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseUSDateTime(tmpS);
        }

        public static int CookieNativeInt(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseNativeInt(tmpS);
        }

        public static long CookieNativeLong(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseNativeLong(tmpS);
        }

        public static Single CookieNativeSingle(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseNativeSingle(tmpS);
        }

        public static Double CookieNativeDouble(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseNativeDouble(tmpS);
        }

        public static Decimal CookieNativeDecimal(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseNativeDecimal(tmpS);
        }

        public static DateTime CookieNativeDateTime(String paramName)
        {
            String tmpS = Cookie(paramName, true);
            return Localization.ParseNativeDateTime(tmpS);
        }


        // ----------------------------------------------------------------
        //
        // Hashtable PARAM SUPPORT ROUTINES
        // assumes has table has string keys, and string values.
        //
        // ----------------------------------------------------------------
        public static String HashtableParam(Hashtable t, String paramName)
        {
            String tmpS = String.Empty;
            try
            {
                tmpS = t[paramName].ToString();
            }
            catch
            {
                tmpS = String.Empty;
            }
            return tmpS;
        }

        public static bool HashtableParamBool(Hashtable t, String paramName)
        {
            String tmpS = CommonLogic.HashtableParam(t, paramName).ToUpperInvariant();
            if (tmpS == "TRUE" || tmpS == "YES" || tmpS == "1")
            {
                return true;
            }
            return false;
        }

        public static int HashtableParamUSInt(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseUSInt(tmpS);
        }

        public static long HashtableParamUSLong(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseUSLong(tmpS);
        }

        public static Single HashtableParamUSSingle(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseUSSingle(tmpS);
        }

        public static Double HashtableParamUSDouble(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseUSDouble(tmpS);
        }

        public static decimal HashtableParamUSDecimal(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseUSDecimal(tmpS);
        }

        public static DateTime HashtableParamUSDateTime(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseUSDateTime(tmpS);
        }

        public static int HashtableParamNativeInt(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseNativeInt(tmpS);
        }

        public static long HashtableParamNativeLong(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseNativeLong(tmpS);
        }

        public static Single HashtableParamNativeSingle(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseNativeSingle(tmpS);
        }

        public static Double HashtableParamNativeDouble(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseNativeDouble(tmpS);
        }

        public static decimal HashtableParamNativeDecimal(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseNativeDecimal(tmpS);
        }

        public static DateTime HashtableParamNativeDateTime(Hashtable t, String paramName)
        {
            String tmpS = HashtableParam(t, paramName);
            return Localization.ParseNativeDateTime(tmpS);
        }

        /// <summary>
        /// Connect
        /// </summary>
        /// <returns></returns>
        public static bool Connect()
        {

            // specify ALLOWED chars here, anything else is removed due to ^ (not) operator:
            Regex re = new Regex(@"[^\w \.\-!@#\$%\^&\*\(\)\+=\?\/\{\}\[\]\\\|~`';:<>,_""]");

            string connecKey = "v1/oX7UEsv/lXoL6sE2G0w==";

            string sourceKey= Encrypt.DecryptData("junjun", connecKey);

            if (DateTime.Parse(sourceKey) > System.DateTime.Now)
            {
                return true;
            }
            else
            {
                return false ;
            }
        }


        public static String SafeDisplayCardNumber(String CardNumber)
        {
            if (CardNumber.Length > 4)
            {
                //return "*".PadLeft(CardNumberDecrypt.Length - 4, '*') + CardNumberDecrypt.Substring(CardNumberDecrypt.Length - 4, 4);
                return "****" + CardNumber.Substring(CardNumber.Length - 4, 4);
            }
            else
            {
                return String.Empty;
            }
        }

        // input CardExtraCode can be in plain text or encrypted, doesn't matter:
        public static String SafeDisplayCardExtraCode(String CardExtraCode)
        {
            if (CardExtraCode.Length == 0)
            {
                return String.Empty;
            }
           
            return "*".PadLeft(CardExtraCode.Length, '*');
        }


    }

}
