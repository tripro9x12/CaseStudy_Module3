using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Categories;

namespace TAnime.Services_Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public int CreateCategory(Category model)
        {
            context.Categories.Add(model);
            return context.SaveChanges();
        }

        public int DeleteCategory(int id)
        {
            var delCategory = Get(id);
            if(delCategory != null)
            {
                context.Categories.Remove(delCategory);
            }
            return context.SaveChanges();
        }

        public int EditCategory(Category model)
        {
            var category = Get(model.CategoryId);
            if(category != null)
            {
                category.CategoryId = model.CategoryId;
                category.categoryName = model.categoryName;
            }
            context.Categories.Update(category);
            return context.SaveChanges();

        }

        public Category Get(int id)
        {
            return context.Categories.FirstOrDefault(c=>c.CategoryId == id);
        }

        public IEnumerable<CategoryViewModel> Gets()
        {
            IEnumerable<CategoryViewModel> models = new List<CategoryViewModel>();
            models = from c in context.Categories
                    select (new CategoryViewModel()
                    {
                        CategoryId = c.CategoryId,
                        categoryName = c.categoryName
                    });
            return models;
        }
    }
}
