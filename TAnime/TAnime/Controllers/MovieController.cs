using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Categories;
using TAnime.Models.ViewModels.Country;
using TAnime.Models.ViewModels.Movies;
using TAnime.Repositories;
using TAnime.Services;
using TAnime.Services_Repository;

namespace TAnime.Controllers
{
    public class MovieController : Controller
    {
        private readonly IAnimeRepository animeRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICountryRepository countryRepository;

        public MovieController(IAnimeRepository animeRepository, 
                               ICategoryRepository categoryRepository,
                               IWebHostEnvironment webHostEnvironment,
                               ICountryRepository countryRepository)
        {
            this.animeRepository = animeRepository;
            this.categoryRepository = categoryRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.countryRepository = countryRepository;
        }
        public IActionResult Index()
        {
            var movies = new List<MovieViewModel>();
            movies = animeRepository.GetMovies().ToList();
            return View(movies);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new CreateMovie()
                {
                    MovieName = model.MovieName,
                    Content = model.Content,
                    Time = model.Time,
                    Country = model.CountryId,
                    categories = model.categories
                };
                var fileName = string.Empty;
                if(model.Image != null)
                {
                    var UploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                    fileName = $"{Guid.NewGuid()}_{model.Image.FileName}";
                    var filePath = Path.Combine(UploadFolder, fileName);
                    using(var fs = new FileStream(filePath, FileMode.Create))
                    {
                        model.Image.CopyTo(fs);
                    }
                }
                movie.Imagepath = fileName;
                var newMovie = animeRepository.CreateMovie(movie);
                return RedirectToAction("Index", "Movie");

            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var anime = animeRepository.GetMovieView(id);
            var editAnime = new EditMovieViewModel()
            {
                MovieId = anime.MovieId,
                MovieName = anime.MovieName,
                ImageOfVideo = anime.ImageOfVideo,
                Content = anime.Content,
                Time = anime.Time,
                CountryId = anime._CountryId,
                categories = anime.Categories
            };
            ViewBag.Categories = GetCategories();
            ViewBag.Coutries = GetCoutries();
            return View(editAnime);
        }
        [HttpPost]
        public IActionResult Edit(EditMovieViewModel model)
        {
            return View();
        }

        private List<CategoryViewModel> GetCategories()
        {
            return categoryRepository.Gets().ToList();
        }
        private List<CountryViewModel> GetCoutries()
        {
            return countryRepository.GetCountries().ToList();
        }
    }
}
