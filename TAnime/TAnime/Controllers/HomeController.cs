using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TAnime.Models;
using TAnime.Repositories;
using TAnime.Services;
using TAnime.Services_Repository;

namespace TAnime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnimeRepository animeRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICountryRepository countryRepository;

        public HomeController(ILogger<HomeController> logger,
                              IAnimeRepository animeRepository,
                               ICategoryRepository categoryRepository,                             
                               ICountryRepository countryRepository)
        {
            _logger = logger;
            this.animeRepository = animeRepository;
            this.categoryRepository = categoryRepository;
            this.countryRepository = countryRepository;
        }

        public IActionResult Index()
        {
            var movie = animeRepository.GetMovies().ToList();
            return View(movie);
        }
        public IActionResult Detail(int? id)
        {
            try
            {
                int.Parse(id.Value.ToString());
                var anime = animeRepository.GetMovieViewModel(id.Value);
                if (anime == null)
                {
                    return View();
                }
                return View(anime);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
