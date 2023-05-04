using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ACCOUNT_ID { get; set; }

        [StringLength(20)]
        public string ACCOUNT_NAME { get; set; }

        [StringLength(20)]
        public string ACCOUNT_PASSWORD { get; set; }

        public string ACCOUNT_CONFIRMEDPASSWORD { get; set; }
        public int? ACCOUNT_ROLE { get; set; }
        public virtual AccountInfo AccountInfo { get; set; }
        public virtual ICollection<Lop> Lops { get; set; }
        public virtual ICollection<BienLai> BienLais { get; set;}
        public virtual ICollection<ThietBi> ThietBis { get; set; }  

        public Account()
        {
            this.Lops = new HashSet<Lop>();
            this.BienLais= new HashSet<BienLai>();
            this.ThietBis = new HashSet<ThietBi>();
        }
    }
}