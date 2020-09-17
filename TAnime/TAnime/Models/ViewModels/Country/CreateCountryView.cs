using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.ViewModels.Country
{
    public class CreateCountryView
    {
        [Display(Name ="Quốc Gia")]
        [Required(ErrorMessage ="Không được để trống")]
        public string CountryName { get; set; }
    }
}
