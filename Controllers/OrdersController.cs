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
    [Authorize]
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
        public async Task<ActionResult<bool>> SaveOrderDetailsAsync(OrderModel orderModel)
        {
            var bl = await orderRepository.SaveOrderDetailsAsync(orderModel);

            if (bl)
            {
                return Ok(new { bl, Message = "Success" });
            }

            return BadRequest(new {bl, Message = "Unsuccessfull" });
        }

        [Route("~/api/GetAllOrdersAsync")]
        [HttpGet]
        public async Task<ActionResult<List<OrderModel>>> GetAllOrdersAsync()
        {
            var orders = await orderRepository.GetAllOrdersAsync();

            if (orders.Count != 0)
            {
                return Ok(new { orders, Message = "Success" });
            }

            return BadRequest(new { Message = "Unsuccessfull" });
        }

        [Route("~/api/GetOrderDetailsByIdAsync")]
        [HttpGet]
        public async Task<ActionResult<OrderModel>> GetOrderDetailsByIdAsync(int id)
        {
            var order = await orderRepository.GetOderDetailsbyIdAsync(id);

            if (order != null)
            {
                //return order;
                return Ok(new { order, Message="Success" });
            }

            return BadRequest(new { Message = "Null" });
        }

        [Route("~/api/EditOrderDetailsAsync")]
        [HttpPut]
        public async Task<ActionResult<bool>> EditOrderDetailsAsync(OrderVM orderModel)
        {
           var result=  await orderRepository.EditOrderDetailsAsync(orderModel);

            if (result)
            {
                return Ok(new { result, Message = "Success" }); ;
            }
            else
            {
                return BadRequest(new { result, Message = "Unsuccessfull" });
            }
           
        }


        [Route("~/api/DeleteOrderAsync")]
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteOrderAsync(int id)
        {
            var result = await orderRepository.DeleteOrderAsync(id);

            if (result)
            {
                return Ok(new {result , Message = "Successfull" });
            }
            else
            {
                return BadRequest(new{result, Message = "Null"});
            }

        }

    }
}
