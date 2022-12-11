using E_ticket.Data;
using E_ticket.Data.Services;
using E_ticket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Formats.Asn1;

namespace E_ticket.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfileURL", "FullName", "Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor is null)
            {
                return View("NotFound");
            }
            return View(actor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor is null)
            {
                return View("NotFound");
            }
            return View(actor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,/*[Bind("ProfileURL", "FullName", "Bio")]*/ Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id,actor);
            return RedirectToAction(nameof(Index));
        }
        // GET
        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor is null)
            {
                return View("NotFound");
            }
            return View(actor);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor= await _service.GetByIdAsync(id);
            if (actor is null)
            {
                return View("NotFound");
            }

            await _service.DeleteAsync(id);
               
            return RedirectToAction(nameof(Index));
        }
    }
}
