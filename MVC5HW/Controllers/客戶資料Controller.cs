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
        客戶資料Repository repo;

        public 客戶資料Controller()
        {
            repo = RepositoryHelper.Get客戶資料Repository();
        }

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
        public ActionResult Index(string sortOrder, string searchString, string 客戶分類ItemList)
        {
            ViewBag.客戶分類ItemList = repo.客戶分類ItemList("");

            ViewBag.CNameSortParm = String.IsNullOrEmpty(sortOrder) ? "客戶名稱D" : "";
            ViewBag.NumSortParm = sortOrder == "統一編號" ? "統一編號D" : "統一編號";
            ViewBag.TelSortParm = sortOrder == "電話" ? "電話D" : "電話";
            ViewBag.FaxSortParm = sortOrder == "傳真" ? "傳真D" : "傳真";
            ViewBag.AddrSortParm = sortOrder == "地址" ? "地址D" : "地址";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "EmailD" : "Email";
            ViewBag.TypeSortParm = sortOrder == "客戶分類" ? "客戶分類D" : "客戶分類";
            var 客where = repo.searchALL(sortOrder, searchString, 客戶分類ItemList);
            return View(客where.ToList());
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類 = repo.客戶分類ItemList("");
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.客戶資料.Add(客戶資料);
                //db.SaveChanges();
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶分類 = repo.客戶分類ItemList("");
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
            //ViewBag.客戶分類 = new SelectList(repo.客戶分類ItemList(), "客戶分類", 客戶資料.客戶分類);
            ViewBag.客戶分類 = repo.客戶分類ItemList(客戶資料.客戶分類);
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶資料).State = EntityState.Modified;
                //db.SaveChanges();
                repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            //ViewBag.客戶分類 = new SelectList(repo.客戶分類ItemList(), 客戶資料.客戶分類);
            ViewBag.客戶分類 = repo.客戶分類ItemList(客戶資料.客戶分類);
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料 客戶資料 = repo.Find(id.Value);
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
            //客戶資料 客戶資料 = db.客戶資料.Find(id);
            ////db.客戶資料.Remove(客戶資料);
            //客戶資料.是否已刪除 = true;
            //db.SaveChanges();
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
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
