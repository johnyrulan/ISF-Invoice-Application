//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ISFInvoiceApplication.Infrastructure.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class IAInvoice
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public System.DateTime OrderTime { get; set; }
        public int UserAccountId { get; set; }
        public System.DateTime Updated { get; set; }
    
        public virtual IAUserAccount IAUserAccount { get; set; }
    }
}
