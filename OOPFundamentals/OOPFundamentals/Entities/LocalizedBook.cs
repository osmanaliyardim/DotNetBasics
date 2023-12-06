using System.Text.Json.Serialization;

namespace OOPFundamentals.Entities;

public class LocalizedBook : Book
{
    [JsonPropertyName("localPublisher")]
    public string LocalPublisher { get; set; }

    [JsonPropertyName("countryOfLocalization")]
    public string CountryOfLocalization { get; set; }

    [JsonIgnore]
    public override Type _documentType => typeof(LocalizedBook);

    public LocalizedBook(
        string title,
        List<string> authors,
        DateTime datePublished,
        int numberOfPages,
        string publisher,
        string localPublisher,
        string countryOfLocalization
        ) : base(title, authors, datePublished, numberOfPages, publisher)
    {
        LocalPublisher = localPublisher;
        CountryOfLocalization = countryOfLocalization;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nLocal Publisher: {LocalPublisher}\nCountry Of Localization: {CountryOfLocalization}";
    }
}