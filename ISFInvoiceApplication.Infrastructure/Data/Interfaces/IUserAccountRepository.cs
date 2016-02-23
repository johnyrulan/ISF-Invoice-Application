using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISFInvoiceApplication.Core.Entities;

namespace ISFInvoiceApplication.Infrastructure.Data.Interfaces
{
    interface IUserAccountRepository
    {
        UserAccount GetUserAccount(string username, string password, bool getInvoices);
        void SaveUserAccount(UserAccount userAccount);
    }
}
