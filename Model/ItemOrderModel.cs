using System;
using System.Collections.Generic;

namespace OrderManagement.Model
{
    public partial class ItemOrderModel
    {
        public string Description { get; set; }
        public string Note { get; set; }
        public decimal Quantity { get; set; }
        public decimal ExclAmount { get; set; }
        public decimal InclAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public int ItemId { get; set; }
        public int OrderId { get; set; }


    }

    public partial class ItemOrderVM
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


    }
}
