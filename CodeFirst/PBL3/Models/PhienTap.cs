﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3.Models
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
    }
}