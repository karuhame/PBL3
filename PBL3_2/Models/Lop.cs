using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

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
            this.PhienTaps= new HashSet<PhienTap>();
            this.BienLais= new HashSet<BienLai>();
            this.Accounts= new HashSet<Account>();
            this.LOP_STATUS = "Waiting";
        }

        public void ConfirmCreate(string sub, Account acc)
        {


            DBGym db = new DBGym();

            if (sub == "Accept")
            {
                db.Lops.Find(this.LOP_ID).LOP_STATUS = "Accepted";
                if(acc != null)
                {
                    BienLai.CreateBienLai(this.LOP_ID, acc.ACCOUNT_ID);

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
   
        public void RemoveClientFromLop(int ID_ACCOUNT, int ID_LOP)
        {
            DBGym db = new DBGym();
            Lop lop = db.Lops.Find(ID_LOP);
            Account user = db.Accounts.Find(ID_ACCOUNT);
            try { 
                lop.Accounts.Remove(user);
            }
            catch(Exception ex)
            {
                return;
            }
            db.SaveChanges();
           
        }

        public List<Account> GetClientByIdLop()
        {
            DBGym db = new DBGym();
            List<Account> accounts = new List<Account>();
            Lop lop = db.Lops.Find(this.LOP_ID);
            return lop.Accounts.ToList();
        }
    }
}