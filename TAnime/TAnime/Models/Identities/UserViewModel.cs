﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Identities
{
    public class UserViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string RolesName { get; set; }
        public string Avatar { get; set; }
    }
}
