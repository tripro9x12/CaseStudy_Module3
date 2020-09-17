using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TAnime.Models.Entities;
using TAnime.Models.ViewModels.Country;

namespace TAnime.Repositories
{
    public interface ICountryRepository
    {
        IEnumerable<CountryViewModel> GetCountries();
        Country GetCountry(int id);
        int CreateCountry(Country country);
        int EditCountry(Country country);
        int DeleteCountry(int id);
    }
}
