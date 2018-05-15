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

        public DbSet<Product> Products { get; set; }
        public DbSet<TestEntity> TestEntities { get; set; }
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
