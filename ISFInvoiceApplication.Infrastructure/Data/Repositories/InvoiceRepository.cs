using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using ISFInvoiceApplication.Core;
using ISFInvoiceApplication.Core.Entities;
using ISFInvoiceApplication.Core.ValueObjects;
using ISFInvoiceApplication.Infrastructure.Data.Interfaces;

namespace ISFInvoiceApplication.Infrastructure.Data.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceApplicationEntities _invoiceApplicationEntites;

        public InvoiceRepository(InvoiceApplicationEntities invoiceApplicationEntities)
        {
            _invoiceApplicationEntites = invoiceApplicationEntities;
        }

        public Invoice GetInvoice(int id)
        {
            var iaInvoice = _invoiceApplicationEntites.IAInvoices
                                                      .FirstOrDefault(i => i.Id == id);

            if (iaInvoice != null)
            {
                return new Invoice(iaInvoice.Id, iaInvoice.UserAccountId,
                                   new OrderDetails(iaInvoice.ProductName,
                                   iaInvoice.Quantity, iaInvoice.Price),
                                   iaInvoice.OrderTime);
            }

            return null;
        }

        public IEnumerable<Invoice> GetInvoicesByUserAccount(int userAccountId)
        {
            return (from i in _invoiceApplicationEntites.IAInvoices
                    where i.UserAccountId == userAccountId
                    select new Invoice(i.Id, i.UserAccountId, 
                               new OrderDetails(i.ProductName, i.Quantity, i.Price),
                               i.OrderTime)).AsEnumerable();
        }

        public void SaveInvoice(Invoice invoice, int userAccountId)
        {
            if (invoice.State == TrackingState.Added)
            {
                _invoiceApplicationEntites.IAInvoices.Add(new IAInvoice
                {
                    UserAccountId = userAccountId,
                    ProductName = invoice.OrderDetails.ProductName,
                    Quantity = invoice.OrderDetails.Quantity,
                    Price = invoice.OrderDetails.Price,
                    OrderTime = invoice.OrderTime
                });
            }

            var iaInvoice = _invoiceApplicationEntites.IAInvoices
                                                      .FirstOrDefault(u => u.Id == userAccountId);

            if (invoice.State == TrackingState.Modified && iaInvoice != null)
            {
                iaInvoice.ProductName = invoice.OrderDetails.ProductName;
                iaInvoice.Quantity = invoice.OrderDetails.Quantity;
                iaInvoice.Price = invoice.OrderDetails.Price;
                iaInvoice.OrderTime = invoice.OrderTime;
                iaInvoice.Updated = DateTime.Now;

                _invoiceApplicationEntites.Entry(iaInvoice).State = EntityState.Modified;
            }

            if (invoice.State == TrackingState.Deleted)
            {
                _invoiceApplicationEntites.Entry(iaInvoice).State = EntityState.Deleted;
            }
        }
    }
}
