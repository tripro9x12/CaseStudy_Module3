using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Movies
{
    public class MovieCategoryViewModel
    {
        public int MovieId { get; set; }
        public int CategoryId { get; set; }
        public string MovieName { get; set; }
        public string CategoryName { get; set; }
    }
}
