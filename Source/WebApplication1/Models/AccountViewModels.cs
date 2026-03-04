using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Jméno je povinné")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress(ErrorMessage = "Neplatný email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Heslo je povinné")]
        [MinLength(6, ErrorMessage = "Heslo musí mít alespoň 6 znaků")]
        public string Password { get; set; } = "";
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email je povinný")]
        [EmailAddress(ErrorMessage = "Neplatný email")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Heslo je povinné")]
        public string Password { get; set; } = "";
    }

    public class ProfileViewModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
    }
}