using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Category;

namespace TAnime.Services_Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryViewModel> Gets();
        Category Get(int id);
        int CreateCategory(Category model);
        int EditCategory(Category model);
        int DeleteCategory(int id);

    }
}
