using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;
using Store.Models.DataViewModels;

namespace Store.Controllers
{
    public class ProviderController : Controller
    {
        private readonly CategoryDbContext _context;
        public ProviderController(CategoryDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string? filter)
        {
            IEnumerable<Provider> provider = _context.Providers
                .Include(_provider => _provider.Provider_Category)
                    .ThenInclude(p_c => p_c.Category)
                .ToList();
            if (filter == null || filter == "up")
            {
                provider = provider.OrderBy(provider => provider.ProviderName);
            }
            else
            {
                provider = provider.OrderByDescending(_provider => _provider.ProviderName);
            }
            return View(provider);
        }
        public IActionResult Create()
        {
            var provider = new Provider
            {
                Provider_Category = new List<Provider_Category>()
            };
            PopulateCategoriesData(provider);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProviderName, ProviderDescription, Provider_Category")]Provider provider, string[] selectedCategories)
        {
            if (selectedCategories != null)
            {
                provider.Provider_Category = new List<Provider_Category>();
                foreach(string category in selectedCategories)
                {
                    provider.Provider_Category.Add(new Provider_Category
                    {
                        ProviderId = provider.Id,
                        CategoryId = int.Parse(category)
                    });
                }
            }
                _context.Add(provider);
                _context.SaveChanges();
                TempData["Success"] = "Provider added successfully";
                return RedirectToAction("Index");

            PopulateCategoriesData(provider);
        }
        public IActionResult Edit(int id)
        {
            Provider provider = _context.Providers
                .Include(_provider => _provider.Provider_Category)
                    .ThenInclude(p_c => p_c.Category)
                .AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
            if (provider == null) return NotFound();
            PopulateCategoriesData(provider);
            return View(provider);
        }
        [HttpPost]
        public IActionResult Edit(int id, string[] selectedCategories)
        {
            Provider provider = _context.Providers
                .Include(_provider => _provider.Provider_Category)
                    .ThenInclude(p_c => p_c.Category)
                .FirstOrDefault(p => p.Id == id);
            if (ModelState.IsValid)
            {
                UpdateProviderCategories(selectedCategories, provider);
                _context.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Error editing category";
            PopulateCategoriesData(provider);
            UpdateProviderCategories(selectedCategories, provider);
            return View(provider);
        }
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var provider = _context.Providers
                .Include(p => p.Provider_Category)
                    .ThenInclude(p_c => p_c.Category)
                .FirstOrDefault(p => p.Id == id);
            PopulateCategoriesData();
            return View(provider);
        }
        [HttpPost]
        public IActionResult Delete(Provider provider)
        {
            _context.Providers.Remove(provider);
            _context.SaveChanges();
            TempData["Success"] = "Provider deleted successfully";
            return RedirectToAction("Index");
        }
        private void PopulateCategoriesData(Provider? provider = null)
        {
            var categories = _context.Categories;
            var productCategory = new HashSet<int>();
            if (provider != null)
            {
                productCategory = new HashSet<int>(provider.Provider_Category.Select(p_c => p_c.CategoryId));
            }
            var dataViewModel = new List<ProvidersCategories>();
            foreach (var category in categories) {
                dataViewModel.Add( new ProvidersCategories{
                    CategoryId = category.Id,
                    Name = category.Name,
                    Enabled = provider != null && productCategory.Contains(category.Id)
                });
            }
            ViewBag.Categories = dataViewModel; 
        }
        private void UpdateProviderCategories(string[] selectedCategories, Provider provider)
        { 
            if (selectedCategories == null)
            {
                provider.Provider_Category = new List<Provider_Category>();
                return;
            }
            var categories = _context.Categories;
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var providercategories = new HashSet<int>(provider.Provider_Category.Select(p_c => p_c.CategoryId));
            foreach (var category in categories) { 
                if(selectedCategoriesHS.Contains(category.Id.ToString())){
                    if(!providercategories.Contains(category.Id))
                    {
                        provider.Provider_Category.Add(new Provider_Category
                        {
                            CategoryId = category.Id,
                            ProviderId = provider.Id
                        });
                    }
                } else
                {
                    if (providercategories.Contains(category.Id))
                    {
                        var providerToRemove = provider.Provider_Category.First(p_c => p_c.CategoryId == category.Id);
                        _context.Remove(providerToRemove);
                    }
                }
            }
        }
    }
}
