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
    public class ItemsRepository: IItemsRepository
    {
        private readonly OrdersDBContext ordersDBContext;
        private readonly IMapper mapper;

        public ItemsRepository(OrdersDBContext _ordersDBContext, IMapper _mapper)
        {
            ordersDBContext = _ordersDBContext;
            mapper = _mapper;
        }

        /// <summary>
        /// get all itemcodes 
        /// </summary>
        /// <returns></returns>
        public async Task<List<ItemModel>> GetAllItemsAsync()
        {
            try
            {
                var items =await ordersDBContext.Item.ToListAsync();

                List<ItemModel> itemsStr = new List<ItemModel>();

                itemsStr = mapper.Map<List<ItemModel>>(items);
                return itemsStr;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(ex.Message));
            }
            
        }

        /// <summary>
        /// Get item details by id
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public async Task<Item> GetItemDetailsAsync(int itemId)
        {
            try
            {
                var item = await ordersDBContext.Item.FirstOrDefaultAsync(c=>c.ItemId == itemId);

                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format(ex.Message));
            }
        }


    }
}
