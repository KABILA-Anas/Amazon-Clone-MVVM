using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Shoping.Data;
using Shoping.Models;

namespace Shoping.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly Shoping.Data.ShopingContext _context;

        public IndexModel(Shoping.Data.ShopingContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        public SelectList? Categories { get; set; }
        [BindProperty(SupportsGet = true)]
        public string? ProductCategory { get; set; }

        public async Task OnGetAsync()
        {
            ViewData["header"] = "Products";

            //get all categories from Categories table without Product table
            IQueryable<string> categoryQuery = from m in _context.Category
                                               select m.Name;

            IQueryable<Product> products = from m in _context.Product.Include(p => p.Category)
                           select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Name.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(ProductCategory))
            {
                products = products.Where(p => p.Category.Name == ProductCategory);
            }
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());
            Product = await products.ToListAsync();

        }

        //post method for adding product to cart
        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
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
