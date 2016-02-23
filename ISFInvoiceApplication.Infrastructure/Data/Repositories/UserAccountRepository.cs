using System;
using System.Data.Entity;
using System.Linq;
using ISFInvoiceApplication.Core;
using ISFInvoiceApplication.Core.Entities;
using ISFInvoiceApplication.Core.ValueObjects;
using ISFInvoiceApplication.Infrastructure.Data.Interfaces;

namespace ISFInvoiceApplication.Infrastructure.Data.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly InvoiceApplicationEntities _invoiceApplicationEntites;
        private readonly IInvoiceRepository _invoiceRepository;

        public UserAccountRepository(InvoiceApplicationEntities invoiceApplicationEntities)
        {
            _invoiceApplicationEntites = invoiceApplicationEntities;
            _invoiceRepository = new InvoiceRepository(invoiceApplicationEntities);
        }

        public UserAccount GetUserAccount(string username, string password, bool getInvoices)
        {
            var iaUserAccount =_invoiceApplicationEntites.IAUserAccounts
                                                         .FirstOrDefault(u => u.Username == username && u.Password == password);

            if (iaUserAccount != null)
            {
                if (getInvoices)
                {
                    return new UserAccount(iaUserAccount.Id,
                                           iaUserAccount.Username,
                                           iaUserAccount.Password,
                                           iaUserAccount.Email,
                                           iaUserAccount.InvoiceTotal,
                                           new InvoiceLimit(iaUserAccount.WarningLimit, iaUserAccount.ErrorLimit),
                                           _invoiceRepository.GetInvoicesByUserAccount(iaUserAccount.Id));
                }

                    return new UserAccount(iaUserAccount.Id, 
                                           iaUserAccount.Username, 
                                           iaUserAccount.Password, 
                                           iaUserAccount.Email, 
                                           iaUserAccount.InvoiceTotal, 
                                           new InvoiceLimit(iaUserAccount.WarningLimit, iaUserAccount.ErrorLimit));
            }

            return null;
        }

        public void SaveUserAccount(UserAccount userAccount)
        {
            if (userAccount.State == TrackingState.Added)
            {
                _invoiceApplicationEntites.IAUserAccounts.Add(new IAUserAccount
                {
                    Username = userAccount.Username,
                    Password = userAccount.Password,
                    Email = userAccount.Email,
                    WarningLimit = userAccount.InvoiceLimit.WarningLimit,
                    ErrorLimit = userAccount.InvoiceLimit.ErrorLimit,
                    InvoiceTotal = 0,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                });
            }
            
            var iaUserAccount = _invoiceApplicationEntites.IAUserAccounts
                                                          .FirstOrDefault(u => u.Id == userAccount.Id);

            if (userAccount.State == TrackingState.Modified 
                && iaUserAccount != null)
            {               
                iaUserAccount.Password = userAccount.Password;
                iaUserAccount.Email = userAccount.Email;
                iaUserAccount.WarningLimit = userAccount.InvoiceLimit.WarningLimit;
                iaUserAccount.ErrorLimit = userAccount.InvoiceLimit.ErrorLimit;
                iaUserAccount.InvoiceTotal = userAccount.InvoiceTotal;

                _invoiceApplicationEntites.Entry(iaUserAccount).State = EntityState.Modified;                                              
            }

            _invoiceApplicationEntites.SaveChanges();
        }

        public void SaveInvoices(UserAccount userAccount)
        {
            foreach (var invoice in userAccount.Invoices)
            {
               _invoiceRepository.SaveInvoice(invoice, userAccount.Id);
            }

            _invoiceApplicationEntites.SaveChanges();
        }
    }
}
