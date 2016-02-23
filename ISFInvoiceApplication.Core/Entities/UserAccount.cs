using System;
using System.Collections.Generic;
using System.Linq;
using ISFInvoiceApplication.Core.ValueObjects;

namespace ISFInvoiceApplication.Core.Entities
{
    public class UserAccount : Entity<int>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int InvoiceTotal { get; set; }
        public InvoiceLimit InvoiceLimit { get; set; }
        public TrackingState State { get; set; }
        public List<Invoice> Invoices { get; private set; } 

        public UserAccount(int id, 
                           string username,
                           string password,
                           string email,
                           int invoiceTotal,
                           InvoiceLimit invoiceLimit) 
            : base(id)
        {
            Username = username;
            Password = password;
            Email = email;
            InvoiceTotal = invoiceTotal;
            InvoiceLimit = invoiceLimit;
            Invoices = new List<Invoice>();
        }

        public UserAccount(int id,
                           string username,
                           string password,
                           string email,
                           int invoiceTotal,
                           InvoiceLimit invoiceLimit,
                           IEnumerable<Invoice> invoices)
            : base(id)
        {
            Username = username;
            Password = password;
            Email = email;
            InvoiceTotal = invoiceTotal;
            InvoiceLimit = invoiceLimit;
            Invoices = (List<Invoice>)invoices;
        }

        public Tuple<int, string, bool> AddInvoice(Invoice invoice)
        {
            int addedTotal = InvoiceTotal + invoice.GetOrderAmount();
            if (addedTotal > InvoiceLimit.ErrorLimit)
            {
                return new Tuple<int, string, bool>(invoice.Id, "The error limit has crossed. Invoice not added.", false);
            }

            string message = (addedTotal > InvoiceLimit.WarningLimit)
                ? "The warning limit has crossed. Be careful when adding more invoices."
                : "Invoice addition successful.";

            invoice.State = TrackingState.Added;
            Invoices.Add(invoice);
            return new Tuple<int, string, bool>(invoice.Id, message, true);
        }

        public bool DeleteInvoice(Invoice invoice)
        {
            var invoiceToDelete = Invoices.FirstOrDefault(i => i.Id == invoice.Id);
            if (invoiceToDelete != null)
            {
                invoiceToDelete.State = TrackingState.Deleted;
                return true;
            }

            return false;
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            var invoiceToUpdate = Invoices.FirstOrDefault(i => i.Id == invoice.Id);
            if (invoiceToUpdate != null)
            {
                invoiceToUpdate.OrderDetails = invoice.OrderDetails;
                invoiceToUpdate.State = TrackingState.Modified;
                return true;
            }

            return false;
        }
    }
}
