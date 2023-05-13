using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PBL3_2.BBL;
using PBL3_2.Models;

namespace PBL3_2.Controllers
{
    public class AccountInfoesController : Controller
    {
        private DBGym db = new DBGym();

        
            public ActionResult Index(string strSearchThietBi, string SortOrder, string SortBy, int? page)

            {

                //ViewBag.strSearch = strSearch;

                ViewBag.SortOrder = SortOrder;
                ViewBag.SortBy = SortBy;
                ViewBag.CurrentFilter = strSearchThietBi;

                var obj = db.AccountInfos.ToList();

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

        // GET: AccountInfoes

        //public ActionResult Index()
        //{
        //    return View(db.AccountInfos.ToList());
        //}

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

        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them2(string account_name, string account_password, string Retype_Account_password,
         DateTime account_birthday, string User_name, string Account_CCCD, Boolean GioiTinh, double Account_Height,
         double Account_Weight, string Account_phone, string Account_email)
        {
            Account da = new Account()
            {
                ACCOUNT_NAME = account_name,
                ACCOUNT_PASSWORD = account_password
            };
            da.ACCOUNT_ROLE = "Khach Hang";
            AccountInfo dc = new AccountInfo()
            {
                ACCOUNT_CCCD = Account_CCCD,
                ACCOUNT_BIRTHDAY = account_birthday,
                ACCOUNT_NAME = account_name,
                ACCOUNT_EMAIL = Account_email,
                ACCOUNT_GENDER = GioiTinh,
                ACCOUNT_HEIGHT = Account_Height,
                ACCOUNT_PHONE = Account_phone,
                ACCOUNT_WEIGHT = Account_Weight,
                USER_NAME = User_name
            };

            AccounsAccountInfo newAccount = new AccounsAccountInfo();
            newAccount.Account = da;
            newAccount.AccountInfo = dc;
            if (Retype_Account_password != account_password)
            {
                ModelState.AddModelError("", "Mật khẩu không khớp");
                return View();
            }

            var tmp = db.AccountInfos.Where(p => p.ACCOUNT_NAME == dc.ACCOUNT_NAME).ToList().Count();    
            if (tmp > 0)
            {
                ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                return View();
            }
            else
            {
                db.AccountInfos.Add(dc);
                db.SaveChanges();
                db.Accounts.Add(da);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        //[HttpPost]
        //public ActionResult Them3(AccounsAccountInfo p) 
        //{
        //    Account da = new Account()
        //    {
        //        ACCOUNT_NAME = p.AccountInfo.ACCOUNT_NAME,
        //        ACCOUNT_PASSWORD = p.Account.ACCOUNT_PASSWORD
        //    };

        //    AccountInfo dc = new AccountInfo()
        //    {
        //        ACCOUNT_CCCD = p.AccountInfo.ACCOUNT_CCCD,
        //        ACCOUNT_BIRTHDAY = p.AccountInfo.ACCOUNT_BIRTHDAY,
        //        ACCOUNT_NAME = p.AccountInfo.ACCOUNT_NAME,
        //        ACCOUNT_EMAIL = p.AccountInfo.ACCOUNT_EMAIL,
        //        ACCOUNT_GENDER = p.AccountInfo.ACCOUNT_GENDER,
        //        ACCOUNT_HEIGHT = p.AccountInfo.ACCOUNT_HEIGHT,
        //        ACCOUNT_PHONE = p.AccountInfo.ACCOUNT_PHONE,
        //        ACCOUNT_WEIGHT = p.AccountInfo.ACCOUNT_HEIGHT,
        //        USER_NAME = p.AccountInfo.USER_NAME
        //    };

        //    AccounsAccountInfo newAccount = new AccounsAccountInfo();
        //    newAccount.Account = da;
        //    newAccount.AccountInfo = dc;
        //    if (p.retypePassword != p.Account.ACCOUNT_PASSWORD)
        //    {
        //        ModelState.AddModelError("", "Mật khẩu không khớp");
        //        return View(p);
        //    }

        //    var tmp = db.AccountInfos.Where(x => x.ACCOUNT_NAME == dc.ACCOUNT_NAME).ToList().Count();
        //    if (tmp > 0)
        //    {
        //        ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
        //        return View(p);
        //    }
        //    else
        //    {
        //        db.AccountInfos.Add(dc);
        //        db.SaveChanges();
        //        db.Accounts.Add(da);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //}


       
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
            var account = db.Accounts.Where(p => p.ACCOUNT_NAME == id).ToList().Count();
            if (account > 0)
            {
                db.Accounts.RemoveRange(db.Accounts.Where(p => p.ACCOUNT_NAME == id).ToList());
                db.SaveChanges();
            }

            AccountInfo accountInfo = db.AccountInfos.Find(id);
            if (accountInfo != null)
            {
                db.AccountInfos.Remove(accountInfo);
                db.SaveChanges();
            }
          
            
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
        public ActionResult XemDanhSachKhachHang(string strSearchThietBi, int? page)

        {

            //ViewBag.strSearch = strSearch;

            ViewBag.CurrentFilter = strSearchThietBi;

            var obj = db.Accounts.Where(p => p.ACCOUNT_ROLE == "Khach Hang").ToList();


            //Tìm iếm
            if (!String.IsNullOrEmpty(strSearchThietBi))
            {
                obj = obj.Where(p => p.AccountInfo.ACCOUNT_NAME.Contains(strSearchThietBi)
                                     || p.AccountInfo.USER_NAME.ToString() == strSearchThietBi
                                     || p.AccountInfo.ACCOUNT_BIRTHDAY.ToString() == strSearchThietBi
                                     || p.AccountInfo.ACCOUNT_GENDER.ToString() == strSearchThietBi
                                     || p.AccountInfo.ACCOUNT_HEIGHT.ToString() == strSearchThietBi
                                     || p.AccountInfo.ACCOUNT_WEIGHT.ToString() == strSearchThietBi
                                     || p.AccountInfo.ACCOUNT_PHONE.ToString() == strSearchThietBi
                                     || p.AccountInfo.ACCOUNT_EMAIL.ToString() == strSearchThietBi
                                     || p.AccountInfo.ACCOUNT_CCCD.ToString() == strSearchThietBi).ToList();
            }



            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(obj.ToPagedList(pageNumber, pageSize));

        }
    }
}
