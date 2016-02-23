using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISFInvoiceApplication.Core.ValueObjects
{
    public class InvoiceLimit
    {
        public int WarningLimit { get; set; }
        public int ErrorLimit { get; set; }

        public InvoiceLimit(int warningLimit, int errorLimit)
        {
            WarningLimit = warningLimit;
            ErrorLimit = errorLimit;
        }
    }
}
