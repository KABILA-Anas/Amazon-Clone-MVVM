using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Shoping.Data;
using Shoping.Models;

namespace Shoping.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly Shoping.Data.ShopingContext _context;

        public IndexModel(Shoping.Data.ShopingContext context)
        {
            _context = context;
        }

        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ViewData["header"] = "Categories";
            if (_context.Category != null)
            {
                Category = await _context.Category.ToListAsync();
            }
        }
    }
}
