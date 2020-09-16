using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Category
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MaxLength(200)]
        public string categoryName { get; set; }
    }
}
