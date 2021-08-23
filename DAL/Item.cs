using System;
using System.Collections.Generic;

namespace OrderManagement.DAL
{
    public partial class Item
    {
        public Item()
        {
            ItemOrder = new HashSet<ItemOrder>();
        }

        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }

        public virtual ICollection<ItemOrder> ItemOrder { get; set; }
    }
}
