using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class ProductController : Controller
    {
        private readonly CategoryDbContext _context;
        public ProductController(CategoryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? filter)
        {
            IEnumerable<Product> products = _context.Products
                .Include(product => product.Category)
                .Include(product => product.Provider);
            if (filter == null || filter == "up")
            {
                products = products.OrderBy(products => products.ProductTitle);
            }
            else
            {
                products = products.OrderByDescending(products => products.ProductTitle);
            }
            return View(products);
        }
        public IActionResult Create()
        {
            PopulateProvidersDropDownList();
            PopulateCategoriesDropDownList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {
            PopulateProvidersDropDownList(product.ProviderId);
            PopulateCategoriesDropDownList(product.CategoryId);

                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["Success"] = "Product added successfully";
                return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            if (id == 0) {
                return NotFound();
            }
            var product = _context.Products.Find(id);
            PopulateProvidersDropDownList(product.ProviderId);
            PopulateCategoriesDropDownList(product.CategoryId);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            PopulateProvidersDropDownList(product.ProviderId);
            PopulateCategoriesDropDownList(product.CategoryId);

                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["Success"] = "Product updated successfully";
                return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var product = _context.Products.Find(id);
            PopulateProvidersDropDownList(product.ProviderId);
            PopulateCategoriesDropDownList(product.CategoryId);
            return View(product);
        }
        [HttpPost]
        public IActionResult Delete(Product product)
        {
            PopulateProvidersDropDownList(product.ProviderId);
            PopulateCategoriesDropDownList(product.CategoryId);

                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["Success"] = "Product deleted successfully";
                return RedirectToAction("Index");
        }
        private void PopulateCategoriesDropDownList(object? selectedCategory = null)
        {
            var categoriesQuery = from d in _context.Categories
                                   orderby d.Name
                                   select d;
            ViewBag.Categories = new SelectList(categoriesQuery.AsNoTracking(), "Id", "Name", selectedCategory);
        }
        private void PopulateProvidersDropDownList(object? selectedProvider = null)
        {
            var providersQuery = from d in _context.Providers
                                  orderby d.ProviderName
                                 select d;
            
            ViewBag.Providers = new SelectList(providersQuery.AsNoTracking(), "Id", "ProviderName", selectedProvider);
        }
    }
}
