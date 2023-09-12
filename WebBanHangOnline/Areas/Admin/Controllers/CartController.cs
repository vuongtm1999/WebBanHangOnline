using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{

    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/Cart
        public ActionResult Index()
        {
            return View();
        }

        private List<CartModel> GetListCarts()
        {
            List<CartModel> carts = Session["CartModel"] as List<CartModel>;
            if (carts == null)//chua co sp nao trong gio hang
            {
                carts = new List<CartModel>();
                Session["CartModel"] = carts;
            }
            return carts;
        }

        // GET: lấy DSSp có tromg giỏ hàng
        public ActionResult ListCarts()
        {
            List<CartModel> carts = GetListCarts();

            ViewBag.CountProduct = carts.Sum(s => s.Quantity);
            ViewBag.Total = carts.Sum(s => s.Total);

            return View(carts);
        }

        public ActionResult AddCart(int id)
        {
            //lấy DSSP co trong gio hang
            List<CartModel> carts = GetListCarts();
            CartModel c = carts.Find(s => s.ProductID == id);
            if (c == null)
            {
                c = new CartModel(id);
                carts.Add(c);
            }
            else
            {
                c.Quantity++;
            }

            return RedirectToAction("ListCarts");
        }

        public ActionResult OrderProduct()
        {
            using (TransactionScope tranScope = new TransactionScope())
            {
                try
                {
                    Order order = new Order();

                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;

                    List<CartModel> carts = GetListCarts();
                    order.CustomerId = 1;
                    order.TotalAmount = carts.Sum(s => s.Total);
                    order.Quantity = carts.Sum(s => s.Quantity);
                    order.TypePayment = 1;
                    order.CreatedBy = "admin";



                    foreach (var item in GetListCarts())
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderId = order.Id;
                        od.ProductId = item.ProductID;
                        od.Price = decimal.Parse(item.UnitPrice.ToString());
                        od.Quantity = short.Parse(item.Quantity.ToString());
                        od.Discount = 0;
                        db.OrderDetails.Add(od);
                    }

                    db.Orders.Add(order);
                    db.SaveChanges();
                    Session.Remove("CartModel");

                    tranScope.Complete();
                }
                catch (Exception)
                {
                    tranScope.Dispose();
                }
            }

            return RedirectToAction("index", "Order");
        }

        public ActionResult DeleteCart(int id)
        {
            // Lấy danh sách CartModel từ session
            List<CartModel> cart = Session["CartModel"] as List<CartModel>;

            if (cart != null)
            {
                // Tìm và xóa dòng dữ liệu có itemId tương ứng
                CartModel itemToRemove = cart.FirstOrDefault(item => item.ProductID == id);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                }

                // Lưu lại danh sách cập nhật vào session
                Session["CartModel"] = cart;
            }

            return RedirectToAction("ListCarts");
        }
    }
}