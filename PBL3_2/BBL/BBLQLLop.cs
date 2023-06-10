using PBL3_2.Controllers;
using PBL3_2.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
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

        public void AddLop(Lop lop, int AccountID, int GoiID, int StaffID)
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

        //Tìm kiếm PT trống trong mọi phiên tập 

        public static bool CompareTime(DateTime t1, DateTime t2)
        {
            long res1 = t1.Hour * 3600 + t1.Minute * 60 + t1.Second;
            long res2 = t2.Hour * 3600 + t2.Minute * 60 + t2.Second;
            if (t1 > t2) return true;
            return false;
        }
        public List<Account> FindPT(int ID_LOP, List<PhienTap> phienTapList)
        {

            List<Account> list = new List<Account>();
            //Chạy hết tài khoản của nhân viên
            //Tìm xem tài khoản nhân viên nào rảnh trong các phiên tập
            //foreach (Account acc in db.Accounts.Where(p => p.ACCOUNT_ROLE == "1"))
            //{                
            //    int cnt = phienTapList.Count();
            //    foreach(PhienTap phien in phienTapList)
            //    {
            //        if(acc.Lich[phien.PHIENTAP_DATE] == null)
            //        {
            //            cnt--;

            //        }
            //        else
            //        {
            //        // Phiên tập day chuyển lại thành int
            //            foreach(PhienTap item in acc.Lich[phien.PHIENTAP_DATE])
            //            {
            //                // Nếu trùng trống thì chuyển sang ngày tiếp 
            //                if (
            //                    !((phien.PHIENTAP_startt >  item.PHIENTAP_startt && phien.PHIENTAP_endd >item.PHIENTAP_endd)
            //                    ||(phien.PHIENTAP_startt < item.PHIENTAP_startt && phien.PHIENTAP_endd < item.PHIENTAP_endd)
            //                    ||(phien.PHIENTAP_startt > item.PHIENTAP_startt && phien.PHIENTAP_endd < item.PHIENTAP_endd))

            //                )
            //                {
            //                    cnt--;
            //                    break;
            //                }
            //            }

            //        }
            //    }
            //    if(cnt == 0)    
            //    {
            //        list.Add(acc);
            //    }


            //}

            Lop temp = db.Lops.Find(ID_LOP);
            var abc = db.Accounts;
            foreach (Account i in abc)
            {

            }
            //Chạy hết tất cả các lớp
            foreach (Account NV in db.Accounts.Where(p => p.ACCOUNT_ROLE == "1"))
            {
                bool check = true;
                foreach (Lop lop in db.Lops.Where(p => p.Staff.ACCOUNT_ID == NV.ACCOUNT_ID))
                {

                    //Lớp không trùng lịch thì break
                    //if(!((temp.LOP_START > lop.LOP_START && temp.LOP_START < lop.LOP_END && temp.LOP_END < lop.LOP_END)
                    //            || (temp.LOP_START < lop.LOP_START && temp.LOP_END < lop.LOP_END && temp.LOP_END >lop.LOP_START)
                    //            || (temp.LOP_START > lop.LOP_START && temp.LOP_END < lop.LOP_END)
                    //            || (temp.LOP_START < lop.LOP_START && temp.LOP_START > lop.LOP_END)))
                    //{
                    //    continue;
                    //}  
                    if (temp.LOP_END < lop.LOP_START || temp.LOP_START > lop.LOP_END)
                    {
                        continue;
                    }

                    foreach (PhienTap phien in db.PhienTaps.Where(p => p.LOP_ID == lop.LOP_ID))
                    {
                        foreach (PhienTap item in phienTapList)
                        {
                            //Nếu trùng lịch của phiên tập thì chuyển trạng thái thành false
                            //if(phien.PHIENTAP_DATE == item.PHIENTAP_DATE 
                            //    && ((phien.PHIENTAP_startt > item.PHIENTAP_startt && phien.PHIENTAP_startt < item.PHIENTAP_endd && phien.PHIENTAP_endd > item.PHIENTAP_endd)
                            //    || (phien.PHIENTAP_startt < item.PHIENTAP_startt && phien.PHIENTAP_endd < item.PHIENTAP_endd && phien.PHIENTAP_endd > item.PHIENTAP_startt)
                            //    || (phien.PHIENTAP_startt > item.PHIENTAP_startt && phien.PHIENTAP_endd < item.PHIENTAP_endd)
                            //    || (phien.PHIENTAP_startt < item.PHIENTAP_startt && phien.PHIENTAP_endd > item.PHIENTAP_endd)))
                            //{
                            //    check = false;
                            //}
                            if (phien.PHIENTAP_DATE == item.PHIENTAP_DATE && !(CompareTime(phien.PHIENTAP_endd, item.PHIENTAP_startt) || CompareTime(phien.PHIENTAP_startt, item.PHIENTAP_endd)))
                            {
                                check = false;
                            }
                        }

                    }

                }
                if (check) list.Add(NV);

            }

            return list;
        }



        //public void ConfirmLop(string sub, int id)
        //{
        //    if (sub == "Accept")
        //    {
        //        db.Lops.Find(id).LOP_STATUS = "Accepted";

        //        db.SaveChanges();

        //    }
        //    else if (sub == "Delete")
        //    {
        //        db.Lops.Remove(db.Lops.Find(id));
        //        db.SaveChanges();
        //    }
        //}


    }
}
