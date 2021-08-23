using System;
using System.Collections.Generic;

namespace OrderManagement.Model
{
    public class OrderModel
    {
        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string ReferNo { get; set; }
        public string Note { get; set; }
        public decimal TotExcl { get; set; }
        public decimal TotTax { get; set; }
        public decimal TotIncl { get; set; }
        public int CustomerId { get; set; }

        
        public ICollection<ItemOrderModel> ItemOrders { get; set; }

    }


    public class OrderVM
    {
        public int OrderId { get; set; }
        public string InvNo { get; set; }
        public DateTime InvDate { get; set; }
        public string ReferNo { get; set; }
        public string Note { get; set; }
        public decimal TotExcl { get; set; }
        public decimal TotTax { get; set; }
        public decimal TotIncl { get; set; }
        public int CustomerId { get; set; }


        public ICollection<ItemOrderVM> ItemOrders { get; set; }

    }
}
