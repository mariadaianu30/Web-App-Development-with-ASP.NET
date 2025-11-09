using Laborator5.Data;
using Laborator5.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laborator5.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext db;

        public CategoriesController(AppDbContext context)
        {
            db = context;
        }

        // Listare categorii
        public IActionResult Index()
        {
            var categories = from category in db.Categories
                             orderby category.CategoryName
                             select category;
            ViewBag.Categories = categories;
            return View();
        }

        // Afișare categorie
        public IActionResult Show(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null) return NotFound();
            ViewBag.Category = category;
            return View();
        }

        // Form pentru categorie nouă
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Category c)
        {
            try
            {
                db.Categories.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        // Form pentru editare categorie
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null) return NotFound();
            ViewBag.Category = category;
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id, Category requestCategory)
        {
            var category = db.Categories.Find(id);
            if (category == null) return NotFound();

            try
            {
                category.CategoryName = requestCategory.CategoryName;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Edit", new { id = category.CategoryId });
            }
        }

        // Ștergere categorie
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null) return NotFound();

            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return BadRequest("A apărut o eroare la ștergere.");
            }
        }
    }
}
