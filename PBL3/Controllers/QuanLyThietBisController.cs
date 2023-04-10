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
    public class QuanLyThietBisController : Controller
    {
        private PBL3Entities db = new PBL3Entities();

        // GET: QuanLyThietBis
        public ActionResult Index()
        {
            var quanLyThietBis = db.QuanLyThietBis.Include(q => q.Account).Include(q => q.ThietBi);
            return View(quanLyThietBis.ToList());
        }

        // GET: QuanLyThietBis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLyThietBi quanLyThietBi = db.QuanLyThietBis.Find(id);
            if (quanLyThietBi == null)
            {
                return HttpNotFound();
            }
            return View(quanLyThietBi);
        }

        // GET: QuanLyThietBis/Create
        public ActionResult Create()
        {
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME");
            ViewBag.THIETBI_ID = new SelectList(db.ThietBis, "THIETBI_ID", "THIETBI_NAME");
            return View();
        }

        // POST: QuanLyThietBis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QUANLYTHIETBI_ID,ACCOUNT_ID,THIETBI_ID")] QuanLyThietBi quanLyThietBi)
        {
            if (ModelState.IsValid)
            {
                db.QuanLyThietBis.Add(quanLyThietBi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", quanLyThietBi.ACCOUNT_ID);
            ViewBag.THIETBI_ID = new SelectList(db.ThietBis, "THIETBI_ID", "THIETBI_NAME", quanLyThietBi.THIETBI_ID);
            return View(quanLyThietBi);
        }

        // GET: QuanLyThietBis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLyThietBi quanLyThietBi = db.QuanLyThietBis.Find(id);
            if (quanLyThietBi == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", quanLyThietBi.ACCOUNT_ID);
            ViewBag.THIETBI_ID = new SelectList(db.ThietBis, "THIETBI_ID", "THIETBI_NAME", quanLyThietBi.THIETBI_ID);
            return View(quanLyThietBi);
        }

        // POST: QuanLyThietBis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QUANLYTHIETBI_ID,ACCOUNT_ID,THIETBI_ID")] QuanLyThietBi quanLyThietBi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanLyThietBi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", quanLyThietBi.ACCOUNT_ID);
            ViewBag.THIETBI_ID = new SelectList(db.ThietBis, "THIETBI_ID", "THIETBI_NAME", quanLyThietBi.THIETBI_ID);
            return View(quanLyThietBi);
        }

        // GET: QuanLyThietBis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLyThietBi quanLyThietBi = db.QuanLyThietBis.Find(id);
            if (quanLyThietBi == null)
            {
                return HttpNotFound();
            }
            return View(quanLyThietBi);
        }

        // POST: QuanLyThietBis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QuanLyThietBi quanLyThietBi = db.QuanLyThietBis.Find(id);
            db.QuanLyThietBis.Remove(quanLyThietBi);
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
