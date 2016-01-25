using System;
using System.Linq;
using T1CreateDbContextForNorthwind;

namespace Task7ConcurencyChanges
{
    class Startup
    {
        /// <summary>
        /// Problem solved by only using 1 db connection
        /// </summary>
        public static void Main()
        {
            using (var db1 = new NorthwindEntities())
            {
                using (var db2 = new NorthwindEntities())
                {
                    Console.WriteLine("First connection");
                    Console.WriteLine("Original -> " + db1.Employees.First().City);
                    db1.Employees.First().City = "Orlandovci";
                    Console.WriteLine("Changed -> " + db1.Employees.First().City);

                    Console.WriteLine("Second connection");
                    Console.WriteLine("Original -> " + db2.Employees.First().City);
                    db2.Employees.First().City = "Krivina";
                    Console.WriteLine("Changed -> " + db2.Employees.First().City);

                    db1.SaveChanges();

                    db2.SaveChanges();
                }
            }

            using (var db3 = new NorthwindEntities())
            {
                Console.WriteLine("Third connection");
                Console.WriteLine(db3.Employees.First().City);
            }
        }
    }
}
