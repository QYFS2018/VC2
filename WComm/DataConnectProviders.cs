using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Configuration.Provider;
using System.Web.Configuration;

namespace WComm
{
    internal class Connector
    {
        private string _connectionString;
        private string _registryKeyValue;
        private string _providerType;


        public string ConnectionString
        {
            set
            {
                _connectionString = value;
            }
            get
            {
                return _connectionString;
            }
        }
        public string RegistryKeyValue
        {
            set
            {
                _registryKeyValue = value;
            }
            get
            {
                return _registryKeyValue;
            }
        }
        public string ProviderType
        {
            set
            {
                _providerType = value;
            }
            get
            {
                return _providerType;
            }
        }

        public Connector(string dataConnectProviders)
        {
            DataConnectProviders _providersSection = (ConfigurationManager.GetSection("DataConnectProviders")) as DataConnectProviders;
            if (_providersSection == null)
            {
                throw new InvalidOperationException("Items configuration section could not be found");
            }
            ProviderSettings _providerSettings;
            if (String.IsNullOrEmpty(dataConnectProviders) == true)
            {
                 _providerSettings = _providersSection.Providers["Default"];
            }
            else
            {
                 _providerSettings = _providersSection.Providers[dataConnectProviders];
            }
            
            if (_providerSettings == null)
            {
                throw new InvalidOperationException("Items configuration section could not be found:" + dataConnectProviders);
            }

            this._registryKeyValue  = _providerSettings.Parameters["RegistryKeyValueProperty"].ToString();
            this._connectionString = _providerSettings.Parameters["ConnectionStringProperty"].ToString();
            this._providerType = _providerSettings.Parameters["ProviderType"].ToString();

            if (this._registryKeyValue.Trim().ToUpper() == "URL")
            {
                this._registryKeyValue = WComm.Utilities.urlPath;
            }

            if (this._registryKeyValue.Trim().ToUpper() == "URLFROMCOOKIE")
            {
                if (String.IsNullOrEmpty(ConInfo.Url) == true)
                {
                    System.Web.HttpContext.Current.Response.Redirect(System.Web.Configuration.WebConfigurationManager.AppSettings["ErrorUrl"].ToString());
                    //throw new InvalidOperationException("Can't get Url from cookie");
                }
                this._registryKeyValue = ConInfo.Url;
            }
            if (this._registryKeyValue.Trim().ToUpper() == "URLFROMVAR")
            {
                if (string.IsNullOrEmpty (ConInfo.Url)  == true)
                {
                    throw new InvalidOperationException("Can't get Url from Var");
                }
                this._registryKeyValue = ConInfo.Url;
            }
            if (this._registryKeyValue.Trim().ToUpper() == "CONNECTIONVAR")
            {
                if (string.IsNullOrEmpty(ConInfo.ConnectionString) == true)
                {
                    throw new InvalidOperationException("Can't get connection string from Var");
                }
                //this._registryKeyValue = ConInfo.Url;
                this._connectionString = ConInfo.ConnectionString;
            }
        }
    }

    internal class Provider : ProviderBase
    {
    }


    internal class DataConnectProviders : ConfigurationSection
    {

        [ConfigurationProperty("Providers")]
        [ConfigurationValidatorAttribute(typeof(ProviderSettingsValidation))]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)this["Providers"]; }

        }
    }
    internal class ProviderSettingsValidation : ConfigurationValidatorBase
    {

        public override bool CanValidate(Type type)
        {
            return type == typeof(ProviderSettingsCollection);
        }

        public override void Validate(object value)
        {
            ProviderSettingsCollection providerCollection = value as ProviderSettingsCollection;
            if (providerCollection != null)
            {
                foreach (ProviderSettings _provider in providerCollection)
                {
                    if (String.IsNullOrEmpty(_provider.Type))
                    {
                        throw new ConfigurationErrorsException("Type was not defined in the provider");
                    }

                    Type dataAccessType = Type.GetType(_provider.Type);
                    if (dataAccessType == null)
                    {
                        throw (new InvalidOperationException("Provider's Type could not be found"));
                    }
                }
            }
        }
    }
    
}
