using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace TheChesedProject.Models
{
    public class Product {
       
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        CultureInfo currentCulture = CultureInfo.GetCultureInfo("en-US");
        public decimal? Price { get; set; }
        public string Image { get; set; }
        //public string[] Sizes { get; set; }
    }
}
