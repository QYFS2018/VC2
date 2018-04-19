using System;
using Microsoft.Win32;

namespace WComm
{
	/// <summary>
	/// RegistryAccess is a class to provide simplified registry access with
	/// utilities that help manage the details of accessing the registry.
	/// <br />
	/// ----------------------------------------------------------
	/// <br />
	/// Created:	RMS	091803
	/// <br />
	/// Purpose:	Simplify the registry access and provide utilities 
	///				for creating/massaging registry parameters 
	///				for more reliable execution.
	///	<br />
	///	Changes:
	///	
	/// <br />
	/// ----------------------------------------------------------
	/// <br />
	/// </summary>
    internal class RegistryAccess
	{
        /// <summary>
        /// 
        /// </summary>
		public RegistryAccess()
		{
			
		}

		/// <summary>
		/// getRegistryKeyValue(string) allows the specification of the value which allows the 
		/// access to multiple application parameter strings.
		/// <br />
		/// Note:  This is a static method so it can be used without the instantiation of
		/// the class itself.
		/// <br />
		/// Usage: RegistryAccess.getRegistryKeyValue("storeConnectionString");
		/// <br />
		/// This would return the connection string to the application.
		/// </summary>
        /// <param name="siteUrl">
        /// 
        /// </param>
		/// <param name="regKey">
		/// This is the key name in the application's registry section.
		/// </param>
		/// <returns>Registry key value to be used for application parameters</returns>
		public static string getRegistryKeyValue(string siteUrl, string regKey)
		{
            RegistryKey reg = Registry.LocalMachine;
			string keyString = System.Configuration.ConfigurationManager.AppSettings["registrySection"] + siteUrl;
			//Open up the sub key of HKEY_LOCAL_MACHINE
			RegistryKey subKey = reg.OpenSubKey(keyString);
			//Get the value from the registry key  
			//	NOTE: Registry values can be string, binary, DWORD, multi-string
			//	, or Expandable String Value assuming string value for the connection strings
		    string conString = (string)subKey.GetValue(regKey);

			//Return the string from the regKey registry value
            subKey.Close();
            reg.Close();

			return conString;
		}
	}
}
