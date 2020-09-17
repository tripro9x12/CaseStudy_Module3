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
    public class AnimeService : IAnimeService
    {
        private readonly AppDbContext context;

        public AnimeService(AppDbContext context)
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
                    CategoryId = c,
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
            var episode = from m in context.Movies
                          join e in context.Episodes on m.MovieId equals e._MovieId
                          select (new Episode()
                          {
                              EpisodeId = e.EpisodeId,
                              EpisodeCode = e.EpisodeCode,
                              VideoPath = e.VideoPath
                          });
            var movieCategories = from m in context.Movies
                                  join mc in context.MovieCategories on m.MovieId equals mc.MovieId
                                  join c in context.Categories on mc.CategoryId equals c.CategoryId
                                  select (new MovieCategoryViewModel()
                                  {
                                      MovieName = m.MovieName,
                                      CategoryName = c.categoryName
                                  });
            movies = (from m in context.Movies
                      join e in context.Episodes on m.MovieId equals e._MovieId
                      join ct in context.Countries on m._CountryId equals ct.CountryId
                      join mc in context.MovieCategories on m.MovieId equals mc.MovieId
                      join c in context.Categories on mc.CategoryId equals c.CategoryId
                      select (new MovieViewModel()
                      {
                          MovieId = m.MovieId,
                          MovieName = m.MovieName,
                          Content = m.Content,
                          Country = ct.CountryName,
                          ImageOfVideo = m.ImageOfVideo,
                          Episodes = episode.ToList(),
                          MovieCategories = movieCategories.ToList()
                      })).ToList();
            return movies;
        }

        public Movie GetMovie(int id)
        {
            return context.Movies.FirstOrDefault(m => m.MovieId == id);
        }

    }
}
