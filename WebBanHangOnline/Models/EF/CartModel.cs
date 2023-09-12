using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    public class CartModel
    {
        ApplicationDbContext da = new ApplicationDbContext();
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get { return UnitPrice * Quantity; } }
        public CartModel(int productID)
        {
            Product p = da.Products.FirstOrDefault(s => s.Id == productID);
            this.ProductID = p.Id;
            this.ProductName = p.Title;
            this.UnitPrice = p.Price;
            this.Quantity = 1;
        }
    }
}