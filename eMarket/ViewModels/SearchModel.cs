using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eMarket.Models
{
    public class SearchModel
    {
        [Display(Name=("Select a Category"))]
        public int CategoryId { get; set; }
        public IList CategoryList { get; set; }
        [Display(Name = "Search Text")]
        public string SearchText { get; set; }
    }
}