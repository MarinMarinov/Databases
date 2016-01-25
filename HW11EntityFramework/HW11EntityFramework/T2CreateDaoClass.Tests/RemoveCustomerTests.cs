using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T1CreateDbContextForNorthwind;

namespace T2CreateDaoClass.Tests
{
    [TestClass]
    public class RemoveCustomerTests
    {
        [TestMethod]
        public void DeletingCustomerShouldWorkFine()
        {
            string customerID = "ATA";

            DaoClass.AddCustomer(customerID, "Ata company", "1", "2", "3", "4", "5", "6", "7", "8", "9");

            DaoClass.RemoveCustomer(customerID);

            using (var db = new NorthwindEntities())
            {
                var deletedCustomer = db.Customers.FirstOrDefault(c => c.CustomerID == customerID);

                Assert.AreEqual(deletedCustomer, null);
            }
        }
    }
}
