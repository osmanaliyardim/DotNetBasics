namespace ORMFundamentals.Entities;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public double Width { get; set; }

    public double Length { get; set; }

    public ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string? ToString()
    {
        return $"ID: {Id}, Name: {Name}, Description: {Description}, Weight: {Weight}, Height: {Height}, Width: {Width}, Length: {Length}";
    }
}
