using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    public class CategorySalesViewModel
    {
        public string Category { get; set; }
        public int TotalQuantitySold { get; set; }
    }
}