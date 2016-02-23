using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISFInvoiceApplication.Core.ValueObjects;

namespace ISFInvoiceApplication.Core.Entities
{
    public class Invoice : Entity<int>
    {
        public int UserAccountId { get; private set; }
        public OrderDetails OrderDetails { get; set; }
        public DateTime OrderTime { get; private set; }
        public TrackingState State { get; set; }

        public Invoice(int id, 
                       int userAccountId,
                       OrderDetails orderDetails,
                       DateTime orderTime)
            : base(id)
        {
            UserAccountId = userAccountId;
            OrderDetails = orderDetails;
            OrderTime = orderTime;
            State = TrackingState.Unchanged;
        }

        public int GetOrderAmount()
        {
            return (OrderDetails.Price * OrderDetails.Quantity);
        }
    }
}
