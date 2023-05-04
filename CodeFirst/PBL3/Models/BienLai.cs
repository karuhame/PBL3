using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3.Models
{
    public class BienLai
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BIENLAI_ID { get; set; }

        public int? BIENLAI_PAYMENT { get; set; }

        public DateTime? BIENLAI_START { get; set; }

        public DateTime? BIENLAI_END { get; set; }

        public virtual Lop Lop { get; set; }
        public virtual Account Account { get; set; }
    }
}