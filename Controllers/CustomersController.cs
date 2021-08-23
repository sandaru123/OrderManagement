using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.DAL;
using OrderManagement.Interface;

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
        public async Task<ActionResult<List<string>>> GetAllCustomersAsync()
        {
            var customers = await customersRepository.GetAllCustomersAsync();

            if (customers.Count != 0)
            {
                return customers;
            }

            return null;
        }


        [Route("~/api/GetACustomerByIdAsync")]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetACustomerByIdAsync(int id)
        {
            var customers = await customersRepository.GetCustomerByIdAsync(id);

            return customers;

        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CustomersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
