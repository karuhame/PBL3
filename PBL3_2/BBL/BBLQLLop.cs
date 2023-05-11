using PBL3_2.Controllers;
using PBL3_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBL3_2.BBL
{
    public class BBLQLLop
    {
        DBGym db = null;
        public BBLQLLop()
        {
            db = new DBGym();
        }

        public void AddLop(Lop lop, int AccountID,int GoiID, int StaffID)
        {
            db.Lops.Add(lop);
            lop.LoaiGoi = db.LoaiGois.Find(GoiID);
            lop.Accounts.Add(db.Accounts.Find(StaffID));
            lop.Accounts.Add(db.Accounts.Find(AccountID));

            db.SaveChanges();
        }

        public void AddPhienTap(int LopID, PhienTap phienTap)
        {
            var lop = db.Lops.Find(LopID);
            lop.PhienTaps.Add(phienTap);
            db.SaveChanges();
        }
        //public List<Account> FindPT(List<PhienTap> phienTapList) {

        //    List<Account> list = new List<Account>();
        //    //Chạy hết tài khoản của nhân viên
        //    //Tìm xem tài khoản nhân viên nào rảnh trong các phiên tập
        //    foreach(Account acc in db.Accounts)
        //    {
        //        if(acc.Equals )
        //    }
        //}


    }
}