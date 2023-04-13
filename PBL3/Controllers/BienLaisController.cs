﻿using System;
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
    public class BienLaisController : Controller
    {
        private PBL3Entities db = new PBL3Entities();

        // GET: BienLais
        public ActionResult Index()
        {
            var bienLais = db.BienLais.Include(b => b.Account).Include(b => b.Lop);
            return View(bienLais.ToList());
        }

        // GET: BienLais/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienLai bienLai = db.BienLais.Find(id);
            if (bienLai == null)
            {
                return HttpNotFound();
            }
            return View(bienLai);
        }

        // GET: BienLais/Create
        public ActionResult Create()
        {
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME");
            ViewBag.LOP_ID = new SelectList(db.Lops, "LOP_ID", "LOP_STAFFID");
            return View();
        }

        // POST: BienLais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BIENLAI_ID,BIENLAI_PAYMENT,LOP_ID,ACCOUNT_ID,BIENLAI_START,BIENLAI_END")] BienLai bienLai)
        {
            if (ModelState.IsValid)
            {
                db.BienLais.Add(bienLai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", bienLai.ACCOUNT_ID);
            ViewBag.LOP_ID = new SelectList(db.Lops, "LOP_ID", "LOP_STAFFID", bienLai.LOP_ID);
            return View(bienLai);
        }

        // GET: BienLais/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienLai bienLai = db.BienLais.Find(id);
            if (bienLai == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", bienLai.ACCOUNT_ID);
            ViewBag.LOP_ID = new SelectList(db.Lops, "LOP_ID", "LOP_STAFFID", bienLai.LOP_ID);
            return View(bienLai);
        }

        // POST: BienLais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BIENLAI_ID,BIENLAI_PAYMENT,LOP_ID,ACCOUNT_ID,BIENLAI_START,BIENLAI_END")] BienLai bienLai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bienLai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", bienLai.ACCOUNT_ID);
            ViewBag.LOP_ID = new SelectList(db.Lops, "LOP_ID", "LOP_STAFFID", bienLai.LOP_ID);
            return View(bienLai);
        }

        // GET: BienLais/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BienLai bienLai = db.BienLais.Find(id);
            if (bienLai == null)
            {
                return HttpNotFound();
            }
            return View(bienLai);
        }

        // POST: BienLais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BienLai bienLai = db.BienLais.Find(id);
            db.BienLais.Remove(bienLai);
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
