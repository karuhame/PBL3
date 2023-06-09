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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Ajax.Utilities;
using System.Web.UI.WebControls;

namespace PBL3_2.Controllers
{
    [Authorize]
    public class AccountInfoesController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountInfoesController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        }

        public AccountInfoesController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private DBGym db = new DBGym();
        // List of Member in Gym
        public ActionResult Index(string strSearchThietBi, string SortOrder, string SortBy, int? page)
        {

            //ViewBag.strSearch = strSearch;

            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;
            ViewBag.CurrentFilter = strSearchThietBi;

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var currentRole = userManager.GetRoles(currentUser.Id).FirstOrDefault();
            var x = currentUser.UserName;
            var obj = db.Accounts.ToList();

            if (currentRole == "Admin")
            {
                obj = db.Accounts.Where(p => p.ACCOUNT_ROLE != "2").ToList();
            }
            if (currentRole == "Nhan Vien")
            {
                obj = db.Accounts.Where(p => p.ACCOUNT_ROLE == "0").ToList();
            }
            if (currentRole == "Khach Hang")
            {
                obj = db.Accounts.Where(p => p.ACCOUNT_NAME == currentUser.UserName).ToList();
            }
            if (currentRole == "Khach Hang")
            {
                return RedirectToAction("WatchYourProfile");

                //int pageSize = 1;
                //int pageNumber = (page ?? 1);
                //return View(obj.ToPagedList(pageNumber, pageSize));

            }
            else
            {
                //Tìm kiếm
                if (!String.IsNullOrEmpty(strSearchThietBi))
                {
                    obj = obj.Where(p => p.ACCOUNT_NAME.Contains(strSearchThietBi)
                                         || p.AccountInfo.USER_NAME.Contains(strSearchThietBi)
                                         || p.AccountInfo.ACCOUNT_BIRTHDAY.ToString().Contains(strSearchThietBi)
                                         || p.AccountInfo.ACCOUNT_GENDER.ToString() == strSearchThietBi
                                         || p.AccountInfo.ACCOUNT_HEIGHT.ToString() == strSearchThietBi
                                         || p.AccountInfo.ACCOUNT_WEIGHT.ToString() == strSearchThietBi
                                         || p.AccountInfo.ACCOUNT_PHONE.ToString().Contains(strSearchThietBi)
                                         || p.AccountInfo.ACCOUNT_EMAIL.ToString().Contains(strSearchThietBi)
                                         || p.ACCOUNT_ROLE.ToString().Contains(strSearchThietBi)
                                         || p.AccountInfo.ACCOUNT_CCCD.ToString().Contains(strSearchThietBi)).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.USER_NAME).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.USER_NAME).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.ACCOUNT_CCCD).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.ACCOUNT_CCCD).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.ACCOUNT_BIRTHDAY).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.ACCOUNT_BIRTHDAY).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.ACCOUNT_GENDER).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.ACCOUNT_GENDER).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.ACCOUNT_HEIGHT).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.ACCOUNT_HEIGHT).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.ACCOUNT_WEIGHT).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.ACCOUNT_WEIGHT).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.ACCOUNT_PHONE).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.ACCOUNT_PHONE).ToList();
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
                                        obj = obj.OrderBy(p => p.AccountInfo.ACCOUNT_EMAIL).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.AccountInfo.ACCOUNT_EMAIL).ToList();
                                        break;
                                    }
                            }
                            break;
                        }
                    case "ACCOUNT_ROLE":
                        {
                            switch (SortOrder)
                            {
                                case "Asc":
                                    {
                                        obj = obj.OrderBy(p => p.ACCOUNT_ROLE).ToList();
                                        break;
                                    }
                                default:
                                    {
                                        obj = obj.OrderByDescending(p => p.ACCOUNT_ROLE).ToList();
                                        break;
                                    }
                            }
                            break;
                        }

                }

                int pageSize = 6;
                int pageNumber = (page ?? 1);
                return View(obj.ToPagedList(pageNumber, pageSize));
            }
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

        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]

        public async Task<ActionResult> Them2(string account_name, string account_password, string Retype_Account_password,
         DateTime account_birthday, string User_name, string Account_CCCD, Boolean GioiTinh, double Account_Height,
         double Account_Weight, string Account_phone, string Account_email)
        {
            Account da = new Account()
            {
                ACCOUNT_NAME = account_name,
                ACCOUNT_PASSWORD = account_password,
                ACCOUNT_ROLE = "0"
            };

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
                return View("Them");
            }
            var user = new ApplicationUser { UserName = account_name, Email = Account_email };
            var result = await UserManager.CreateAsync(user, account_password);
            if (result.Succeeded)
            {
                await UserManager.AddToRoleAsync(user.Id, "Khach Hang");
                var Newuser = await UserManager.FindByNameAsync(user.UserName);


                db.AccountInfos.Add(dc);
                db.SaveChanges();
                da.ACCOUNT_PASSWORD = Newuser.PasswordHash;
                db.Accounts.Add(da);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                //fail
                ModelState.AddModelError("", "Có lỗi xãy ra");
                return View("Them");
            }


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

        // update role in identity table 
        public async Task UpdateUserRoleAsync(string userName, string newRole)
        {
            using (var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext()))
            using (var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext()))
            using (var userManager = new UserManager<ApplicationUser>(userStore))
            using (var roleManager = new RoleManager<IdentityRole>(roleStore))
            {
                // Find the user by their username
                var user = await userManager.FindByNameAsync(userName);

                if (user != null)
                {
                    // Get the user's roles
                    var userRoles = await userManager.GetRolesAsync(user.Id);

                    // Remove the user from their current roles
                    foreach (var role in userRoles)
                    {
                        await userManager.RemoveFromRoleAsync(user.Id, role);
                    }

                    // If the new role does not exist, create it
                    if (!await roleManager.RoleExistsAsync(newRole))
                    {
                        var role = new IdentityRole(newRole);
                        await roleManager.CreateAsync(role);
                    }

                    // Add the user to the new role
                    await userManager.AddToRoleAsync(user.Id, newRole);
                }
                else
                {
                    // Handle the case when the user with the specified username is not found
                }
            }
        }


        public void UpdateRole_inIdentity(string userName, string newRole)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindByName(userName);
            if (user != null)
            {
                var roles = userManager.GetRoles(user.Id);
                var currentRole = roles.FirstOrDefault().ToString();

                userManager.RemoveFromRole(user.Id, currentRole);
                userManager.AddToRole(user.Id, newRole);
            }

        }
        // GET: AccountInfoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfos.Find(id);
            ViewBag.Role = db.Accounts.Where(p => p.ACCOUNT_NAME == id).FirstOrDefault().ACCOUNT_ROLE;
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
        public ActionResult Edit([Bind(Include = "ACCOUNT_NAME,USER_NAME,ACCOUNT_CCCD,ACCOUNT_BIRTHDAY,ACCOUNT_GENDER,ACCOUNT_HEIGHT,ACCOUNT_WEIGHT,ACCOUNT_PHONE,ACCOUNT_EMAIL")] AccountInfo accountInfo, string role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountInfo).State = EntityState.Modified;
                db.SaveChanges();


                // update role in table accounts 
                //string s = "UPDATE Accounts\r\nSET ACCOUNT_ROLE = " + role + "\r\nWHERE ACCOUNT_NAME = " + accountInfo.ACCOUNT_NAME+ ";";
                var tmp = db.Accounts.Where(p => p.ACCOUNT_NAME == accountInfo.ACCOUNT_NAME).FirstOrDefault();
                tmp.ACCOUNT_ROLE = role;
                db.Entry(tmp).State = EntityState.Modified;
                db.SaveChanges();

                // update role in table identity
                if (role == "2")
                {
                    role = "Admin";
                }
                if (role == "1")
                {
                    role = "Nhan Vien";
                }
                if (role == "0")
                {
                    role = "Khach Hang";
                }
                UpdateRole_inIdentity(accountInfo.ACCOUNT_NAME, role);
                return RedirectToAction("Index");
            }
            return View(accountInfo);
        }


        public ActionResult ChangePassWord(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfos.Find(id);
            ViewBag.username = db.Accounts.Where(p => p.ACCOUNT_NAME == id).FirstOrDefault().ACCOUNT_NAME;
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassWord(string Account_password, string Retype_Account_password, string id)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindByName(id);
            ViewBag.username = id;
            if (user != null)
            {
                if (Account_password == Retype_Account_password && Account_password.Length >= 6)
                {
                    userManager.RemovePassword(user.Id);
                    userManager.AddPassword(user.Id, Account_password);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return HttpNotFound();
            }

            return View();
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
        public async Task<ActionResult> DeleteConfirmedAsync(string id)
        {
            var account = db.Accounts.Where(p => p.ACCOUNT_NAME == id).ToList().Count();
            if (account > 0)
            {
                db.Accounts.RemoveRange(db.Accounts.Where(p => p.ACCOUNT_NAME == id).ToList());
                db.SaveChanges();

                AccountInfo accountInfo = db.AccountInfos.Find(id);
                db.AccountInfos.Remove(accountInfo);
                db.SaveChanges();

                var user = await userManager.FindByNameAsync(id); // replace "username" with the actual username of the user you want to delete
                if (user != null)
                {
                    var result = await userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        // user account deleted successfully
                    }
                    else
                    {
                        // handle error
                    }
                }
                else
                {
                    // user not found
                }

            }

            return RedirectToAction("Index");
        }


        public ActionResult WatchYourProfile()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var currentRole = userManager.GetRoles(currentUser.Id).FirstOrDefault();

            var obj = db.Accounts.ToList();
            obj = db.Accounts.Where(p => p.ACCOUNT_NAME == currentUser.UserName).ToList();

            return View(obj);
        }

        public ActionResult Edit_PersonalProfile()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = userManager.FindById(User.Identity.GetUserId());
            var currentRole = userManager.GetRoles(currentUser.Id).FirstOrDefault();
            string id = currentUser.UserName;

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_PersonalProfile([Bind(Include = "ACCOUNT_NAME,USER_NAME,ACCOUNT_CCCD,ACCOUNT_BIRTHDAY,ACCOUNT_GENDER,ACCOUNT_HEIGHT,ACCOUNT_WEIGHT,ACCOUNT_PHONE,ACCOUNT_EMAIL")] AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountInfo);
        }







        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

















        public ActionResult XemDanhSachThongTinDuaTren_Role(string strSearchThietBi, string SortOrder, string SortBy, int? page)

        {

            //ViewBag.strSearch = strSearch;

            ViewBag.SortOrder = SortOrder;
            ViewBag.SortBy = SortBy;
            ViewBag.CurrentFilter = strSearchThietBi;

            var obj = db.AccountInfos.ToList();

            if (User.Identity.IsAuthenticated)
            { // da dang nhap chua 
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var currentUser = userManager.FindById(User.Identity.GetUserId());
                var currentRole = userManager.GetRoles(currentUser.Id).FirstOrDefault();


                if (currentRole == "Admin")
                {
                    obj = db.AccountInfos
                    .Join(db.Accounts, a => a.ACCOUNT_NAME, b => b.ACCOUNT_NAME, (a, b) => new { AccountInfo = a, Account = b })
                    .Where(ab => ab.Account.ACCOUNT_ROLE == "0" || ab.Account.ACCOUNT_ROLE == "1")
                    .Select(ab => ab.AccountInfo).ToList();

                    var currentUserInfo = db.AccountInfos.FirstOrDefault(a => a.ACCOUNT_NAME == currentUser.UserName);
                    if (currentUserInfo != null)
                    {
                        obj.Add(currentUserInfo);
                    }



                }
                else if (currentRole == "Nhan Vien")
                {
                    obj = db.AccountInfos
                   .Join(db.Accounts, a => a.ACCOUNT_NAME, b => b.ACCOUNT_NAME, (a, b) => new { AccountInfo = a, Account = b })
                   .Where(ab => ab.Account.ACCOUNT_ROLE == "0")
                   .Select(ab => ab.AccountInfo).ToList();


                    var currentUserInfo = db.AccountInfos.FirstOrDefault(a => a.ACCOUNT_NAME == currentUser.UserName);
                    if (currentUserInfo != null)
                    {
                        obj.Add(currentUserInfo);
                    }
                }
                else if (currentRole == "Khach Hang")
                {
                    obj = db.AccountInfos.Where(a => a.ACCOUNT_NAME == currentUser.UserName).ToList();
                }
            }

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

    }
}
