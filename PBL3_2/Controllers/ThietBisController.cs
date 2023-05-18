using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using Microsoft.Ajax.Utilities;
using PagedList;
using PBL3_2.Models;

namespace PBL3_2.Controllers
{
    //public static string strSearch;
    public class ThietBisController : Controller
    {
        private DBGym db = new DBGym();

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
                obj = obj.Where(p => p.THIETBI_NAME.Contains(strSearchThietBi)).ToList();
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

            int pageSize = 5;
            int pageNumber = 1;
            return View(obj.ToPagedList(pageNumber, pageSize));

        }


        // GET: ThietBis/Details/5
        public ActionResult Details(int id)
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
        public ActionResult Create([Bind(Include = "THIETBI_NAME,THIETBI_STATUS,THIETBI_NUM")] ThietBi thietBi)
        {
            // Xu ly exception
            if (ModelState.IsValid && thietBi.THIETBI_STATUS > thietBi.THIETBI_NUM)
            {
                ModelState.AddModelError("", "So luong thiet bi hong khong duoc vuot qua tong so luong thiet bi");
                return View(thietBi);
            }
            if (ModelState.IsValid && thietBi.THIETBI_STATUS != null && thietBi.THIETBI_NUM == null)
            {
                ModelState.AddModelError("", "So luong thiet bi chua xac dinh");
                return View(thietBi);
            }
            // 

            if (ModelState.IsValid)
            {
                db.ThietBis.Add(thietBi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thietBi);
        }

        // GET: ThietBis/Edit/5
        public ActionResult Edit(int id)
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
            // Xu ly exception
            if (ModelState.IsValid && thietBi.THIETBI_STATUS > thietBi.THIETBI_NUM)
            {
                ModelState.AddModelError("", "So luong thiet bi hong khong duoc vuot qua tong so luong thiet bi");
                return View(thietBi);
            }
            if (ModelState.IsValid && thietBi.THIETBI_STATUS != null && thietBi.THIETBI_NUM == null)
            {
                ModelState.AddModelError("", "So luong thiet bi chua xac dinh");
                return View(thietBi);
            }
            //

            if (ModelState.IsValid)
            {
                db.Entry(thietBi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(thietBi);
        }

        // GET: ThietBis/Delete/5
        public ActionResult Delete(int id)
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
        public ActionResult DeleteConfirmed(int id)
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
