using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            return customers.Where(c => c.Orders.Count() > limit);
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if ( customers == null )
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Select(c => (c, suppliers.Where(s => s.Country == c.Country && s.City == c.City)));
        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            return customers.Select(c => (c, suppliers
                             .Where(s => s.Country == c.Country && s.City == c.City)
                              .GroupBy(s => s.Country + s.City)
                               .Select(g => g.First())));
        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            return from customer in customers
                   where customer.Orders.Any(o => o.Total > limit)
                   select customer;
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            var queryResult = customers.Where(c => c.Orders.Any());

            return queryResult.Select(c => (c, c.Orders.Min(o => o.OrderDate)));
        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var queryResult = customers.Where(c => c.Orders.Any())
                .Select(c => (customer: c,
                    dateOfEntry: c.Orders.Min(o => o.OrderDate),
                    turnover: c.Orders.Sum(o => o.Total))
                ).OrderBy(result => result.dateOfEntry.Year)
                    .ThenBy(result => result.dateOfEntry.Month)
                    .ThenByDescending(result => result.turnover)
                    .ThenBy(result => result.customer.CompanyName).Select(c => (c.customer, c.dateOfEntry));
            
            return queryResult;
        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            return from customer in customers
                   where !int.TryParse(customer.PostalCode, out _) || string.IsNullOrEmpty(customer.Region) 
                            || !customer.Phone.Contains("(") || !customer.Phone.Contains(")")
                   select customer;
        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            return products.GroupBy(p => p.Category)
                               .Select(g => new Linq7CategoryGroup
                               {
                                   Category = g.Key,
                                   UnitsInStockGroup = g
                                           .GroupBy(p => p.UnitsInStock)
                                           .Select(g2 => new Linq7UnitsInStockGroup
                                           {
                                               UnitsInStock = g2.Key,
                                               Prices = g2.Select(p => p.UnitPrice)
                                           })
                               });
        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            var queryResult = products
                .GroupBy(p => 
                    ProductRange(p, cheap, middle, expensive), resultSelector: (key, value) => 
                        new ValueTuple<decimal, IEnumerable<Product>>(key, value));

            return queryResult;
        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            throw new NotImplementedException();
        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            var queryResult = suppliers.Select(s => s.Country).Distinct().Distinct().OrderBy(a => a.Length).ThenBy(a => a);
            
            return string.Join("", queryResult);
        }

        public static decimal ProductRange(Product product, decimal cheap, decimal middle, decimal expensive)
        {
            if (product.UnitPrice <= cheap)
                return cheap;
            else if (product.UnitPrice <= middle)
                return middle;
            else 
                return expensive;
        }
    }
}