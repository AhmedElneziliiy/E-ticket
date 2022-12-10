using E_ticket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_ticket.Controllers
{
    public class ProducersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProducersController (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data= await _context.Producers.ToListAsync();
            return View(data);
        }
    }
}
