using System.Runtime.Caching;
using System.Text;
using System.Text.Json.Serialization;

namespace OOPFundamentals.Entities
{
    public abstract class Document
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonIgnore]
        static int nextID;

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("authors")]
        public List<string> Authors { get; set; }

        [JsonPropertyName("datePublished")]
        public DateTime DatePublished { get; set; }

        [JsonIgnore]
        public abstract Type _documentType { get; }

        [JsonPropertyName("documentType")]
        public string DocumentType { get => _documentType.Name; }

        [JsonIgnore]
        public virtual CacheItemPolicy CacheItemPolicy => new CacheItemPolicy();

        [JsonConstructor]
        public Document(string title, List<string> authors, DateTime datePublished)
        {
            this.Title = title;
            this.Authors = authors ?? new List<string>();
            this.DatePublished = DateTime.Now;
            this.ID = Interlocked.Increment(ref nextID);
        }

        public override string? ToString()
        {
            StringBuilder authorsString = new StringBuilder();
            authorsString.AppendLine();

            foreach (string author in Authors)
                authorsString.AppendLine("\t" + author);

            return $"Document Type:{DocumentType}\nID: {ID}\nTitle: {Title}\nDate Published: {DatePublished}\nAuthors: {authorsString}";
        }
    }
}