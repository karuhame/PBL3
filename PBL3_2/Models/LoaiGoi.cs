using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3_2.Models
{
    public class LoaiGoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GOI_ID { get; set; }
        public string GOI_TYPE { get; set; }

        //Phí cho từng buổi tập
        public int GOI_FEE { get; set; }

        public int GOI_MAX_CUSTOMER { get; set; }
        public bool GOI_PT { get; set; }



    }
}