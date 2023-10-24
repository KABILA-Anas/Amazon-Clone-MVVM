using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shoping.Models;

namespace Shoping.Data
{
    public class ShopingContext : DbContext
    {
        public ShopingContext (DbContextOptions<ShopingContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Product> Product { get; set; } = default!;

    }
}
