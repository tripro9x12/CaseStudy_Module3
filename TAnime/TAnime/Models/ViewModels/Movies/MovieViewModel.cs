using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;

namespace TAnime.Models.ViewModels.Movies
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public string Content { get; set; }
        public string ImageOfVideo { get; set; }
        public string Country { get; set; }
        public List<MovieCategoryViewModel> MovieCategories { get; set; }
        public List<Episode> Episodes { get; set; }
    }
}
