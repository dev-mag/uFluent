using System;
using System.Configuration;
using System.Web;
using Umbraco.Core.IO;

namespace uFluent.Configuration
{
    public static class uFluentSettings
    {
        private const string Settingfile = "uFluent.config";
        private static readonly uFluentSection Settings;

        static uFluentSettings()
        {
            try
            {
                var fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = IOHelper.MapPath(string.Format("~/config/{0}", Settingfile))
                };

                // load the settings file
                var config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);

                Settings = (uFluentSection)config.GetSection("uFluent");
            }
            catch (Exception ex)
            {
                throw new FluentException(string.Format("Error processing configuration file. {0}", ex.Message));
            }
        }

        public static bool Enabled
        {
            get { return Settings.EnableMigrations; }
        }
    }
}