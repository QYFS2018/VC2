using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Xml;
using System.Text;


namespace WComm
{
	public class XMLHTTPClass
	{

		private string _siteUrl;
		private string _content;
		private string _returns;

		public string SiteUrl
		{
			get
			{
				return this._siteUrl;
			}
			set
			{
				this._siteUrl = value;
			}
		}

		public string Content
		{
			get
			{
				return this._content;
			}
			set
			{
				this._content = value;
			}
		}

		public string Returns
		{
			get
			{
				return this._returns;
			}
			set
			{
				this._returns = value;
			}
		}


		public XMLHTTPClass()
		{
		}
		public XMLHTTPClass(string siteUrl,string content)
		{
			this._content =content;
			this._siteUrl =siteUrl;
		}


		public string  Post()
		{
			HttpWebRequest objRequest =
				(HttpWebRequest)WebRequest.Create(_siteUrl);
			objRequest.Method = "POST";
			objRequest.ContentType = "text/xml";

			objRequest.ContentLength = _content.Length;
			
			StreamWriter myWriter = new StreamWriter(objRequest.GetRequestStream());
			myWriter.Write(_content);
			myWriter.Close();

			string input="";

            HttpWebResponse objResponse = null;

            try
            {
                objResponse = (HttpWebResponse)objRequest.GetResponse();
            }
            catch (Exception ex)
            {
                string aa = ex.Message;
            }

			StreamReader sr;
            StringBuilder sb = new StringBuilder();
			using (sr = new
				StreamReader(objResponse.GetResponseStream()) )
			{
				while ((input=sr.ReadLine())!=null) 
				{
                    sb .Append( input);
				}
				sr.Close();
			}
            _returns = sb.ToString();
			return _returns;

		}

	}
}
