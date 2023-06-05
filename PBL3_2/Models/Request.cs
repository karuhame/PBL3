using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;

namespace PBL3_2.Models
{
    public class Request
    {
        [Key, Required]
        public int REQUEST_ID { get; set; }
        public int ACCOUNT_ID { get; set; }
        public int LOP_ID { get; set; }
        public int query { get; set; }
        public bool status { get; set; }

        [ForeignKey("ACCOUNT_ID")]

        public virtual Account Account { get; set; }

        [ForeignKey("LOP_ID")]
        public virtual Lop Lop { get; set; }

        public Lop EditLop { get; set; }

        public ICollection<PhienTap> phienTaps { get; set; }

        public static void CreateRequest(int ACCOUNT_ID, int LOP_ID, int query)
        {
            DBGym db = new DBGym();
            Request rq = new Request();
            rq.ACCOUNT_ID = ACCOUNT_ID;
            rq.LOP_ID = LOP_ID;
            rq.query = query;
            rq.status = false;

            db.Requests.Add(rq);
            db.SaveChanges();
        }
        public void JoinRequest()
        {
            if(this.query == 0)
            {
                DBGym db = new DBGym();
                Lop lop = db.Lops.Find(this.LOP_ID);
                Account acc = db.Accounts.Find(this.ACCOUNT_ID);
                lop.Accounts.Add(acc);
                this.status = true;
                db.SaveChanges();
            }
        }
        public void ImplementRequest(LopPhienTapsView view)
        {
            DBGym db = new DBGym();
            switch (this.query)
            {
                //Yeu cau join lop
                case 0:


                    break;

                //Yeu cau chinh sua thong tin lop
                case 1:
                    this.ACCOUNT_ID = view.lop.LOP_ID;
                    foreach (PhienTap i in view.phienTaps)
                    {
                        PhienTap temp = i;
                        this.phienTaps.Add(temp);
                    }
                    db.SaveChanges();
                    break;
                default: break;
            }
            db.SaveChanges();
        
        }

    }

}