using E_ticket.Data.Repository;
using E_ticket.Models;

namespace E_ticket.Data.Services
{
    public class CinemasService:BaseRepository<Cinema>,ICinemasService
    {
        public CinemasService(ApplicationDbContext context):base(context)
        {

        }
    }
}
