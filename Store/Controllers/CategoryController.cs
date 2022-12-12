using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store.Data;
using Store.Models;

namespace Store.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryDbContext _context;

        public CategoryController(CategoryDbContext context)
        {
            _context = context; 
        }

        public IActionResult Index(string? filter)
        {
            IEnumerable<Category> categories = _context.Categories
                .Include(cat => cat.Provider_Category)
                    .ThenInclude(p_c => p_c.Provider);
            if(filter == null || filter == "up")
            {
                categories = categories.OrderBy(category => category.Name);
            } else
            {
                categories = categories.OrderByDescending(category => category.Name);
            }
            return View(categories);
        }

        public IActionResult Create()
        {
            PopulateProvidersDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name", "DisplayOrder")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);  
                _context.SaveChanges();
                TempData["Success"] = "Category added successfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Error adding category";
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (category == null) return NotFound();
            if(ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Error editing category";
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null) return NotFound();
            var category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            if (category == null) return NotFound();
            _context.Categories.Remove(category);
            _context.SaveChanges();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
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
