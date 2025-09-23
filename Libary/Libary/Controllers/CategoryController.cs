using Libary.Data;
using Libary.Models;
using Microsoft.AspNetCore.Mvc;

namespace Libary.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index() => View(LibraryData.Categories);

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Category category)
        {
            category.Id = LibraryData.Categories.Max(c => c.Id) + 1;
            LibraryData.Categories.Add(category);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var cat = LibraryData.Categories.FirstOrDefault(c => c.Id == id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var existing = LibraryData.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (existing == null) return NotFound();
            existing.Name = category.Name;
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var cat = LibraryData.Categories.FirstOrDefault(c => c.Id == id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var cat = LibraryData.Categories.FirstOrDefault(c => c.Id == id);
            if (cat != null) LibraryData.Categories.Remove(cat);
            return RedirectToAction("Index");
        }
    }
}