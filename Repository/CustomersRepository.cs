using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderManagement.DAL;
using OrderManagement.Interface;
using OrderManagement.Model;
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
        public async Task<List<CustomerModel>> GetAllCustomersAsync()
        {
            try
            {
                List<CustomerModel> customers_ = new List<CustomerModel>();

                var cust = await ordersDBContext.Customer.ToListAsync();
              
                customers_ = mapper.Map<List<CustomerModel>>(cust);

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
