using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData.ApplicantData
{
    public class ApplicantViewModel
    {
        public string FullName { get; set; }
        public string ApplicationType { get; set; }
        public string Status { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string BVN { get; set; }
        public string gender { get; set; }
        public string LGA { get; set; }
        public DateTime? date_of_birth { get; set; }
        public string BusinessAddress { get; set; }
        public string City { get; set; }
        public string BusinessSector { get; set; }
        public string BusinessType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalOutstandVolumeOfLoan { get; set; }
        public string Comment { get; set; }
        public string BusinessName { get; set; }
        public DateTime? Disbursedate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public decimal? amountApprove { get; set; }
        public decimal? amntDisbursed { get; set; }
        public decimal? disbursedBalance { get; set; }
        public int? NoOftranchDisb { get; set; }
        public int? TotalNoOfTranDisb { get; set; }
        public string freguency { get; set; }
        public string DisburseType { get; set; }

    }
}