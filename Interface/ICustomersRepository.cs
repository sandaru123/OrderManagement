using OrderManagement.DAL;
using OrderManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Interface
{
    public interface ICustomersRepository
    {
        Task<List<CustomerModel>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
