using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderManagement.DAL;
using OrderManagement.Interface;
using OrderManagement.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository _orderRepository)
        {
            orderRepository = _orderRepository;
        }


        // POST api/<OrdersController>
        [Route("~/api/SaveOrderDetailsAsync")]
        [HttpPost]
        public async Task SaveOrderDetailsAsync(OrderModel orderModel)
        {
            var bl = await orderRepository.SaveOrderDetailsAsync(orderModel);
        }

        [Route("~/api/GetAllOrdersAsync")]
        [HttpGet]
        public async Task<ActionResult<List<OrderModel>>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetAllOrdersAsync();

            if (orders.Count != 0)
            {
                return orders;
            }

            return null;
        }

        [Route("~/api/GetOrderDetailsByIdAsync")]
        [HttpGet]
        public async Task<ActionResult<OrderModel>> GetOrderDetailsByIdAsync(int id)
        {
            var order = await orderRepository.GetOderDetailsbyIdAsync(id);

            if (order != null)
            {
                return order;
            }

            return null;
        }

        [Route("~/api/EditOrderDetailsAsync")]
        [HttpPut]
        public async Task EditOrderDetailsAsync(OrderModel orderModel)
        {
            var bl = await orderRepository.SaveOrderDetailsAsync(orderModel);
        }


    }
}
