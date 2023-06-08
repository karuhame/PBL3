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
using PagedList;
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
            return View(db.Lops.Where(p => p.LOP_STATUS == "Waiting").ToList());
        }



        //Admin: hiển thị ra mọi request tham gia lớp
        //Staff: hiên thị ra request tham gia lớp của bảng thân 
        public ActionResult AdminLopView1()
        {
            var userName = User.Identity.GetUserName();
            Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == userName).FirstOrDefault();

            ViewBag.Role = user.ACCOUNT_ROLE;

            List<Request> obj;
            obj = PBL3_2.Models.Request.GetRequestById(user.ACCOUNT_ID).Where(p => p.query == 0).ToList();

            return View(obj);
        }

        public ActionResult EditLopView()
        {
            var userName = User.Identity.GetUserName();
            Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == userName).FirstOrDefault();

            ViewBag.Role = user.ACCOUNT_ROLE;

            List<Request> obj;
            obj = PBL3_2.Models.Request.GetRequestById(user.ACCOUNT_ID).Where(p => p.query == 1).ToList();

            return View(obj);
        }

        [HttpGet]
        public ActionResult EditRequest(int id)
        {
            DBGym db = new DBGym();
            Request rq = db.Requests.Find(id);
            int lop_id = rq.LOP_ID;
            PBL3_2.Models.Request.DeleteRequest(id);
            return RedirectToAction("Edit", "PhienTaps", new { id = lop_id });
        }

        //Confirm khi tạo lớp mới
        public ActionResult ConfirmLop(int id, string sub)
        {

            var lop = db.Lops.Find(id);
            lop.ConfirmCreate(sub, lop.Accounts.FirstOrDefault().ACCOUNT_ID);
            return RedirectToAction("AdminLopView", db.Lops.ToList());



        }


        //Confirm khi join vào lớp 
        public ActionResult testConfirm(int id, string sub, string query)
        {
            Lop.ConfirmLopAdmin(id, sub, Convert.ToInt16(query));

            return RedirectToAction("AdminLopView1");
        }



        // GET: Lops
        public ActionResult Index()
        {

            var userName = User.Identity.GetUserName();
            Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == userName).FirstOrDefault();
            ViewBag.userRole = user.ACCOUNT_ROLE;

            if (user.ACCOUNT_ROLE != "2")
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
            string name = User.Identity.GetUserName();
            Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == name).FirstOrDefault();
            var obj = Lop.GetLopNotJoiningByAccId(user.ACCOUNT_ID);
            return View(obj.ToList());
        }

        public ActionResult ChooseJoin(string sub)
        {
            if (sub == "create")
            {
                return RedirectToAction("Create");
            }
            else
            {

                //Thêm mới vào trong lớp
                var lop = db.Lops.Find(Convert.ToInt32(sub));
                string name = User.Identity.GetUserName();
                Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == name).FirstOrDefault();

                //lop.Accounts.Add(user);

                PBL3_2.Models.Request.CreateRequest(user.ACCOUNT_ID, lop.LOP_ID, 0);



                return RedirectToAction("Index");

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
                string name = User.Identity.GetUserName();
                Account acc = db.Accounts.Where(p => p.ACCOUNT_NAME == name).FirstOrDefault();
                //LopPhienTapsView view = new LopPhienTapsView(lop, null);


                db.Lops.Add(lop);

                //Lỗi ở đây

                //Nếu là khách thì thêm lớp vào danh sách lớp, admin với nhân viên thì không
                if (acc.ACCOUNT_ROLE == "0")
                {
                    lop.Accounts.Add(acc);


                    //Them vao danh sach Request mỗi khi tạo lớp
                    //Request rq = new Request();
                    //rq.ACCOUNT_ID = acc.ACCOUNT_ID;
                    //rq.LOP_ID = lop.LOP_ID;
                    //db.Requests.Add(rq);

                }
                else if (acc.ACCOUNT_ROLE == "1")
                {
                    lop.Staff = acc;
                }


                db.SaveChanges();
                return RedirectToAction("Create", "PhienTaps", new { id = lop.LOP_ID });


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
            ViewBag.loai = new SelectList(db.LoaiGois.ToList(), "GOI_ID", "GOI_TYPE");

            return RedirectToAction("Edit", "PhienTaps", new { id = lop.LOP_ID });

        }

        // POST: Lops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Lop lop, string sub)
        {
            if (ModelState.IsValid)
            {
                if (sub == "Cancel")
                {
                    return RedirectToAction("Index");
                }

                if (lop.LOP_NUMBERSESSION != db.Lops.Find(lop.LOP_ID).LOP_NUMBERSESSION)
                {

                }
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
            accounts = bbl.FindPT(ID_LOP, lop.PhienTaps.ToList());
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


        //Hiển thị danh sách client theo lớp
        public ActionResult ListClient(string strSearchThietBi, string SortOrder, string SortBy, int? page, int ID_LOP = 0)
        {
            var userName = User.Identity.GetUserName();
            Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == userName).FirstOrDefault();
            ViewBag.IdLop = ID_LOP;
            if (user.ACCOUNT_ROLE != "0")
            {
                //ViewBag.strSearch = strSearch;

                ViewBag.SortOrder = SortOrder;
                ViewBag.SortBy = SortBy;
                ViewBag.CurrentFilter = strSearchThietBi;


                var obj = Lop.GetClientInfoByIdLop(ID_LOP).ToList();

                //Tìm kiếm
                if (!String.IsNullOrEmpty(strSearchThietBi))
                {
                    obj = obj.Where(p => p.ACCOUNT_NAME.Contains(strSearchThietBi)
                                         || p.USER_NAME.ToString() == strSearchThietBi
                                         || p.ACCOUNT_BIRTHDAY.ToString() == strSearchThietBi
                                         || p.ACCOUNT_GENDER.ToString() == strSearchThietBi
                                         || p.ACCOUNT_HEIGHT.ToString() == strSearchThietBi
                                         || p.ACCOUNT_WEIGHT.ToString() == strSearchThietBi
                                         || p.ACCOUNT_PHONE.ToString() == strSearchThietBi
                                         || p.ACCOUNT_EMAIL.ToString() == strSearchThietBi
                                         || p.ACCOUNT_CCCD.ToString() == strSearchThietBi).ToList();
                }

                //Sắp xếp
                switch (SortBy)
                {
                    case "ACCOUNT_NAME":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_NAME).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_NAME).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "USER_NAME":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.USER_NAME).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.USER_NAME).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_CCCD":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_CCCD).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_CCCD).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_BIRTHDAY":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_BIRTHDAY).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_BIRTHDAY).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_GENDER":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_GENDER).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_GENDER).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_HEIGHT":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_HEIGHT).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_HEIGHT).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_WEIGHT":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_WEIGHT).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_WEIGHT).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_PHONE":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_PHONE).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_PHONE).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_EMAIL":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_EMAIL).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_EMAIL).ToList();
                                        break;
                                    }
                            }
                            break;
                        }

                }

                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(obj.ToPagedList(pageNumber, pageSize));
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromLop(int IdLop = 0, string acc_name = "")
        {
            Lop.RemoveClientFromLop(acc_name, IdLop);
            return RedirectToAction("ListClient", new { ID_LOP = IdLop });
        }
    }
}

