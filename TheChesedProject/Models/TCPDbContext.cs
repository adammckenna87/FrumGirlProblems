using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TheChesedProject.Models
{
    public class TCPDbContext : IdentityDbContext<TCPUser>
    {

        public TCPDbContext() : base()
        {

        }

        public TCPDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Gemach> Gemachs { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TestEntity> TestEntities { get; set; }
    }

    public class Gemach
    {
        public int ID { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public int  TimeOpen { get; set; }
        public int TimeClose { get; set; }
    }

    public class Cart
    {
        public Cart()
        {
            this.CartItems = new HashSet<CartItem>();
        }

        public int ID { get; set; }
        public Guid CookieIdentifier { get; set; }
        public DateTime LastModified { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

    }

    public class CartItem
    {
        public int ID { get; set; }
        public Cart Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }


    public class TCPUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class TestEntity
    {
        public int id { get; set; }
        public string MyProperty { get; set; }
    }
}
