using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Categories;
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
            var categories = categoryRepository.Gets().ToList();
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
                var categories = categoryRepository.Gets().ToList();
                foreach (var name in categories)
                {
                   if(name.categoryName == model.categoryName)
                    {
                        ModelState.AddModelError("", "tên đã tồn tại");
                        return View();
                    }
                }
                var category = new Category()
                {
                    categoryName = model.categoryName
                };
                var result = categoryRepository.CreateCategory(category);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = categoryRepository.Get(id);
            if(category == null)
            {
                return View();
            }
            var editCategory = new EditCategoryViewModel()
            {
                CategoryId = category.CategoryId,
                categoryName = category.categoryName
            };
            return View(editCategory);
        }
        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var categories = categoryRepository.Gets().ToList();
                foreach (var name in categories)
                {
                    if (name.categoryName == model.categoryName)
                    {
                        ModelState.AddModelError("", "tên đã tồn tại");
                        return View();
                    }
                }
                var categoty = categoryRepository.Get(model.CategoryId);
                categoty.categoryName = model.categoryName;
                var result = categoryRepository.EditCategory(categoty);
                if(result > 0)
                {
                    return RedirectToAction("Index", "Category");
                }
            }
            return View(model);
        }
        [Route("Category/Delete/{CategoryId}")]
        public IActionResult Delete(int CategoryId)
        {
            var delCategory = categoryRepository.DeleteCategory(CategoryId);
            return Json(new {delCategory });
        }
    }
}
