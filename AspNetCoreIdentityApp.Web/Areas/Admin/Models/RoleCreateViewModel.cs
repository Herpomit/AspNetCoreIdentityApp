﻿using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.Areas.Admin.Models
{
    public class RoleCreateViewModel
    {
        [Required(ErrorMessage = "Rol İsim alanı boş bırakılamaz!")]
        [Display(Name = "Rol İsmi :")]
        public string Name { get; set; }
    }
}
