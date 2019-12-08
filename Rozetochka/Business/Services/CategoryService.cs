using Business.Interfaces;
using DataAccess.Dto;
using DataAccess.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Services
{
    public class CategoryService: ICategoryService
    {
        public List<CategoryDto> GetCategories()
        {
            return CategotyRepository.GetCategories();
        }

        public async Task AddCategory(string name)
        {
            await CategotyRepository.AddCategory(name);
        }
    }
}
