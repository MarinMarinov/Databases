using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using T1CreateDbContextForNorthwind;

namespace T2CreateDaoClass.Tests
{
    [TestClass]
    public class ModifyCustomerTests
    {
        [TestMethod]
        public void ModifyCustomerShouldWorkCorrectly()
        {
            string customerID = "BETA";

            using (var db = new NorthwindEntities())
            {
                if (db.Customers.Where(c => c.CustomerID == customerID).ToList().Count == 0)
                {
                    DaoClass.AddCustomer(customerID, "Beta company", "1", "2", "3", "4", "5", "6", "7", "8", "9");
                }

                DaoClass.ModifyCustomer(customerID, "Beta modified", null, null, null, null, null, null, null, null, null);

                var modifiedCustomer = db.Customers.FirstOrDefault(c => c.CustomerID == customerID);

                Assert.AreEqual("Beta modified", modifiedCustomer.CompanyName);
            }
        }
    }
}