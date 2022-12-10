using E_ticket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace E_ticket.Controllers
{
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Actors.ToListAsync();
            return View(data);
        }
    }
}
