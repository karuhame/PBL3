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
    public class LoaiGoisController : Controller
    {
        private DBGym db = new DBGym();

        // GET: LoaiGois
        public ActionResult Index()
        {
            return View(db.LoaiGois.ToList());
        }

        // GET: LoaiGois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiGoi loaiGoi = db.LoaiGois.Find(id);
            if (loaiGoi == null)
            {
                return HttpNotFound();
            }
            return View(loaiGoi);
        }

        // GET: LoaiGois/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiGois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GOI_ID,GOI_NUMBERSESSION")] LoaiGoi loaiGoi)
        {
            if (ModelState.IsValid)
            {
                db.LoaiGois.Add(loaiGoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiGoi);
        }

        // GET: LoaiGois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiGoi loaiGoi = db.LoaiGois.Find(id);
            if (loaiGoi == null)
            {
                return HttpNotFound();
            }
            return View(loaiGoi);
        }

        // POST: LoaiGois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GOI_ID,GOI_NUMBERSESSION")] LoaiGoi loaiGoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiGoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiGoi);
        }

        // GET: LoaiGois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiGoi loaiGoi = db.LoaiGois.Find(id);
            if (loaiGoi == null)
            {
                return HttpNotFound();
            }
            return View(loaiGoi);
        }

        // POST: LoaiGois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiGoi loaiGoi = db.LoaiGois.Find(id);
            db.LoaiGois.Remove(loaiGoi);
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
