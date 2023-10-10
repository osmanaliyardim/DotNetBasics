using System;

namespace Task1
{
    public class Product : IEquatable<Product>
    {
        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public string Name { get; set; }

        public double Price { get; set; }

        public bool Equals(Product product)
        {
            return product != null &&
                     Name == product.Name &&
                       Price == product.Price;
        }
    }
}
