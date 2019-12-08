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
    public static class OrderedGoodRepository
    {
        public static async Task<int> GetOrderedGoodAmount(int orderId, int goodId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return await dbContext.PurchaseGoods.Where(p => p.OrderID.Equals(orderId) && p.GoodsID.Equals(goodId))
                    .Select(p => p.Amount).FirstOrDefaultAsync();
            }
        }

        public static async Task<OrderedGoodDto> AddToOrderedGood(int goodId, int amount, int buyerId, int orderId, decimal totalPrice)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var orderedGood = await dbContext.PurchaseGoods.Where(p => p.GoodsID.Equals(goodId) && p.OrderID.Equals(orderId)).FirstOrDefaultAsync();

                if (orderedGood == null)
                {
                    orderedGood = new OrderedGood
                    {
                        GoodsID = goodId,
                        Amount = amount,
                        CurrentPrice = totalPrice,
                        OrderID = orderId
                    };

                    dbContext.PurchaseGoods.Add(orderedGood);
                    await dbContext.SaveChangesAsync();

                }
                else
                {
                    orderedGood.Amount += amount;
                    dbContext.PurchaseGoods.AddOrUpdate(orderedGood);
                    await dbContext.SaveChangesAsync();

                }

                return new OrderedGoodDto
                {
                    ID = orderedGood.ID,
                    Amount = orderedGood.Amount,
                    CurrentPrice = orderedGood.CurrentPrice,
                    GoodsID = orderedGood.GoodsID,
                    OrderID = orderedGood.OrderID
                };
            }
        }
        public static List<OrderedGoodDto> GetAllOrderedGoods(int orderId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.PurchaseGoods.Where(g => g.OrderID == orderId).Select(g => new {
                    g.Amount,
                    g.ID,
                    g.OrderID,
                    g.CurrentPrice,
                    g.GoodsID,
                }).Join(dbContext.Merchandise, c => c.GoodsID, goods => goods.ID, (r, t) => 
                    new OrderedGoodDto
                    {
                        ID = r.ID,
                        Amount = r.Amount,
                        CurrentPrice = r.CurrentPrice,
                        Description = t.Description,
                        OrderID = r.OrderID,
                        GoodsID = r.GoodsID,
                        Name = t.Name
                    }).ToList();
            }
        }
    }
}
