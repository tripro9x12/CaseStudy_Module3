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
        IEnumerable<MovieViewModel> GetMoviesOfFinish(int finishId);
        IEnumerable<MovieViewModel> GetMoviesOfView();


        MovieViewModel GetMovieViewModel(int id);
        Movie GetMovie(int id);
        GetMovie GetMovieView(int id);
        int CreateMovie(CreateMovie movie);
        int EditMovie(EditMovieViewModel movie);
        int DeleteMovie(int movieId);
        int changeFinish(int id, bool isFinish);
        int UpdateView(int id);
        List<Movies> GetListMovies();
    }
}
