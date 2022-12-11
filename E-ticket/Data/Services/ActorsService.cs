using E_ticket.Data.Repository;
using E_ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace E_ticket.Data.Services
{
    public class ActorsService : BaseRepository<Actor>,IActorsService
    {
        public ActorsService(ApplicationDbContext context) : base(context) { }
        
    }
}
