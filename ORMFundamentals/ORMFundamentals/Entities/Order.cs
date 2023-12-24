using System.ComponentModel.DataAnnotations.Schema;

namespace ORMFundamentals.Entities;

public class Order
{
    public int Id { get; set; }

    public string Status { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public override string? ToString()
    {
        return $"ID: {Id}, Status: {Status}, CreatedDate: {CreatedDate}, UpdatedDate: {UpdatedDate},\n\t Product: {Product}";
    }
}