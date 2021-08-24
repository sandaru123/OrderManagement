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
                    Order order = new Order();

                    order.CustomerId = orderModel.CustomerId;
                    order.InvNo = orderModel.InvNo;
                    order.InvDate = orderModel.InvDate;
                    order.InvDate = orderModel.InvDate;
                    order.Note = orderModel.Note;
                    order.ReferNo = orderModel.ReferNo;
                    order.TotExcl = 0;
                    order.TotIncl = 0;
                    order.TotTax = 0;

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

                throw new Exception(string.Format(e.Message));
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

                throw new Exception(string.Format(e.Message));
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
                var order = await ordersDBContext.Order.Include(a=>a.ItemOrder).FirstOrDefaultAsync(a=>a.OrderId == orderId);
                if (order!= null)
                {
                    orderModel = mapper.Map<OrderModel>(order);
                    List<ItemOrderModel> itemOrderModel = mapper.Map<List<ItemOrderModel>>(order.ItemOrder);

                    orderModel.ItemOrders = itemOrderModel;
                    return orderModel;
                }
                return null;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(e.Message));
            }
        }



        /// <summary>
        /// Edit order details
        /// </summary>
        /// <param name="orderVm"></param>
        /// <returns></returns>
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


                    //object items less than db object
                    //get all he order items according to the orderid
                    var io_obj_list = await ordersDBContext.ItemOrder.Where(a => a.OrderId == orderVm.OrderId).ToListAsync();

                    List<int> inDb_and_obj = new List<int>();
                    List<int> inDb = new List<int>();


                    foreach (var ind in io_obj_list)
                    {
                        inDb.Add(ind.ItemId);
                    }

                    foreach (var io_objs in io_obj_list)
                    {
                        foreach (var itm_odrs in orderVm.ItemOrders)
                        {
                            if (itm_odrs.ItemId == io_objs.ItemId)
                            {
                                inDb_and_obj.Add(itm_odrs.ItemId);
                            }
                        }  
                    }

                    var not_inDb_list = inDb.Except(inDb_and_obj).ToList();

                    if (not_inDb_list.Count != 0)
                    {
                        foreach (var itms in not_inDb_list)
                        {
                            var dltobj = await ordersDBContext.ItemOrder.FirstOrDefaultAsync(a=>a.ItemId == itms && a.OrderId == orderVm.OrderId);
                            ordersDBContext.ItemOrder.Remove(dltobj);
                            await ordersDBContext.SaveChangesAsync();
                        }
                    }
                    //edit items_order
                    foreach (var obj in orderVm.ItemOrders)
                    {

                        var io_obj = await ordersDBContext.ItemOrder.FirstOrDefaultAsync(a=>a.OrderId == obj.OrderId && a.ItemId == obj.ItemId);


                        if (io_obj!= null && io_obj.ItemId == obj.ItemId)
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
                            //if another item add to the edit
                            // obj - itemorders
                            ItemOrder itemOrder = mapper.Map<ItemOrder>(obj);
                            var item = await ordersDBContext.Item.FirstOrDefaultAsync(v => v.ItemId == obj.ItemId);
                            itemOrder.ExclAmount = obj.Quantity * item.Price;
                            itemOrder.TaxAmount = itemOrder.ExclAmount * item.Tax / 100;
                            itemOrder.InclAmount = itemOrder.ExclAmount + itemOrder.TaxAmount;

                            await ordersDBContext.ItemOrder.AddAsync(itemOrder);
                            await ordersDBContext.SaveChangesAsync();
                            tot_excl += itemOrder.ExclAmount;
                            tot_tax += itemOrder.TaxAmount;
                            tot_inc += itemOrder.InclAmount;
                        }

                    }

                    Order orderObj2 = await ordersDBContext.Order.FirstOrDefaultAsync(x => x.OrderId == orderVm.OrderId);
                  
                    orderObj2.InvNo = orderVm.InvNo;
                    orderObj2.InvDate = orderVm.InvDate;
                    orderObj2.ReferNo = orderVm.ReferNo;
                    orderObj2.Note = orderVm.Note;

                    orderObj2.TotExcl = tot_excl;
                    orderObj2.TotIncl = tot_inc;
                    orderObj2.TotTax = tot_tax;
                    await ordersDBContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(e.Message));
            }
        }

        /// <summary>
        /// delete order details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteOrderAsync(int id)
        {
            try
            {
                var itemOrdersList = await ordersDBContext.ItemOrder.Where(a => a.OrderId == id).ToListAsync();

                if (itemOrdersList.Count != 0)
                {
                    foreach (var itm in itemOrdersList)
                    {
                        ordersDBContext.ItemOrder.Remove(itm);
                        await ordersDBContext.SaveChangesAsync();
                    }
                }


                var orderObj = await ordersDBContext.Order.FirstOrDefaultAsync(c=>c.OrderId == id);

                if (orderObj!= null)
                {
                    ordersDBContext.Order.Remove(orderObj);
                    await ordersDBContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                throw new Exception(string.Format(e.Message));
            }
        }
    }
}
