using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TAnime.Models.Identities
{
    public class CreateRoleViewModel
    {
        
        [Required(ErrorMessage ="Trường này không được để trống")]
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
