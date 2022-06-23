using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData.ApplicantData
{
    public class ApplicantModel
    {
        public long applicant_Id { get; set; }
        public Nullable<long> Upload_Id { get; set; }
        [Required(ErrorMessage = "SurName is required.")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string FirstName { get; set; }
        public string Middlename { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BusinessName { get; set; }
        public string gender { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string StateOfOrigin { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string LGA { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string ResidAddress { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string Position { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string PersonalTaxRegNo { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BusinessSector { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BusinessType { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BusinessAddress { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string State { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BusinesPhoneNo { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BusinesEmail { get; set; }

        public string BusinesWebsite { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BusinesTaxRegNo { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public Nullable<System.DateTime> LastTaxPayDate { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public Nullable<System.DateTime> BusinesStartDate { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public Nullable<int> NoOfYrInOperation { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public Nullable<int> NOofEmployee { get; set; }
        public Nullable<int> NOofFemale { get; set; }
        public Nullable<int> NOofMale { get; set; }
        public string AgeGrade { get; set; }
        public string EstimValOfBusTurnoverXLB1 { get; set; }
        public decimal? EstimValOfBusTurnoverXLB { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BankName { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string AcctNo { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string BVN { get; set; }
        public Nullable<int> Status { get; set; }
        public decimal? Amount { get; set; }
        public string AnyRunningLoanPriorToCovid { get; set; }
        public decimal? TotalOutstandVolumeOfLoan { get; set; }
        public string TotalOutstandVolumeOfLoan1 { get; set; }
        public string HowOftenWereYouPaying { get; set; }
        public string HowMuchWereyouPaying1 { get; set; }
        public decimal? HowMuchWereyouPaying { get; set; }
        public string DidYourRepaymentChange { get; set; }
        public string RepaymntChangeToHowMuch1 { get; set; }
        public decimal? RepaymntChangeToHowMuch { get; set; }
        public string findItDiffToMeetPaymentOblig { get; set; }
        public string BusinImpactByCovid { get; set; }
        public string BusiNegImpactByCovid { get; set; }
        public string specifyOther { get; set; }
        public string PlanningToLayOffEmployeeDueToNeg { get; set; }
        public string DidYouLayOffEmployee { get; set; }
        public string NoOffEmployeeLayOff { get; set; }
        public string DidBusContDuringCovidCrisis { get; set; }
        public string WhtToolCudVeMadeItPosib { get; set; }
        public string WhtToolCudVeMadeBusMoreEffnt { get; set; }
        public string EmployeeAbleToWrkFrmHom { get; set; }
        public string WhtToolCudVeReqToMakeItPosib { get; set; }
        public Nullable<int> GrantTypeReqFor { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string fullname { get; set; }
        public string Declaration { get; set; }
        public string AllSectnOfFormFilled { get; set; }
        public string IsBusinessCertAttach { get; set; }
        public string IsEvidOfTaxPaymentAttach { get; set; }
        public string IsEvOf1YrTOverAttachOrVerifid { get; set; }
        public string VeBnkDetailProvided { get; set; }
        public string IsImpactOfCovidOnBusnVrfd { get; set; }
        public string GrantyTpe { get; set; }
        public string CreditGrantAmount1 { get; set; }
        public decimal? CreditGrantAmount { get; set; }
        public string OperatnGrantAmount1 { get; set; }
        public decimal? OperatnGrantAmount { get; set; }
        public string ITEnhanceGrantAmount1 { get; set; }
        public decimal? ITEnhanceGrantAmount { get; set; }
        public string approveComment { get; set; }
        public string rejectComment { get; set; }
        public string authId { get; set; }
        public string userId { get; set; }
        public Nullable<int> ApprovalLevel { get; set; }
        public Nullable<int> rejected { get; set; }
        public Nullable<int> Approved { get; set; }
        public string ApprovedBy { get; set; }
        public string rejectedBy { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<int> ReworkStatus { get; set; }
        public decimal? amountApprove { get; set; }
        public Nullable<int> DisburseType { get; set; }
        public Nullable<decimal> amntDisbursed { get; set; }
        public Nullable<decimal> disbursedBalance { get; set; }
        public Nullable<decimal> AmntDue { get; set; }
        public Nullable<int> NoOftranchDisb { get; set; }
        public Nullable<int> TotalNoOfTranDisb { get; set; }
        public string freguency { get; set; }

        public string phone { get; set; }
        public byte[] LstTaxDatePayEvidnce { get; set; }
        public string LstTaxDateEvidnceType { get; set; }
        public string BusnRegNum { get; set; }
        public byte[] BusnRegNumEvdnce { get; set; }
        public string BusnRegNumEvidnceType { get; set; }
        public string Lst3YrAvrgeAnnualBusnTOver1 { get; set; }
        public Nullable<decimal> Lst3YrAvrgeAnnualBusnTOver { get; set; }
        public byte[] Lst3YrAvrgeAnnualBusnTOverEvdnce { get; set; }
        public string Lst3YrAvrgeAnnualBusnTOverEvdnceType { get; set; }
        public string ApplicantPassportName { get; set; }
        public byte[] ApplicantPassport { get; set; }
        public string ApplicantPassportType { get; set; }
        public byte[] ApplicantSign { get; set; }
        public string ApplicantSignType { get; set; }
        public string EvidnceOfLoanOrCreditGrantName { get; set; }
        public byte[] EvidnceOfLoanOrCreditGrant { get; set; }
        public string EvidnceOfLoanOrCreditGrantType { get; set; }
        public string EvidnceOfOpertnExp4pst3mnthName { get; set; }
        public byte[] EvidnceOfOpertnExp4pst3mnth { get; set; }
        public string EvidnceOfOpertnExp4pst3mnthType { get; set; }
        public string EvidnceOfOCorpOrAssocMemberName { get; set; }
        public byte[] EvidnceOfOCorpOrAssocMember { get; set; }
        public string EvidnceOfOCorpOrAssocMemberType { get; set; }
        public string EnumeratorName { get; set; }
        public byte[] EnumeratorSign { get; set; }
        public string EnumeratorSignType { get; set; }
        public string DL1TeamLeadComment { get; set; }
        public byte[] DL1teamLeadSignWDate { get; set; }
        public string DL1teamLeadSignType { get; set; }
        public Nullable<decimal> DL1teamLeadApprovAmnt { get; set; }
        public string DL2TeamLeadComment { get; set; }
        public byte[] DL2teamLeadSign { get; set; }
        public string DL2teamLeadSignType { get; set; }
        public Nullable<decimal> DL2teamLeadApprovAmnt { get; set; }
        public string DL3TeamLeadComment { get; set; }
        public byte[] DL3teamLeadSignWdDate { get; set; }
        public string DL3teamLeadSignType { get; set; }
        public Nullable<decimal> DL3teamLeadApprovAmnt { get; set; }
        public string StateCareCordinatorComment { get; set; }
        public byte[] StateCareCordinatorSign { get; set; }
        public string StateCareCordinatorSignType { get; set; }
        public decimal? StateCareCordinatorApprovAmnt { get; set; }


        public Nullable<int> C18_35 { get; set; }
        public Nullable<int> C36_45 { get; set; }
        public Nullable<int> C46andabove { get; set; }
        public DateTime? Datedue { get; set; }
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? date_of_birth { get; set; }
        public int? installdue { get; set; }
        public int? Age { get; set; }
    }
}