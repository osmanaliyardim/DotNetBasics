using System.Configuration;

namespace ConfigurationManager
{
    public class ConfigurationManagerConfigurationProvider : ReflectionUI.IConfigurationProvider
    {
        public ConfigurationManagerConfigurationProvider()
        {
        }

        public object GetSetting(string key, Type type)
        {
            try
            {
                var appSettings = System.Configuration.ConfigurationManager.AppSettings;
                string result = appSettings[key] ?? "Not Found";

                return result;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error when reading app settings");

                return null;
            }
        }

        public void SetSetting(string key, string value, Type type)
        {
            try
            {
                var configFile = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;

                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }

                configFile.Save(ConfigurationSaveMode.Full);
                System.Configuration.ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error when writing app settings");
            }
        }
    }
}