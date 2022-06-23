using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData.UserProfile
{
    public class UserProfile
    {
        public int id { get; set; }
        public string Userid { get; set; }
        public string userpassword { get; set; }
        public string fullname { get; set; }
        public string deptcode { get; set; }
        public Nullable<int> Role_id { get; set; }
        public string ipaddress { get; set; }
        public string loginstatus { get; set; }
        public Nullable<int> Authoriser { get; set; }
        public string branchcode { get; set; }
        public string Staff_status { get; set; }
        public string PostGL_Acctno { get; set; }
        public Nullable<System.DateTime> passchange_date { get; set; }
        public Nullable<System.DateTime> Next_Passchange_date { get; set; }
        public Nullable<System.DateTime> Create_date { get; set; }
        public Nullable<long> SScode { get; set; }
        public Nullable<int> sessionid { get; set; }
        public string Computername { get; set; }
        public string machaddress { get; set; }
        public Nullable<int> lockcount { get; set; }
        public string ReportLevel { get; set; }
        public string AuthUserid { get; set; }
        public string PostUserId { get; set; }
        public string email { get; set; }
        public string phoneno { get; set; }
        public Nullable<int> Status { get; set; }
        public string authid { get; set; }
        public Nullable<int> smsalert { get; set; }
        public Nullable<int> emailalert { get; set; }
        public Nullable<int> offlinealert { get; set; }
        public string authorisedby { get; set; }
        public Nullable<int> enforce_pwd { get; set; }
        public Nullable<int> excemptlock { get; set; }
        public Nullable<int> statement { get; set; }
        public Nullable<int> remotelogin { get; set; }
        public Nullable<decimal> targetamt { get; set; }
        public Nullable<int> logincount { get; set; }
        public Nullable<int> multilogin { get; set; }
        public Nullable<int> SBU { get; set; }
        public Nullable<int> reportflag { get; set; }
        public System.Guid ActivationCode { get; set; }
        [DataType(DataType.Password)]
        [Compare("userpassword", ErrorMessage = "New password and confirm password does not match")]
        public string ConfirmPassword { get; set; }
        public string PasswordResetCode { get; set; }
    }
}