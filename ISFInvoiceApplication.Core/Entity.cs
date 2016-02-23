using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISFInvoiceApplication.Core
{
    public abstract class Entity<T>
    {
        public T Id { get; private set; }

        protected Entity() { } 

        protected Entity(T id)
        {
            Id = id;
        }
    }
}
