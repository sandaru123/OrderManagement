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
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersDBContext ordersDBContext;
        private readonly IMapper mapper;

        public OrderRepository(OrdersDBContext _ordersDBContext, IMapper _mapper)
        {
            ordersDBContext = _ordersDBContext;
            mapper = _mapper;
        }

        /// <summary>
        /// save order details 
        /// </summary>
        /// <param name="orderModel"></param>
        /// <returns></returns>
        public async Task<bool> SaveOrderDetailsAsync(OrderModel orderModel)
        {
            try
            {
                if (orderModel!= null)
                {
                    Order order =  mapper.Map<Order>(orderModel);

                    await ordersDBContext.Order.AddAsync(order);
                    await ordersDBContext.SaveChangesAsync();

                    decimal tot_excl = 0;
                    decimal tot_tax = 0;
                    decimal tot_inc = 0;

                    if (orderModel.ItemOrders.Count != 0)
                    {
                        List<ItemOrder> itemOrders = mapper.Map<List<ItemOrder>>(orderModel.ItemOrders);

                        foreach (var itm in itemOrders)
                        {
                            var itemDetail = await ordersDBContext.Item.FirstOrDefaultAsync(c=>c.ItemId == itm.ItemId);

                            itm.ExclAmount = itm.Quantity * itemDetail.Price;
                            itm.TaxAmount = itm.ExclAmount * itemDetail.Tax / 100;
                            itm.InclAmount = itm.ExclAmount + itm.TaxAmount;

                            itm.OrderId = order.OrderId;

                            await ordersDBContext.ItemOrder.AddAsync(itm);
                            tot_excl += itm.ExclAmount;
                            tot_tax += itm.TaxAmount;
                            tot_inc += itm.InclAmount;
                        }
                        await ordersDBContext.SaveChangesAsync();

                        Order order_ = await ordersDBContext.Order.FirstOrDefaultAsync(a=>a.OrderId == order.OrderId);
                        if (order_ != null)
                        {
                            order_.TotExcl = tot_excl;
                            order_.TotIncl = tot_inc;
                            order_.TotTax = tot_tax;
                            await ordersDBContext.SaveChangesAsync();
                            return true;
                        }
                    }

                    
                }
                return false;
            }
            catch (Exception e)
            {

                throw e;
            }

          
        }

        /// <summary>
        /// Get All orders 
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrderModel>> GetAllOrdersAsync()
        {
            try
            {
                List<OrderModel> ords = new List<OrderModel>();
                var orders = await ordersDBContext.Order.Include(r => r.ItemOrder).ToListAsync();

                foreach (var order in orders)
                {
                    OrderModel order1 = mapper.Map<OrderModel>(order);

                    List<ItemOrderModel> iom = mapper.Map<List<ItemOrderModel>>(order.ItemOrder);
                    order1.ItemOrders = iom;

                    ords.Add(order1);
                }

                return ords;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Get order Details by id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public async Task<OrderModel> GetOderDetailsbyIdAsync(int orderId)
        {
            try
            {
                OrderModel orderModel = new OrderModel();
                var order = await ordersDBContext.Order.FirstOrDefaultAsync(a=>a.OrderId == orderId);
                if (order!= null)
                {
                    orderModel = mapper.Map<OrderModel>(order);
                    List<ItemOrderModel> itemOrderModel = mapper.Map<List<ItemOrderModel>>(order.ItemOrder);

                    orderModel.ItemOrders = itemOrderModel;
                }
                return orderModel;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public async Task<bool> EditOrderDetailsAsync(OrderVM orderVm)
        {
            try
            {
                decimal tot_excl = 0;
                decimal tot_tax = 0;
                decimal tot_inc = 0;

                //get order
                var orderObj = await ordersDBContext.Order.FirstOrDefaultAsync(x=>x.OrderId == orderVm.OrderId);

                if (orderObj!= null)
                {
                    //edit items_order
                    foreach (var obj in orderVm.ItemOrders)
                    {
                        var io_obj = await ordersDBContext.ItemOrder.FirstOrDefaultAsync(a=>a.ItemOrderId == obj.ItemOrderId);

                        if (io_obj!= null)
                        {
                            //calculate tax and total if quantity is different
                            if (io_obj.Quantity != obj.Quantity)
                            {
                                var item = await ordersDBContext.Item.FirstOrDefaultAsync(v => v.ItemId == obj.ItemId);
                                io_obj.ExclAmount = obj.Quantity * item.Price;
                                io_obj.TaxAmount = io_obj.ExclAmount * item.Tax / 100;
                                io_obj.InclAmount = io_obj.ExclAmount + io_obj.TaxAmount;


                                //edit and save itemorder tables
                                io_obj.OrderId = obj.OrderId;
                                io_obj.ItemId = obj.ItemId;
                                io_obj.Description = obj.Description;
                                io_obj.Note = obj.Note;
                                io_obj.Quantity = obj.Quantity;
                                await ordersDBContext.SaveChangesAsync();

                                tot_excl += io_obj.ExclAmount;
                                tot_tax += io_obj.TaxAmount;
                                tot_inc += io_obj.InclAmount;
                            }
                            else
                            {
                                io_obj = mapper.Map<ItemOrder>(obj);
                                await ordersDBContext.SaveChangesAsync();

                                tot_excl += obj.ExclAmount;
                                tot_tax += obj.TaxAmount;
                                tot_inc += obj.InclAmount;
                            }
                        }

                    }




                    orderObj = mapper.Map<Order>(orderVm);
                    orderObj.TotExcl = tot_excl;
                    orderObj.TotIncl = tot_inc;
                    orderObj.TotTax = tot_tax;
                    await ordersDBContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
