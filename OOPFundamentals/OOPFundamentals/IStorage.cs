using OOPFundamentals.Entities;

namespace OOPFundamentals;

public interface IStorage
{
    public Document Read(string fileName);

    public void Write(Document document);

    public List<Document> Search(int documentNumber);
}