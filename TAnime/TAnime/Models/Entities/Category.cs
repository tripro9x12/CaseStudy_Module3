using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(200)]
        public string categoryName { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}
