using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TAnime.Models.Identities
{
    public class CreateUserViewModel : RegisterUserViewModel
    {
        [Display(Name ="Ủy Quyền")]
        public string RoleId { get; set; }
    }
}
