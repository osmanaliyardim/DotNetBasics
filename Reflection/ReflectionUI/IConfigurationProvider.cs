namespace ReflectionUI
{
    public interface IConfigurationProvider
    {
        object GetSetting(string key, Type type);

        void SetSetting(string key, string value, Type type);
    }
}