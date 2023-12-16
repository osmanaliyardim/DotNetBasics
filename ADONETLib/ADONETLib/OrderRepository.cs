using System.Data.SqlClient;
using System.Data;

namespace ADONETLib;

public class OrderRepository
{
    private readonly ProductRepository _productRepository;

    public OrderRepository(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public int CreateOrder(OrderModel order)
    {
        int id = 0;

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            string queryString = "INSERT INTO \"Order\" " +
                                    "(Status, CreatedDate, UpdatedDate, ProductId) Values(@Status, @CreatedDate, @UpdatedDate, @ProductId);"
                                    + "SELECT CAST(scope_identity() AS int)";
            
            SqlCommand command = new SqlCommand(queryString, connection);

            SqlParameter sqlParameter = command.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
            sqlParameter.Value = order.Status;

            sqlParameter = command.Parameters.Add("@CreatedDate", SqlDbType.DateTime);
            sqlParameter.Value = order.CreatedDate;

            sqlParameter = command.Parameters.Add("@UpdatedDate", SqlDbType.DateTime);
            sqlParameter.Value = order.UpdatedDate;

            sqlParameter = command.Parameters.Add("@ProductId", SqlDbType.Int);
            sqlParameter.Value = order.Product.Id;

            try
            {
                connection.Open();
                id = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        return id;
    }

    public void UpdateOrder(OrderModel order)
    {
        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            string queryString = "Update \"Order\" SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductId = @ProductId " +
                                "Where Id = @Id";
            SqlCommand command = new SqlCommand(queryString, connection);

            SqlParameter sqlParameter = command.Parameters.Add("@Status", SqlDbType.NVarChar, 50);
            sqlParameter.Value = order.Status;

            sqlParameter = command.Parameters.Add("@CreatedDate", SqlDbType.DateTime);
            sqlParameter.Value = order.CreatedDate;

            sqlParameter = command.Parameters.Add("@UpdatedDate", SqlDbType.DateTime);
            sqlParameter.Value = DateTime.Now;

            sqlParameter = command.Parameters.Add("@ProductId", SqlDbType.Int);
            sqlParameter.Value = order.Product.Id;

            sqlParameter = command.Parameters.Add("@Id", SqlDbType.Int);
            sqlParameter.Value = order.Id;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void DeleteOrder(int id)
    {
        string queryString = "DELETE FROM \"Order\" Where Id = @Id";

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);

            SqlParameter sqlParameter = command.Parameters.Add("@Id", SqlDbType.Int);
            sqlParameter.Value = id;

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public OrderModel GetOrder(int id)
    {
        OrderModel order;

        string queryString = "SELECT * FROM \"Order\" Where Id = @Id";

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(queryString, connection))
            {
                SqlParameter sqlParameter = command.Parameters.Add("@Id", SqlDbType.Int);
                sqlParameter.Value = id;

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    reader.Read();

                    order = new OrderModel
                    {
                        Id = reader.GetInt32(0),
                        Status = reader.GetString(1),
                        CreatedDate = reader.GetDateTime(2),
                        UpdatedDate = reader.GetDateTime(3),
                        Product = _productRepository.GetProduct(reader.GetInt32(4))
                    };

                }
            }

            return order;
        }
    }

    public DataSet GetAllOrders()
    {
        DataSet ordersProducts = new DataSet();


        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        using (SqlDataAdapter orderAdapter = new("SELECT * FROM \"Order\"; SELECT * FROM Product", connection))
        {

            orderAdapter.TableMappings.Add("Table", "Order");
            orderAdapter.TableMappings.Add("Table1", "Product");

            orderAdapter.Fill(ordersProducts);

            DataColumn ordersProductId = ordersProducts.Tables["Order"].Columns["ProductId"];
            DataColumn productsId = ordersProducts.Tables["Product"].Columns["Id"];
            DataRelation relation = new("Product_Order", productsId, ordersProductId);
            ordersProducts.Relations.Add(relation);

        }

        return ordersProducts;
    }

    public DataTable GetOrdersByFilter(int? month = null, int? year = null, string status = null, int? productId = null)
    {
        DataTable orders = new DataTable();

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("GetOrdersByFilter", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("@Month", SqlDbType.Int).Value = (object)month ?? DBNull.Value;
                command.Parameters.Add("@Year", SqlDbType.Int).Value = (object)year ?? DBNull.Value;
                command.Parameters.Add("@Status", SqlDbType.NVarChar).Value = (object)status ?? DBNull.Value;
                command.Parameters.Add("@ProductId", SqlDbType.Int).Value = (object)productId ?? DBNull.Value;

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(orders);
                }
            }
        }

        return orders;
    }

    public DataTable DeleteOrdersByFilter(int? month = null, int? year = null, string status = null, int? productId = null, int batchSize = 7)
    {
        DataTable orders = new DataTable();
        string selectCommand = "SELECT * FROM[Order] ";

        string deleteCommand = "DELETE FROM \"Order\" " +
            "WHERE (@Month IS NULL OR MONTH(CreatedDate) = @Month) " +
            "AND (@Year IS NULL OR YEAR(CreatedDate) = @Year) " +
            "AND (@Status IS NULL OR Status = @Status) " +
            "AND (@ProductId IS NULL OR ProductId = @ProductId)";

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand, connection);

            adapter.Fill(orders);

            adapter.DeleteCommand = new SqlCommand(deleteCommand, connection);

            adapter.DeleteCommand.Parameters.Add("@Month", SqlDbType.Int).Value = (object)month ?? DBNull.Value;
            adapter.DeleteCommand.Parameters.Add("@Year", SqlDbType.Int).Value = (object)year ?? DBNull.Value;
            adapter.DeleteCommand.Parameters.Add("@Status", SqlDbType.NVarChar).Value = (object)status ?? DBNull.Value;
            adapter.DeleteCommand.Parameters.Add("@ProductId", SqlDbType.Int).Value = (object)productId ?? DBNull.Value;
            adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;

            adapter.UpdateBatchSize = batchSize;

            foreach (DataRow row in orders.Rows)
            {
                row.Delete();
            }

            adapter.Update(orders);

            adapter.Dispose();
        }

        return orders;
    }
}