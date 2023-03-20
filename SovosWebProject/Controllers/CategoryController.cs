using Microsoft.AspNetCore.Mvc;
using SovosWebProject.Data;
using SovosWebProject.Models;

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
    }
}
