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
    public class AccountInfoesController : Controller
    {
        private PBL3Entities db = new PBL3Entities();

        // GET: AccountInfoes
        public ActionResult Index()
        {
            var accountInfoes = db.AccountInfoes.Include(a => a.Account);
            return View(accountInfoes.ToList());
        }

        // GET: AccountInfoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfoes.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View(accountInfo);
        }

        // GET: AccountInfoes/Create
        public ActionResult Create()
        {
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME");
            return View();
        }

        // POST: AccountInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ACCOUNT_ID,USER_NAME,ACCOUNT_CCCD,ACCOUNT_BIRTHDAY,ACCOUNT_GENDER,ACCOUNT_HEIGHT,ACCOUNT_WEIGHT,ACCOUNT_PHONE,ACCOUNT_EMAIL")] AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                db.AccountInfoes.Add(accountInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", accountInfo.ACCOUNT_ID);
            return View(accountInfo);
        }

        // GET: AccountInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfoes.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", accountInfo.ACCOUNT_ID);
            return View(accountInfo);
        }

        // POST: AccountInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ACCOUNT_ID,USER_NAME,ACCOUNT_CCCD,ACCOUNT_BIRTHDAY,ACCOUNT_GENDER,ACCOUNT_HEIGHT,ACCOUNT_WEIGHT,ACCOUNT_PHONE,ACCOUNT_EMAIL")] AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.Accounts, "ACCOUNT_ID", "ACCOUNT_NAME", accountInfo.ACCOUNT_ID);
            return View(accountInfo);
        }

        // GET: AccountInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfoes.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View(accountInfo);
        }

        // POST: AccountInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AccountInfo accountInfo = db.AccountInfoes.Find(id);
            db.AccountInfoes.Remove(accountInfo);
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
