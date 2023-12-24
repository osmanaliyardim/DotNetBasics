using Dapper;
using Microsoft.Extensions.Configuration;
using ORMFundamentalsDapper.Entities;
using System.Data.SqlClient;

namespace ORMFundamentalsDapper.Repos;

public class ProductRepository
{
    private readonly IConfiguration _config;

    public ProductRepository(IConfiguration config)
    {
        _config = config;
    }

    public void CreateProduct(Product product)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Execute("insert into products (name, description, weight, height, width, length) values (@Name, @Description, @Weight, @Height, @Width, @Length)", product);
        }
    }

    public void UpdateProduct(Product product)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Execute("update products set name = @Name, description = @Description, weight = @Weight, height = @Height, width = @Width, length = @Length where id = @Id", product);
        }
    }

    public void DeleteProduct(int id)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Execute("delete from products where id = @Id", new { Id = id });
        }
    }

    public Product? GetProductById(int id)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            return connection.QueryFirst<Product>("select * from products where id = @Id", new { Id = id });
        }
    }

    public List<Product> GetAllProducts()
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            return connection.Query<Product>("select * from products").ToList();
        }
    }
}