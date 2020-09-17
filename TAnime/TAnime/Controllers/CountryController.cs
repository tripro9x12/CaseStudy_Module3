using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Country;
using TAnime.Repositories;

namespace TAnime.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            this.countryRepository = countryRepository;
        }
        public IActionResult Index()
        {
            var countries = countryRepository.GetCountries().ToList();
            return View(countries);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCountryView model)
        {
            if (ModelState.IsValid)
            {
                var countries = countryRepository.GetCountries().ToList();
                foreach (var item in countries)
                {
                    if (item.CountryName == model.CountryName)
                    {
                        ModelState.AddModelError(" ", "Quốc gia đã tồn tại");
                        return View();
                    }
                }
                var country = new Country()
                {
                    CountryName = model.CountryName
                };
                var result = countryRepository.CreateCountry(country);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Country");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var country = countryRepository.GetCountry(id);
            var editCountry = new EditCountryView()
            {
                CountryId = country.CountryId,
                CountryName = country.CountryName
            };
            return View(editCountry);
        }
        [HttpPost]
        public IActionResult Edit(EditCountryView model)
        {
            if (ModelState.IsValid)
            {
                var countries = countryRepository.GetCountries().ToList();
                foreach (var item in countries)
                {
                    if (item.CountryName == model.CountryName)
                    {
                        ModelState.AddModelError(" ", "Quốc gia đã tồn tại");
                        return View();
                    }
                }
                var country = new Country()
                {
                    CountryId = model.CountryId,
                    CountryName = model.CountryName
                };
                var result = countryRepository.EditCountry(country);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Country");
                }
            }
            return View(model);
        }
        [Route("/Country/Delete/{CountryId}")]
        public IActionResult Delete(int CountryId)
        {
            var delCountry = countryRepository.DeleteCountry(CountryId);
            return Json(new { delCountry });
        }
    }
}
