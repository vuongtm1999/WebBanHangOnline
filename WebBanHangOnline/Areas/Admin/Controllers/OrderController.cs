using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using PagedList;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var items = db.Orders.OrderByDescending(x => x.CreatedDate).ToList();

            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = pageNumber;
            return View(items.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult View(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }

        public ActionResult Partial_SanPham(int id)
        {
            var items = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(items);
        }

        [HttpPost]
        public ActionResult UpdateTT(int id, int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                db.Entry(item).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", Success = true });
            }
            return Json(new { message = "Unsuccess", Success = false });
        }

        public ActionResult AddDiscount(int id, string codeDiscountText, int customerId)
        {
            string keyUpper = codeDiscountText.ToUpper();
            var item = db.CodeDiscounts.FirstOrDefault(m => m.code.ToUpper().Contains(keyUpper));

            var order = db.Orders.Find(id);
            if(order.CodeDiscountId == null)
            {
                if (item != null)
                {
                    db.Orders.Attach((Models.EF.Order)order);
                    order.CodeDiscountId = item.Id;
                    order.TotalAmount = (decimal?)((float)order.TotalAmount * (1 - item.Discount));

                    db.CodeDiscounts.Attach((Models.EF.CodeDiscount)item);

                    item.OrderId = id;
                    item.CustomerId = customerId;
                    db.Entry(item).Property(x => x.CustomerId).IsModified = true;
                    db.Entry(item).Property(x => x.OrderId).IsModified = true;

                    db.SaveChanges();
                    return Json(new { message = "Success", Success = true });
                } else
                {
                    return Json(new { message = "Mã khuyến mãi không tồn tại!", Success = false });

                }
            } else
            {
                return Json(new { message = "Đơn hàng đã có mã khuyến mãi!", Success = false });
            }
        }

        [HttpPost]
        public ActionResult delete(int id)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }
    }
}