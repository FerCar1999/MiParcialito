using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;
using System.Diagnostics;

namespace MiParcialito.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Cg102319Context _context;

        public HomeController(ILogger<HomeController> logger, Cg102319Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserType")!=null ||
                HttpContext.Session.GetString("UserType") == "Administrador" ||
                HttpContext.Session.GetString("UserType") == "Profesor")
            {
                var user = await _context.Users
                .Include(u => u.UserType)
                .FirstAsync(m => m.Id == HttpContext.Session.GetInt32("Id"));
                return View(user);
            }
            else
            {
                return RedirectToAction("Usuario", "Login");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}