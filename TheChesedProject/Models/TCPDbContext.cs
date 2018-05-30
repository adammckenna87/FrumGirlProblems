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

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Order> Orders { get; set; }
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
        public string OwnerFirstName { get; set; }
        public string OwnerLastName { get; set; }
        public string Description { get; set; }
        public string Conditions { get; set; }
        public string Image { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Community { get; set; }
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
    
    public class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<OrderItem>();
        }

        public int ID { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string BillingAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string State { get; set; }
        public string Locale { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
    }

    public class OrderItem 
    {

        public int ID { get; set; }
        public Order Cart { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int ProductID { get; internal set; }
        public string ProductName { get; internal set; }
        public decimal ProductPrice { get; internal set; }
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
