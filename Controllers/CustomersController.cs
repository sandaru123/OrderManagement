using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.DAL;
using OrderManagement.Interface;
using OrderManagement.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManagement.Controllers
{
    
    [Produces("application/json")]
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersRepository customersRepository;

        public CustomersController(ICustomersRepository _customersRepository)
        {
            customersRepository = _customersRepository;
        }

        // GET: api/GetAllCustomersAsync

        [Route("~/api/GetAllCustomersAsync")]
        [HttpGet]
        public async Task<ActionResult<List<CustomerModel>>> GetAllCustomersAsync()
        {
            var customers = await customersRepository.GetAllCustomersAsync();

            if (customers.Count != 0)
            {
                return Ok(new { customers, Message = "Success" });
            }

            return BadRequest(new { customers, Message = "Unsuccessfull" });
        }


        [Route("~/api/GetACustomerByIdAsync")]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetACustomerByIdAsync(int id)
        {
            var customers = await customersRepository.GetCustomerByIdAsync(id);

            if (customers != null)
            {
                return Ok(new { customers, Message = "Success" });
            }

            return BadRequest(new { customers, Message = "Unsuccessfull" });

        }

        
    }
}
