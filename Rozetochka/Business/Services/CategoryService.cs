using Business.Interfaces;
using DataAccess.Dto;
using DataAccess.Repository;
using System.Collections.Generic;

namespace Business.Services
{
    public class CategoryService: ICategoryService
    {
        public List<CategoryDto> GetCategories()
        {
            return CategotyRepository.GetCategories();
        }
    }
}
