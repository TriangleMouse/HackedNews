using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
