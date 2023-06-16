using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace PrimeStore.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [DisplayName("Имя пользователя")]
        public string Username { get; set; }

        [Required]
        [DisplayName("Логин")]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Пароль")]
        public string Password { get;set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DisplayName("Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
