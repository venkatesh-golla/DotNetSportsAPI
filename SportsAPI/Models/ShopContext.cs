using HPlusSport.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsAPI.Models
{
    public class ShopContext:DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasMany(p => p.Products).WithOne(c => c.Category).HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<Order>().HasMany(p => p.Products);
            modelBuilder.Entity<Order>().HasOne(u => u.User);
            modelBuilder.Entity<User>().HasMany(o => o.Orders).WithOne(u => u.User).HasForeignKey(u => u.UserId);
            modelBuilder.Seed();
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
