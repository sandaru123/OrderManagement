using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagement.DAL;
using OrderManagement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Repository
{
    public class CustomersRepository : ICustomersRepository
    {
        private readonly OrdersDBContext ordersDBContext;
        private readonly IMapper mapper;

        public CustomersRepository(OrdersDBContext _ordersDBContext, IMapper _mapper)
        {
            ordersDBContext = _ordersDBContext;
            mapper = _mapper;
        }

        /// <summary>
        /// get all customer names 
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllCustomersAsync()
        {
            try
            {
                List<string> customers_ = new List<string>();

                var cust = await ordersDBContext.Customer.ToListAsync();
                if (cust.Count != 0)
                {
                    foreach (var c in cust)
                    {
                        customers_.Add(c.CustomerName);
                    }
                }
                

                return customers_;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        /// <summary>
        /// get customer details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await ordersDBContext.Customer.FirstOrDefaultAsync(c=>c.CustomerId == id);
                return customer;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
