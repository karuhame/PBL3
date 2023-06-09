using PBL3_2.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3_2.Models
{
    public class BienLai
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BIENLAI_ID { get; set; }

        public int? BIENLAI_PAYMENT { get; set; }

        public DateTime? BIENLAI_START { get; set; }

        public DateTime? BIENLAI_END { get; set; }

        public int ACCOUNT_ID { get; set; }
        public virtual Lop Lop { get; set; }

        [ForeignKey("ACCOUNT_ID")]
        public virtual Account Account { get; set; }

        public static void CreateBienLai(int ID_LOP, int ID_ACC)
        {
            using (DBGym db = new DBGym())
            {
                var lop = db.Lops.Find(ID_LOP);
                var acc = db.Accounts.Find(ID_ACC);

                BienLai bl = new BienLai()
                {
                    ACCOUNT_ID = ID_ACC,
                    BIENLAI_START = lop.LOP_START,
                    BIENLAI_END = lop.LOP_END,
                    BIENLAI_PAYMENT = QLBienLai.Cal_Money(ID_LOP)


                };
                db.BienLais.Add(bl);
                db.SaveChanges();
            }
        }
    }
}