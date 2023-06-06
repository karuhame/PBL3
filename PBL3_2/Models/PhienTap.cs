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
        public int LOP_ID { get; set; }
        [ForeignKey("LOP_ID")]
        public virtual Lop Lop { get; set; }

        public static void updatePhienTaps(List<PhienTap> phienTaps)
        {
            DBGym db = new DBGym();
            foreach (PhienTap item in phienTaps)
            {
                PhienTap temp = db.PhienTaps.Find(item.PHIENTAP_ID);
                temp = item;
                db.SaveChanges();
            }
        }
    }
}
