using E_ticket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_ticket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.Include(m=>m.Cinema).OrderBy(n=>n.Name).ToListAsync();
            return View(movies);
        }
    }
}
