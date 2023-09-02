using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [Display(Name = "Yeni Şifre :")]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Şifre aynı değildir!")]
        [Required(ErrorMessage = "Yeni Şifre Tekrar alanı boş bırakılamaz!")]
        [Display(Name = "Yeni Şifre Tekrar :")]
        public string PasswordConfirm { get; set; }
    }
}
