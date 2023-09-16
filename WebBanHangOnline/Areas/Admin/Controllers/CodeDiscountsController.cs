using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class CodeDiscountsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin/CodeDiscounts
        public ActionResult Index()
        {
            return View(db.CodeDiscounts.OrderByDescending(x => x.Id).ToList());
        }

        // GET: Admin/CodeDiscounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeDiscount codeDiscount = db.CodeDiscounts.Find(id);
            if (codeDiscount == null)
            {
                return HttpNotFound();
            }
            return View(codeDiscount);
        }

        // GET: Admin/CodeDiscounts/Create
        public ActionResult Create()
        {
            // Tạo SelectList
            var statusList = new SelectList(new[]
           {
                new SelectListItem { Value = "1", Text = "Còn sử dụng" },
                new SelectListItem { Value = "0", Text = "Hết hạn" }
            }, "Value", "Text");

            // Gán SelectList vào ViewBag hoặc ViewModel
            ViewBag.StatusList = statusList;

            return View();
        }

        // POST: Admin/CodeDiscounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CodeDiscount codeDiscount)
        {
            if (ModelState.IsValid)
            {
                codeDiscount.CreatedDate = DateTime.Now;
                codeDiscount.ModifiedDate = DateTime.Now;
                codeDiscount.CreatedBy = "admin";
                codeDiscount.Modifiedby = "admin";
                db.CodeDiscounts.Add(codeDiscount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var statusList = new SelectList(new[]
            {
                new SelectListItem { Value = "1", Text = "Còn sử dụng" },
                new SelectListItem { Value = "0", Text = "Hết hạn" }
            }, "Value", "Text");

            ViewBag.StatusList = statusList;

            return View(codeDiscount);
        }

        // GET: Admin/CodeDiscounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeDiscount codeDiscount = db.CodeDiscounts.Find(id);
            if (codeDiscount == null)
            {
                return HttpNotFound();
            }
            return View(codeDiscount);
        }

        // POST: Admin/CodeDiscounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,code,OrderId,CustomerId,isActive,Discount,CreatedBy,CreatedDate,ModifiedDate,Modifiedby")] CodeDiscount codeDiscount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codeDiscount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codeDiscount);
        }

        // GET: Admin/CodeDiscounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodeDiscount codeDiscount = db.CodeDiscounts.Find(id);
            if (codeDiscount == null)
            {
                return HttpNotFound();
            }
            return View(codeDiscount);
        }

        // POST: Admin/CodeDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodeDiscount codeDiscount = db.CodeDiscounts.Find(id);
            List<Order> listOrder = db.Orders.Where(x => x.CodeDiscountId == id).ToList();
            if (listOrder.Any())
            {
                foreach (var order in listOrder)
                {
                    order.CodeDiscountId = null;
                }

                db.SaveChanges();
            }

            db.CodeDiscounts.Remove(codeDiscount);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
