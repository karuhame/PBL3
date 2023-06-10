using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PBL3_2.Models;
using PagedList;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Net.PeerToPeer;
using PBL3_2.Service;

namespace PBL3_2.Controllers
{
    public class BienLaisController : Controller
    {
        private DBGym db = new DBGym();
        // GET: BienLais
        public ActionResult Index(DateTime ?Batdau,DateTime ?Ketthuc, string TenNguoiTraTien = "", int SortBy = 0, int SortOrder = 0,int page =1 )
        {
            ViewBag.TenNguoiTraTien = TenNguoiTraTien;
            ViewBag.SortBy = SortBy;
            ViewBag.SortOrder = SortOrder;

            ViewBag.Batdau = Batdau;
            ViewBag.Ketthuc = Ketthuc;

            // Lap danh sach bien lai 
            QLBienLai QLBL = new QLBienLai();
            List<BienLai> l = new List<BienLai>();


            if (User.Identity.IsAuthenticated) // da dang nhap chua 
            { 
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = userManager.FindById(User.Identity.GetUserId());
                var currentRole = userManager.GetRoles(currentUser.Id).FirstOrDefault();
                var ID = User.Identity.GetUserId();
                var userName = User.Identity.GetUserName();

                l = QLBL.ListBL();
                if (currentRole == "Khach Hang" || currentRole == "Nhan Vien")
                {
                    l = QLBL.getBienLaiByUserName(userName);
                }

                //
                // Search 
                //
                if (TenNguoiTraTien != "")
                {
                    if (currentRole == "Khach Hang" || currentRole == "Nhan Vien")
                    {
                        l = QLBL.getBienLaiByUserNameAndTenNguoiTraTien(userName, TenNguoiTraTien);
                    }
                    else l = db.BienLais.Where(p => p.Account.ACCOUNT_NAME.Contains(TenNguoiTraTien)).ToList();
                }

                //
                // Loc theo Date 
                //
                if (Batdau != null && Ketthuc != null) {
                    l = QLBL.SearchBLByDate(l, Batdau.Value.Date ,Ketthuc.Value.Date);
                }

            }


            // Sort 
            l = QLBL.SortBL(l, SortBy, SortOrder);

            ViewBag.Cost = QLBL.TotalCost(l);


            int pageSize = 3;
            return View(l.ToPagedList(page, pageSize));
        }

        // GET: BienLais/Details/5
        public ActionResult Details(int? id)
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
            return View();
        }

        // POST: BienLais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BIENLAI_ID,BIENLAI_PAYMENT,BIENLAI_START,BIENLAI_END")] BienLai bienLai)
        {
            if (ModelState.IsValid)
            {
                db.BienLais.Add(bienLai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bienLai);
        }

        // GET: BienLais/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: BienLais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BIENLAI_ID,BIENLAI_PAYMENT,BIENLAI_START,BIENLAI_END")] BienLai bienLai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bienLai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bienLai);
        }

        // GET: BienLais/Delete/5
        public ActionResult Delete(int? id)
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
        public ActionResult DeleteConfirmed(int id)
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
