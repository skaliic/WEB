using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Text.Json;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private static string _filePath = "users.json";

        private List<RegisterViewModel> LoadUsers()
        {
            if (!System.IO.File.Exists(_filePath)) return new List<RegisterViewModel>();
            var json = System.IO.File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<RegisterViewModel>>(json) ?? new List<RegisterViewModel>();
        }

        private void SaveUsers(List<RegisterViewModel> users)
        {
            var json = JsonSerializer.Serialize(users);
            System.IO.File.WriteAllText(_filePath, json);
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var users = LoadUsers();
            if (users.Any(u => u.Email == model.Email))
            {
                ModelState.AddModelError("Email", "Tento email je již registrován.");
                return View(model);
            }

            users.Add(model);
            SaveUsers(users);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var users = LoadUsers();
            var user = users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Špatný email nebo heslo.");
                return View(model);
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.Name);
            return RedirectToAction("Profile");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var email = HttpContext.Session.GetString("UserEmail");
            if (email == null) return RedirectToAction("Login");

            var model = new ProfileViewModel
            {
                Email = email,
                Name = HttpContext.Session.GetString("UserName") ?? ""
            };
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}