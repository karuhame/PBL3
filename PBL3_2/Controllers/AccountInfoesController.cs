using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3_2.Models;

namespace PBL3_2.Controllers
{
    public class AccountInfoesController : Controller
    {
        private DBGym db = new DBGym();

        // GET: AccountInfoes
        public ActionResult Index()
        {
            return View(db.AccountInfos.ToList());
        }

        // GET: AccountInfoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfos.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View(accountInfo);
        }

        // GET: AccountInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ACCOUNT_NAME,USER_NAME,ACCOUNT_CCCD,ACCOUNT_BIRTHDAY,ACCOUNT_GENDER,ACCOUNT_HEIGHT,ACCOUNT_WEIGHT,ACCOUNT_PHONE,ACCOUNT_EMAIL")] AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                db.AccountInfos.Add(accountInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountInfo);
        }

        // GET: AccountInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfos.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View(accountInfo);
        }

        // POST: AccountInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ACCOUNT_NAME,USER_NAME,ACCOUNT_CCCD,ACCOUNT_BIRTHDAY,ACCOUNT_GENDER,ACCOUNT_HEIGHT,ACCOUNT_WEIGHT,ACCOUNT_PHONE,ACCOUNT_EMAIL")] AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountInfo);
        }

        // GET: AccountInfoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfos.Find(id);
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
            AccountInfo accountInfo = db.AccountInfos.Find(id);
            db.AccountInfos.Remove(accountInfo);
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
