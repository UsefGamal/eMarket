using eMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMarket.Interfaces
{
    public interface ICategory
    {
        Category GetById(int Id);
        IEnumerable<Category> GetAllCategories();


        Task AddCategory(Category category);
        Task DeleteCategory(int categoryId);
        Task UpdatCategory(int categoryId, string newName);

        Task IncrementCategoryProductsNumber(int id);
        Task DecrementCategoryProductsNumber(int id);
    }
}
