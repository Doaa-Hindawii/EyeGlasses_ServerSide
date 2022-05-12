using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class AppDB_Context : IdentityDbContext<User_Identity>
    {
        public AppDB_Context()
        {
        }
        public AppDB_Context(DbContextOptions<AppDB_Context> options) 
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EyeGlasses;Integrated Security=True;MultipleActiveResultSets=true");
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Shopping_Cart> Shopping_Carts { get; set; }
        public virtual DbSet<ShoppingCart_Item> ShoppingCart_Items { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_Item> Order_Items { get; set; }
    }
}

