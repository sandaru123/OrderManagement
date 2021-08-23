using System;
using System.Collections.Generic;

namespace OrderManagement.DAL
{
    public partial class Order
    {
        public Order()
        {
            ItemOrder = new HashSet<ItemOrder>();
        }

        public int OrderId { get; set; }
        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string ReferNo { get; set; }
        public string Note { get; set; }
        public decimal TotExcl { get; set; }
        public decimal TotTax { get; set; }
        public decimal TotIncl { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ItemOrder> ItemOrder { get; set; }
    }
}
