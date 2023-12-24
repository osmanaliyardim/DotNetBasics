using ORMFundamentals.Entities;

namespace ORMFundamentals.Repos;

public class ProductRepository
{
    public void CreateProduct(Product product)
    {
        using (var db = new EFDbContext())
        {
            db.Products.Add(product);
            db.SaveChanges();
        }
    }

    public void UpdateProduct(Product product)
    {
        using (var db = new EFDbContext())
        {
            db.Products.Update(product);
            db.SaveChanges();
        }
    }

    public void DeleteProduct(Product product)
    {
        using (var db = new EFDbContext())
        {
            db.Products.Remove(product);
            db.SaveChanges();
        }
    }

    public Product? GetProductById(int id)
    {
        using (var db = new EFDbContext())
        {
            return db.Products.Single(x => x.Id == id);
        }
    }

    public List<Product> GetAllProducts()
    {
        using (var db = new EFDbContext())
        {
            return db.Products.ToList();
        }
    }
}