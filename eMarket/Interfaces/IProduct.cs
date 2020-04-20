using eMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMarket.Interfaces
{
    public interface IProduct
    {
        Product GetById(int Id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByCategoryId(int CategoryId);


        Task AddProduct(Product product);
        Task DeleteProduct(int productId);
        Task UpdateProduct(int productId, string newImage, string newName, double newPrice, int newCategoryId, string newDescripition);
    }
}
