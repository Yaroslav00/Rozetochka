using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Dto;
using DataAccess.Models;
namespace DataAccess.Repository
{
    public static class CategotyRepository
    {
        public static List<CategoryDto> GetCategories()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            using (dbContext)
            {
                return dbContext.Categories.Select(cat =>
                new CategoryDto
                { 
                    ID = cat.ID,
                    Name = cat.Name
                }).ToList();
            }
        }

        public static async Task<int> AddCategory(string name)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                var category = new Category(name);
                

                dbContext.Categories.Add(category);
                await dbContext.SaveChangesAsync();
                return category.ID;
            }
        }

        public static async Task<bool> DeleteCategory(int categoryId)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                try
                {
                    int itemsCount = await dbContext.Merchandise.CountAsync(i => i.CategoryID.Equals(categoryId));

                    if (itemsCount > 0)
                        return false;

                    var toBeDeleted = new Category {ID = categoryId};

                    dbContext.Categories.Attach(toBeDeleted);
                    dbContext.Categories.Remove(toBeDeleted);

                    await dbContext.SaveChangesAsync();
                    return true;
                } catch (Exception) {
                    return false;
                }
            }
        }

    }
}
