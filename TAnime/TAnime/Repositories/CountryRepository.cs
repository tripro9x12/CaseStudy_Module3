using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Country;

namespace TAnime.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext context;

        public CountryRepository(AppDbContext context)
        {
            this.context = context;
        }
        public int CreateCountry(Country country)
        {
            context.Countries.Add(country);
            return context.SaveChanges();
        }

        public int DeleteCountry(int id)
        {
            var delCountry = GetCountry(id);
            context.Countries.Remove(delCountry);
            return context.SaveChanges();
        }

        public int EditCountry(Country country)
        {
            var editCountry = context.Countries.Attach(country);
            editCountry.State = EntityState.Modified;
            return context.SaveChanges();

        }

        public IEnumerable<CountryViewModel> GetCountries()
        {
            IEnumerable<CountryViewModel> countries = new List<CountryViewModel>();
            countries = context.Countries.Select(c => new CountryViewModel()
            {
                CountryId = c.CountryId,
                CountryName = c.CountryName
            });
            return countries;
        }

        public Country GetCountry(int id)
        {
            return context.Countries.FirstOrDefault(c => c.CountryId == id);
        }
    }
}
