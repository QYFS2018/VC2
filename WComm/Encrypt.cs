using System;
using System.Configuration;
using System.Text;
using System.IO;
using System.Security;
using System.Security.Cryptography;

namespace WComm
{
	/// <summary>
	/// Summary description for Encrypt.
	/// </summary>
	public class Encrypt
	{
		static private Byte[] m_Key = new Byte[8]; 
		static private Byte[] m_IV = new Byte[8]; 

		public Encrypt()
		{}

		static public string EncryptData(String strKey, String strData)
		{
            string strValue;
            strValue = "";


            if (strKey.Length > 0)
            {
                if (strKey.Length < 16)
                {
                    strKey = strKey + "XXXXXXXXXXXXXXXX".Substring(0, 16 - strKey.Length);

                }
                if (strKey.Length > 16)
                {
                    strKey = strKey.Substring(0, 16);
                }


                // create encryption keys
                Byte[] byteKey = Encoding.UTF8.GetBytes(strKey.Substring(0, 8));
                Byte[] byteVector = Encoding.UTF8.GetBytes(strKey.Substring(strKey.Length - 9, 8));


                // convert data to byte array
                Byte[] byteData = Encoding.UTF8.GetBytes(strData);

                // encrypt 
                DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
                MemoryStream objMemoryStream = new MemoryStream();
                CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateEncryptor(byteKey, byteVector), CryptoStreamMode.Write);
                objCryptoStream.Write(byteData, 0, byteData.Length);
                objCryptoStream.FlushFinalBlock();





                //convert to string and Base64 encode
                strValue = Convert.ToBase64String(objMemoryStream.ToArray());

                // do something 
            }
            else
            {
                strValue = strData;
            }

            return strValue;
		}

		static public string DecryptData(String strKey, String strData)
		{
            if (strData == null)
            {
                return "";
            }
            string strValue = "";
            if (strKey.Length > 0)
            {
                if (strKey.Length < 16)
                {
                    strKey = strKey + "XXXXXXXXXXXXXXXX".Substring(0, 16 - strKey.Length);
                }

                if (strKey.Length > 16)
                {
                    strKey = strKey.Substring(0, 16);
                }
                // create encryption keys
                Byte[] byteKey = Encoding.UTF8.GetBytes(strKey.Substring(0, 8));
                Byte[] byteVector = Encoding.UTF8.GetBytes(strKey.Substring(strKey.Length - 9, 8));

                // convert data to byte array and Base64 decode
                Byte[] byteData = new Byte[strData.Length];
                try
                {
                    byteData = Convert.FromBase64String(strData);

                }

                catch (Exception ex)
                {
                    strValue = strData;
                }

                if (strValue.Length == 0)
                {
                    try
                    {
                        // decrypt
                        DESCryptoServiceProvider objDES = new DESCryptoServiceProvider();
                        MemoryStream objMemoryStream = new MemoryStream();
                        CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDES.CreateDecryptor(byteKey, byteVector), CryptoStreamMode.Write);
                        objCryptoStream.Write(byteData, 0, byteData.Length);
                        objCryptoStream.FlushFinalBlock();


                        // convert to string
                        Encoding objEncoding = Encoding.UTF8;


                        strValue = objEncoding.GetString(objMemoryStream.ToArray());


                    }
                    catch
                    {
                        strValue = "";
                    }

                }

            }
            else
            {
                strValue = strData;


            }

            return strValue;
		}

			
	}

   

   
}
