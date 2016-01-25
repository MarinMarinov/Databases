namespace T2CreateDaoClass
{
    using System.Linq;
    using T1CreateDbContextForNorthwind;

    public class DaoClass
    {
        public static void Main()
        {
            //var db = new NorthwindEntities();
            //var categories = db.Categories.Select(c => c.CategoryName).ToList();
            //Console.WriteLine(string.Join(Environment.NewLine, categories));

            //AddCustomer("ALFA", "Alfa Commerce");
            //AddCustomer("BETA", "Beta Commerce", "Ivan Peshov", "Manager", "Mladost 3", "Sofia", null, "1000", "Bulgaria");

            //RemoveCustomer("ALFA");

            //ModifyCustomer("BETA", null, "Petar Grigorov", "The New Manager", null, null, null, null, null, "0888123456", null);
        }

        public static void AddCustomer(
            string customerId,
            string companyName,
            string contactName = null,
            string contactTitle = null,
            string address = null,
            string city = null,
            string region = null,
            string postalCode = null,
            string country = null,
            string phone = null,
            string fax = null)
        {
            var db = new NorthwindEntities();

            using (db)
            {
                db.Customers.Add(new Customer
                {
                    CustomerID = customerId,
                    CompanyName = companyName,
                    ContactName = contactName,
                    ContactTitle = contactTitle,
                    Address = address,
                    City = city,
                    Region = region,
                    PostalCode = postalCode,
                    Country = country,
                    Phone = phone,
                    Fax = fax
                });

                db.SaveChanges();
            }
        }

        public static void RemoveCustomer(string customerId)
        {
            var db = new NorthwindEntities();

            using (db)
            {
                var customerToRemove = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);

                db.Customers.Remove(customerToRemove);

                db.SaveChanges();
            }
        }

        public static void ModifyCustomer(
            string customerId,
            string companyName,
            string contactName,
            string contactTitle,
            string address,
            string city,
            string region,
            string postalCode,
            string country,
            string phone,
            string fax)
        {
            var db = new NorthwindEntities();

            using (db)
            {
                Customer customerToModify = db.Customers.FirstOrDefault(c => c.CustomerID == customerId);

                customerToModify.CompanyName = companyName ?? customerToModify.CompanyName;
                customerToModify.ContactName = contactName ?? customerToModify.ContactName;
                customerToModify.ContactTitle = contactTitle ?? customerToModify.ContactTitle;
                customerToModify.Address = address ?? customerToModify.Address;
                customerToModify.City = city ?? customerToModify.City;
                customerToModify.Region = region ?? customerToModify.Region;
                customerToModify.PostalCode = postalCode ?? customerToModify.PostalCode;
                customerToModify.Country = country ?? customerToModify.Country;
                customerToModify.Phone = phone ?? customerToModify.Phone;
                customerToModify.Fax = fax ?? customerToModify.Fax;

                db.SaveChanges();
            }
        }
    }
}
