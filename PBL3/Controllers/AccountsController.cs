using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PBL3.Models;

namespace PBL3.Controllers
{
    public class AccountsController : Controller
    {
        private PBL3Entities db = new PBL3Entities();

        // GET: Accounts

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.SortOrder = sortOrder;

           
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var accounts = db.Accounts.Include(a => a.AccountInfo);
            if (!String.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(s => s.ACCOUNT_NAME.Contains(searchString)) ;

            }

            switch (sortOrder)
            {
                case "name_asc":
                    accounts = accounts.OrderBy(s => s.ACCOUNT_NAME);
                    break;
                case "name_desc":
                    accounts= accounts.OrderByDescending(s => s.ACCOUNT_NAME);
                    break;
                
                case "password_asc":
                    accounts = accounts.OrderBy(s => s.ACCOUNT_PASSWORD);
                   break;
                case "password_desc":
                    accounts = accounts.OrderByDescending(s => s.ACCOUNT_PASSWORD);
                    break;

                case "role_asc":
                    accounts = accounts.OrderBy(s => s.ACCOUNT_ROLE);
                    break;
                case "role_desc":
                    accounts = accounts.OrderByDescending(s => s.ACCOUNT_ROLE);
                    break;

                case "ID_asc":
                    accounts = accounts.OrderBy(s => s.ACCOUNT_ID);
                    break;
                case "ID_desc":
                    accounts = accounts.OrderByDescending(s => s.ACCOUNT_ID);
                    break;

                default:  
                    accounts = accounts.OrderBy(s => s.ACCOUNT_NAME);
                break;
            }
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(accounts.ToPagedList(pageNumber, pageSize));
        }

        // GET: Accounts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.ACCOUNT_ID = new SelectList(db.AccountInfoes, "ACCOUNT_ID", "USER_NAME");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ACCOUNT_ID,ACCOUNT_NAME,ACCOUNT_PASSWORD,ACCOUNT_ROLE")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ACCOUNT_ID = new SelectList(db.AccountInfoes, "ACCOUNT_ID", "USER_NAME", account.ACCOUNT_ID);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.AccountInfoes, "ACCOUNT_ID", "USER_NAME", account.ACCOUNT_ID);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ACCOUNT_ID,ACCOUNT_NAME,ACCOUNT_PASSWORD,ACCOUNT_ROLE")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ACCOUNT_ID = new SelectList(db.AccountInfoes, "ACCOUNT_ID", "USER_NAME", account.ACCOUNT_ID);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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
