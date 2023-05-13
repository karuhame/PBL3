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

        // Xem đã được xác nhận chưa {Accepted, Wating}
        public string LOP_STATUS { get; set; }
        public int GOI_ID { get; set; }

        public virtual Account Staff { get; set; }
        


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
        }
    }
}