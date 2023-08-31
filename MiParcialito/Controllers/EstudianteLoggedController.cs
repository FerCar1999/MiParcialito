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
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserType")==null && HttpContext.Session.GetString("UserType")!="Estudiante")
            {
                return RedirectToAction("Estudiantes", "Login");
            }
            var estudiante = _context.Estudiantes.First(e => e.EstudianteId == HttpContext.Session.GetInt32("Id"));
            var inscripcion = _context.Inscripciones.Include(c => c.Curso).ThenInclude(u=>u.User).Where(e=>e.EstudianteId== HttpContext.Session.GetInt32("Id")).ToList();
            ViewBag.estudiante = estudiante;
            return View(inscripcion);
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

            //Checando si el estudiante ya está en ese curso

            var inscription_exist = await _context.Inscripciones.Where(e => e.EstudianteId == estudianteId.Value)
                .Where( e => e.CursoId == id).FirstOrDefaultAsync();

            if (inscription_exist != null) {
                return RedirectToAction(nameof(Index));
            }

            Estudiante estudiante = await _context.Estudiantes.FirstAsync(e=>e.EstudianteId==estudianteId.Value);
            Curso curso = await _context.Cursos.Include(u => u.User).ThenInclude(e => e.UserType).FirstAsync(e=>e.CursoId==id);

            Inscripcion inscripcion = new Inscripcion();
            inscripcion.CursoId = id;
            inscripcion.Curso = curso;
            inscripcion.EstudianteId = estudianteId.Value;
            inscripcion.Estudiante = estudiante;

            _context.Add(inscripcion);
            await _context.SaveChangesAsync();
            
            return RedirectToAction("Index", "EstudianteLogged");
        }
    }
}
