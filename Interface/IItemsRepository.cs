using OrderManagement.DAL;
using OrderManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Interface
{
    public interface IItemsRepository
    {
        Task<List<Item>> GetAllItemsAsync();

        Task<Item> GetItemDetailsAsync(int itemId);
    }
}
