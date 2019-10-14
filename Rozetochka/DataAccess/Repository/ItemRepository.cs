using System.Collections.Generic;
using System.Linq;
using DataAccess.Dto;
using DataAccess.Models;

namespace DataAccess.Repository
{
    public static class ItemRepository
    {
        public static List<GoodDto> GetItems(int? categoryId)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            using (dbContext)
            {
                var allItems = dbContext.Merchandise.Select(good =>
                new GoodDto
                {
                    CategoryID = good.CategoryID,
                    Description = good.Description,
                    ID = good.ID,
                    Name = good.Name,
                    Price = good.Price
                });
                return categoryId != null ? allItems.Where(item => item.CategoryID == categoryId).ToList() : allItems.ToList();
            }
        }
    }
}
