using PBL3_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer;
using System.Web;

namespace PBL3_2.Service
{
    public class QLBienLai
    {
        static int Convert_Date_to_Int(string date)
        {
            if (date == "Monday")
            {
                return 0;
            }
            else if (date == "Tuesday")
            {
                return 1;
            }
            else if (date == "Wednesday")
            {
                return 2;
            }
            else if (date == "Thursday")
            {
                return 3;
            }
            else if (date == "Friday")
            {
                return 4;
            }
            else if (date == "Saturday")
            {
                return 5;
            }
            else if (date == "Sunday")
            {
                return 6;
            }
            return 0;
        }
        public static int Cal_Money(int ID_LOP)
        {
            DBGym db = new DBGym();
            int money = 0;


            Lop lop = db.Lops.Find(ID_LOP);
            DateTime iterator = lop.LOP_START.Value.Date;
            if (iterator <= DateTime.Now.Date) iterator = DateTime.Now.Date;

            if (lop.Staff == null) // Tinh tien cho lop ko co PT
            {
                while(iterator<= lop.LOP_END.Value.Date)
                {
                    money += lop.LoaiGoi.GOI_FEE;
                    iterator = iterator.AddDays(1);
                }
                return money;
            }

            // Tinh tien lop co PT 

            while (iterator <= lop.LOP_END.Value.Date)
            {
                foreach (PhienTap i in lop.PhienTaps)
                {
                    if (i.PHIENTAP_DATE == Convert_Date_to_Int(iterator.DayOfWeek.ToString()))
                    {
                        money += lop.LoaiGoi.GOI_FEE;
                    }
                }
                iterator = iterator.AddDays(1);
            }
            return money;
        }

        public List<BienLai> ListBL()
        {
            DBGym db = new DBGym();
            return db.BienLais.ToList();
        }
        public List<BienLai> getBienLaiByUserName(string userName)
        {
            DBGym db = new DBGym();
            List<BienLai> l = db.BienLais.Where(p =>
                p.Account.ACCOUNT_NAME == userName).ToList();
            return l;
        }

        public List<BienLai> getBienLaiByUserNameAndTenNguoiTraTien(string userName, string TenNguoiTraTien)
        {
            DBGym db = new DBGym();
            return db.BienLais.Where(p => p.Account.ACCOUNT_NAME.Contains(TenNguoiTraTien) &&
                        p.Account.ACCOUNT_NAME == userName
                        ).ToList();
        }

        public List<BienLai> SearchBLByDate(List<BienLai> tmp, DateTime Batdau, DateTime Ketthuc)
        {
            List<BienLai> list = new List<BienLai>();
            foreach (var i in tmp)
            {
                if (Batdau <= i.BIENLAI_START.Value.Date && i.BIENLAI_END.Value.Date <= Ketthuc)
                {
                    list.Add(i);
                }
            }
            return list;
        }
        public List<BienLai> SearchBLByBegin(List<BienLai> tmp, DateTime Batdau)
        {
            List<BienLai> list = new List<BienLai>();
            foreach (var i in tmp)
            {
                if (Batdau <= i.BIENLAI_START.Value.Date)
                {
                    list.Add(i);
                }
            }
            return list;
        }
        public List<BienLai> SearchBLByKethuc(List<BienLai> tmp, DateTime Ketthuc)
        {
            List<BienLai> list = new List<BienLai>();
            foreach (var i in tmp)
            {
                if (i.BIENLAI_END.Value.Date <= Ketthuc)
                {
                    list.Add(i);
                }
            }
            return list;
        }


        public List<BienLai> SortBL(List<BienLai> l, int SortBy, int SortOrder)
        {
            if (SortBy == 1)
            {
                if (SortOrder == 1) l = l.OrderBy(p => p.Account.ACCOUNT_NAME).ToList();
                else if (SortOrder == 2) l = l.OrderByDescending(p => p.Account.ACCOUNT_NAME).ToList();
            }
            else if (SortBy == 2)
            {
                if (SortOrder == 1) l = l.OrderBy(p => p.BIENLAI_PAYMENT).ToList();
                else if (SortOrder == 2) l = l.OrderByDescending(p => p.BIENLAI_PAYMENT).ToList();
            }
            else if (SortBy == 3)
            {
                if (SortOrder == 1) l = l.OrderBy(p => p.BIENLAI_START.Value.Date).ToList();
                else if (SortOrder == 2) l = l.OrderByDescending(p => p.BIENLAI_START.Value.Date).ToList();
            }
            else if (SortBy == 4)
            {
                if (SortOrder == 1) l = l.OrderBy(p => p.BIENLAI_END.Value.Date).ToList();
                else if (SortOrder == 2) l = l.OrderByDescending(p => p.BIENLAI_END.Value.Date).ToList();
            }
            return l;
        }

        public int TotalCost(List<BienLai> l)
        {
            int total = 0;
            foreach (var i in l)
            {
                total += i.BIENLAI_PAYMENT.Value;
            }
            return total;
        }

    }
}