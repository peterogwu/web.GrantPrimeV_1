using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData
{
    public class CustomerModel
    {
        public long ID { get; set; }
        public long RegistrationId { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string Middlename { get; set; }
        public string gender { get; set; }
        public string StateOfOrigin { get; set; }
        public string LGA { get; set; }
        public string ResidAddress { get; set; }
        public string PostFunct { get; set; }
        public string TaxReg { get; set; }
        public string Email { get; set; }
        public string BusSector { get; set; }
        public string BusAdress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Nationality { get; set; }
        public string WebsitAdress { get; set; }
        public string TaxRegNO { get; set; }
        public byte[] LastTax { get; set; }
        public string LastTaximageType { get; set; }
        public Nullable<System.DateTime> BussComDate { get; set; }
        public Nullable<int> NumYr { get; set; }
        public byte[] BussRegNumb { get; set; }
        public string BussRegNumbType2 { get; set; }
        public Nullable<int> NOofEmp { get; set; }
        public Nullable<int> NOofFemale { get; set; }
        public Nullable<int> NOofMale { get; set; }
        public string AgeGrade { get; set; }
        public byte[] Turnover { get; set; }
        public string TurnoverimageType3 { get; set; }
        public string BussSet { get; set; }
        public string BankName { get; set; }
        public string AcctNo { get; set; }
        public string BVN { get; set; }
        public string Status { get; set; }
        public string Rating { get; set; }
        public string Runinloan { get; set; }
        public string paymSchedule { get; set; }
        public Nullable<decimal> AmountPaid { get; set; }
        public string paymentChange { get; set; }
        public string repaymenobligation { get; set; }
        public string bussImpact { get; set; }
        public string bussNegImpact { get; set; }
        public string plantolayoff { get; set; }
        public string dolayoff { get; set; }
        public string bussoperat { get; set; }
        public string workfromhome { get; set; }
        public string grandrequest { get; set; }
        public string declareation { get; set; }
        public string approveComment { get; set; }
        public string rejectComment { get; set; }
        public string authId { get; set; }
        public string userId { get; set; }
        public Nullable<int> ApprovalLevel { get; set; }
        public string ApprovedBy { get; set; }
        public string rejectedBy { get; set; }
        public Nullable<int> grantType { get; set; }
    }
}