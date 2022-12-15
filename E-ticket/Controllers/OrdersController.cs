using E_ticket.Data.Cart;
using E_ticket.Data.Services;
using E_ticket.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace E_ticket.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _service;
        private readonly ShoppingCart _shoppingCart;//contains (service)method tto delet and add items from ShoppingCartItems Table
        private readonly IOrdersService _ordersService;
        public OrdersController(IOrdersService ordersService, IMoviesService service, ShoppingCart shoppingCart)
        {
            _service = service;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole= User.FindFirstValue(ClaimTypes.Role);

            var orders=await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items=_shoppingCart.GetShoppingCartItems();  //retriving items
            _shoppingCart.ShoppingCartItems = items; //setting items to list of items in shopping cart class

            var response = new ShoppingCartVM
            {
                ShoppingCart= _shoppingCart,
                ShoppingCartTotal=_shoppingCart.GetShoppingCartTotal(),
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)  //id refers to item id
        {
            var item = await _service.GetMovieByIdAsync(id);
            if (item is not null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)  //id refers to item id
        {
            var item = await _service.GetMovieByIdAsync(id);
            if (item is not null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress= User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }
    }
}
