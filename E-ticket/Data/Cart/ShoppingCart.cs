using E_ticket.Models;
using Microsoft.EntityFrameworkCore;

namespace E_ticket.Data.Cart
{
    public class ShoppingCart
    {
        public ApplicationDbContext _context { get; set; }
        public ShoppingCart(ApplicationDbContext context)
        {
            _context = context;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
    

        //shopping cart session
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context=services.GetService<ApplicationDbContext>();

            string cartId=session.GetString("CartId")??Guid.NewGuid().ToString();
            session.SetString("CartId",cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        
        //Get all shopping cart items
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems?? (ShoppingCartItems=_context.ShoppingCartItems
                .Where(n=>n.ShoppingCartId==ShoppingCartId)
                .Include(n=>n.Movie).ToList());
        }

        //calculate shopping cart items price
        public double GetShoppingCartTotal()
        {
            return _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amount).Sum();
        }

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id &&
                                                                             n.ShoppingCartId == ShoppingCartId);
            //if you first add item to shopping cart
            if (shoppingCartItem is null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem);
            }
            //in case you need to increase amount of that item 
            else
            {
                shoppingCartItem.Amount +=1;
            }

            _context.SaveChanges();

        }

        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems.FirstOrDefault(n => n.Movie.Id == movie.Id &&
                                                                            n.ShoppingCartId == ShoppingCartId);


            if (shoppingCartItem is not null)
            {
                if (shoppingCartItem.Amount>1)
                {
                    shoppingCartItem.Amount -= 1;
                }
                else //in case that amount is (1)
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            _context.SaveChanges();
        }

        public async Task ClearShoppingCartAsync()
        {
            var items=  await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
