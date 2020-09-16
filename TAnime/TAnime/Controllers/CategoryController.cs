using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Category;
using TAnime.Services_Repository;

namespace TAnime.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<CategoryViewModel> categories = new List<CategoryViewModel>();
            categories = from c in categoryRepository.Gets()
                         select (new CategoryViewModel()
                         {
                             CategoryId = c.CategoryId,
                             categoryName = c.categoryName
                         });
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category() {
                    categoryName = model.categoryName
                };
                var result = categoryRepository.CreateCategory(category);
                if(result > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
            }
            return View(model);
        }
    }
}
