using OrderManagement.DAL;
using OrderManagement.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderManagement.Interface
{
    public interface IOrderRepository
    {
        Task<bool> SaveOrderDetailsAsync(OrderModel orderModel);

        Task<List<OrderModel>> GetAllOrdersAsync();

        Task<OrderModel> GetOderDetailsbyIdAsync(int orderId);
        Task<bool> EditOrderDetailsAsync(OrderVM orderVm);

        Task<bool> DeleteOrderAsync(int id);


    }
}
