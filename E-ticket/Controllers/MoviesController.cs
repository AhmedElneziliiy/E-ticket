using E_ticket.Data;
using E_ticket.Data.Services;
using E_ticket.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace E_ticket.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await _service.GetAllAsync(c=>c.Cinema);
            return View(movies);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

               // var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allMovies);
        }
        //get
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }
        //GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdown = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas,"Id","Name");
            ViewBag.Producers = new SelectList(movieDropdown.Producers,"Id","FullName");
            ViewBag.Actors = new SelectList(movieDropdown.Actors,"Id", "FullName");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdown = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //GET: Movies/Create
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if (movieDetails is null)
            {
                return View(nameof(NotFound));
            }
            var respose = new NewMovieVM
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate=movieDetails.EndDate,
                ImageURL = movieDetails.ImageUrl,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movies.Select(n => n.ActorId).ToList()
            };

            var movieDropdown = await _service.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");

            return View(respose);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if (id!=movie.Id)
            {
                return View(nameof(NotFound));
            }
            if (!ModelState.IsValid)
            {
                var movieDropdown = await _service.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdown.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdown.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdown.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }
    }
}
