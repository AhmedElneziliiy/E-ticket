using E_ticket.Data.Repository;
using E_ticket.Data.ViewModels;
using E_ticket.Models;

namespace E_ticket.Data.Services
{
    public interface IMoviesService: IBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues();
        Task AddNewMovieAsync(NewMovieVM movieVM);
        Task UpdateMovieAsync(NewMovieVM movieVM);
    }
}
