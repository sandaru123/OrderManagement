using System;
using System.Collections.Generic;

namespace OrderManagement.Model
{
    public class ItemModel
    {
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }

    }
}
