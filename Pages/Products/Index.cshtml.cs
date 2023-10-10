using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            IQueryable<string> categoryQuery = from m in _context.Category
                                               select m.Name;
            Categories = new SelectList(await categoryQuery.Distinct().ToListAsync());

            if (_context.Product != null)
            {
                if (string.IsNullOrEmpty(SearchString) && string.IsNullOrEmpty(ProductCategory))
                {
                    Product = await _context.Product
                    .Include(p => p.Category).ToListAsync();
                }

                if (!string.IsNullOrEmpty(SearchString) && !string.IsNullOrEmpty(ProductCategory))
                {
                    Product = await _context.Product
                    .Include(p => p.Category)
                    .Where(p => p.Category.Name == ProductCategory)
                    .Where(p => p.Name.Contains(SearchString))
                    .ToListAsync();
                }

                if (!string.IsNullOrEmpty(SearchString) && string.IsNullOrEmpty(ProductCategory))
                {
                    Product = await _context.Product
                    .Include(p => p.Category)
                    .Where(p => p.Name.Contains(SearchString))
                    .ToListAsync();
                }

                if (string.IsNullOrEmpty(SearchString) && !string.IsNullOrEmpty(ProductCategory))
                {
                    Product = await _context.Product
                    .Include(p => p.Category)
                    .Where(p => p.Category.Name == ProductCategory)
                    .ToListAsync();
                }

            }
        }
    }
}
