using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Movies;

namespace TAnime.Services
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AppDbContext context;

        public AnimeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public int CreateMovie(CreateMovie movie)
        {
            var newmovie = new Movie()
            {
                MovieName = movie.MovieName,
                Time = movie.Time,
                Content = movie.Content,
                _CountryId = movie.Country,
                ImageOfVideo = movie.Imagepath
            };
            
            context.Movies.Add(newmovie);
            context.SaveChanges();
            if(movie.categories != null)
            {
                var mcList = new List<MovieCategory>();
                mcList = movie.categories.Select(c => new MovieCategory()
                {
                    CategoryId = c.CategoryId,
                    MovieId = newmovie.MovieId
                }).ToList();
                context.MovieCategories.AddRange(mcList);
                movie.MovieId = newmovie.MovieId;
            }
            return context.SaveChanges();
        }

        public int DeleteMovie(int movieId)
        {
            var delMovie = GetMovie(movieId);
            context.Movies.Remove(delMovie);
            return context.SaveChanges();
        }

        public int EditMovie(Movie movie)
        {
            var editMovie = context.Movies.Attach(movie);
            editMovie.State = EntityState.Modified;
            return context.SaveChanges();
        }

        public MovieViewModel GetMovieViewModel(int id)
        {
            IEnumerable<MovieViewModel> movies = new List<MovieViewModel>();
            MovieViewModel movie = new MovieViewModel();
            movie = GetMovies().FirstOrDefault(m => m.MovieId == id);
            return movie;
        }

        public IEnumerable<MovieViewModel> GetMovies()
        {
            IEnumerable<MovieViewModel> movies = new List<MovieViewModel>();
            movies = (from m in context.Movies
                      join c in context.Countries on m._CountryId equals c.CountryId
                      select new MovieViewModel()
                      {
                          MovieId = m.MovieId,
                          MovieName = m.MovieName,
                          Content = m.Content,
                          Country = c.CountryName,
                          Time = m.Time,                     
                      }).ToList();
            
            foreach (var movie in movies)
            {
                movie.Categories = (from c in context.Categories
                                    join mc in context.MovieCategories on c.CategoryId equals mc.CategoryId
                                    where mc.MovieId == movie.MovieId
                                    select c.categoryName).ToList();
            }
           
            return movies;
        }

        public Movie GetMovie(int id)
        {
            return context.Movies.FirstOrDefault(m => m.MovieId == id);
        }

        public GetMovie GetMovieView(int id)
        {
            List<GetMovie> movies = new List<GetMovie>();
            movies = (from m in context.Movies
                      join c in context.Countries on m._CountryId equals c.CountryId
                      select new GetMovie()
                      {
                          MovieId = m.MovieId,
                          MovieName = m.MovieName,
                          Time = m.Time,
                          Content = m.Content,
                          ImageOfVideo = m.ImageOfVideo,
                          _CountryId = c.CountryId
                      }).ToList();
            foreach (var movie in movies)
            {
                movie.Categories = (from c in context.Categories
                                    join mc in context.MovieCategories on c.CategoryId equals mc.CategoryId
                                    where mc.MovieId == movie.MovieId
                                    select c).ToList();
            }
            return movies.FirstOrDefault(m=>m.MovieId == id);
        }
    }
}
