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

        public List<BienLai> getBienLaiByUserNameAndTenNguoiTraTien(string userName,string TenNguoiTraTien) {
            DBGym db = new DBGym();
            return db.BienLais.Where(p => p.Account.ACCOUNT_NAME.Contains(TenNguoiTraTien) &&
                        p.Account.ACCOUNT_NAME == userName
                        ).ToList();
        }

        public List<BienLai> SearchBLByDate(List<BienLai> tmp,DateTime Batdau,DateTime Ketthuc)
        {
            List <BienLai> list = new List<BienLai>();
            foreach (var i in tmp)
            {
                if (Batdau <= i.BIENLAI_START.Value.Date && i.BIENLAI_END.Value.Date <= Ketthuc)
                {
                    list.Add(i);
                }
            }
            return list;
        }


        public List<BienLai> SortBL(List<BienLai> l,int SortBy,int SortOrder)
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

    }
}