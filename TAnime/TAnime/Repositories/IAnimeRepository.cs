using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Movies;

namespace TAnime.Services
{
    public interface IAnimeRepository
    {
        IEnumerable<MovieViewModel> GetMovies();
        IEnumerable<MovieViewModel> GetMoviesOfCategory(int CategoryId);
        IEnumerable<MovieViewModel> GetMoviesOfCountry(int countryId);

        MovieViewModel GetMovieViewModel(int id);
        Movie GetMovie(int id);
        GetMovie GetMovieView(int id);
        int CreateMovie(CreateMovie movie);
        int EditMovie(EditMovieViewModel movie);
        int DeleteMovie(int movieId);
        List<Movies> GetListMovies();
    }
}
