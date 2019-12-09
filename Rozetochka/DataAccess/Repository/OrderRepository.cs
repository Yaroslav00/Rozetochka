﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public static class OrderRepository
    {
        public static int CreateNewOrder()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var order = new Order
                {
                    Data = DateTime.UtcNow,
                    TotalPrice = 0,
                    PaymentStatus = false
                };

                dbContext.Purchases.Add(order);
                dbContext.SaveChanges();

                return order.ID;
            }
        }

        public static int CreateNewOrder(int buyerId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var order = new Order
                {
                    Data = DateTime.UtcNow,
                    TotalPrice = 0,
                    PaymentStatus = false,
                    BuyerID = buyerId
                };
                
                dbContext.Purchases.Add(order);
                dbContext.SaveChanges();

                return order.ID;
            }
        }

        public static async Task UpdateOrderPrice(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var sumAllItemsInCart = await dbContext.PurchaseGoods.Where(p => p.OrderID.Equals(orderId)).ToListAsync();

                decimal price = 0;

                sumAllItemsInCart.ForEach(i =>
                {
                    price += i.Amount*i.CurrentPrice;
                });

                var orderDto = await dbContext.Purchases.Select(p => new OrderDto
                {
                    PaymentStatus = p.PaymentStatus,
                    Data = p.Data,
                    TotalPrice = p.TotalPrice,
                    ID = p.ID,
                    BuyerID = p.BuyerID
                }).FirstOrDefaultAsync(p => p.ID == orderId);

                var order = new Order
                {
                    ID = orderDto.ID,
                    TotalPrice = price,
                    PaymentStatus = orderDto.PaymentStatus,
                    BuyerID = orderDto.BuyerID,
                    Data = orderDto.Data,
                };

                dbContext.Purchases.AddOrUpdate(order);

                await dbContext.SaveChangesAsync();
            }
        }

        public static List<CartDto> GetCartsHistory(List<int> orderIds)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderGoods = dbContext.PurchaseGoods.Where(p => orderIds.Contains(p.OrderID)).Select(p => new {
                    p.ID,
                    p.Amount,
                    p.CurrentPrice,
                    p.OrderID,
                    p.GoodsID,
                }).Join(dbContext.Merchandise, arg => arg.GoodsID, goods => goods.ID, (p, a) => new OrderedGoodDto
                {
                    ID = p.ID,
                    Amount = p.Amount,
                    CurrentPrice = p.CurrentPrice,
                    OrderID = p.OrderID,
                    GoodsID = p.GoodsID,
                    Name = a.Name,
                    Description = a.Description
                }).ToList();

                var orders = GetOrders(orderIds);

                List<CartDto> ordersHistory = new List<CartDto>();

                orders.ForEach(o => ordersHistory.Add(new CartDto
                {
                    ID = o.ID,
                    PaymentStatus = o.PaymentStatus,
                    TotalPrice = o.TotalPrice,
                    Data = o.Data,
                    OrderedGoods = orderGoods.Where(g => o.ID.Equals(g.OrderID)).ToList()
                }));

                return ordersHistory;
            }
        }

        private static List<Order> GetOrders(List<int> orderIds)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Purchases.Where(p => orderIds.Contains(p.ID) && p.PaymentStatus).ToList();

            }
        }

        public static CartDto GetCart(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderGoods = dbContext.PurchaseGoods.Where(p => p.OrderID == orderId).Select(p => new {
                    p.ID,
                    p.Amount,
                    p.CurrentPrice,
                    p.OrderID,
                    p.GoodsID,
                }).Join(dbContext.Merchandise,arg => arg.GoodsID, goods => goods.ID,(p, a) => new OrderedGoodDto
                {
                    ID = p.ID,
                    Amount = p.Amount,
                    CurrentPrice = p.CurrentPrice,
                    OrderID = p.OrderID,
                    GoodsID = p.GoodsID,
                    Name = a.Name,
                    Description = a.Description
                }).ToList();

                var order = GetOrderById(orderId)?? GetOrderById(CreateNewOrder());

                return !order.PaymentStatus ? new CartDto() : new CartDto
                {
                    ID = order.ID,
                    Data = order.Data,
                    PaymentStatus = order.PaymentStatus,
                    TotalPrice = order.TotalPrice,
                    OrderedGoods = orderGoods
                };
            }
        }

        private static Order GetOrderById(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Purchases.FirstOrDefault(p => p.ID.Equals(orderId));
            }
        }

        public static async Task Checkout(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var order = await dbContext.Purchases.FirstOrDefaultAsync(p => p.ID == orderId);

                order.PaymentStatus = true;

                dbContext.Purchases.AddOrUpdate(order);

                await dbContext.SaveChangesAsync();
            }
        }

        public static int? FindOrderIdIfExists(int buyerId, bool takeCheckouted)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderId = dbContext.Purchases.Where(p => p.BuyerID == buyerId && p.PaymentStatus.Equals(takeCheckouted)).Select(p => p.ID)
                    .FirstOrDefault();
                return orderId;
            }
        }

        public static List<int> FindAllOrderIds(int buyerId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Purchases.Where(p => p.BuyerID.Equals(buyerId)).Select(p => p.ID).ToList();
            }
        }
    }
}
