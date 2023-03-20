using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SovosWebProject.Data;
using SovosWebProject.Models;
using System.ComponentModel;

namespace SovosWebProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _dbContext.Categories;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var category = _dbContext.Categories.Find(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as Name");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                TempData["Success"] = "Category edited succesfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();
            var category = _dbContext.Categories.Find(id);
            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var category = _dbContext.Categories.Find(id);
            if (category == null) return NotFound();
            _dbContext.Remove(category);
            _dbContext.SaveChanges();
            TempData["Success"] = "Category deleted succesfully";
            return RedirectToAction("Index");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as Name");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                TempData["Success"] = "Category created succesfully";
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
