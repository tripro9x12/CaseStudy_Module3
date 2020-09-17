using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models;
using TAnime.Models.Entities;

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
            throw new NotImplementedException();
        }

        public int DeleteCountry(int id)
        {
            throw new NotImplementedException();
        }

        public int EditCountry(Country country)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Country> GetCountries()
        {
            return context.Countries;
        }

        public Country GetCountry(int id)
        {
            return context.Countries.FirstOrDefault(c => c.CountryId == id);
        }
    }
}
