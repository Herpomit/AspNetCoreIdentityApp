using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class PasswordChangeViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [Display(Name = "Şifre")]
        [MinLength(6,ErrorMessage ="Şifreniz En az 6 karakter olabilir.")]
        public string PasswordOld { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [MinLength(6,ErrorMessage ="Şifreniz En az 6 karakter olabilir.")]
        [Display(Name = "Yeni Şifre")]
        public string PasswordNew { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew), ErrorMessage = "Şifre aynı değildir!")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz!")]
        [MinLength(6,ErrorMessage ="Şifreniz En az 6 karakter olabilir.")]
        [Display(Name = "Şifre Tekrar")]
        public string PasswordNewConfirm { get; set; }
    }
}
