//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PBL3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuanLyThietBi
    {
        public string QUANLYTHIETBI_ID { get; set; }
        public string ACCOUNT_ID { get; set; }
        public string THIETBI_ID { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual ThietBi ThietBi { get; set; }
    }
}
