using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Categories;
using TAnime.Models.ViewModels.Movies;
using TAnime.Services;
using TAnime.Services_Repository;

namespace TAnime.Controllers
{
    public class MovieController : Controller
    {
        private readonly IAnimeService animeService;
        private readonly ICategoryRepository categoryRepository;

        public MovieController(IAnimeService animeService, ICategoryRepository categoryRepository)
        {
            this.animeService = animeService;
            this.categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            IEnumerable<MovieViewModel> movies = new List<MovieViewModel>();
            movies = animeService.GetMovies().ToList();
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
            var movie = new CreateMovie()
            {
                MovieName = model.MovieName,
                Content = model.Content
            };
            return View();
        }
        private List<CategoryViewModel> GetCategories()
        {
            return categoryRepository.Gets().ToList();
        }
    }
}
