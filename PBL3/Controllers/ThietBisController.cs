using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;
using PBL3.Models;

namespace PBL3.Controllers
{
    //public static string strSearch;
    public class ThietBisController : Controller
    {
        private PBL3Entities db = new PBL3Entities();
        
        // GET: ThietBis
        public ActionResult Index(string strSearchThietBi, string SortOrder, string SortBy)

        {
            //ViewBag.strSearch = strSearch;

            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;
            ViewBag.CurrentFilter = strSearchThietBi;

            var obj = db.ThietBis.ToList();

            //Tìm kiếm
            if (!String.IsNullOrEmpty(strSearchThietBi))
            {
                obj = obj.Where(p => p.THIETBI_NAME.Contains(strSearchThietBi)
                                     || p.THIETBI_STATUS.ToString() == strSearchThietBi
                                     || p.THIETBI_NUM.ToString() == strSearchThietBi).ToList();
            }
           
            //Sắp xếp
            switch (SortBy)
            {
                case "THIETBI_NAME":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(p => p.THIETBI_NAME).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderByDescending(p => p.THIETBI_NAME).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "THIETBI_STATUS":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(p => p.THIETBI_STATUS).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderByDescending(p => p.THIETBI_STATUS).ToList();
                                    break;
                                }
                        }
                        break;
                    }
                case "THIETBI_NUM":
                    {
                        switch (SortOrder)
                        {
                            case "Asc":
                                {
                                    obj = obj.OrderBy(p => p.THIETBI_NUM).ToList();
                                    break;
                                }
                            default:
                                {
                                    obj = obj.OrderByDescending(p => p.THIETBI_NUM).ToList();
                                    break;
                                }
                        }
                        break;
                    }

            }
                  
            
            return View(obj);
        }
        

        // GET: ThietBis/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThietBi thietBi = db.ThietBis.Find(id);
            if (thietBi == null)
            {
                return HttpNotFound();
            }
            return View(thietBi);
        }

        // GET: ThietBis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ThietBis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "THIETBI_ID,THIETBI_NAME,THIETBI_STATUS,THIETBI_NUM")] ThietBi thietBi)
        {
            if (ModelState.IsValid)
            {
                db.ThietBis.Add(thietBi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thietBi);
        }

        // GET: ThietBis/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThietBi thietBi = db.ThietBis.Find(id);
            if (thietBi == null)
            {
                return HttpNotFound();
            }
            return View(thietBi);
        }

        // POST: ThietBis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "THIETBI_ID,THIETBI_NAME,THIETBI_STATUS,THIETBI_NUM")] ThietBi thietBi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thietBi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thietBi);
        }

        // GET: ThietBis/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThietBi thietBi = db.ThietBis.Find(id);
            if (thietBi == null)
            {
                return HttpNotFound();
            }
            return View(thietBi);
        }

        // POST: ThietBis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ThietBi thietBi = db.ThietBis.Find(id);
            db.ThietBis.Remove(thietBi);
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
