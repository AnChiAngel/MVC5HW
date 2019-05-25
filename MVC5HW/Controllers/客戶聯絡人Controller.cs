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
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities1 db = new 客戶資料Entities1();
        客戶聯絡人Repository repo;
        客戶資料Repository repoCust;

        public 客戶聯絡人Controller()
        {
            repo = RepositoryHelper.Get客戶聯絡人Repository();
            repoCust = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
        }

        // GET: 客戶聯絡人
        /*public ActionResult Index()
        {
            var 客戶聯絡人 = db.客戶聯絡人.Include(客 => 客.客戶資料);
            return View(客戶聯絡人.Where(p => false == p.是否已刪除).ToList());
        }*/
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "職稱D" : "";
            ViewBag.NameSortParm = sortOrder == "姓名" ? "姓名D" : "姓名";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "EmailD" : "Email";
            ViewBag.PhoneSortParm = sortOrder == "電話" ? "電話D" : "電話";
            ViewBag.TelSortParm = sortOrder == "手機" ? "手機D" : "手機";
            var 客where = repo.searchALL(sortOrder, searchString);
            return View(客where.ToList());
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            var s = repo.EmailFind(客戶聯絡人.Email);
            if (ModelState.IsValid && (s == null)) //if (ModelState.IsValid)
            {
                //db.客戶聯絡人.Add(客戶聯絡人);
                //db.SaveChanges();
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else if (s != null) {
                ViewBag.ErrorMessage = "此 Email 已存在！";
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶聯絡人).State = EntityState.Modified;
                //db.SaveChanges();
                repo.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            ////db.客戶聯絡人.Remove(客戶聯絡人);
            //客戶聯絡人.是否已刪除 = true;
            //db.SaveChanges();
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
            repo.Delete(客戶聯絡人);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
