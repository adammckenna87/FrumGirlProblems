using System;
using TheChesedProject.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TheChesedProject
{
    internal static class DbInitializer
    {
        internal static void Initialize(TCPDbContext db)
        {
            db.Database.Migrate();

            if (db.Products.Count() == 0)
            {
                db.Products.Add(new Product
                {
                    Description = "THE CHESED PROJECT T-SHIRT",
                    Image = @"\images\tshirt.jpg ",
                    Name = "THE CHESED PROJECT",
                    Price = 29.99m
                });

                db.Products.Add(new Product
                {
                    Description = "TCP T-SHIRT",
                    Image = @"\images\clothes.jpg",
                    Name = "TCP",
                    Price = 29.99m
                });

                db.SaveChanges();

            }
        }
    }
}