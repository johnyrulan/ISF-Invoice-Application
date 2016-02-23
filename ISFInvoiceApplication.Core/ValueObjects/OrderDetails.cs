using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISFInvoiceApplication.Core.ValueObjects
{
    public class OrderDetails
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public int Price { get; private set; }

        public OrderDetails(string productName, int quantity, int price)
        {
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
}
