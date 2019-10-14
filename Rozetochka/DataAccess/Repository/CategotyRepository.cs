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
    }
}
