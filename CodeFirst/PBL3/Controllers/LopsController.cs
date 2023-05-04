 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3.Models;

namespace PBL3.Controllers
{
    public class LopsController : Controller
    {
        private DBGym db = new DBGym();

        // GET: Lops
        public ActionResult Index()
        {
            return View(db.Lops.ToList());
        }


        // GET: Lops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // GET: Lops/Create
        // Chọn loại ->  Hiển thị danh sách loại ( số buổi - details, nhân viên )
        // Chọn Join/ Create => nghĩ thêm về vừa join vừa create. 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmCreate(string action, int id)
        {
            var lop = db.Lops.Find(id);
            if (ModelState.IsValid)
            {
                // Tham gia vào lớp có sẵn
                if(action == "join")
                {
                    var user = db.Accounts.Where(p => p.ACCOUNT_NAME == (string)Session["AccountName"]).FirstOrDefault();
                    lop.Accounts.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                // Tạo một lớp mới
                else if(action == "create")
                {
                    db.Lops.Add(lop);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(lop);
        }

        // POST: Lops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LOP_ID,LOP_START,LOP_END")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Lops.Add(lop);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lop);
        }

        //Tham gia vào lớp mới

        public ActionResult Join()  
        {
            var list = db.Lops.ToList();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(Lop lop)
        {
            var user = db.Accounts.Where(p => p.ACCOUNT_NAME == (string)Session["AccountName"]).FirstOrDefault();
            lop.Accounts.Add(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Lops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LOP_ID,LOP_START,LOP_END")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lop);
        }

        // GET: Lops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lop lop = db.Lops.Find(id);
            db.Lops.Remove(lop);
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
