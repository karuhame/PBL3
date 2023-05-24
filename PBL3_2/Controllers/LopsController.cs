using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.AspNet.Identity;
using PBL3_2.BBL;
using PBL3_2.Models;

namespace PBL3_2.Controllers
{

    //Join: Chọn xem tham gia lớp đã tồn tại hay tạo lớp mới
    //Create: Tạo lớp mới -> chọn số lượng phiên tập => PhienTaps/Create -> chọn PT
    //Edit: Hiển thị ra danh sách các phiên tập
    //Del: Xóa lớp

    //Admin: Xem danh sách tất cả các lớp/ Xem danh sách của các lớp cần Confirm
    //User: Xem danh sách lớp tập của bản thân -> tham gia vào lớp mới -> trở về danh sách lớp tập 
    //Nhân viên: Xem danh sách lớp mà bản thân dạy-> xem thông tin của những học viên trong lớp đó
    //
    
    //Tính năng cần làm:
    //- Trong trường hợp bận, vắng thì làm sao
    //- Tính ngày hết hạn 
    //- Tạo biên lai

    public class LopsController : Controller
    {
        private DBGym db = new DBGym();

    

        public ActionResult AdminLopView()
        {
            return View(db.Lops.Where(p=> p.LOP_STATUS == "Waiting").ToList());
        }

        public ActionResult ConfirmLop(int id, string sub)
        {

            var lop = db.Lops.Find(id);
            lop.ConfirmCreate(sub, lop.Accounts.FirstOrDefault());
            return RedirectToAction("AdminLopView", db.Lops.ToList());
        }



        // GET: Lops
        public ActionResult Index()
        {
                
            //!!

            var userName = User.Identity.GetUserName();
            Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == userName).FirstOrDefault();
            ViewBag.userRole = user.ACCOUNT_ROLE;

            if(user.ACCOUNT_ROLE != "2")
            {
                return View(user.Lops.ToList());
            }
            return View(db.Lops.ToList());
        }

        // GET: Lops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        public ActionResult Join()
        {
            return View(db.Lops.Where(p=>p.LOP_STATUS == "Accepted" ).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Join(string sub)
        {
            if(sub == "create")
            {
                return RedirectToAction("Create");
            }
            else
            {

                //Thêm mới vào trong lớp
                var lop = db.Lops.Find(Convert.ToInt32(sub));
                string name= User.Identity.GetUserName();
                Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == name).FirstOrDefault(); 
                
                lop.Accounts.Add(user);
                return View("Index");

            }
        }


        // GET: Lops/Create
        public ActionResult Create()
        {
            
            ViewBag.loai = new SelectList(db.LoaiGois.ToList(), "GOI_ID", "GOI_TYPE");
            return View();
        }

        // POST: Lops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Lop lop, string loai)
        {
            if (ModelState.IsValid)
            {
                lop.GOI_ID = Convert.ToInt32(loai);
                db.Lops.Add(lop);

                //Lỗi ở đây
                string name = User.Identity.GetUserName();
                
                Account acc = db.Accounts.Where(p => p.ACCOUNT_NAME == name).FirstOrDefault();

                //Nếu là khách thì thêm lớp vào danh sách lớp, admin với nhân viên thì không
                if(acc.ACCOUNT_ROLE == "0")
                {
                    lop.Accounts.Add(acc);

                }
                else if(acc.ACCOUNT_ROLE == "1")
                {
                    lop.Staff = acc;
                }
 
                db.SaveChanges();
                return RedirectToAction("Create", "PhienTaps", new {id = lop.LOP_ID});


            }

            return View(lop);
        }

        // GET: Lops/Edit/5
        // In ra Màn hình danh sách các buổi tập
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LOP_ID,LOP_START,LOP_END,LOP_NUMBERSESSION")] Lop lop)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lop);
        }

        // GET: Lops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lop lop = db.Lops.Find(id);
            if (lop == null)
            {
                return HttpNotFound();
            }
            return View(lop);
        }

        // POST: Lops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lop lop = db.Lops.Find(id);
            db.Lops.Remove(lop);
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


        public ActionResult AddPT(int ID_LOP)
        {
            var lop = db.Lops.Find(ID_LOP);
            List<Account> accounts = new List<Account>();

            BBLQLLop bbl = new BBLQLLop();
            accounts = bbl.FindPT(lop.PhienTaps.ToList());
            ViewBag.ID_LOP = ID_LOP;
            return View(accounts);

        }

        public ActionResult ChoosePT(int ID_LOP, int id)
        {
            var lop = db.Lops.Find(ID_LOP);
            lop.Staff = db.Accounts.Find(id);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
 
