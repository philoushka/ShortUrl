using System.Configuration;
namespace ShortUrl.Models
{
    public class AuthProviders : ConfigurationSection
    {
        [ConfigurationProperty("providers")]
        public ProviderKeyCollection Providers
        {
            get { return (ProviderKeyCollection)this["providers"]; }
        }
    }
     
    public class ProviderKeyCollection : KeyValueConfigurationCollection
    {
        public new AuthProvider this[string provider]
        {
            get { return BaseGet(provider) as AuthProvider; }
            set
            {
                if (BaseGet(provider) != null)
                {
                    int index = BaseIndexOf(BaseGet(provider));
                    BaseRemoveAt(index);
                }
                BaseAdd(0, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AuthProvider();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AuthProvider)element).ProviderName;
        }
    }
 
    public class AuthProvider: ConfigurationElement
    {
        [ConfigurationProperty("key", IsKey= true,IsRequired = true)]
        public string Key
        {
            get { return (string)this["key"]; }        
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string ProviderName
        {
            get { return (string)this["name"]; }        
        }

        [ConfigurationProperty("secret", IsRequired = true)]
        public string Secret
        {
            get { return (string)this["secret"]; }        
        }      
    }     
}