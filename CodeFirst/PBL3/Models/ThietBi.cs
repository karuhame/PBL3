using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3.Models
{
    public class ThietBi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int THIETBI_ID { get; set; }

        public string THIETBI_NAME { get; set; }

        public int? THIETBI_STATUS { get; set; }

        public int? THIETBI_NUM { get; set; }
        public virtual Account Account { get; set; }
    }
}