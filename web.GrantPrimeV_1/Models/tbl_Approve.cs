//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace web.GrantPrimeV_1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Approve
    {
        public long approve_Id { get; set; }
        public Nullable<long> applicant_Id { get; set; }
        public string AuthId { get; set; }
        public Nullable<decimal> ApproveAmount { get; set; }
        public string ApproveComment { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<int> approvalLevel { get; set; }
        public Nullable<int> finalOrTranche { get; set; }
        public Nullable<int> Tranche { get; set; }
        public Nullable<int> status { get; set; }
        public string userid { get; set; }
    }
}
