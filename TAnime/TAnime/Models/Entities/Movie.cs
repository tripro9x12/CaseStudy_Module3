using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Entities
{
    public class Movie
    {
        public int MovieId { get; set; }
        [MaxLength(300)]
        [Required]
        public string MovieName { get; set; }
        public string Content { get; set; }
        public string Country { get; set; }
        public ICollection<MovieCategory> movieCategories { get; set; }
        public ICollection<Episode> Episode { get; set; }
    }
}
