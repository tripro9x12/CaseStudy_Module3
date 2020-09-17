using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Movies;

namespace TAnime.Services
{
    public interface IAnimeService
    {
        IEnumerable<MovieViewModel> GetMovies();
        MovieViewModel GetMovieViewModel(int id);
        Movie GetMovie(int id);
        int CreateMovie(CreateMovie movie);
        int EditMovie(Movie movie);
        int DeleteMovie(int movieId);
    }
}
