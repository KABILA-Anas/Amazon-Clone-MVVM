using Microsoft.AspNetCore.Mvc;
using Shoping.Models;
using System.Text.Json;

namespace Shoping.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddToCart(int productId)
        {
            //get cart from session or create new cart using serialization
            var item = HttpContext.Session.GetString("Cart");
            Cart cart = item == null ? new Cart() : JsonSerializer.Deserialize<Cart>(item);
            //add product to cart
            cart.AddItem(productId);
            //save cart to session
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));

            //redirect to cart page
            return RedirectToPage("/Carts/Index");
        }
    }
}
