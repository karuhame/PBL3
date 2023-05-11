using PBL3_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace PBL3_2.Controllers
{
    public class HomeController : Controller
    {
        private DBGym db = new DBGym();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.Accounts.FirstOrDefault(s => s.ACCOUNT_NAME == _user.ACCOUNT_NAME);
                if (check == null)
                {
                    _user.ACCOUNT_PASSWORD = GetMD5(_user.ACCOUNT_PASSWORD);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Accounts.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Name already exists";
                    return View();
                }


            }
            return View();


        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
              
                var data = db.Accounts.Where(s => s.ACCOUNT_NAME.Equals(email) && s.ACCOUNT_PASSWORD.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().AccountInfo.USER_NAME;
                    Session["AccountName"] = data.FirstOrDefault().ACCOUNT_NAME;
                    Session["AccountID"] = data.FirstOrDefault().ACCOUNT_ID;
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        //Hash password
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}