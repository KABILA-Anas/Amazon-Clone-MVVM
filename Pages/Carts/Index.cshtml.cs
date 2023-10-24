using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shoping.Models;
using System.Text.Json;

namespace Shoping.Pages.Carts
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
			//get cart from session or create new cart using serialization
			var item = HttpContext.Session.GetString("Cart");
			Cart cart = item == null ? new Cart() : JsonSerializer.Deserialize<Cart>(item);
			//store items in view bag
			ViewData["Items"] = cart.Items;
		}
    }
}
