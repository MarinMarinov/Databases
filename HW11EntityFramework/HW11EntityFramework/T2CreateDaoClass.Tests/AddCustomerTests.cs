using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using T1CreateDbContextForNorthwind;

namespace T2CreateDaoClass.Tests
{
    [TestClass]
    public class AddCustomerTests
    {
        [TestMethod]
        [ExpectedException(typeof(System.Data.Entity.Validation.DbEntityValidationException))]
        public void AddingNullCompanyNameCustomerShouldThrowException()
        {
            DaoClass.AddCustomer(null, null);
        }

        [TestMethod]
        public void AddingCustomerShouldWorkFine()
        {
            string customerID = "ALA";

            DaoClass.AddCustomer(customerID, "Ala company", "1", "2", "3", "4", "5", "6", "7", "8", "9");

            using (var db = new NorthwindEntities())
            {
                var addedCustomer = db.Customers.FirstOrDefault(c => c.CustomerID == customerID);
                var addedCustomerName = addedCustomer.CompanyName;

                Assert.AreEqual("Ala company", addedCustomerName);

                DaoClass.RemoveCustomer(customerID);
            }
        }
    }
}
