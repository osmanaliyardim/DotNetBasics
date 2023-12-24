using Microsoft.EntityFrameworkCore;
using ORMFundamentals.Entities;

namespace ORMFundamentals.Repos;

public class OrderRepository
{
    public void CreateOrder(Order order)
    {
        order.UpdatedDate = DateTime.Now;
        using (var db = new EFDbContext())
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }
    }

    public void UpdateOrder(Order order)
    {
        order.UpdatedDate = DateTime.Now;

        using (var db = new EFDbContext())
        {
            db.Orders.Update(order);
            db.SaveChanges();
        }
    }

    public void DeleteOrder(Order order)
    {
        using (var db = new EFDbContext())
        {
            db.Orders.Remove(order);
            db.SaveChanges();
        }
    }

    public Order? GetOrderByID(int id)
    {
        using (var db = new EFDbContext())
        {
            Order order = db.Orders.Single(x => x.Id == id);
            db.Entry(order).Reference(r => r.Product).Load();

            return order;
        }
    }

    public List<Order> GetOrdersByFilter(int? month = null, int? year = null, string status = null, int? productId = null)
    {
        using (var db = new EFDbContext())
        {
            var orders = db.Orders.FromSqlRaw($"GetOrdersByFilter {month}, {year}, {status}, {productId}").ToList();
            return orders;
        }
    }

    public void BulkDeleteOrders(int? month = null, int? year = null, string status = null, int? productId = null)
    {
        List<Order> ordersToDelete = GetOrdersByFilter(month, year, status, productId);

        if (ordersToDelete != null || ordersToDelete.Count > 0)
        {
            using (var db = new EFDbContext())
            {
                db.Orders.RemoveRange(ordersToDelete);
                db.SaveChanges();
            }
        }
    }
}
