using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Categories
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MaxLength(200)]
        [Display(Name ="Category Name")]
        public string categoryName { get; set; }
    }
}
