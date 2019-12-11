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

        public async Task<int> AddCategoryWithIdReturn(string name)
        {
            int categoryId = await CategotyRepository.AddCategory(name);
            return categoryId;
        }
        public async Task<bool> DeleteCategory(int categoryId)
        {
            return await CategotyRepository.DeleteCategory(categoryId);
        }
    }
}
