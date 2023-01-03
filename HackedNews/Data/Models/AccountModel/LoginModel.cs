using System.ComponentModel.DataAnnotations;

namespace HackedNews.Data.Models.AccountModel
{
    public class LoginModel
    {
        [Required(ErrorMessage = "EmailTxt")]
        [EmailAddress]
        public string Name { get; set; }

        [Required(ErrorMessage = "PasswordTxt")]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}