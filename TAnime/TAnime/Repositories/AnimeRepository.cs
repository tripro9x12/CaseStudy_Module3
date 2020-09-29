using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TAnime.Models;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Movies;

namespace TAnime.Services
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AnimeRepository(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
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

            IEnumerable<MovieCategory> mcList = new List<MovieCategory>();
            mcList = (from mc in context.MovieCategories
                      where mc.MovieId == movieId
                      select mc
                      ).ToList();
            context.MovieCategories.RemoveRange(mcList);
            context.SaveChanges();
            var delMovie = GetMovie(movieId);
            if (!string.IsNullOrEmpty(delMovie.ImageOfVideo))
            {
                var delFile = Path.Combine(webHostEnvironment.WebRootPath, "images", delMovie.ImageOfVideo);
               File.Delete(delFile);
            }
            context.Movies.Remove(delMovie);
            return context.SaveChanges();
        }

        public int EditMovie(EditMovieViewModel movie)
        {
            IEnumerable<MovieCategory> mcList = new List<MovieCategory>();
            mcList = (from mc in context.MovieCategories
                      where mc.MovieId == movie.MovieId
                      select mc
                      ).ToList();
            context.MovieCategories.RemoveRange(mcList);
            context.SaveChanges();
            if(movie.categories != null)
            {
                IEnumerable<MovieCategory> addList = new List<MovieCategory>();
                addList = movie.categories.Select(c => new MovieCategory()
                {
                    CategoryId = c,
                    MovieId = movie.MovieId
                }).ToList();
                context.MovieCategories.AddRange(addList);
                context.SaveChanges();
            }
            var editMovie = context.Movies.Attach(new Movie()
            {
                MovieId = movie.MovieId,
                MovieName = movie.MovieName,
                Content = movie.Content,
                Time = movie.Time,
                _CountryId = movie.CountryId,
                ImageOfVideo = movie.ImageOfVideo
            });
            editMovie.State = EntityState.Modified;
            return context.SaveChanges();
        }

        public MovieViewModel GetMovieViewModel(int id)
        {
            IEnumerable<MovieViewModel> movies = new List<MovieViewModel>();
            movies = GetMovies().ToList();
            List<Episode> episodes = new List<Episode>();
            foreach(var movie in movies)
            {
                if(movie.MovieId == id)
                {
                   foreach(var item in context.Episodes)
                   {
                        if(item._MovieId == id)
                        {
                            episodes.Add(item);
                        }
                   }
                    movie.Episodes = episodes;
                    return movie;
                }
            }
            return movies.FirstOrDefault(m=>m.MovieId == id);
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
                          ImageOfVideo = m.ImageOfVideo,
                          CountryId = c.CountryId
                      }).ToList();
            
            foreach (var movie in movies)
            {
                movie.Categories = (from c in context.Categories
                                    join mc in context.MovieCategories on c.CategoryId equals mc.CategoryId
                                    where mc.MovieId == movie.MovieId
                                    select c).ToList();
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
                                    select c.CategoryId).ToList();
            }
            return movies.FirstOrDefault(m=>m.MovieId == id);
        }

        //chỉ lấy danh sách tên phim;
        public List<Movies> GetListMovies()
        {
            List<Movies> ListMovies = (from m in context.Movies
                                        select new Movies()
                                        {
                                            MovieId = m.MovieId,
                                            MovieName = m.MovieName
                                        }).ToList();
            return ListMovies;
        }

        public IEnumerable<MovieViewModel> GetMoviesOfCategory(int CategoryId)
        {
            var model = GetMovies().ToList();
            List <MovieViewModel> movies= new List<MovieViewModel>();
            foreach (var items in model)
            {
                foreach (var item in items.Categories)
                {
                    if(item.CategoryId == CategoryId)
                    {
                        movies.Add(items);
                    }
                }
            }
            return movies;
        }

        public IEnumerable<MovieViewModel> GetMoviesOfCountry(int countryId)
        {
            var model = GetMovies().ToList();
            List<MovieViewModel> movies = new List<MovieViewModel>();
            foreach(var items in model)
            {
                if(items.CountryId == countryId)
                {
                    movies.Add(items);
                }
            }
            return movies;
        }
    }
}
