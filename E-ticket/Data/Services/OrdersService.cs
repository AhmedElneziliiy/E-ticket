using E_ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace E_ticket.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly ApplicationDbContext _context;

        public OrdersService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(oi => oi.OrderItems)
                .ThenInclude(m => m.Movie)
                .Where(u => u.UserId == userId).ToListAsync();
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order=new Order
            {
                UserId = userId,
                Email = userEmailAddress
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price

                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
