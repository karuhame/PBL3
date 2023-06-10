using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PBL3_2.BBL;
using PBL3_2.Models;

namespace PBL3_2.Controllers
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
        //Truyền vào id lớp để truyền đối tượng lop sang View
        // Lấy được numberSession để tạo bảng
        public ActionResult Create(int? ID)
        {
            Lop temp = db.Lops.Find(ID);
            ViewData["lop"] = temp;

            string name = User.Identity.GetUserName();
            var acc = db.Accounts.Where(p => p.ACCOUNT_NAME == name).FirstOrDefault();

            ViewBag.Role = acc.ACCOUNT_ROLE;
            ViewBag.TKB = new SelectList(new SelectListItem[]
           {
               new SelectListItem(){ Text = "Thứ hai", Value = "0"},
               new SelectListItem(){ Text = "Thứ ba", Value = "1"},
               new SelectListItem(){ Text = "Thứ tư", Value = "2"},
               new SelectListItem(){ Text = "Thứ năm", Value = "3"},
               new SelectListItem(){ Text = "Thứ sáu", Value = "4"},
               new SelectListItem(){ Text = "Thứ bảy", Value = "5"},
               new SelectListItem(){ Text = "Chủ nhật", Value = "6"}
           }, "Value", "Text");
            //ViewData["soBuoi"] = lop.LOP_NUMBERSESSION;

            //ViewBag.soBuoi = 2;

            return View();
        }

        // POST: PhienTaps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //Truyền vào List<phienTaps> rồi add vào LopID tương ứng    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(List<PhienTap> phienTaps, string sub, int ID)
        {
            Lop temp = db.Lops.Find(ID);
            ViewData["lop"] = temp;

            ViewBag.TKB = new SelectList(new SelectListItem[]
            {
            new SelectListItem(){ Text = "Thứ hai", Value = "0"},
            new SelectListItem(){ Text = "Thứ ba", Value = "1"},
            new SelectListItem(){ Text = "Thứ tư", Value = "2"},
            new SelectListItem(){ Text = "Thứ năm", Value = "3"},
            new SelectListItem(){ Text = "Thứ sáu", Value = "4"},
            new SelectListItem(){ Text = "Thứ bảy", Value = "5"},
            new SelectListItem(){ Text = "Chủ nhật" +
            "", Value = "6"}
            }, "Value", "Text");
            if (sub == "Create")
            {
                foreach (PhienTap i in phienTaps)
                {
                    if (ModelState.IsValid && i.PHIENTAP_endd < i.PHIENTAP_startt)
                    {
                        ModelState.AddModelError("", "Invalid Time");

                        return View(phienTaps);
                    }
                }
                if (ModelState.IsValid)
                {
                    //db.PhienTaps.Add(phienTap);
                    for (int i = 0; i < phienTaps.Count; i++)
                    {
                        Lop x = db.Lops.Find(ID);
                        BBLQLLop bbl = new BBLQLLop();
                        db.PhienTaps.Add(phienTaps[i]);
                        phienTaps[i].LOP_ID = ID;
                    }
                    db.SaveChanges();

                    string name = User.Identity.GetUserName();
                    var acc = db.Accounts.Where(p => p.ACCOUNT_NAME == name).FirstOrDefault();


                    if (acc.ACCOUNT_ROLE != "1")
                    {
                        return RedirectToAction("AddPT", "Lops", new { ID_LOP = ID });
                    }

                    //Neu la nhan vien thi tra ve man hinh index
                    return RedirectToAction("Index", "Lops");
                }

                return View(phienTaps);
            }
            else if (sub == "Cancel")
            {
                db.Lops.Remove(db.Lops.Find(ID));
                db.SaveChanges();
            }
            return View(phienTaps);
        }

        // GET: PhienTaps/Edit/5
        // Edit các phiên của 1 lớp Phiên tập của I
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Lop temp = db.Lops.Find(id);
            if(temp.LoaiGoi.GOI_PT == false)
            {

            }
            ViewData["lop"] = temp;
            List<PhienTap> phienTaps = temp.PhienTaps.ToList();

            ViewBag.TKB = new SelectList(new SelectListItem[]
           {
               new SelectListItem(){ Text = "Thứ hai", Value = "0"},
               new SelectListItem(){ Text = "Thứ ba", Value = "1"},
               new SelectListItem(){ Text = "Thứ tư", Value = "2"},
               new SelectListItem(){ Text = "Thứ năm", Value = "3"},
               new SelectListItem(){ Text = "Thứ sáu", Value = "4"},
               new SelectListItem(){ Text = "Thứ bảy", Value = "5"},
               new SelectListItem(){ Text = "Chủ nhật" +
               "", Value = "6"}
           }, "Value", "Text");
            if (phienTaps == null)
            {
                return HttpNotFound();
            }
            return View(phienTaps);
        }

        // POST: PhienTaps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(List<PhienTap> phienTaps, int id)
        {
            foreach (PhienTap i in phienTaps)
            {
                if (ModelState.IsValid && BBLQLLop.CompareTime(i.PHIENTAP_startt, i.PHIENTAP_endd))
                {
                    ModelState.AddModelError("", "Invalid Time");
                    Lop temp = db.Lops.Find(id);
                    ViewData["lop"] = temp;

                    ViewBag.TKB = new SelectList(new SelectListItem[]
                   {
                       new SelectListItem(){ Text = "Thứ hai", Value = "0"},
                       new SelectListItem(){ Text = "Thứ ba", Value = "1"},
                       new SelectListItem(){ Text = "Thứ tư", Value = "2"},
                       new SelectListItem(){ Text = "Thứ năm", Value = "3"},
                       new SelectListItem(){ Text = "Thứ sáu", Value = "4"},
                       new SelectListItem(){ Text = "Thứ bảy", Value = "5"},
                       new SelectListItem(){ Text = "Chủ nhật" +
                       "", Value = "6"}
                   }, "Value", "Text");

                    return View(phienTaps);
                }


            }
            if (ModelState.IsValid)
            {
                var userName = User.Identity.GetUserName();
                Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == userName).FirstOrDefault();

                // Create Request here where query = 1
                PBL3_2.Models.Request.CreateEditRequest(user.ACCOUNT_ID, id, phienTaps);

                return RedirectToAction("Index", "Lops");
            }
            return View(phienTaps);
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
