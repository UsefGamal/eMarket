using eMarket.Interfaces;
using eMarket.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;


namespace eMarket.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationdbContext _context;


        public ProductService(ApplicationdbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products
                .Include(prod => prod.Category);
        }
        public IEnumerable<Product> GetProductsByCategoryId(int CategoryId)
        {
            return GetAllProducts()
                .Where(prod => prod.categryId == CategoryId)
                .OrderBy(z => z.name);
        }
        public Product GetById(int Id)
        {
            return _context.Products
                .Where(p => p.id == Id)
                .Include(p => p.Category)
                .FirstOrDefault();
        }
        public async Task AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProduct(int productId)
        {
            var product = GetById(productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }



        public async Task UpdateProduct(int productId, string newImage,
                                        string newName, double newPrice,
                                        int newCategoryId, string newDescripition)
        {
            var prod = GetById(productId);
            if (prod.Image == null || (!prod.Image.Equals(newImage) && newImage != null))
            {
                prod.Image = newImage;
            }
            if (prod.name.Equals(newName))
            {
                prod.name = newName;
            }

            prod.price = newPrice;
            prod.categryId = newCategoryId;
            prod.description = newDescripition;

            _context.Entry(prod).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}