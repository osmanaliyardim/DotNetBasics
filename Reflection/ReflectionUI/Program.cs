using ReflectionUI;

Settings settings = new();
settings.LoadSettings();

Console.WriteLine(settings.Author);
Console.WriteLine(settings.HomeTask);
Console.WriteLine(settings.Task);

//settings.HomeTask = "changed";
//settings.Task = "test";
//settings.SaveSettings();

Console.ReadLine();

class Settings : ConfigurationComponentBase
{
    [ConfigurationManagerConfigurationItem("Author")]
    public string Author { get; set; }

    [ConfigurationManagerConfigurationItem("HomeTask")]
    public string HomeTask { get; set; }

    [FileConfigurationItem("Task")]
    public string Task { get; set; } = "test";
}