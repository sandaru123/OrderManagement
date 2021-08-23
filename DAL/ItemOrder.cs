using System;
using System.Collections.Generic;

namespace OrderManagement.DAL
{
    public partial class ItemOrder
    {
        public int ItemOrderId { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExclAmount { get; set; }
        public decimal InclAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Order Order { get; set; }
    }
}
