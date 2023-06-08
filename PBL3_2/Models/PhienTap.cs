using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3_2.Models
{
    public class PhienTap
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PHIENTAP_ID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PHIENTAP_DAY { get; set; }

        public int? PHIENTAP_START { get; set; }

        public int? PHIENTAP_END { get; set; }


        public int PHIENTAP_DATE { get; set; }
        public DateTime PHIENTAP_startt { get; set; }
        public DateTime PHIENTAP_endd { get; set; }
        public int? LOP_ID { get; set; }
        [ForeignKey("LOP_ID")]
        public virtual Lop Lop { get; set; }

        public int? REQUEST_ID { get; set; }
        [ForeignKey("REQUEST_ID ")]
        public virtual Request Request { get; set; }


        public void UpdatePhienTapByPhienTap(PhienTap i)
        {
            DBGym db = new DBGym();
            this.PHIENTAP_DATE = i.PHIENTAP_DATE;
            this.PHIENTAP_startt = i.PHIENTAP_startt;
            this.PHIENTAP_endd = i.PHIENTAP_endd;
            db.SaveChanges();
        }

        public static void updatePhienTaps(List<PhienTap> phienTaps, int id)
        {
            DBGym db = new DBGym();
            Lop lop = db.Lops.Find(id);

            int cnt = 0;
            foreach(PhienTap item in lop.PhienTaps)
            {
                item.UpdatePhienTapByPhienTap(phienTaps[cnt++]);
                db.SaveChanges();
            }
            List<PhienTap> list = lop.PhienTaps.ToList();
        }
    }
}
