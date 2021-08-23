using Microsoft.EntityFrameworkCore;
using OrderManagement.DAL;
using OrderManagement.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Repository
{
    public class ItemsRepository: IItemsRepository
    {
        private readonly OrdersDBContext ordersDBContext;

        public ItemsRepository(OrdersDBContext _ordersDBContext)
        {
            ordersDBContext = _ordersDBContext;
        }

        /// <summary>
        /// get all itemcodes 
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetAllItemsAsync()
        {
            try
            {
                var items =await ordersDBContext.Item.ToListAsync();

                List<string> itemsStr = new List<string>();

                if (items.Count != 0 )
                {
                    foreach (var item in items)
                    {
                        itemsStr.Add(item.ItemCode);
                    }

                    return itemsStr;
                }
                return itemsStr;
            }
            catch (Exception ex)
            {

                throw ex;
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

                throw;
            }
        }


    }
}
