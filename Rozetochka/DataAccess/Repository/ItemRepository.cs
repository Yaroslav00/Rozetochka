using System.Collections.Generic;
using System.Linq;
using DataAccess.Dto;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public static class ItemRepository
    {
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
    }
}
