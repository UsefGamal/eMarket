using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eMarket.Models
{
    public class ProductIndexModel
    {
       public  IEnumerable<ProductListingModel> ProductList { get; set; }
    }
}