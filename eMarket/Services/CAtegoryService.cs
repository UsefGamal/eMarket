using eMarket.Interfaces;
using eMarket.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace eMarket.Services
{
    public class CategoryService : ICategory
    {

        private readonly ApplicationdbContext _context;
        public CategoryService(ApplicationdbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories
                 .Include(p => p.Products);
        }

        public Category GetById(int Id)
        {
            return _context.Categories
                .Where(p => p.id == Id)
                .Include(p => p.Products)
                .FirstOrDefault();
        }
        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatCategory(int categoryId, string newName)
        {
            var category = GetById(categoryId);
            category.name = newName;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteCategory(int categoryId)
        {

            var category = GetById(categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }




        public async Task DecrementCategoryProductsNumber(int id)
        {
            var category = GetById(id);
            category.number_of_products -= 1;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task IncrementCategoryProductsNumber(int id)
        {
            var category = GetById(id);
            category.number_of_products += 1;

            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

      
    }
}