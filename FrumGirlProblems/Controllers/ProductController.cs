using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FrumGirlProblems.Models;

namespace FrumGirlProblems.Controllers
{
    public class ProductController : Controller
    {

        private List<Product> _products;

        public ProductController()
        {
            _products = new List<Product>
            {
                new Product
                {
                    ID = 1,
                    Name = "THE CHESED PROJECT",
                    Description = "THE CHESED PROJECT T-Shirt",
                    Image = "/images/clothes.jpg",
                    Price = 29.99m,
                    //Sizes = new string[] {"Small", "Medium", "Large" }
                },

                new Product
                {
                    ID = 2,
                    Name = "TCP",
                    Description = "TCP T-Shirt",
                    Image = "/images/tshirt.jpg",
                    Price = 29.99m,
                    //Sizes = new string[] {"Small", "Medium", "Large" }
                }

            };
        }


        public IActionResult Index()
        {

            ////For now, I'm using a List to mock up my product data
            //List<Product> products = new List<Product>();

            //string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            //using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
           
            ////While the connection is open, I can use it to run statements from code.
            //{
            //    connection.Open();

            //    using (System.Data.SqlClient.SqlCommand command = connection.CreateCommand())
            //    {
            //        //I usually write a query that I want in the Query window, and copy it here when I'm happy with how it works...
            //        //Realistically, I should make my query a stored procedure and call that instead.  It'll run faster that way.
            //        command.CommandText = "sp_GetAllProducts";
            //        command.CommandType = System.Data.CommandType.StoredProcedure;
            //        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
            //        {
            //            int idColumn = reader.GetOrdinal("ProductID");
            //            int nameColumn = reader.GetOrdinal("ProductName");
            //            int descriptionColumn = reader.GetOrdinal("ProductDescription");

            //            while (reader.Read())
            //            {
            //                int productId = reader.GetInt32(idColumn);
            //                string name = reader.GetString(nameColumn);
            //                string description = reader.GetString(descriptionColumn);
            //                products.Add(new Product
            //                {
            //                    ID = productId,
            //                    Description = description,
            //                    Name = name
            //                });
            //            }
            //        }
            //    }

            //    foreach (var product in products)
            //    {
            //        using (System.Data.SqlClient.SqlCommand imageAndPriceCommand = connection.CreateCommand())
            //        {
            //            imageAndPriceCommand.CommandText = "sp_GetProductImages";
            //            imageAndPriceCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //            imageAndPriceCommand.Parameters.AddWithValue("@productID", product.ID);
            //            using (System.Data.SqlClient.SqlDataReader reader2 = imageAndPriceCommand.ExecuteReader())
            //            {
            //                while (reader2.Read())
            //                {
            //                    product.Price = reader2.IsDBNull(0) ? (decimal?)null : reader2.GetSqlMoney(0).ToDecimal();
            //                    byte[] imageBytes = (byte[])reader2[1];
            //                    product.Image = "data:image/jpeg;base64, " + Convert.ToBase64String(imageBytes);
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
            //By passing a parameter to my View method, I'm passing it to the CSHTML so it can be "bound" up to the View.
            return View();
        }
        public IActionResult Details(int? id)
        {
            //if (id.HasValue)
            //{
            //    Product p = null;
            //    string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=AdventureWorks2016;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            //    using (System.Data.SqlClient.SqlConnection connection = new System.Data.SqlClient.SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        System.Data.SqlClient.SqlCommand command = connection.CreateCommand();

            //        //I usually write a query that I want in the Query window, and copy it here when I'm happy with how it works...
            //        //Realistically, I should make my query a stored procedure and call that instead.  It'll run faster that way.
            //        command.CommandText = "sp_GetProduct";
            //        command.CommandType = System.Data.CommandType.StoredProcedure;
            //        command.Parameters.AddWithValue("@productModelID", id.Value);

            //        using (System.Data.SqlClient.SqlDataReader reader = command.ExecuteReader())
            //        {


            //            while (reader.Read())
            //            {
            //                p = new Product
            //                {
            //                    ID = reader.GetInt32(0),
            //                    Name = reader.GetString(1),
            //                    Description = reader.GetString(2)
            //                };

            //            }
            //        }
            //        if (p != null)
            //        {
            //            using (System.Data.SqlClient.SqlCommand imageAndPriceCommand = connection.CreateCommand())
            //            {
            //                imageAndPriceCommand.CommandText = "sp_GetProductImages";
            //                imageAndPriceCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //                imageAndPriceCommand.Parameters.AddWithValue("@productModelID", p.ID);
            //                using (System.Data.SqlClient.SqlDataReader reader2 = imageAndPriceCommand.ExecuteReader())
            //                {
            //                    while (reader2.Read())
            //                    {
            //                        p.Price = reader2.IsDBNull(0) ? (decimal?)null : reader2.GetSqlMoney(0).ToDecimal();
            //                        byte[] imageBytes = (byte[])reader2[1];
            //                        p.Image = "data:image/jpeg;base64, " + Convert.ToBase64String(imageBytes);
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    if (p != null)
            //    {
            //        return View(p);
            //    }
            //}
            return View();
        }
    }
}