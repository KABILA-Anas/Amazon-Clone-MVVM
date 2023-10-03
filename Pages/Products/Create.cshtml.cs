using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shoping.Data;
using Shoping.Models;

namespace Shoping.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly Shoping.Data.ShopingContext _context;

        public CreateModel(Shoping.Data.ShopingContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            /*if (!ModelState.IsValid || _context.Product == null || Product == null)
              {
                  return Page();
              }*/

            Category category = await _context.Category.FindAsync(Product.CategoryId);
            if (category == null)
            {
                return NotFound();
            }
            Product.Category = category;

            if (Product.ImageContent != null)
            {
                var fileName = Path.GetFileName(Product.ImageContent.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await Product.ImageContent.CopyToAsync(stream);
                }
                Product.Image = fileName;
            }

            _context.Product.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
