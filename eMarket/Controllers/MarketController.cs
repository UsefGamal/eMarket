using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using eMarket.Models;
using System.IO;
using System.Data.Entity.Validation;
using eMarket.Services;
using eMarket.Interfaces;

namespace eMarket.Controllers
{
    public class MarketController : Controller
    {
        private readonly IProduct _productService;
        private readonly ICategory _categoryService;

       
        public MarketController(IProduct productService, ICategory categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public ActionResult Index()
        {
            var products = _productService.GetAllProducts()
                .Select(prod => new ProductListingModel
                {
                    Number = prod.id,
                    Price = prod.price.Value,
                    Descripton = prod.description,
                    Img = "/root/images/ProductImages/" + prod.Image,
                    ProductName = prod.name,
                    Category = GetCategoryListingForProduct(prod)

                });
            ViewBag.categryId = new SelectList(_categoryService.GetAllCategories(), "id", "name");

            return View(products);
        }

        [HttpPost]

        public ActionResult Index(int? categryId)
        {
            var products = _productService.GetAllProducts()
                .Select(prod => new ProductListingModel
                {
                    Number = prod.id,
                    Price = prod.price.Value,
                    Descripton = prod.description,
                    Img = "/root/images/ProductImages/" + prod.Image,
                    ProductName = prod.name,
                    Category = GetCategoryListingForProduct(prod)

                });
            if (categryId !=null)
            {
             products = _productService.GetAllProducts()
                    .Where(a => a.categryId == categryId)
                    .Select(prod => new ProductListingModel
                    {
                        Number = prod.id,
                        Price = prod.price.Value,
                        Descripton = prod.description,
                        Img = "/root/images/ProductImages/" + prod.Image,
                        ProductName = prod.name,
                        Category = GetCategoryListingForProduct(prod)

                    });
            }
            ViewBag.categryId = new SelectList(_categoryService.GetAllCategories(), "id", "name");
            return View(products);
        }
        // GET: Products/AddProduct
        public ActionResult AddProduct()
        {
            ViewBag.CategryId = new SelectList(_categoryService.GetAllCategories(), "Id", "name");
            var model = new ProductAddModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(ProductAddModel product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    product.Img = "";
                    if (file != null)
                    {
                        product.Img = DateTime.Now.ToString("yymmssfff") +
                            Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/root/images/ProductImages/"),
                            product.Img);
                        file.SaveAs(path);
                        ViewBag.FileStatus = "File uploaded successfully.";
                    }

                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }
                try
                {
                    Product prod = new Product
                    {
                        name = product.Name,
                        categryId = product.CategryId,
                        description = product.Descripition,
                        Image = product.Img,
                        price = product.Price
                    };

                    _categoryService.IncrementCategoryProductsNumber(prod.categryId);
                    _productService.AddProduct(prod);

                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\"" +
                            " has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
                return RedirectToAction("Index");
            }

            ViewBag.CategryId = new SelectList(_categoryService.GetAllCategories(), "Id", "name", product.CategryId);
            return View(product);
        }

        // GET: Categories/ProductDetails/5
        public ActionResult ProductDetails(int id)
        {
            Product product = _productService.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductViewModel model = new ProductViewModel
            {
                ProductName = product.name,
                Number = product.id,
                Price = product.price ?? 0,
                Img = BuildRetrieveProductImage(product.Image),
                Descripition = product.description,
                Category = BuildCategoryListingModel(product)
            };
            return View(model);
        }

        public ActionResult UpdateProduct(int id)
        {

            Product product = _productService.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            var model = new ProductAddModel
            {
                Name = product.name,
                CategryId = product.categryId,
                Descripition = product.description,
                Img = product.Image,
                Price = product.price,
                Number = product.id
            };
            ViewBag.CategryId = new SelectList(_categoryService.GetAllCategories(), "Id", "name", product.categryId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateProduct(ProductAddModel product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    if (file != null)
                    {
                        product.Img = DateTime.Now.ToString("yymmssfff") +
                            Path.GetFileName(file.FileName);
                        string path = Path.Combine(Server.MapPath("~/root/images/ProductImages/"),
                            product.Img);
                        file.SaveAs(path);
                        ViewBag.FileStatus = "File uploaded successfully.";
                    }

                }
                catch (Exception)
                {
                    ViewBag.FileStatus = "Error while file uploading."; ;
                }

                await _productService.UpdateProduct(product.Number,
                    product.Img,
                    product.Name,
                    product.Price ?? 0,
                    product.CategryId,
                    product.Descripition);

                return RedirectToAction("Index");
            }
            var model = new ProductAddModel
            {
                Name = product.Name,
                CategryId = product.CategryId,
                Descripition = product.Descripition,
                Img = product.Img,
                Price = product.Price,
                Number = product.Number
            };
            ViewBag.CategryId = new SelectList(_categoryService.GetAllCategories(),
                "Id", "name", product.CategryId);
            return View(model);
        }

        // GET: Products/DeleteProduct/5
        public ActionResult DeleteProduct(int id)
        {
            Product product = _productService.GetById(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }


        // POST: Products/DeleteProduct/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _categoryService.DecrementCategoryProductsNumber(id);
            await _productService.DeleteProduct(id);

            return RedirectToAction("Index");
        }
        /*
        To Do: Seperate those as a service project 
             
             Author: Youssef Gamal

             Modification Date: 14/04/2020
             
        */

        private string BuildRetrieveProductImage(string img)
        {
            return "/root/images/ProductImages/" + img;
        }

        private CategoryListingModel BuildCategoryListingModel(Product product)
        {
            var category = product.Category;
            return new CategoryListingModel
            {
                Id = category.id,
                Name = category.name,
                Number_of_products = category.number_of_products ?? 0
            };
        }
        private CategoryListingModel GetCategoryListingForProduct(Product Product)
        {
            var category = Product.Category;
            return new CategoryListingModel
            {
                Id = category.id,
                Name = category.name,
                Number_of_products = category.number_of_products ?? 0
            };
        }


        [HttpPost]
        public ActionResult SelectCategory(int? selectedCategoryId)
        {
            var products = _productService.GetAllProducts();
            if (selectedCategoryId.HasValue)
                products = _productService.GetProductsByCategoryId(selectedCategoryId.Value);

            return PartialView("ProductsUserControl", products);
        }

        //// GET: Categories/CategoryDetails/5
        //public async Task<ActionResult> CategoryDetails(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = await db.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        // GET: Categories/AddCategory
        //public ActionResult AddCategory()
        //{
        //    return View();
        //}

        // POST: Categories/AddCategory
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> AddCategory([Bind(Include = "Id,name,NoOfProfucts")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Categories.Add(category);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(category);
        //}

        // GET: Categories/UpdateCategory/5
        //public async Task<ActionResult> UpdateCategory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = await db.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        // POST: Categories/UpdateCategory/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> UpdateCategory([Bind(Include = "Id,name,NoOfProfucts")] Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(category).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}

        // GET: Categories/DeleteCategory/5
        //public async Task<ActionResult> DeleteCategory(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Category category = await db.Categories.FindAsync(id);
        //    if (category == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(category);
        //}

        // POST: Categories/DeleteCategory/5
        //[HttpPost, ActionName("DeleteCategory")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteCategory(int id)
        //{
        //    Category category = await db.Categories.FindAsync(id);
        //    db.Categories.Remove(category);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}
    }
}