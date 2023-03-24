using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SovosWebProject.Models;
using SovosWebProject.Services;
using System.ComponentModel;

namespace SovosWebProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IApiService _apiService;

        public CategoryController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> objCategoryList = await _apiService.ListAll();
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //GET
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || id == 0) return NotFound();
            Category remoteCategory = await _apiService.GetById(id);
            if (remoteCategory == null) return NotFound();
            return View(remoteCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as Name");
            }
            if (ModelState.IsValid)
            {
                //_dbContext.Categories.Update(category);
                bool response = await _apiService.Update(category);
                //_dbContext.SaveChanges();
                if (response)
                {
                    TempData["Success"] = "Category edited succesfully";
                    return RedirectToAction("Index");
                }
            } else
            {
                ModelState.AddModelError("Name", "Invalid Model");
            }
            return RedirectToAction("Index");
        }

        //GET
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            bool saved = await _apiService.Delete(id);
            if (saved)
            {
                TempData["Success"] = "Category deleted succesfully";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            bool saved = await _apiService.Delete(id);
            if (saved)
            {

                TempData["Success"] = "Category deleted succesfully";
            }
            return RedirectToAction("Index");
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The DisplayOrder cannot be the same as Name");
            }
            if (ModelState.IsValid)
            {
                bool saved = await _apiService.Create(category);
                if (saved)
                {
                    TempData["Success"] = "Category created succesfully";
                    return RedirectToAction("Index");

                }
            }
            return View(category);
        }
    }
}
