using Dapper;
using Microsoft.Extensions.Configuration;
using ORMFundamentalsDapper.Entities;
using System.Data.SqlClient;

namespace ORMFundamentalsDapper.Repos;

public class OrderRepository
{
    private readonly IConfiguration _config;

    public OrderRepository(IConfiguration config)
    {
        _config = config;
    }

    public void CreateOrder(Order order)
    {
        order.UpdatedDate = DateTime.Now;

        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Execute("insert into orders (status, createddate, updateddate, productid) values (@Status, @CreatedDate, @UpdatedDate, @ProductId)", order);
        }

    }

    public void UpdateOrder(Order order)
    {
        order.UpdatedDate = DateTime.Now;

        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Execute("update orders set status = @Status, productid = @ProductId where id = @Id", order);
        }
    }

    public void DeleteOrder(int id)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            connection.Execute("delete from orders where id = @Id", new { Id = id });
        }
    }

    public Order? GetOrderByID(int id)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            var order = connection.Query<Order, Product, Order>($"select * from orders as a inner join products as b on a.ProductId = b.Id where a.id = @Id",
                (order, product) =>
                {
                    order.Product = product;
                    return order;
                },
                splitOn: "Id",
                param: new { Id = id }
                )
                .Distinct().FirstOrDefault();
            return order;
        }
    }

    public List<Order> GetOrdersByFilter(int? month = null, int? year = null, string status = null, int? productId = null)
    {
        using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
        {
            var parameters = new { Month = month, Year = year, Status = status, ProductId = productId };
            var orders = connection.Query<Order, Product, Order>("GetOrdersByFilter",
                (order, product) =>
                {
                    order.Product = product;
                    return order;
                },
                splitOn: "Id",
                param: parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
            return orders;
        }
    }

    public void BulkDeleteOrders(int? month = null, int? year = null, string status = null, int? productId = null)
    {
        List<Order> ordersToDelete = GetOrdersByFilter(month, year, status, productId);

        if (ordersToDelete != null || ordersToDelete.Count > 0)
        {
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                connection.Execute("delete from orders where id in @ids", new { ids = ordersToDelete.Select(x => x.Id) });
            }
        }
    }
}