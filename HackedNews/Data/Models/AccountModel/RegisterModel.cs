using System.ComponentModel.DataAnnotations;

namespace HackedNews.Data.Models.AccountModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "EmailTxt")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "PasswordTxt")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "PasswordLength", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "PasswordConfirmTxt")]
        [Compare("Password", ErrorMessage = "PasswordConfirmTxtComapre")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
    }
}