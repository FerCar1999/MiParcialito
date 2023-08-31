using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiParcialito.Models;

namespace MiParcialito.Controllers
{
    public class PublicController : Controller
    {
        private readonly Cg102319Context _context;

        public PublicController(Cg102319Context context)
        {
            _context = context;
        }

        // GET: Public
        public async Task<IActionResult> Index()
        {
            var cg102319Context = _context.Cursos.Include(c => c.User);
            return View(await cg102319Context.ToListAsync());
        }
    }
}
