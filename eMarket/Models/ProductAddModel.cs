using eMarket.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eMarket.Models
{
    public class ProductAddModel
    {
        public int Number { get; set; }
        [Required(ErrorMessage = "Enter Product Name")]
        [Display(Name = "Product Name")]
        [StringLength(20, ErrorMessage = "Don't exceed 20 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        public Nullable<double> Price { get; set; }
        [Display(Name = "Upload File")]
        public string Img { get; set; }
        public string Descripition { get; set; }
        [Required(ErrorMessage = "Select Category")]
        public int CategryId { get; set; }
    }
}
