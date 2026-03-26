using System.ComponentModel.DataAnnotations;
namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";

        public string PasswordHash { get; set; } = ""; // přidej tento řádek
    }
}