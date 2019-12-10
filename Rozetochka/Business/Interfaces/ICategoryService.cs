using DataAccess;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryDto> GetCategories();

        Task AddCategory(string name);

        Task<bool> DeleteCategory(int categoryId);
    }
}
