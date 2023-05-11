using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PBL3_2.Models
{
    public class AccountInfo
    {

        
        [Key, StringLength(28)]
        public string ACCOUNT_NAME { get; set; }

        [StringLength(28)]
        public string USER_NAME { get; set; }

        [StringLength(20)]
        public string ACCOUNT_CCCD { get; set; }
         
        public DateTime? ACCOUNT_BIRTHDAY { get; set; }

        public bool? ACCOUNT_GENDER { get; set; }

        public double? ACCOUNT_HEIGHT { get; set; }

        public double? ACCOUNT_WEIGHT { get; set; }

        [StringLength(10)]
        public string ACCOUNT_PHONE { get; set; }

        [StringLength(28)]
        public string ACCOUNT_EMAIL { get; set; }

        
    }
}