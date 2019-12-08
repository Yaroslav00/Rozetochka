using System;
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
        public static async Task<decimal> GetOrderTotalPrice(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return await dbContext.PurchaseGoods.Where(p => p.OrderID == orderId).SumAsync(p => p.CurrentPrice * p.Amount);
            }
        }

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

        public static int CreateNewOrderAsync(int buyerId)
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

        public static async Task UpdateOrderPrice(int orderId, decimal price)
        {
            using (var dbContext = new ApplicationDbContext())
            {
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

        public static CartDto GetCart(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderGoods = dbContext.PurchaseGoods.Where(p => p.OrderID == orderId).Select(p => new OrderedGoodDto
                {
                    ID = p.ID,
                    Amount = p.Amount,
                    CurrentPrice = p.CurrentPrice,
                    OrderID = p.OrderID,
                    GoodsID = p.GoodsID
                }).ToList();

                var order = GetOrderById(orderId)?? GetOrderById(CreateNewOrder());

                return new CartDto
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

        public static int? FindOrderIdIfExists(int buyerId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderId = dbContext.Purchases.Where(p => p.BuyerID == buyerId).Select(p => p.ID)
                    .FirstOrDefault();
                return orderId;
            }
        }
    }
}
