using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Entities
{
    public class Country
    {
        public int CountryId { get; set; }
        [MaxLength(100)]
        [Required]
        public string CountryName { get; set; }
        public ICollection<Movie> movies { get; set; }
    }
}
