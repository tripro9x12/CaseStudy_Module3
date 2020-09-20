using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Movies
{
    public class EditMovieViewModel : CreateMovieViewModel
    {
        public int MovieId { get; set; }
    }
}
