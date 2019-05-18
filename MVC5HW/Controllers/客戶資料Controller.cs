using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5HW.Models;

namespace MVC5HW.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities1 db = new 客戶資料Entities1();

        public ActionResult 客戶關聯資料表()
        {
            return View(db.V_客戶關聯資料統計表.ToList());
        }

        // GET: 客戶資料
        /*public ActionResult Index(string keyword)
        {
            //return View(db.客戶資料.Where(p => false == p.是否已刪除).ToList());
            var data = db.客戶資料.Where(p => false == p.是否已刪除).AsQueryable();
            if (!String.IsNullOrEmpty(keyword)) {
                data = data.Where(p => p.客戶名稱.Contains(keyword));
            }
            return View(data.ToList());
        }*/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.CNameSortParm = String.IsNullOrEmpty(sortOrder) ? "CName_Desc" : "";
            ViewBag.NumSortParm = sortOrder == "Num" ? "Num_Desc" : "Num";
            ViewBag.TelSortParm = sortOrder == "Tel" ? "Tel_Desc" : "Tel";
            ViewBag.FaxSortParm = sortOrder == "Fax" ? "Fax_Desc" : "Fax";
            ViewBag.AddrSortParm = sortOrder == "Addr" ? "Addr_Desc" : "Addr";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "Email_Desc" : "Email";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "Type_Desc" : "Type";
            var customers = from c in db.客戶資料.Where(p => false == p.是否已刪除).AsQueryable() select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.客戶名稱.Contains(searchString) || s.地址.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "CName_Desc":
                    customers = customers.OrderByDescending(s => s.客戶名稱);
                    break;
                case "Num":
                    customers = customers.OrderBy(s => s.統一編號);
                    break;
                case "Num_Desc":
                    customers = customers.OrderByDescending(s => s.統一編號);
                    break;
                case "Tel":
                    customers = customers.OrderBy(s => s.電話);
                    break;
                case "Tel_Desc":
                    customers = customers.OrderByDescending(s => s.電話);
                    break;
                case "Fax":
                    customers = customers.OrderBy(s => s.傳真);
                    break;
                case "Fax_Desc":
                    customers = customers.OrderByDescending(s => s.傳真);
                    break;
                case "Email":
                    customers = customers.OrderBy(s => s.地址);
                    break;
                case "Email_Desc":
                    customers = customers.OrderByDescending(s => s.地址);
                    break;
                case "Addr":
                    customers = customers.OrderBy(s => s.Email);
                    break;
                case "Addr_Desc":
                    customers = customers.OrderByDescending(s => s.Email);
                    break;
                case "Type":
                    customers = customers.OrderBy(s => s.客戶分類);
                    break;
                case "Type_Desc":
                    customers = customers.OrderByDescending(s => s.客戶分類);
                    break;
                default:
                    customers = customers.OrderBy(s => s.客戶名稱);
                    break;
            }
            return View(customers.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            //db.客戶資料.Remove(客戶資料);
            客戶資料.是否已刪除 = true;
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
