using System.Data;
using System.Data.SqlClient;

namespace ADONETLib;

public class ProductRepository
{
    // SqlDataReader
    public List<ProductModel> GetProducts()
    {
        List<ProductModel> products = new();

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand("SELECT * FROM Product", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    products.Add(new ProductModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Weight = reader.GetDouble(3),
                        Height = reader.GetDouble(4),
                        Width = reader.GetDouble(5),
                        Length = reader.GetDouble(6)
                    });
                }
            }
        }

        return products;
    }

    // SqlDataAdapter and DataTable
    public DataTable GetAllProducts()
    {
        DataTable products = new();

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        using (SqlDataAdapter adapter = new("SELECT * FROM Product", connection))
        {
            adapter.Fill(products);
        }

        return products;
    }

    public int CreateProduct(ProductModel product)
    {
        int id = 0;

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            string queryString = "INSERT INTO Product " +
                                    "(Name, Description, Weight, Height, Width, Length) Values(@Name, @Description, @Weight, @Height, @Width, @Length);"
                                    + "SELECT CAST(scope_identity() AS int)";
            SqlCommand command = new SqlCommand(queryString, connection);

            SqlParameter sqlParameter = command.Parameters.Add("@Name", SqlDbType.NVarChar, 255);
            sqlParameter.Value = product.Name;

            sqlParameter = command.Parameters.Add("@Description", SqlDbType.NVarChar, 255);
            sqlParameter.Value = product.Description;

            sqlParameter = command.Parameters.Add("@Weight", SqlDbType.Float);
            sqlParameter.Value = product.Weight;

            sqlParameter = command.Parameters.Add("@Height", SqlDbType.Float);
            sqlParameter.Value = product.Height;

            sqlParameter = command.Parameters.Add("@Width", SqlDbType.Float);
            sqlParameter.Value = product.Width;

            sqlParameter = command.Parameters.Add("@Length", SqlDbType.Float);
            sqlParameter.Value = product.Length;

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

    public void UpdateProduct(ProductModel product)
    {
        string queryString = "Update Product SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length " +
                                "Where Id = @Id";

        using (var connection = new SqlConnection(DBConfig.ConnectionString))
        {
            SqlCommand command = new SqlCommand(queryString, connection);

            SqlParameter sqlParameter = command.Parameters.Add("@Name", SqlDbType.NVarChar, 255);
            sqlParameter.Value = product.Name;

            sqlParameter = command.Parameters.Add("@Description", SqlDbType.NVarChar, 255);
            sqlParameter.Value = product.Description;

            sqlParameter = command.Parameters.Add("@Weight", SqlDbType.Float);
            sqlParameter.Value = product.Weight;

            sqlParameter = command.Parameters.Add("@Height", SqlDbType.Float);
            sqlParameter.Value = product.Height;

            sqlParameter = command.Parameters.Add("@Width", SqlDbType.Float);
            sqlParameter.Value = product.Width;

            sqlParameter = command.Parameters.Add("@Length", SqlDbType.Float);
            sqlParameter.Value = product.Length;

            sqlParameter = command.Parameters.Add("@Id", SqlDbType.Int);
            sqlParameter.Value = product.Id;

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

    public void DeleteProduct(int id)
    {
        string queryString = "DELETE FROM Product Where Id = @Id";

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

    public ProductModel GetProduct(int id)
    {
        string queryString = "SELECT * FROM Product Where Id = @Id";
        ProductModel product;

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

                    product = new ProductModel
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Weight = reader.GetDouble(3),
                        Height = reader.GetDouble(4),
                        Width = reader.GetDouble(5),
                        Length = reader.GetDouble(6)
                    };

                }
            }
            return product;
        }
    }
}