using E_ticket.Data.Repository;
using E_ticket.Data.ViewModels;
using E_ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace E_ticket.Data.Services
{
    public class MoviesService : BaseRepository<Movie>, IMoviesService
    {
        private readonly ApplicationDbContext _context;
        public MoviesService(ApplicationDbContext context) : base(context) { _context = context; }

        public async Task AddNewMovieAsync(NewMovieVM movieVM)
        {
            var movie = new Movie
            {
                Name = movieVM.Name,
                Description = movieVM.Description,
                Price = movieVM.Price,
                ImageUrl = movieVM.ImageURL,
                CinemaId = movieVM.CinemaId,
                StartDate = movieVM.StartDate,
                EndDate = movieVM.EndDate,
                MovieCategory = movieVM.MovieCategory,
                ProducerId = movieVM.ProducerId
            };
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            foreach (var actorId in movieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId = movie.Id, //after saving changes id generated automatically
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actor_Movies).ThenInclude(a => a.Actor)
                .SingleOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var respose = new NewMovieDropdownsVM
            {
                Actors = await _context.Actors.OrderBy(a => a.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(c => c.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(c => c.FullName).ToListAsync()
            };
            return respose;

        }

        public async Task UpdateMovieAsync(NewMovieVM movieVM)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == movieVM.Id);
            if (dbMovie != null)
            {
                dbMovie.Name = movieVM.Name;
                dbMovie.Description = movieVM.Description;
                dbMovie.Price = movieVM.Price;
                dbMovie.ImageUrl = movieVM.ImageURL;
                dbMovie.CinemaId = movieVM.CinemaId;
                dbMovie.StartDate = movieVM.StartDate;
                dbMovie.EndDate = movieVM.EndDate;
                dbMovie.MovieCategory = movieVM.MovieCategory;
                dbMovie.ProducerId = movieVM.ProducerId;
                await _context.SaveChangesAsync();
            }
            //Remove existting actors

            var existingActorsDb = _context.Actors_Movies.Where(a => a.MovieId == movieVM.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            foreach (var actorId in movieVM.ActorIds)
            {
                var newActorMovie = new Actor_Movie
                {
                    MovieId = movieVM.Id, //after saving changes id generated automatically
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
