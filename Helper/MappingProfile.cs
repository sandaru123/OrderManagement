using AutoMapper;
using OrderManagement.DAL;
using OrderManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerModel>();
            CreateMap<CustomerModel, Customer>(); 
            
            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>();

            CreateMap<ItemOrder, ItemOrderModel>();
            CreateMap<ItemOrderModel, ItemOrder>();

            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();

            CreateMap<ItemOrder, ItemOrderVM>();
            CreateMap<ItemOrderVM, ItemOrder>();

        }
    }
}
