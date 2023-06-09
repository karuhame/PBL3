using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebGrease.Css.Extensions;

namespace PBL3_2.Models
{
    public class Lop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int LOP_ID { get; set; }
        public DateTime? LOP_START { get; set; }

        public DateTime? LOP_END { get; set; }

        public int? LOP_NUMBERSESSION { get; set; }
        public int GOI_ID { get; set; }

        public virtual Account Staff { get; set; }

        public string LOP_STATUS { get; set; }



        public virtual ICollection<PhienTap> PhienTaps { get; set; }

        [ForeignKey("GOI_ID")]
        public virtual LoaiGoi LoaiGoi { get; set; }
        public virtual ICollection<BienLai> BienLais { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }

        public Lop()
        {
            this.PhienTaps = new HashSet<PhienTap>();
            this.BienLais = new HashSet<BienLai>();
            this.Accounts = new HashSet<Account>();
            this.LOP_STATUS = "Waiting";
        }

        public void ConfirmCreate(string sub, int acc_ID)
        {


            DBGym db = new DBGym();

            if (sub == "Accept")
            {
                db.Lops.Find(this.LOP_ID).LOP_STATUS = "Accepted";
                if (acc_ID != null)
                {
                    BienLai.CreateBienLai(this.LOP_ID, acc_ID);
                }
            }
            else if (sub == "Delete")
            {
                db.Lops.Remove(db.Lops.Find(this.LOP_ID));
            }
            db.SaveChanges();
        }

        public void AddNewClient(int ID)
        {
            DBGym db = new DBGym();
            this.Accounts.Add(db.Accounts.Find(ID));
            db.SaveChanges();
        }

        public void AddNewLop()
        {
            DBGym db = new DBGym();
            db.Lops.Add(this);
            db.SaveChanges();
        }

        public static void RemoveClientFromLop(string Acc_Name, int ID_LOP)
        {
            DBGym db = new DBGym();
            Lop lop = db.Lops.Find(ID_LOP);
            Account user = db.Accounts.Where(p => p.ACCOUNT_NAME == Acc_Name).FirstOrDefault();
            try
            {
                lop.Accounts.Remove(user);
            }
            catch (Exception ex)
            {
                return;
            }
            db.SaveChanges();

        }

        public static List<Lop> GetLopByIdStaff(int id)
        {
            DBGym db = new DBGym();
            Account x = db.Accounts.Find(id);
            List<Lop> list = new List<Lop>();
            foreach(Lop item in db.Lops)
            {
                if(item.Staff.ACCOUNT_ID == id)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public static List<AccountInfo> GetClientInfoByIdLop(int LOP_ID)
        {
            DBGym db = new DBGym();
            List<AccountInfo> accountInfos = new List<AccountInfo>();
            List<Account> accounts = new List<Account>();
            Lop lop = db.Lops.Find(LOP_ID);

            foreach (Account i in lop.Accounts)
            {
                accountInfos.Add(i.AccountInfo);
            }
            return accountInfos;
        }

        public static List<Lop> GetLopNotJoiningByAccId(int Acc_ID)
        {
            DBGym db = new DBGym();
            List<Lop> list = new List<Lop>();
            Account acc = db.Accounts.Find(Acc_ID);
            foreach (Lop item in db.Lops)
            {
                bool check = true;
                foreach (Lop i in acc.Lops)
                {
                    if (item.LOP_ID == i.LOP_ID) check = false;
                }
                if (check) list.Add(item);
            }
            return list.ToList();
        }

        public static void ConfirmLopAdmin(int request_id, string sub, int query)
        {
            DBGym db = new DBGym();
            Request rq = db.Requests.Find(request_id); 
            if (sub == "AcceptJoin")
            {
                if (query == 0)
                {
                    rq.JoinRequest();
                }
                else if (query == 1)
                {
                    rq.status = true;
                    db.SaveChanges();
                    rq.EditRequest();
                }
            }
            else
            {
                if (query == 0)
                {
                    db.PhienTaps.RemoveRange(db.PhienTaps.Where(p => p.REQUEST_ID == request_id));
                    db.Requests.Remove(rq);
                }
                else if (query == 1)
                {
                    db.PhienTaps.RemoveRange(db.PhienTaps.Where(p => p.REQUEST_ID == request_id));
                    db.SaveChanges();
                    db.Requests.Remove(rq);
                }
                db.SaveChanges();
            }
        }

        public static void CheckValidClass()
        {
            DBGym db = new DBGym();
            foreach(Lop lop in db.Lops)
            {
                if(lop.LOP_END < DateTime.Now)
                {
                    Lop.DeleteLop(lop.LOP_ID);
                }
            }
            db.SaveChanges();
        }

        public static void DeleteLop(int ID_LOP)
        {
            DBGym db = new DBGym();
            //foreach(PhienTap pt in db.PhienTaps)
            //{
            //    if(pt.LOP_ID == ID_LOP)
            //    {
            //        PhienTap.DeletePhienTap(pt.PHIENTAP_ID);
            //    }
            //}
            //db.PhienTaps.RemoveRange(db.PhienTaps.Where(p => p.LOP_ID == ID_LOP));
            foreach(PhienTap pt in db.PhienTaps.Where(p => p.LOP_ID == ID_LOP))
            {
               if(db.Requests.Find(pt.REQUEST_ID) != null)
                {
                    db.Requests.Remove(db.Requests.Find(pt.REQUEST_ID));
                }
                db.PhienTaps.Remove(pt);
            }
            db.SaveChanges();
            db.Lops.Remove(db.Lops.Find(ID_LOP));
            db.SaveChanges();
        }
    }

    public class LopPhienTapsView
    {
        [Key, Required]
        public int ID { get; set; }
        public Lop lop { get; set; }
        public ICollection<PhienTap> phienTaps { get; set; }

        public LopPhienTapsView(Lop LOP, List<PhienTap> listPhienTaps)
        {
            DBGym db = new DBGym();

            this.lop = LOP;
            foreach (PhienTap i in listPhienTaps)
            {
                PhienTap temp = i;
                this.phienTaps.Add(temp);
            }

            db.SaveChanges();
        }


    }
}