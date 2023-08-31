using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;

namespace MiParcialito.Controllers
{
    public class EstudianteLoggedController : Controller
    {
        private readonly Cg102319Context _context;
        public EstudianteLoggedController(Cg102319Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserType")==null && HttpContext.Session.GetString("UserType")!="Estudiante")
            {
                return RedirectToAction("Estudiantes", "Login");
            }
            var estudiante = await _context.Estudiantes.Include(e=>e.Inscripciones.Select(c=>c.Curso)).FirstAsync(e => e.EstudianteId == HttpContext.Session.GetInt32("Id"));
            var cursos = await _context.Inscripciones.Where(e=>e.EstudianteId== HttpContext.Session.GetInt32("Id")).Include(c=>c.Curso).ToListAsync();
            ViewBag.Estudiante = estudiante;
            return View(estudiante);
        }

        public async Task<IActionResult> RegistroCurso()
        {
            var cg102319Context = _context.Cursos.Include(c => c.User);
            return View(await cg102319Context.ToListAsync());
        }

        public async Task<IActionResult> InscripcionCurso(int id)
        {
            int? estudianteId = HttpContext.Session.GetInt32("Id");

            if (estudianteId == null)
            {
                return NotFound();
            }

            Inscripcion inscripcion = new Inscripcion();
            inscripcion.CursoId = id;
            inscripcion.EstudianteId = estudianteId.Value;

            _context.Add(inscripcion);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "EstudianteLogged");
        }
    }
}
