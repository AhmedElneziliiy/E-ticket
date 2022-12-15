using E_ticket.Models;

namespace E_ticket.Data.Services
{
    public interface IOrdersService
    {
        //Add order-orderitems- To DB
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        
        //get all items from DB
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
