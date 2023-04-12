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
    
    public partial class Lop
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Lop()
        {
            this.BienLais = new HashSet<BienLai>();
            this.QuanLyLops = new HashSet<QuanLyLop>();
        }
    
        public string LOP_ID { get; set; }
        public string LOP_STAFFID { get; set; }
        public string LOP_CUSTOMERID { get; set; }
        public string PHIENTAP_ID { get; set; }
        public string GOI_ID { get; set; }
        public Nullable<System.DateTime> LOP_START { get; set; }
        public Nullable<System.DateTime> LOP_END { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BienLai> BienLais { get; set; }
        public virtual LoaiGoi LoaiGoi { get; set; }
        public virtual PhienTap PhienTap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuanLyLop> QuanLyLops { get; set; }
    }
}