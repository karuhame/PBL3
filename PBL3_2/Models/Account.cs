using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3_2.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ACCOUNT_ID { get; set; }

        [StringLength(200)]
        public string ACCOUNT_NAME { get; set; }
         
        [StringLength(200)]
        public string ACCOUNT_PASSWORD { get; set; }

        public string ACCOUNT_ROLE { get; set; }
        public List<PhienTap>[] Lich { get; set; }
        public virtual AccountInfo AccountInfo { get; set; }
        public virtual ICollection<Lop> Lops { get; set; }
        public virtual ICollection<BienLai> BienLais { get; set;}
        public Account()
        {
            this.Lich = new List<PhienTap>[7];
            this.Lops = new HashSet<Lop>();
            this.BienLais= new HashSet<BienLai>();
        }
    }
}
