using System;
using System.Collections.Generic;
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
                return await dbContext.PurchaseGoods.Where(p => p.OrderID == orderId).SumAsync(p => p.CurrentPrice);
            }
        }

        public static async Task<int> CreateNewOrder()
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
                await dbContext.SaveChangesAsync();

                return order.ID;
            }
        }

        public static async void UpdateOrderPrice(int orderId, decimal price)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var order = await dbContext.Purchases.Select(p => new Order
                {
                    PaymentStatus = p.PaymentStatus,
                    Data = p.Data,
                    TotalPrice = p.TotalPrice,
                    ID = p.ID
                }).FirstOrDefaultAsync(p => p.ID == orderId);

                order.TotalPrice = price;

                dbContext.Purchases.AddOrUpdate(order);

                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task<CartDto> GetCart(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderGoods = await dbContext.PurchaseGoods.Where(p => p.OrderID == orderId).Select(p => new OrderedGoodDto
                {
                    ID = p.ID,
                    Amount = p.Amount,
                    BuyerID = p.BuyerID,
                    CurrentPrice = p.CurrentPrice,
                    OrderID = p.OrderID,
                    GoodsID = p.GoodsID
                }).ToListAsync();

                var order = await dbContext.Purchases.FirstOrDefaultAsync(p => p.ID == orderId);

                return new CartDto
                {
                    Data = order.Data,
                    PaymentStatus = order.PaymentStatus,
                    TotalPrice = order.TotalPrice,
                    OrderedGoods = orderGoods
                };
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
    }
}
