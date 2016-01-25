using System;
using System.Linq;
using T1CreateDbContextForNorthwind;

namespace T3T4T5
{
    public class Startup
    {
        public static void Main()
        {
            ExecuteTask3();

            ExecuteTask4();

            ExecuteTask5("RJ", new DateTime(1997, 1, 5), new DateTime(2000, 8, 25));
        }

        /// <summary>
        /// Write a method that finds all customers who have orders made in 1997 and shipped to Canada.
        /// </summary>
        private static void ExecuteTask3()
        {
            var db = new NorthwindEntities();

            var shippersToCanada =
                db.Customers.Where(c => c.Orders.Any(o => o.OrderDate.Value.Year == 1997 && o.ShipCountry == "Canada")).ToList();

            foreach (var shippers in shippersToCanada)
            {
                Console.WriteLine(shippers.CompanyName);
            }
        }

        /// <summary>
        /// Implement previous by using native SQL query and executing it through the DbContext.
        /// </summary>
        private static void ExecuteTask4()
        {
            var db = new NorthwindEntities();

            var query = @"SELECT DISTINCT c.CompanyName 
                            FROM Customers c
                            JOIN Orders o
                            ON c.CustomerID = o.CustomerID
                            WHERE YEAR(o.ShippedDate) = '1997' AND o.ShipCountry = 'Canada'";

            var shippersToCanada = db.Database.SqlQuery<string>(query).ToList();

            foreach (var shippers in shippersToCanada)
            {
                Console.WriteLine(shippers);
            }
        }

        /// <summary>
        /// Write a method that finds all the sales by specified region and period (start / end dates).
        /// </summary>
        private static void ExecuteTask5(string region, DateTime start, DateTime end)
        {
            var db = new NorthwindEntities();

            var salesByRegion = db
                .Orders
                .Where(o => o.ShipRegion == region && o.ShippedDate.Value > start && o.ShippedDate.Value < end)
                .ToList();

            foreach (var sales in salesByRegion)
            {
                Console.WriteLine(sales.Shipper.CompanyName);
                Console.WriteLine("Order ID" + sales.OrderID);
            }
        }



    }
}
