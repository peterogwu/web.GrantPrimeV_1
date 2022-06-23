using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData
{
    public class AuthoriseModel
    {
        public long applicant_Id { get; set; }
        public decimal rejectAmount { get; set; }
        public int? rejectType { get; set; }
        public int? approveType { get; set; }
        public decimal approveAmount { get; set; }
        public string rejectComment { get; set; }
        public string ApproveComment { get; set; }
        public string App_bvn { get; set; }
        public string userId { get; set; }
        public int? NtimeDisburse { get; set; }
        public int? Frequency { get; set; }
        public int? grantType { get; set; }
    }
}