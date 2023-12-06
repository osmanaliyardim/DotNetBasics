using System.Text.Json;
using System.Text.Json.Serialization;
using OOPFundamentals.Entities;

namespace OOPFundamentals;

public class DocumentFactory : JsonConverter<Document>
{

    public override Document? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader);
        JsonElement jsonObject = jsonDocument.RootElement;

        if (!jsonObject.TryGetProperty("DocumentType", out JsonElement typeProperty))
        {
            throw new InvalidOperationException("Invalid type value in JSON.");
        }

        Document document;

        switch (typeProperty.GetString())
        {
            case "Patent":
                document = JsonSerializer.Deserialize<Patent>(jsonObject.GetRawText(), options);
                break;
            case "Book":
                document = JsonSerializer.Deserialize<Book>(jsonObject.GetRawText(), options);
                break;
            case "LocalizedBook":
                document = JsonSerializer.Deserialize<LocalizedBook>(jsonObject.GetRawText(), options);
                break;
            default:
                throw new InvalidOperationException("Invalid type value in JSON.");
        }

        return document;
    }

    public override void Write(Utf8JsonWriter writer, Document value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, options);
        throw new NotImplementedException();
    }
}