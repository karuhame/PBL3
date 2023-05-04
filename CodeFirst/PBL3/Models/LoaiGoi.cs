using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3.Models
{
    public class LoaiGoi
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int  GOI_ID { get; set; }
        public string GOI_TYPE { get; set; }
        
        //Phí cho từng buổi tập
        public int GOI_FEE { get; set; }
       
        

    }
}