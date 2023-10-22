using Microsoft.Extensions.Configuration;

namespace ConfigurationManager
{
    public class FileConfigurationProvider : ReflectionUI.IConfigurationProvider
    {
        private readonly IConfigurationRoot configuration;

        public FileConfigurationProvider()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("customconfig.json")
                        .Build();
        }

        public object GetSetting(string key, Type type)
        {
            Console.WriteLine("File provider GET");
            return configuration[key];
        }

        public void SetSetting(string key, string value, Type type)
        {
            Console.WriteLine("File provider SET");
        }
    }
}