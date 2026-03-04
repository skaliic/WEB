 using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Models
{
    public class AccountViewModels : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
