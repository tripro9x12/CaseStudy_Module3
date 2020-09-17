using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;

namespace TAnime.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<Country> GetCountries();
        Country GetCountry(int id);
        int CreateCountry(Country country);
        int EditCountry(Country country);
        int DeleteCountry(int id);
    }
}
