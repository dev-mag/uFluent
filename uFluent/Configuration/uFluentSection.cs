using System.Configuration;

namespace uFluent.Configuration
{
    public class uFluentSection : ConfigurationSection
    {
        [ConfigurationProperty("enableMigrations", IsRequired = true)]
        public bool EnableMigrations
        {
            get { return (bool)this["enableMigrations"]; }
            set { this["enableMigrations"] = value; }
        }
    }
}