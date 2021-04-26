using Entity.Business.Abstract;
using Oblivion.Models;
using Microsoft.AspNetCore.Mvc;


namespace Oblivion.Controllers
{
    public class CategoryController:Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Index()
        {
            var model = new CategoryListViewModel
            {
                Categories = _categoryService.GetAll()
            };
            return View(model);
        }
    }
}
