using System.Text.Json.Serialization;

namespace OOPFundamentals.Entities;

public class Patent : Document
{
    [JsonPropertyName("expirationDate")]
    public DateTime ExpirationDate { get; set; }

    [JsonIgnore]
    public override Type _documentType => typeof(Patent);

    public Patent(
        string title,
        List<string> authors,
        DateTime datePublished,
        DateTime expirationDate) : base(title, authors, datePublished)
    {
        ExpirationDate = expirationDate;
    }

    public override string ToString()
    {
        return base.ToString() + $"\nExpiration Date: {ExpirationDate}";
    }
}