using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;

namespace MiParcialito.Controllers
{
    public class LoginController : Controller
    {
        private readonly Cg102319Context _context;

        public LoginController(Cg102319Context context)
        {
            _context = context;
        }

        public IActionResult Estudiantes()
        {
            return View("LoginEstudiantes");
        }
        public IActionResult Usuario()
        {
            return View("LoginUser");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser([Bind("Email,Password")] User user)
        {
            User userexist = await _context.Users.Include(ut=>ut.UserType).FirstAsync(u => u.Email == user.Email);
            if (userexist != null && BCrypt.Net.BCrypt.Verify(user.Password, userexist?.Password))
            {
                HttpContext.Session.SetString("UserType",userexist.UserType.Nombre);
                HttpContext.Session.SetInt32("Id",userexist.Id);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return View("LoginUser");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginEstudiante([Bind("Correo,Password")] Estudiante estudiante)
        {
            Estudiante estudianteexist = await _context.Estudiantes.FirstAsync(e => e.Correo == estudiante.Correo);
            if (estudianteexist != null && BCrypt.Net.BCrypt.Verify(estudiante.Password, estudianteexist?.Password))
            {
                HttpContext.Session.SetString("UserType", "Estudiante");
                HttpContext.Session.SetInt32("Id", estudianteexist.EstudianteId);
                return RedirectToAction("Index", "EstudianteLogged");
            }
            else
            {
                return View("LoginEstudiantes");
            }
        }
    }   
}
