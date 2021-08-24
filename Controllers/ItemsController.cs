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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository itemsRepository;

        public ItemsController(IItemsRepository _itemsRepository)
        {
            itemsRepository = _itemsRepository;
        }


        // GET: api/<ItemsController>
        [Route("~/api/GetAllItemsCodesAsync")]
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetAllItemsCodesAsync()
        {
            var itms = await itemsRepository.GetAllItemsAsync();

            if (itms.Count!= 0)
            {
                return Ok(new { itms, Message = "Success" });
            }

            return BadRequest(new { itms, Message = "Unsuccessfull" });
        }

        // GET: api/<ItemsController>
        [Route("~/api/GetItemDetailsAsync")]
        [HttpGet]
        public async Task<ActionResult<Item>> GetItemDetailsAsync(int itemId)
        {
            var itms = await itemsRepository.GetItemDetailsAsync(itemId);

            if (itms != null)
            {
                return Ok(new { itms, Message = "Success" });
            }

            return BadRequest(new { itms, Message = "Unsuccessfull" });
        }


    }
}
