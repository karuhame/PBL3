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

        //Tìm kiếm PT trống trong mọi phiên tập 
        public List<Account> FindPT(List<PhienTap> phienTapList)
        {

            List<Account> list = new List<Account>();
            //Chạy hết tài khoản của nhân viên
            //Tìm xem tài khoản nhân viên nào rảnh trong các phiên tập
            foreach (Account acc in db.Accounts.Where(p => p.ACCOUNT_ROLE == "Nhan Vien"))
            {                
                int cnt = phienTapList.Count();
                foreach(PhienTap phien in phienTapList)
                {
                    if(acc.Lich[phien.PHIENTAP_DATE] == null)
                    {
                        cnt--;
                        
                    }
                    else
                    {
                    // Phiên tập day chuyển lại thành int
                        foreach(PhienTap item in acc.Lich[phien.PHIENTAP_DATE])
                        {
                            // Nếu trùng trống thì chuyển sang ngày tiếp 
                            if (
                                !((phien.PHIENTAP_startt >  item.PHIENTAP_startt && phien.PHIENTAP_endd >item.PHIENTAP_endd)
                                ||(phien.PHIENTAP_startt < item.PHIENTAP_startt && phien.PHIENTAP_endd < item.PHIENTAP_endd)
                                ||(phien.PHIENTAP_startt > item.PHIENTAP_startt && phien.PHIENTAP_endd < item.PHIENTAP_endd))
                            
                            )
                            {
                                cnt--;
                                break;
                            }
                        }

                    }
                }
                if(cnt == 0)    
                {
                    list.Add(acc);
                }

                
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
