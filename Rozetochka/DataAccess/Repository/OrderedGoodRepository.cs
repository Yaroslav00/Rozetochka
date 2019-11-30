using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Dto;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public static class OrderedGoodRepository
    {
        public static async Task<OrderedGoodDto> AddToOrderedGood(int goodId, int amount, int buyerId, int orderId, decimal totalPrice)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderedGood = new OrderedGood
                {
                    GoodsID = goodId,
                    Amount = amount,
                    BuyerID = buyerId,
                    CurrentPrice = totalPrice,
                    OrderID = orderId
                };

                dbContext.PurchaseGoods.Add(orderedGood);
                await dbContext.SaveChangesAsync();

                return new OrderedGoodDto
                {
                    ID = orderedGood.ID,
                    Amount = orderedGood.Amount,
                    BuyerID = orderedGood.BuyerID,
                    CurrentPrice = orderedGood.CurrentPrice,
                    GoodsID = orderedGood.GoodsID,
                    OrderID = orderedGood.OrderID
                };
            }
        }

        public static async Task<int?> FindOrderIdIfExists(int buyerId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderId = await dbContext.PurchaseGoods.Where(p => p.BuyerID == buyerId).Select(p => p.OrderID)
                    .FirstAsync();
                return orderId;
            }
        }
    }
}
