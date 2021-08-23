using OrderManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Interface
{
    public interface IItemsRepository
    {
        Task<List<string>> GetAllItemsAsync();

        Task<Item> GetItemDetailsAsync(int itemId);
    }
}
