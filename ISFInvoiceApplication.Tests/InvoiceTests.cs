using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISFInvoiceApplication.Core.Entities;
using ISFInvoiceApplication.Core.ValueObjects;
using NUnit.Framework;

namespace ISFInvoiceApplication.Tests
{
    [TestFixture]
    public class InvoiceTests
    {
        // Add Invoice
        [Test]
        public void Add_Invoice()
        {
            var userAccount = new UserAccount(0, "testUser", "password", "userAccount@sample.com", 100, new InvoiceLimit(200, 500));
            var result = userAccount.AddInvoice(new Invoice(0, 0, new OrderDetails("Sample Product", 20, 5), DateTime.Now));

            Assert.AreEqual(userAccount.Invoices.Count, 1);
            Assert.AreEqual(result.Item3, true);
            Assert.AreEqual(result.Item2, "Invoice addition successful.");
        }

        // Add Crossing Warning Limit
        [Test]
        public void Add_Invoice_That_Crosses_Warning_Limit()
        {
            var userAccount = new UserAccount(0, "testUser", "password", "userAccount@sample.com", 100, new InvoiceLimit(200, 500));
            var result = userAccount.AddInvoice(new Invoice(0, 0, new OrderDetails("Sample Product", 20, 11), DateTime.Now));

            Assert.AreEqual(userAccount.Invoices.Count, 1);
            Assert.AreEqual(result.Item3, true);
            Assert.AreEqual(result.Item2, "The warning limit has crossed. Be careful when adding more invoices.");
        }

        // Add Crossing Error Limit
        [Test]
        public void Add_Invoice_That_Crosses_Error_Limit()
        {
            var userAccount = new UserAccount(0, "testUser", "password", "userAccount@sample.com", 100, new InvoiceLimit(200, 500));
            var result = userAccount.AddInvoice(new Invoice(0, 0, new OrderDetails("Sample Product", 20, 50), DateTime.Now));

            Assert.AreEqual(userAccount.Invoices.Count, 0);
            Assert.AreEqual(result.Item3, false);
            Assert.AreEqual(result.Item2, "The error limit has crossed. Invoice not added.");
        }

        // Update Invoice
        [Test]
        public void Update_Invoice()
        {
            var userAccount = new UserAccount(0, "testUser", "password", "userAccount@sample.com", 100, new InvoiceLimit(200, 500));
            userAccount.AddInvoice(new Invoice(0, 0, new OrderDetails("Sample Product", 20, 5), DateTime.Now));
            userAccount.UpdateInvoice(new Invoice(0, 0, new OrderDetails("Sample Product", 20, 7), DateTime.Now));

            Assert.AreEqual(userAccount.Invoices.FirstOrDefault().GetOrderAmount(), 140);
        }

        // Delete Invoice
        [Test]
        public void Delete_Invoice()
        {
            var userAccount = new UserAccount(0, "testUser", "password", "userAccount@sample.com", 100, new InvoiceLimit(200, 500));
            userAccount.AddInvoice(new Invoice(0, 0, new OrderDetails("Sample Product", 20, 5), DateTime.Now));
            var result = userAccount.DeleteInvoice(new Invoice(0, 0, new OrderDetails("Sample Product", 20, 5), DateTime.Now));

            Assert.AreEqual(result, true);
        }
    }
}
