using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        // GET: Admin/Statistical
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpGet]
        //Thong Ke theo ngay
        public ActionResult GetStatistical(string fromDate, string toDate)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails
                        on o.Id equals od.OrderId
                        join p in db.Products
                        on od.ProductId equals p.Id
                        select new
                        {
                            CreateDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            PriceSell = o.TotalAmount,
                            OriginalPrice = p.OriginalPrice
                        };

            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }

            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyy", null);
                query = query.Where(x => x.CreateDate <= endDate);
            }

            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreateDate)).Select(x=>new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y=>y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y=>y.PriceSell)
            }).Select(x=>new {
                Date=x.Date,
                DoanhThu = x.TotalSell,
                LoiNhuan =  x.TotalSell - x.TotalBuy
            });

            return Json(new { Data = result } , JsonRequestBehavior.AllowGet );
        }

        public ActionResult ThongKeTheoThang()
        {
            return View();
        }

        [HttpGet]
        public ActionResult MonthlyStatistics(int year)
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails
                        on o.Id equals od.OrderId
                        join p in db.Products
                        on od.ProductId equals p.Id
                        where o.CreatedDate.Year == year
                        select new
                        {
                            CreateDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            PriceSell = o.TotalAmount,
                            OriginalPrice = p.OriginalPrice
                        };

            var result = query.GroupBy(x => new { Month = x.CreateDate.Month })
                             .Select(x => new
                             {
                                 Month = x.Key.Month,
                                 TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                                 TotalSell = x.Sum(y => y.PriceSell)
                             })
                             .Select(x => new
                             {
                                 Month = x.Month,
                                 DoanhThu = x.TotalSell,
                                 LoiNhuan = x.TotalSell - x.TotalBuy
                             });

            return Json(new { Data = result, Year = year }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ThongKeTheoNam()
        {
            return View();
        }

        [HttpGet]
        public ActionResult YearlyStatistics()
        {
            var query = from o in db.Orders
                        join od in db.OrderDetails
                        on o.Id equals od.OrderId
                        join p in db.Products
                        on od.ProductId equals p.Id
                        select new
                        {
                            CreateDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            PriceSell = o.TotalAmount,
                            OriginalPrice = p.OriginalPrice
                        };

            var result = query.GroupBy(x => new { Year = x.CreateDate.Year })
                             .Select(x => new
                             {
                                 Year = x.Key.Year,
                                 TotalBuy = x.Sum(y => y.Quantity * y.OriginalPrice),
                                 TotalSell = x.Sum(y => y.PriceSell)
                             })
                             .Select(x => new
                             {
                                 Year = x.Year,
                                 DoanhThu = x.TotalSell,
                                 LoiNhuan = x.TotalSell - x.TotalBuy
                             });

            return Json(new { Data = result }, JsonRequestBehavior.AllowGet);
        }

    }
}