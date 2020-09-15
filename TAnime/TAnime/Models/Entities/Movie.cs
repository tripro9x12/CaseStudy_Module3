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
        public string MovieName { get; set; }
        public string Content { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Episode> Episode { get; set; }
    }
}
