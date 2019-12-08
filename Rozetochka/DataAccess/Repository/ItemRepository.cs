using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Dto;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public static class ItemRepository
    {
        public static async Task<decimal> GetItemPrice(int itemId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return await dbContext.Merchandise.Where(p => p.ID == itemId).Select(p => p.Price).FirstOrDefaultAsync();
            }
        }

        public static List<GoodDto> GetItems(int? categoryId, string orderBy)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            using (dbContext)
            {
                System.Reflection.PropertyInfo orderbyObj;
                switch (orderBy)
                {
                    case "За алфавітом":
                        orderbyObj = typeof(GoodDto).GetProperty("Name");
                        break;
                    case "Ціна за зростанням":
                        orderbyObj = typeof(GoodDto).GetProperty("Price");
                        break;
                    default:
                        orderbyObj = typeof(GoodDto).GetProperty("Name");
                        break;
                }

                var allItems = dbContext.Merchandise.Select(good =>
                new GoodDto
                {
                    CategoryID = good.CategoryID,
                    Description = good.Description,
                    ID = good.ID,
                    Name = good.Name,
                    Price = good.Price
                });

                if (orderBy == "Ціна за спаданням")
                {
                    return categoryId != null ? allItems.Where(item => item.CategoryID == categoryId).OrderByDescending(el => el.Price)
                        .ToList() : allItems.OrderByDescending(el => el.Price).ToList();
                }

                return categoryId != null ? allItems.Where(item => item.CategoryID == categoryId).AsEnumerable()
                    .OrderBy(o => orderbyObj.GetValue(o,null)).ToList() :
                    allItems.AsEnumerable().OrderBy(o => orderbyObj.GetValue(o, null)).ToList();
            }
        }

        public static async Task AddItem(int categoryId, string name, string description, decimal price)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var item = new Goods(name, price, description, categoryId);
                dbContext.Merchandise.Add(item);
                await dbContext.SaveChangesAsync();
            };
        }
    }
}
