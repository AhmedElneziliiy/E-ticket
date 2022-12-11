using E_ticket.Data.Repository;
using E_ticket.Models;

namespace E_ticket.Data.Services
{
    public class ProducersService:BaseRepository<Producer> ,IProducersService
    {
        public ProducersService(ApplicationDbContext context):base(context)
        {
        }
    }
}
