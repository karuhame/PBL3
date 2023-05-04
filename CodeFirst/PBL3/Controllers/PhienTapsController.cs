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
    public class PhienTapsController : Controller
    {
        private DBGym db = new DBGym();

        // GET: PhienTaps
        public ActionResult Index()
        {
            return View(db.PhienTaps.ToList());
        }

        // GET: PhienTaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhienTap phienTap = db.PhienTaps.Find(id);
            if (phienTap == null)
            {
                return HttpNotFound();
            }
            return View(phienTap);
        }

        // GET: PhienTaps/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PhienTaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PHIENTAP_ID,PHIENTAP_DAY,PHIENTAP_START,PHIENTAP_END")] PhienTap phienTap)
        {
            if (ModelState.IsValid)
            {
                db.PhienTaps.Add(phienTap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(phienTap);
        }

        // GET: PhienTaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhienTap phienTap = db.PhienTaps.Find(id);
            if (phienTap == null)
            {
                return HttpNotFound();
            }
            return View(phienTap);
        }

        // POST: PhienTaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PHIENTAP_ID,PHIENTAP_DAY,PHIENTAP_START,PHIENTAP_END")] PhienTap phienTap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phienTap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(phienTap);
        }

        // GET: PhienTaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhienTap phienTap = db.PhienTaps.Find(id);
            if (phienTap == null)
            {
                return HttpNotFound();
            }
            return View(phienTap);
        }

        // POST: PhienTaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhienTap phienTap = db.PhienTaps.Find(id);
            db.PhienTaps.Remove(phienTap);
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
