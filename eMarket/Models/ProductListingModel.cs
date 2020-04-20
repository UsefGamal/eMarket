using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMarket.Models
{
    public class ProductListingModel
    {
        public int Number { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Descripton { get; set; }
        public string Img { get; set; }


        public CategoryListingModel Category { get; set; }
    }
}