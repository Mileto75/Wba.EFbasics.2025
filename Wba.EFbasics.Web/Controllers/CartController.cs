using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Wba.EFbasics.Web.Data;
using Wba.EFbasics.Web.ViewModels;

namespace Wba.EFbasics.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly HorseDbContext _horseDbContext;

        public CartController(HorseDbContext horseDbContext)
        {
            _horseDbContext = horseDbContext;
        }

        public IActionResult Index()
        {
            //show the list of items in cart
            //create the viewmodel
            var cartIndexViewModel = new CartIndexViewModel();
            cartIndexViewModel.CartItems = new();
            //check the session
            if(HttpContext.Session.Keys.Contains("cartItems"))
            {
                //get the session var
                cartIndexViewModel.CartItems
                    = JsonSerializer.Deserialize<List<CartItemModel>>
                    (HttpContext.Session.GetString("cartItems"));
            }
            //pass to the view
            return View(cartIndexViewModel);
        }
        public async Task<IActionResult> Add(int id)
        {
            var horse = await _horseDbContext
                .Horses
                .FirstOrDefaultAsync(h => h.Id == id);
            if(horse is null)
            {
                return NotFound();
            }
            //create the viewmodel
            var cartIndexViewModel = new CartIndexViewModel();
            cartIndexViewModel.CartItems = new();
            //check if a session exists
            if(HttpContext.Session.Keys.Contains("cartItems"))
            {
                //get the session cartItems value
                cartIndexViewModel.CartItems
                    = JsonSerializer.Deserialize<List<CartItemModel>>
                    (HttpContext.Session.GetString("cartItems"));
                //check if in cart
                var horseInCart = cartIndexViewModel.CartItems
                        .FirstOrDefault(c => c.Id == horse.Id);
                if (horseInCart is not null)
                {
                    //change the quantity
                    horseInCart.Quantity++;
                    
                }
                else
                {
                    //if not => add to cartitems
                    cartIndexViewModel.CartItems
                        .Add(new CartItemModel
                        {
                            Id = horse.Id,
                            Quantity = 1,
                            Price = horse.Price,
                            Value = horse.Name
                        });
                }
            }
            else
            {
                //first horse in cart!
                cartIndexViewModel.CartItems.Add(new CartItemModel
                {
                    Id = horse.Id,
                    Quantity = 1,
                    Price = horse.Price,
                    Value = horse.Name
                });
            }
            //Add to session
            HttpContext.Session.SetString("cartItems",
                JsonSerializer.Serialize(cartIndexViewModel.CartItems));
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
