using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Categories
{
    public class EditCategoryViewModel : CreateCategoryViewModel
    {
        public int CategoryId { get; set; }
    }
}
