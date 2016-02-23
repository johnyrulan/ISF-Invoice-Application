using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISFInvoiceApplication.Core.Entities;

namespace ISFInvoiceApplication.Infrastructure.Data.Interfaces
{
    interface IInvoiceRepository
    {
        void SaveInvoice(Invoice invoice, int userAccountId);
        IEnumerable<Invoice> GetInvoicesByUserAccount(int userAccountId);
    }
}
