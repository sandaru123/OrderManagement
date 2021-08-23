using OrderManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Interface
{
    public interface ICustomersRepository
    {
        Task<List<string>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
    }
}
