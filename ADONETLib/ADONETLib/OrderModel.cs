namespace ADONETLib;

public class OrderModel
{
    public int Id { get; set; }

    public string Status { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public ProductModel Product { get; set; }

    public override string? ToString()
    {
        return $"ID: {Id}, Status: {Status}, CreatedDate: {CreatedDate}, UpdatedDate: {UpdatedDate},\n\t Product: {Product}";
    }
}