using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentityApp.Web.ViewModels
{
    public class SignUpViewModel
    {
        [Required(ErrorMessage ="Kullanıcı Adı alanı boş bırakılamaz!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage ="Email Formatı Yanlıştır!")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon alanı boş bırakılamaz!")]
        [Display(Name = "Telefon")]
        public string Phone { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz!")]
        [MinLength(6,ErrorMessage ="Şifreniz En az 6 karakter olabilir.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage ="Şifre aynı değildir!")]
        [Required(ErrorMessage = "Şifre Tekrar alanı boş bırakılamaz!")]
        [MinLength(6,ErrorMessage ="Şifreniz En az 6 karakter olabilir.")]
        [Display(Name = "Şifre Tekrar")]
        public string PasswordConfirm { get; set; }
    }
}
