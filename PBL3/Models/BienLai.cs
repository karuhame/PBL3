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
    
    public partial class BienLai
    {
        public string BIENLAI_ID { get; set; }
        public Nullable<int> BIENLAI_PAYMENT { get; set; }
        public string LOP_ID { get; set; }
        public string ACCOUNT_ID { get; set; }
        public Nullable<System.DateTime> BIENLAI_START { get; set; }
        public Nullable<System.DateTime> BIENLAI_END { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Lop Lop { get; set; }
    }
}
