using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Identities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
    }
}
