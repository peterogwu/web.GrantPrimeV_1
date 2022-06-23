using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.StateTown;
using web.GrantPrimeV_1.Models.UserData;
using web.GrantPrimeV_1.Models.UserData.ApplicantData;

namespace PrimeRepo.Repository
{
    public class UserRepo : IUserRepo
    {

        private readonly PrimeGrantEntities _entity = new PrimeGrantEntities();

        public UserRepo(PrimeGrantEntities entity)
        {

            _entity = entity;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<ApplicantModel> GetCusterList(string user)
        {
            var AppList = new List<ApplicantModel>();
            try {
                 AppList = _entity.Database.SqlQuery<ApplicantModel>("Proc_GetAllApplicants @UserId",
                     new SqlParameter("@UserId", user)).ToList();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }

           

            return AppList;
        }
        public IEnumerable<ApplicantModel> GetApplicantsDisburse(int grantType, string bvn)
        {
            var AppList = new List<ApplicantModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantModel>("Proc_GetAllApplicantsDisburse @grantType, @BVn",
                    new SqlParameter("@grantType", grantType),
                    new SqlParameter("@BVn", bvn)
                    ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return AppList;
        }
        

                    public IEnumerable<ApplicantModel> GetAllTranchDisburseGrant()
        {
            var AppList = new List<ApplicantModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantModel>("Proc_Get_AllTrancheDisbursedGrant").ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return AppList;
        }
        public IEnumerable<ApplicantModel> GetAllAprovedApplicantsYetToDisburse(int grantType)
        {
            var AppList = new List<ApplicantModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantModel>("Proc_GetAllApprovedApplicants @grantType",
                    new SqlParameter("@grantType", grantType)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return AppList;
        }
        public ApplicantModel GetCusterListBYId(long applicant_Id)
        {
            var AppList = new ApplicantModel();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantModel>("Proc_GetGrantAppListByID @applicant_Id",
                   new SqlParameter("@applicant_Id", applicant_Id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }  


            return AppList;
        }

        public IEnumerable<StateModel> GetStates()
        {
            var AppList = new List<StateModel>();
            try {
                AppList = _entity.Database.SqlQuery<StateModel>("proc_getState").ToList();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            
            }
            

            return AppList;
        }

        public IEnumerable<TownModel> GetTown()
        {
            var AppList = new List<TownModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<TownModel>("proc_getTown").ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }


            return AppList;
        }

        public IEnumerable<CountryModel> GetCountry()
        {
            var AppList = new List<CountryModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<CountryModel>("proc_getCountry").ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }


            return AppList;
        }
        //AuthoriseModel proc_getCountry  CountryModel

        public int GetUserStatus(AuthoriseModel mode)
        {
            SqlParameter authvalue = new SqlParameter("@authoriser", SqlDbType.Int);
            authvalue.Direction = System.Data.ParameterDirection.Output;
            int retValue=45;
            try
            {
                retValue = _entity.Database.SqlQuery<int>("Proc_GetUserStatus @userId, @authoriser output,",
                   new SqlParameter("@RegID", mode.userId), authvalue).SingleOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retValue= Convert.ToInt32(authvalue.Value);

            return retValue;
        }

        public ReturnModel ApproveApplication(AuthoriseModel mode)
            {
                //var AppList = new List<CustomerModel>();
            var retVal = new ReturnModel();
            SqlParameter Retval = new SqlParameter("@retVal", SqlDbType.Int);
            Retval.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg = new SqlParameter("@retmeg", SqlDbType.VarChar, 150);
            RetMsg.Direction = System.Data.ParameterDirection.Output;
            try
            {
          var AppList = _entity.Database.ExecuteSqlCommand("Proc_ApproveApplication @applicant_Id,@approveType,@approveAmount,@approveComment,@userId,@retVal output,@retmeg output",
                   new SqlParameter("@applicant_Id", mode.applicant_Id),
                   new SqlParameter("@approveType", mode.approveType),
                   new SqlParameter("@approveAmount", mode.approveAmount),
                   new SqlParameter("@approveComment", mode.ApproveComment),
                   new SqlParameter("@userId", mode.userId), Retval, RetMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval.Value);
            retVal.retmsg = RetMsg.Value.ToString();
            return retVal ?? new ReturnModel();

        }

        public ReturnModel DisburseGrant(AuthoriseModel mode)
        {
            if (mode.NtimeDisburse == null)
            {
                mode.NtimeDisburse = 0;
            }
            if (mode.Frequency == null)
            {
                mode.Frequency = 0;
            }
            if (mode.App_bvn == null)
            {
                mode.App_bvn = "";
            }
            //var AppList = new List<CustomerModel>();
            var retVal = new ReturnModel();
            SqlParameter Retval = new SqlParameter("@retVal", SqlDbType.Int);
            Retval.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg = new SqlParameter("@retmeg", SqlDbType.VarChar, 150);
            RetMsg.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("Proc_DisburseGrant @NDisburse,@BVN,@freq, @applicant_Id,@grantype,@disburseType,@disburseComment,@userId,@retVal output,@retmeg output",
                         new SqlParameter("@NDisburse", mode.NtimeDisburse),
                         new SqlParameter("@BVN", mode.App_bvn),
                         new SqlParameter("@freq", mode.Frequency),
                         new SqlParameter("@applicant_Id", mode.applicant_Id),
                         new SqlParameter("@grantype", mode.grantType),
                         new SqlParameter("@disburseType", mode.approveType),
                         new SqlParameter("@disburseComment", mode.ApproveComment),
                         new SqlParameter("@userId", mode.userId), Retval, RetMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval.Value);
            retVal.retmsg = RetMsg.Value.ToString();
            return retVal ?? new ReturnModel();

        }

        public ReturnModel RejectApplication(AuthoriseModel mode)
        {
            //var AppList = new List<CustomerModel>();
            var retVal = new ReturnModel();
            SqlParameter Retval = new SqlParameter("@retVal", SqlDbType.Int);
            Retval.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg = new SqlParameter("@retmeg", SqlDbType.VarChar, 150);
            RetMsg.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("Proc_RejectApplication @applicant_Id,@rejectType,@rejectAmount,@rejectComment,@userId,@retVal output, @retmeg output",
                         new SqlParameter("@applicant_Id", mode.applicant_Id),
                         new SqlParameter("@rejectType", mode.rejectType),
                         new SqlParameter("@rejectAmount", mode.rejectAmount),
                         new SqlParameter("@rejectComment", mode.rejectComment),
                         new SqlParameter("@userId", mode.userId), Retval, RetMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval.Value);
            retVal.retmsg = RetMsg.Value.ToString();
            return retVal ?? new ReturnModel();

        }
        //Proc_GetApprovedAppLevel1

        public IEnumerable<CustomerModel> GetApprovedAppLevel1()
        {
            var AppList = new List<CustomerModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<CustomerModel>("Proc_GetApprovedAppLevel1").ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return AppList;
        }

        public IEnumerable<CustomerModel> GetApprovedAppLevel2()
        {
            var AppList = new List<CustomerModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<CustomerModel>("Proc_GetApprovedAppLevel2").ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return AppList;
        }

        //public ActionResult DownloadAttachment(int studentId)
        //{
        //    // Find user by passed id
        //    // Student student = db.Students.FirstOrDefault(s => s.Id == studentId);

        //    var file = _entity.tbl_customer.FirstOrDefault(x => x.RegistrationId == studentId);


        //    byte[] fileBytes = file.LastTax;
        //    //File file = new File();

        //    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Last Task Receipt");

        //}

        // Proc_GetRejectApplication
        public IEnumerable<ApplicantModel> GetRejectedApplication(int dli)
        {
            var AppList = new List<ApplicantModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantModel>("Proc_GetRejectApplication @grantType",
                    new SqlParameter("@grantType", dli)).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return AppList;
        }

        public int UploadDocument(ApplicantModel model,HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload, HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload,long applicant_Id)
        {
         
            if (LastTaxUpload != null)
            {
                byte[] LastTaxUpload1 = null;

                //LastTaxUpload1 = new byte[LastTaxUpload.InputStream.Length];
                //LastTaxUpload.InputStream.Read(LastTaxUpload1, 0, LastTaxUpload1.Length);

                Stream stream = LastTaxUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                 LastTaxUpload1 = reader.ReadBytes((int)stream.Length);

                model.LstTaxDatePayEvidnce = LastTaxUpload1;
                model.LstTaxDateEvidnceType = LastTaxUpload.ContentType;



            }
            else
            {
                model.LstTaxDatePayEvidnce = new byte[0];
                model.LstTaxDateEvidnceType = "";
            }

            if (BusRegUpload != null)
            {
                byte[] BusRegUpload1 = null;

                //BusRegUpload1 = new byte[BusRegUpload.InputStream.Length];
                //BusRegUpload.InputStream.Read(BusRegUpload1, 0, BusRegUpload1.Length);
                Stream stream = BusRegUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                BusRegUpload1 = reader.ReadBytes((int)stream.Length);

                model.BusnRegNumEvdnce = BusRegUpload1;
                model.BusnRegNumEvidnceType = BusRegUpload.ContentType;

            }
            else
            {

                model.BusnRegNumEvdnce = new byte[0];
                model.BusnRegNumEvidnceType = "";
            }

            if (TurnoverUpload != null)
            {
                byte[] TurnoverUpload1 = null;

                //TurnoverUpload1 = new byte[TurnoverUpload.InputStream.Length];
                //TurnoverUpload.InputStream.Read(TurnoverUpload1, 0, TurnoverUpload1.Length);

                Stream stream = TurnoverUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                TurnoverUpload1 = reader.ReadBytes((int)stream.Length);

                model.Lst3YrAvrgeAnnualBusnTOverEvdnce = TurnoverUpload1;
                model.Lst3YrAvrgeAnnualBusnTOverEvdnceType = TurnoverUpload.ContentType;

            }
            else
            {

                model.Lst3YrAvrgeAnnualBusnTOverEvdnce = new byte[0];
                model.Lst3YrAvrgeAnnualBusnTOverEvdnceType = "";
            }


            if (ApplicantSignWdateUpload != null)
            {
                byte[] ApplicantSignWdateUpload1 = null;

                //ApplicantSignWdateUpload1 = new byte[ApplicantSignWdateUpload.InputStream.Length];
                //ApplicantSignWdateUpload.InputStream.Read(ApplicantSignWdateUpload1, 0, ApplicantSignWdateUpload1.Length);
                Stream stream = ApplicantSignWdateUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                ApplicantSignWdateUpload1 = reader.ReadBytes((int)stream.Length);

                model.ApplicantSign = ApplicantSignWdateUpload1;
                model.ApplicantSignType = ApplicantSignWdateUpload.ContentType;

            }
            else
            {

                model.ApplicantSign = new byte[0];
                model.ApplicantSignType = "";
            }

            if (passport != null)
            {
                byte[] passport1 = null;

                //passport1 = new byte[passport.InputStream.Length];
                //passport.InputStream.Read(passport1, 0, passport1.Length);

                Stream stream = passport.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                passport1 = reader.ReadBytes((int)stream.Length);

                model.ApplicantPassport = passport1;
                model.ApplicantPassportType = passport.ContentType;
                model.ApplicantPassportName = passport.FileName;

            }
            else
            {

                model.ApplicantPassport = new byte[0];
                model.ApplicantPassportType = "";
                model.ApplicantPassportName = "";
            }


            if (Lst3mnthExpUpload != null)
            {
                byte[] Lst3mnthExpUpload1 = null;

                //Lst3mnthExpUpload1 = new byte[Lst3mnthExpUpload.InputStream.Length];
                //Lst3mnthExpUpload.InputStream.Read(Lst3mnthExpUpload1, 0, Lst3mnthExpUpload1.Length);

                Stream stream = Lst3mnthExpUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                Lst3mnthExpUpload1 = reader.ReadBytes((int)stream.Length);

                model.EvidnceOfOpertnExp4pst3mnth = Lst3mnthExpUpload1;
                model.EvidnceOfOpertnExp4pst3mnthType = Lst3mnthExpUpload.ContentType;
                model.EvidnceOfOpertnExp4pst3mnthName = Lst3mnthExpUpload.FileName;

            }
            else
            {

                model.EvidnceOfOpertnExp4pst3mnth = new byte[0];
                model.EvidnceOfOpertnExp4pst3mnthType = "";
                model.EvidnceOfOpertnExp4pst3mnthName = "";
            }

            if (EvOfRunningLoanUpload != null)
            {
                byte[] EvOfRunningLoanUpload1 = null;

                //EvOfRunningLoanUpload1 = new byte[EvOfRunningLoanUpload.InputStream.Length];
                //EvOfRunningLoanUpload.InputStream.Read(EvOfRunningLoanUpload1, 0, EvOfRunningLoanUpload1.Length);

                Stream stream = EvOfRunningLoanUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                EvOfRunningLoanUpload1 = reader.ReadBytes((int)stream.Length);

                model.EvidnceOfLoanOrCreditGrant = EvOfRunningLoanUpload1;
                model.EvidnceOfLoanOrCreditGrantType = EvOfRunningLoanUpload.ContentType;
                model.EvidnceOfLoanOrCreditGrantName = EvOfRunningLoanUpload.FileName;

            }
            else
            {

                model.EvidnceOfLoanOrCreditGrant = new byte[0];
                model.EvidnceOfLoanOrCreditGrantType = "";
                model.EvidnceOfLoanOrCreditGrantName = "";
            }

            if (CooperativeUpload != null)
            {
                byte[] CooperativeUpload1 = null;


                //CooperativeUpload1 = new byte[CooperativeUpload.InputStream.Length];
                //CooperativeUpload.InputStream.Read(CooperativeUpload1, 0, CooperativeUpload1.Length);

                Stream stream = CooperativeUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                CooperativeUpload1 = reader.ReadBytes((int)stream.Length);

                model.EvidnceOfOCorpOrAssocMember = CooperativeUpload1;
                model.EvidnceOfOCorpOrAssocMemberType = CooperativeUpload.ContentType;
                model.EvidnceOfOCorpOrAssocMemberName = CooperativeUpload.FileName;

            }
            else
            {

                model.EvidnceOfOCorpOrAssocMember = new byte[0];
                model.EvidnceOfOCorpOrAssocMemberType = "";
                model.EvidnceOfOCorpOrAssocMemberName = "";
            }

            int retV = 0;
            string retmessg = "";

            SqlParameter Retval2 = new SqlParameter("@retVal", SqlDbType.Int);
            Retval2.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg2 = new SqlParameter("@retmesg", SqlDbType.VarChar, 150);
            RetMsg2.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("proc_ApplicantUpload @applicant_Id, @phone,@BVN,@LastTaxPayDate,@LstTaxDatePayEvidnce,@LstTaxDateEvidnceType," +
                    "@BusnRegNum,@BusnRegNumEvdnce,@BusnRegNumEvidnceType,@Lst3YrAvrgeAnnualBusnTOver,@Lst3YrAvrgeAnnualBusnTOverEvdnce,@Lst3YrAvrgeAnnualBusnTOverEvdnceType," +
                    "@ApplicantPassportName,@ApplicantPassport,@ApplicantPassportType,@EvidnceOfLoanOrCreditGrantName,@EvidnceOfLoanOrCreditGrant,@EvidnceOfLoanOrCreditGrantType," +
                    "@EvidnceOfOpertnExp4pst3mnthName,@EvidnceOfOpertnExp4pst3mnth,@EvidnceOfOpertnExp4pst3mnthType,@EvidnceOfOCorpOrAssocMemberName,@EvidnceOfOCorpOrAssocMember," +
                    "@EvidnceOfOCorpOrAssocMemberType,@retVal output,@retmesg output",
                         new SqlParameter("@applicant_Id", applicant_Id),
                         new SqlParameter("@phone", model.Mobile),
                         new SqlParameter("@BVN", model.BVN),
                         new SqlParameter("@LastTaxPayDate", model.LastTaxPayDate),
                         new SqlParameter("@LstTaxDatePayEvidnce", model.LstTaxDatePayEvidnce),
                         new SqlParameter("@LstTaxDateEvidnceType", model.LstTaxDateEvidnceType),
                         new SqlParameter("@BusnRegNum", model.BusnRegNum),
                         new SqlParameter("@BusnRegNumEvdnce", model.BusnRegNumEvdnce),
                         new SqlParameter("@BusnRegNumEvidnceType", model.BusnRegNumEvidnceType),
                         new SqlParameter("@Lst3YrAvrgeAnnualBusnTOver", model.Lst3YrAvrgeAnnualBusnTOver),
                         new SqlParameter("@Lst3YrAvrgeAnnualBusnTOverEvdnce", model.Lst3YrAvrgeAnnualBusnTOverEvdnce),
                         new SqlParameter("@Lst3YrAvrgeAnnualBusnTOverEvdnceType", model.Lst3YrAvrgeAnnualBusnTOverEvdnceType),
                         new SqlParameter("@ApplicantPassportName", model.ApplicantPassportName),
                         new SqlParameter("@ApplicantPassport", model.ApplicantPassport),
                         new SqlParameter("@ApplicantPassportType", model.ApplicantPassportType),
                         new SqlParameter("@EvidnceOfLoanOrCreditGrantName", model.EvidnceOfLoanOrCreditGrantName),
                         new SqlParameter("@EvidnceOfLoanOrCreditGrant", model.EvidnceOfLoanOrCreditGrant),
                         new SqlParameter("@EvidnceOfLoanOrCreditGrantType", model.EvidnceOfLoanOrCreditGrantType),
                         new SqlParameter("@EvidnceOfOpertnExp4pst3mnthName", model.EvidnceOfOpertnExp4pst3mnthName),
                         new SqlParameter("@EvidnceOfOpertnExp4pst3mnth", model.EvidnceOfOpertnExp4pst3mnth),
                         new SqlParameter("@EvidnceOfOpertnExp4pst3mnthType", model.EvidnceOfOpertnExp4pst3mnthType),
                         new SqlParameter("@EvidnceOfOCorpOrAssocMemberName", model.EvidnceOfOCorpOrAssocMemberName),
                         new SqlParameter("@EvidnceOfOCorpOrAssocMember", model.EvidnceOfOCorpOrAssocMember),
                         new SqlParameter("@EvidnceOfOCorpOrAssocMemberType", model.EvidnceOfOCorpOrAssocMemberType),Retval2, RetMsg2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retV = Convert.ToInt32(Retval2.Value);
            retmessg = RetMsg2.Value.ToString();
            if (retV == 0)
            {
                return 0;
            }
            return 21;
        }
        public ReturnModel ApplyForGrant(ApplicantModel model, HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload, HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload)
        {

            SqlParameter Next_ID2 = new SqlParameter("@NextID", SqlDbType.BigInt);
            Next_ID2.Direction = System.Data.ParameterDirection.Output;

            var getId = _entity.Database.ExecuteSqlCommand("proc_GetMax @NextID output", Next_ID2);

            long Aplicant_Id = Convert.ToInt64(Next_ID2.Value);
            if (model.Lst3YrAvrgeAnnualBusnTOver1 != null)
            {
                model.Lst3YrAvrgeAnnualBusnTOver = Convert.ToDecimal(model.Lst3YrAvrgeAnnualBusnTOver1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.EstimValOfBusTurnoverXLB1 != null)
            {
                model.EstimValOfBusTurnoverXLB = Convert.ToDecimal(model.EstimValOfBusTurnoverXLB1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.TotalOutstandVolumeOfLoan1 != null)
            {
                model.TotalOutstandVolumeOfLoan = Convert.ToDecimal(model.TotalOutstandVolumeOfLoan1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.HowMuchWereyouPaying1 != null)
            {
                model.HowMuchWereyouPaying = Convert.ToDecimal(model.HowMuchWereyouPaying1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.RepaymntChangeToHowMuch1 != null)
            {
                model.RepaymntChangeToHowMuch = Convert.ToDecimal(model.RepaymntChangeToHowMuch1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.CreditGrantAmount1 != null)
            {
                model.CreditGrantAmount = Convert.ToDecimal(model.CreditGrantAmount1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.OperatnGrantAmount1 != null)
            {
                model.OperatnGrantAmount = Convert.ToDecimal(model.OperatnGrantAmount1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.ITEnhanceGrantAmount1 != null)
            {
                model.ITEnhanceGrantAmount = Convert.ToDecimal(model.ITEnhanceGrantAmount1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.CreditGrantAmount != null)
            {
                model.Amount = model.CreditGrantAmount;

            }
            if (model.OperatnGrantAmount != null)
            {
                model.Amount = model.OperatnGrantAmount;

            }
            if (model.ITEnhanceGrantAmount != null)
            {
                model.Amount = model.ITEnhanceGrantAmount;

            }
            if (model.Middlename == null)
            {
                model.Middlename = "";
            }
            if (model.EstimValOfBusTurnoverXLB == null)
            {
                model.EstimValOfBusTurnoverXLB = 0;
            }
            if (model.TotalOutstandVolumeOfLoan == null)
            {
                model.TotalOutstandVolumeOfLoan = 0;
            }
            if (model.AnyRunningLoanPriorToCovid == null)
            {
                model.AnyRunningLoanPriorToCovid = "";
            }
            if (model.TotalOutstandVolumeOfLoan == null)
            {
                model.TotalOutstandVolumeOfLoan = 0;
            }
            if (model.HowOftenWereYouPaying == null)
            {
                model.HowOftenWereYouPaying = "";
            }
            if (model.HowMuchWereyouPaying == null)
            {
                model.HowMuchWereyouPaying = 0;
            }
            if (model.DidYourRepaymentChange == null)
            {
                model.DidYourRepaymentChange = "";
            }
            if (model.RepaymntChangeToHowMuch == null)
            {
                model.RepaymntChangeToHowMuch = 0;
            }
            if (model.findItDiffToMeetPaymentOblig == null)
            {
                model.findItDiffToMeetPaymentOblig = "";
            }
            if (model.BusinImpactByCovid == null)
            {
                model.BusinImpactByCovid = "";
            }
            if (model.BusiNegImpactByCovid == null)
            {
                model.BusiNegImpactByCovid = "";
            }
            if (model.specifyOther == null)
            {
                model.specifyOther = "";
            }
            if (model.PlanningToLayOffEmployeeDueToNeg == null)
            {
                model.PlanningToLayOffEmployeeDueToNeg = "";
            }
            if (model.DidYouLayOffEmployee == null)
            {
                model.DidYouLayOffEmployee = "";
            }
            if (model.NoOffEmployeeLayOff == null)
            {
                model.NoOffEmployeeLayOff = "";
            }
            if (model.DidBusContDuringCovidCrisis == null)
            {
                model.DidBusContDuringCovidCrisis = "";
            }
            if (model.WhtToolCudVeMadeItPosib == null)
            {
                model.WhtToolCudVeMadeItPosib = "";
            }
            if (model.WhtToolCudVeMadeBusMoreEffnt == null)
            {
                model.WhtToolCudVeMadeBusMoreEffnt = "";
            }
            if (model.EmployeeAbleToWrkFrmHom == null)
            {
                model.EmployeeAbleToWrkFrmHom = "";
            }
            if (model.WhtToolCudVeReqToMakeItPosib == null)
            {
                model.WhtToolCudVeReqToMakeItPosib = "";
            }
            if (model.AllSectnOfFormFilled == null)
            {
                model.AllSectnOfFormFilled = "";
            }
            if (model.IsBusinessCertAttach == null)
            {
                model.IsBusinessCertAttach = "";
            }
            if (model.IsEvidOfTaxPaymentAttach == null)
            {
                model.IsEvidOfTaxPaymentAttach = "";
            }
            if (model.IsEvOf1YrTOverAttachOrVerifid == null)
            {
                model.IsEvOf1YrTOverAttachOrVerifid = "";
            }
            if (model.VeBnkDetailProvided == null)
            {
                model.VeBnkDetailProvided = "";
            }
            if (model.IsImpactOfCovidOnBusnVrfd == null)
            {
                model.IsImpactOfCovidOnBusnVrfd = "";
            }
            if (model.CreditGrantAmount == null)
            {
                model.CreditGrantAmount = 0;
            }
            if (model.OperatnGrantAmount == null)
            {
                model.OperatnGrantAmount = 0;
            }
            if (model.ITEnhanceGrantAmount == null)
            {
                model.ITEnhanceGrantAmount = 0;
            }
            if (model.authId == null)
            {
                model.authId = "";
            }
            if (model.userId == null)
            {
                model.userId = "";
            }
            if (model.Score == null)
            {
                model.Score = 0;
            }
            if (model.C18_35 == null)
            {
                model.C18_35 = 0;
            }
            if (model.C36_45 == null)
            {
                model.C36_45 = 0;
            }
            if (model.C46andabove == null)
            {
                model.C46andabove = 0;
            }
            if (model.Amount == null)
            {
                model.Amount = 0;
            }

            var retVal = new ReturnModel();
            SqlParameter Retval3 = new SqlParameter("@retVal", SqlDbType.Int);
            Retval3.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg3 = new SqlParameter("@retmesg", SqlDbType.VarChar, 150);
            RetMsg3.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("proc_ApplyForGrant @status,@date_of_birth, @amount, @upload_Id, @SurName,@FirstName,@Middlename,@BusinessName,@gender,@StateOfOrigin,@LGA,@ResidAddress,@Position," +
                    "@Mobile,@Email,@PersonalTaxRegNo,@BusinessSector,@BusinessType,@BusinessAddress,@City,@State,@Country,@BusinesPhoneNo,@BusinesEmail," +
                    "@BusinesWebsite,@BusinesTaxRegNo,@LastTaxPayDate,@BusinesStartDate,@NoOfYrInOperation,@NOofEmployee,@NOofFemale,@NOofMale,@EstimValOfBusTurnoverXLB," +
                    "@BankName,@AcctNo,@BVN,@AnyRunningLoanPriorToCovid,@TotalOutstandVolumeOfLoan,@HowOftenWereYouPaying,@HowMuchWereyouPaying,@DidYourRepaymentChange," +
                    "@RepaymntChangeToHowMuch,@findItDiffToMeetPaymentOblig,@BusinImpactByCovid,@BusiNegImpactByCovid,@specifyOther,@PlanningToLayOffEmployeeDueToNeg," +
                    "@DidYouLayOffEmployee,@NoOffEmployeeLayOff,@DidBusContDuringCovidCrisis,@WhtToolCudVeMadeItPosib,@WhtToolCudVeMadeBusMoreEffnt,@EmployeeAbleToWrkFrmHom," +
                    "@WhtToolCudVeReqToMakeItPosib,@fullname,@AllSectnOfFormFilled,@IsBusinessCertAttach,@IsEvidOfTaxPaymentAttach,@IsEvOf1YrTOverAttachOrVerifid," +
                    "@VeBnkDetailProvided,@IsImpactOfCovidOnBusnVrfd,@GrantyTpe,@CreditGrantAmount,@OperatnGrantAmount,@ITEnhanceGrantAmount,@authId,@userId,@Score," +
                    "@T1835,@T3645,@T46andabove,@retVal output,@retmesg output",

                         new SqlParameter("@status", 2),
                         new SqlParameter("@date_of_birth", model.date_of_birth),
                         new SqlParameter("@amount", model.Amount),
                         new SqlParameter("@upload_Id", Aplicant_Id),
                         new SqlParameter("@SurName", model.SurName),
                         new SqlParameter("@FirstName", model.FirstName),
                         new SqlParameter("@Middlename", model.Middlename),
                         new SqlParameter("@BusinessName", model.BusinessName),
                         new SqlParameter("@gender", model.gender),
                         new SqlParameter("@StateOfOrigin", model.StateOfOrigin),
                         new SqlParameter("@LGA", model.LGA),
                         new SqlParameter("@ResidAddress", model.ResidAddress),
                         new SqlParameter("@Position", model.Position),
                         new SqlParameter("@Mobile", model.Mobile),
                         new SqlParameter("@Email", model.Email),
                         new SqlParameter("@PersonalTaxRegNo", model.PersonalTaxRegNo),
                         new SqlParameter("@BusinessSector", model.BusinessSector),
                         new SqlParameter("@BusinessType", model.BusinessType),
                         new SqlParameter("@BusinessAddress", model.BusinessAddress),
                         new SqlParameter("@City", model.City),
                         new SqlParameter("@State", model.State),
                         new SqlParameter("@Country", model.Country),
                         new SqlParameter("@BusinesPhoneNo", model.BusinesPhoneNo),
                         new SqlParameter("@BusinesEmail", model.BusinesEmail),
                         new SqlParameter("@BusinesWebsite", model.BusinesWebsite),
                         new SqlParameter("@BusinesTaxRegNo", model.BusinesTaxRegNo),
                         new SqlParameter("@LastTaxPayDate", model.LastTaxPayDate),
                         new SqlParameter("@BusinesStartDate", model.BusinesStartDate),
                         new SqlParameter("@NoOfYrInOperation", model.NoOfYrInOperation),
                         new SqlParameter("@NOofEmployee", model.NOofEmployee),
                         new SqlParameter("@NOofFemale", model.NOofFemale),
                         new SqlParameter("@NOofMale", model.NOofMale),
                         new SqlParameter("@EstimValOfBusTurnoverXLB", model.EstimValOfBusTurnoverXLB),
                         new SqlParameter("@BankName", model.BankName),
                         new SqlParameter("@AcctNo", model.AcctNo),
                         new SqlParameter("@BVN", model.BVN),
                         new SqlParameter("@AnyRunningLoanPriorToCovid", model.AnyRunningLoanPriorToCovid),
                         new SqlParameter("@TotalOutstandVolumeOfLoan", model.TotalOutstandVolumeOfLoan),
                         new SqlParameter("@HowOftenWereYouPaying", model.HowOftenWereYouPaying),
                         new SqlParameter("@HowMuchWereyouPaying", model.HowMuchWereyouPaying),
                         new SqlParameter("@DidYourRepaymentChange", model.DidYourRepaymentChange),
                         new SqlParameter("@RepaymntChangeToHowMuch", model.RepaymntChangeToHowMuch),
                         new SqlParameter("@findItDiffToMeetPaymentOblig", model.findItDiffToMeetPaymentOblig),
                         new SqlParameter("@BusinImpactByCovid", model.BusinImpactByCovid),
                         new SqlParameter("@BusiNegImpactByCovid", model.BusiNegImpactByCovid),
                         new SqlParameter("@specifyOther", model.specifyOther),
                         new SqlParameter("@PlanningToLayOffEmployeeDueToNeg", model.PlanningToLayOffEmployeeDueToNeg),
                         new SqlParameter("@DidYouLayOffEmployee", model.DidYouLayOffEmployee),
                         new SqlParameter("@NoOffEmployeeLayOff", model.NoOffEmployeeLayOff),
                         new SqlParameter("@DidBusContDuringCovidCrisis", model.DidBusContDuringCovidCrisis),
                         new SqlParameter("@WhtToolCudVeMadeItPosib", model.WhtToolCudVeMadeItPosib),
                         new SqlParameter("@WhtToolCudVeMadeBusMoreEffnt", model.WhtToolCudVeMadeBusMoreEffnt),
                         new SqlParameter("@EmployeeAbleToWrkFrmHom", model.EmployeeAbleToWrkFrmHom),
                         new SqlParameter("@WhtToolCudVeReqToMakeItPosib", model.WhtToolCudVeReqToMakeItPosib),
                         new SqlParameter("@fullname", model.fullname),
                         new SqlParameter("@AllSectnOfFormFilled", model.AllSectnOfFormFilled),
                         new SqlParameter("@IsBusinessCertAttach", model.IsBusinessCertAttach),
                         new SqlParameter("@IsEvidOfTaxPaymentAttach", model.IsEvidOfTaxPaymentAttach),
                         new SqlParameter("@IsEvOf1YrTOverAttachOrVerifid", model.IsEvOf1YrTOverAttachOrVerifid),
                         new SqlParameter("@VeBnkDetailProvided", model.VeBnkDetailProvided),
                         new SqlParameter("@IsImpactOfCovidOnBusnVrfd", model.IsImpactOfCovidOnBusnVrfd),
                         new SqlParameter("@GrantyTpe", model.GrantyTpe),
                         new SqlParameter("@CreditGrantAmount", model.CreditGrantAmount),
                         new SqlParameter("@OperatnGrantAmount", model.OperatnGrantAmount),
                         new SqlParameter("@ITEnhanceGrantAmount", model.ITEnhanceGrantAmount),
                         new SqlParameter("@authId", model.authId),
                         new SqlParameter("@userId", model.userId),
                         new SqlParameter("@Score", model.Score),
                         new SqlParameter("@T1835", model.C18_35),
                         new SqlParameter("@T3645", model.C36_45),
                         new SqlParameter("@T46andabove", model.C46andabove),
                         Retval3, RetMsg3);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval3.Value);
            retVal.retmsg = RetMsg3.Value.ToString();

            if (retVal.retVal == 0)
            {
                var result = UploadDocument(model, LastTaxUpload, BusRegUpload, TurnoverUpload, ApplicantSignWdateUpload, passport, Lst3mnthExpUpload, EvOfRunningLoanUpload, CooperativeUpload, Aplicant_Id);
                if (result != 21)
                {
                    retVal.retVal = 0;
                    retVal.retmsg = "APplication for grant is successful.";
                    return retVal ?? new ReturnModel();
                }
            }
            return null;
        }



        public int UpdateUploadDocument(ApplicantModel model, HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload, HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload, long applicant_Id)
        {

            if (LastTaxUpload != null)
            {
                byte[] LastTaxUpload1 = null;

                //LastTaxUpload1 = new byte[LastTaxUpload.InputStream.Length];
                //LastTaxUpload.InputStream.Read(LastTaxUpload1, 0, LastTaxUpload1.Length);

                Stream stream = LastTaxUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                LastTaxUpload1 = reader.ReadBytes((int)stream.Length);

                model.LstTaxDatePayEvidnce = LastTaxUpload1;
                model.LstTaxDateEvidnceType = LastTaxUpload.ContentType;



            }
            else
            {
                model.LstTaxDatePayEvidnce = new byte[0];
                model.LstTaxDateEvidnceType = "";
            }

            if (BusRegUpload != null)
            {
                byte[] BusRegUpload1 = null;

                //BusRegUpload1 = new byte[BusRegUpload.InputStream.Length];
                //BusRegUpload.InputStream.Read(BusRegUpload1, 0, BusRegUpload1.Length);
                Stream stream = BusRegUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                BusRegUpload1 = reader.ReadBytes((int)stream.Length);

                model.BusnRegNumEvdnce = BusRegUpload1;
                model.BusnRegNumEvidnceType = BusRegUpload.ContentType;

            }
            else
            {

                model.BusnRegNumEvdnce = new byte[0];
                model.BusnRegNumEvidnceType = "";
            }

            if (TurnoverUpload != null)
            {
                byte[] TurnoverUpload1 = null;

                //TurnoverUpload1 = new byte[TurnoverUpload.InputStream.Length];
                //TurnoverUpload.InputStream.Read(TurnoverUpload1, 0, TurnoverUpload1.Length);

                Stream stream = TurnoverUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                TurnoverUpload1 = reader.ReadBytes((int)stream.Length);

                model.Lst3YrAvrgeAnnualBusnTOverEvdnce = TurnoverUpload1;
                model.Lst3YrAvrgeAnnualBusnTOverEvdnceType = TurnoverUpload.ContentType;

            }
            else
            {

                model.Lst3YrAvrgeAnnualBusnTOverEvdnce = new byte[0];
                model.Lst3YrAvrgeAnnualBusnTOverEvdnceType = "";
            }


            if (ApplicantSignWdateUpload != null)
            {
                byte[] ApplicantSignWdateUpload1 = null;

                //ApplicantSignWdateUpload1 = new byte[ApplicantSignWdateUpload.InputStream.Length];
                //ApplicantSignWdateUpload.InputStream.Read(ApplicantSignWdateUpload1, 0, ApplicantSignWdateUpload1.Length);
                Stream stream = ApplicantSignWdateUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                ApplicantSignWdateUpload1 = reader.ReadBytes((int)stream.Length);

                model.ApplicantSign = ApplicantSignWdateUpload1;
                model.ApplicantSignType = ApplicantSignWdateUpload.ContentType;

            }
            else
            {

                model.ApplicantSign = new byte[0];
                model.ApplicantSignType = "";
            }

            if (passport != null)
            {
                byte[] passport1 = null;

                //passport1 = new byte[passport.InputStream.Length];
                //passport.InputStream.Read(passport1, 0, passport1.Length);

                Stream stream = passport.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                passport1 = reader.ReadBytes((int)stream.Length);

                model.ApplicantPassport = passport1;
                model.ApplicantPassportType = passport.ContentType;
                model.ApplicantPassportName = passport.FileName;

            }
            else
            {

                model.ApplicantPassport = new byte[0];
                model.ApplicantPassportType = "";
                model.ApplicantPassportName = "";
            }


            if (Lst3mnthExpUpload != null)
            {
                byte[] Lst3mnthExpUpload1 = null;

                //Lst3mnthExpUpload1 = new byte[Lst3mnthExpUpload.InputStream.Length];
                //Lst3mnthExpUpload.InputStream.Read(Lst3mnthExpUpload1, 0, Lst3mnthExpUpload1.Length);

                Stream stream = Lst3mnthExpUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                Lst3mnthExpUpload1 = reader.ReadBytes((int)stream.Length);

                model.EvidnceOfOpertnExp4pst3mnth = Lst3mnthExpUpload1;
                model.EvidnceOfOpertnExp4pst3mnthType = Lst3mnthExpUpload.ContentType;
                model.EvidnceOfOpertnExp4pst3mnthName = Lst3mnthExpUpload.FileName;

            }
            else
            {

                model.EvidnceOfOpertnExp4pst3mnth = new byte[0];
                model.EvidnceOfOpertnExp4pst3mnthType = "";
                model.EvidnceOfOpertnExp4pst3mnthName = "";
            }

            if (EvOfRunningLoanUpload != null)
            {
                byte[] EvOfRunningLoanUpload1 = null;

                //EvOfRunningLoanUpload1 = new byte[EvOfRunningLoanUpload.InputStream.Length];
                //EvOfRunningLoanUpload.InputStream.Read(EvOfRunningLoanUpload1, 0, EvOfRunningLoanUpload1.Length);

                Stream stream = EvOfRunningLoanUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                EvOfRunningLoanUpload1 = reader.ReadBytes((int)stream.Length);

                model.EvidnceOfLoanOrCreditGrant = EvOfRunningLoanUpload1;
                model.EvidnceOfLoanOrCreditGrantType = EvOfRunningLoanUpload.ContentType;
                model.EvidnceOfLoanOrCreditGrantName = EvOfRunningLoanUpload.FileName;

            }
            else
            {

                model.EvidnceOfLoanOrCreditGrant = new byte[0];
                model.EvidnceOfLoanOrCreditGrantType = "";
                model.EvidnceOfLoanOrCreditGrantName = "";
            }

            if (CooperativeUpload != null)
            {
                byte[] CooperativeUpload1 = null;


                //CooperativeUpload1 = new byte[CooperativeUpload.InputStream.Length];
                //CooperativeUpload.InputStream.Read(CooperativeUpload1, 0, CooperativeUpload1.Length);

                Stream stream = CooperativeUpload.InputStream;
                BinaryReader reader = new BinaryReader(stream);
                CooperativeUpload1 = reader.ReadBytes((int)stream.Length);

                model.EvidnceOfOCorpOrAssocMember = CooperativeUpload1;
                model.EvidnceOfOCorpOrAssocMemberType = CooperativeUpload.ContentType;
                model.EvidnceOfOCorpOrAssocMemberName = CooperativeUpload.FileName;

            }
            else
            {

                model.EvidnceOfOCorpOrAssocMember = new byte[0];
                model.EvidnceOfOCorpOrAssocMemberType = "";
                model.EvidnceOfOCorpOrAssocMemberName = "";
            }

            int retV = 0;
            string retmessg = "";

            SqlParameter Retval2 = new SqlParameter("@retVal", SqlDbType.Int);
            Retval2.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg2 = new SqlParameter("@retmesg", SqlDbType.VarChar, 150);
            RetMsg2.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("proc_Update_ApplicantUpload  @applicant_Id, @phone,@BVN,@LastTaxPayDate,@LstTaxDatePayEvidnce,@LstTaxDateEvidnceType," +
                    "@BusnRegNum,@BusnRegNumEvdnce,@BusnRegNumEvidnceType,@Lst3YrAvrgeAnnualBusnTOver,@Lst3YrAvrgeAnnualBusnTOverEvdnce,@Lst3YrAvrgeAnnualBusnTOverEvdnceType," +
                    "@ApplicantPassportName,@ApplicantPassport,@ApplicantPassportType,@EvidnceOfLoanOrCreditGrantName,@EvidnceOfLoanOrCreditGrant,@EvidnceOfLoanOrCreditGrantType," +
                    "@EvidnceOfOpertnExp4pst3mnthName,@EvidnceOfOpertnExp4pst3mnth,@EvidnceOfOpertnExp4pst3mnthType,@EvidnceOfOCorpOrAssocMemberName,@EvidnceOfOCorpOrAssocMember," +
                    "@EvidnceOfOCorpOrAssocMemberType,@retVal output,@retmesg output",
                         new SqlParameter("@applicant_Id", applicant_Id),
                         new SqlParameter("@date_of_birth", model.date_of_birth),
                         new SqlParameter("@phone", model.Mobile),
                         new SqlParameter("@BVN", model.BVN),
                         new SqlParameter("@LastTaxPayDate", model.LastTaxPayDate),
                         new SqlParameter("@LstTaxDatePayEvidnce", model.LstTaxDatePayEvidnce),
                         new SqlParameter("@LstTaxDateEvidnceType", model.LstTaxDateEvidnceType),
                         new SqlParameter("@BusnRegNum", model.BusnRegNum),
                         new SqlParameter("@BusnRegNumEvdnce", model.BusnRegNumEvdnce),
                         new SqlParameter("@BusnRegNumEvidnceType", model.BusnRegNumEvidnceType),
                         new SqlParameter("@Lst3YrAvrgeAnnualBusnTOver", model.Lst3YrAvrgeAnnualBusnTOver),
                         new SqlParameter("@Lst3YrAvrgeAnnualBusnTOverEvdnce", model.Lst3YrAvrgeAnnualBusnTOverEvdnce),
                         new SqlParameter("@Lst3YrAvrgeAnnualBusnTOverEvdnceType", model.Lst3YrAvrgeAnnualBusnTOverEvdnceType),
                         new SqlParameter("@ApplicantPassportName", model.ApplicantPassportName),
                         new SqlParameter("@ApplicantPassport", model.ApplicantPassport),
                         new SqlParameter("@ApplicantPassportType", model.ApplicantPassportType),
                         new SqlParameter("@EvidnceOfLoanOrCreditGrantName", model.EvidnceOfLoanOrCreditGrantName),
                         new SqlParameter("@EvidnceOfLoanOrCreditGrant", model.EvidnceOfLoanOrCreditGrant),
                         new SqlParameter("@EvidnceOfLoanOrCreditGrantType", model.EvidnceOfLoanOrCreditGrantType),
                         new SqlParameter("@EvidnceOfOpertnExp4pst3mnthName", model.EvidnceOfOpertnExp4pst3mnthName),
                         new SqlParameter("@EvidnceOfOpertnExp4pst3mnth", model.EvidnceOfOpertnExp4pst3mnth),
                         new SqlParameter("@EvidnceOfOpertnExp4pst3mnthType", model.EvidnceOfOpertnExp4pst3mnthType),
                         new SqlParameter("@EvidnceOfOCorpOrAssocMemberName", model.EvidnceOfOCorpOrAssocMemberName),
                         new SqlParameter("@EvidnceOfOCorpOrAssocMember", model.EvidnceOfOCorpOrAssocMember),
                         new SqlParameter("@EvidnceOfOCorpOrAssocMemberType", model.EvidnceOfOCorpOrAssocMemberType), Retval2, RetMsg2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retV = Convert.ToInt32(Retval2.Value);
            retmessg = RetMsg2.Value.ToString();
            if (retV == 0)
            {
                return 0;
            }
            return 21;
        }
        public ReturnModel UpdateApplyForGrant(ApplicantModel model, HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload, HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload)
        {
            if (model.Lst3YrAvrgeAnnualBusnTOver1 != null)
            {
                model.Lst3YrAvrgeAnnualBusnTOver = Convert.ToDecimal(model.Lst3YrAvrgeAnnualBusnTOver1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.EstimValOfBusTurnoverXLB1 != null)
            {
                model.EstimValOfBusTurnoverXLB = Convert.ToDecimal(model.EstimValOfBusTurnoverXLB1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.TotalOutstandVolumeOfLoan1 != null)
            {
                model.TotalOutstandVolumeOfLoan = Convert.ToDecimal(model.TotalOutstandVolumeOfLoan1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.HowMuchWereyouPaying1 != null)
            {
                model.HowMuchWereyouPaying = Convert.ToDecimal(model.HowMuchWereyouPaying1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.RepaymntChangeToHowMuch1 != null)
            {
                model.RepaymntChangeToHowMuch = Convert.ToDecimal(model.RepaymntChangeToHowMuch1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.CreditGrantAmount1 != null)
            {
                model.CreditGrantAmount = Convert.ToDecimal(model.CreditGrantAmount1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.OperatnGrantAmount1 != null)
            {
                model.OperatnGrantAmount = Convert.ToDecimal(model.OperatnGrantAmount1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.ITEnhanceGrantAmount1 != null)
            {
                model.ITEnhanceGrantAmount = Convert.ToDecimal(model.ITEnhanceGrantAmount1.Replace(",", string.Empty), CultureInfo.InvariantCulture);
            }
            if (model.CreditGrantAmount != null)
            {
                model.Amount = model.CreditGrantAmount;

            }
            if (model.OperatnGrantAmount != null)
            {
                model.Amount = model.OperatnGrantAmount;

            }
            if (model.ITEnhanceGrantAmount != null)
            {
                model.Amount = model.ITEnhanceGrantAmount;

            }
            if (model.Amount == null)
            {
                model.Amount = 0;
            }
            if (model.Middlename == null)
            {
                model.Middlename = "";
            }
            if (model.EstimValOfBusTurnoverXLB == null)
            {
                model.EstimValOfBusTurnoverXLB = 0;
            }
            if (model.TotalOutstandVolumeOfLoan == null)
            {
                model.TotalOutstandVolumeOfLoan = 0;
            }
            if (model.AnyRunningLoanPriorToCovid == null)
            {
                model.AnyRunningLoanPriorToCovid = "";
            }
            if (model.TotalOutstandVolumeOfLoan == null)
            {
                model.TotalOutstandVolumeOfLoan = 0;
            }
            if (model.HowOftenWereYouPaying == null)
            {
                model.HowOftenWereYouPaying = "";
            }
            if (model.HowMuchWereyouPaying == null)
            {
                model.HowMuchWereyouPaying = 0;
            }
            if (model.DidYourRepaymentChange == null)
            {
                model.DidYourRepaymentChange = "";
            }
            if (model.RepaymntChangeToHowMuch == null)
            {
                model.RepaymntChangeToHowMuch = 0;
            }
            if (model.findItDiffToMeetPaymentOblig == null)
            {
                model.findItDiffToMeetPaymentOblig = "";
            }
            if (model.BusinImpactByCovid == null)
            {
                model.BusinImpactByCovid = "";
            }
            if (model.BusiNegImpactByCovid == null)
            {
                model.BusiNegImpactByCovid = "";
            }
            if (model.specifyOther == null)
            {
                model.specifyOther = "";
            }
            if (model.PlanningToLayOffEmployeeDueToNeg == null)
            {
                model.PlanningToLayOffEmployeeDueToNeg = "";
            }
            if (model.DidYouLayOffEmployee == null)
            {
                model.DidYouLayOffEmployee = "";
            }
            if (model.NoOffEmployeeLayOff == null)
            {
                model.NoOffEmployeeLayOff = "";
            }
            if (model.DidBusContDuringCovidCrisis == null)
            {
                model.DidBusContDuringCovidCrisis = "";
            }
            if (model.WhtToolCudVeMadeItPosib == null)
            {
                model.WhtToolCudVeMadeItPosib = "";
            }
            if (model.WhtToolCudVeMadeBusMoreEffnt == null)
            {
                model.WhtToolCudVeMadeBusMoreEffnt = "";
            }
            if (model.EmployeeAbleToWrkFrmHom == null)
            {
                model.EmployeeAbleToWrkFrmHom = "";
            }
            if (model.WhtToolCudVeReqToMakeItPosib == null)
            {
                model.WhtToolCudVeReqToMakeItPosib = "";
            }
            if (model.AllSectnOfFormFilled == null)
            {
                model.AllSectnOfFormFilled = "";
            }
            if (model.IsBusinessCertAttach == null)
            {
                model.IsBusinessCertAttach = "";
            }
            if (model.IsEvidOfTaxPaymentAttach == null)
            {
                model.IsEvidOfTaxPaymentAttach = "";
            }
            if (model.IsEvOf1YrTOverAttachOrVerifid == null)
            {
                model.IsEvOf1YrTOverAttachOrVerifid = "";
            }
            if (model.VeBnkDetailProvided == null)
            {
                model.VeBnkDetailProvided = "";
            }
            if (model.IsImpactOfCovidOnBusnVrfd == null)
            {
                model.IsImpactOfCovidOnBusnVrfd = "";
            }
            if (model.CreditGrantAmount == null)
            {
                model.CreditGrantAmount = 0;
            }
            if (model.OperatnGrantAmount == null)
            {
                model.OperatnGrantAmount = 0;
            }
            if (model.ITEnhanceGrantAmount == null)
            {
                model.ITEnhanceGrantAmount = 0;
            }
            if (model.authId == null)
            {
                model.authId = "";
            }
            if (model.State == null)
            {
                model.State = "Ekiti State";
            }
            if (model.userId == null)
            {
                model.userId = "";
            }
            if (model.Score == null)
            {
                model.Score = 0;
            }
            if (model.C18_35 == null)
            {
                model.C18_35 = 0;
            }
            if (model.C36_45 == null)
            {
                model.C36_45 = 0;
            }
            if (model.C46andabove == null)
            {
                model.C46andabove = 0;
            }
            var retVal = new ReturnModel();
            SqlParameter Retval1 = new SqlParameter("@retVal", SqlDbType.Int);
            Retval1.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg1 = new SqlParameter("@retmesg", SqlDbType.VarChar, 150);
            RetMsg1.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("proc_UpdateApplicantData @applicant_Id,@date_of_birth, @amount,@upload_Id, @SurName,@FirstName," +
                    "@Middlename,@BusinessName,@gender,@StateOfOrigin,@LGA,@ResidAddress,@Position," +
                    "@Mobile,@Email,@PersonalTaxRegNo,@BusinessSector,@BusinessType,@BusinessAddress,@City,@State,@Country,@BusinesPhoneNo,@BusinesEmail," +
                    "@BusinesWebsite,@BusinesTaxRegNo,@LastTaxPayDate,@BusinesStartDate,@NoOfYrInOperation,@NOofEmployee,@NOofFemale,@NOofMale,@EstimValOfBusTurnoverXLB," +
                    "@BankName,@AcctNo,@BVN,@AnyRunningLoanPriorToCovid,@TotalOutstandVolumeOfLoan,@HowOftenWereYouPaying,@HowMuchWereyouPaying,@DidYourRepaymentChange," +
                    "@RepaymntChangeToHowMuch,@findItDiffToMeetPaymentOblig,@BusinImpactByCovid,@BusiNegImpactByCovid,@specifyOther,@PlanningToLayOffEmployeeDueToNeg," +
                    "@DidYouLayOffEmployee,@NoOffEmployeeLayOff,@DidBusContDuringCovidCrisis,@WhtToolCudVeMadeItPosib,@WhtToolCudVeMadeBusMoreEffnt,@EmployeeAbleToWrkFrmHom," +
                    "@WhtToolCudVeReqToMakeItPosib,@fullname,@AllSectnOfFormFilled,@IsBusinessCertAttach,@IsEvidOfTaxPaymentAttach,@IsEvOf1YrTOverAttachOrVerifid," +
                    "@VeBnkDetailProvided,@IsImpactOfCovidOnBusnVrfd,@GrantyTpe,@CreditGrantAmount,@OperatnGrantAmount,@ITEnhanceGrantAmount,@authId,@userId,@Score," +
                    "@T1835,@T3645,@T46andabove,@retVal output,@retmesg output",
                         new SqlParameter("@applicant_Id", model.applicant_Id),
                         new SqlParameter("@date_of_birth", model.date_of_birth),
                         new SqlParameter("@amount", model.Amount),
                         new SqlParameter("@upload_Id", model.applicant_Id),
                         new SqlParameter("@SurName", model.SurName),
                         new SqlParameter("@FirstName", model.FirstName),
                         new SqlParameter("@Middlename", model.Middlename),
                         new SqlParameter("@BusinessName", model.BusinessName),
                         new SqlParameter("@gender", model.gender),
                         new SqlParameter("@StateOfOrigin", model.StateOfOrigin),
                         new SqlParameter("@LGA", model.LGA),
                         new SqlParameter("@ResidAddress", model.ResidAddress),
                         new SqlParameter("@Position", model.Position),
                         new SqlParameter("@Mobile", model.Mobile),
                         new SqlParameter("@Email", model.Email),
                         new SqlParameter("@PersonalTaxRegNo", model.PersonalTaxRegNo),
                         new SqlParameter("@BusinessSector", model.BusinessSector),
                         new SqlParameter("@BusinessType", model.BusinessType),
                         new SqlParameter("@BusinessAddress", model.BusinessAddress),
                         new SqlParameter("@City", model.City),
                         new SqlParameter("@State", model.State),
                         new SqlParameter("@Country", model.Country),
                         new SqlParameter("@BusinesPhoneNo", model.BusinesPhoneNo),
                         new SqlParameter("@BusinesEmail", model.BusinesEmail),
                         new SqlParameter("@BusinesWebsite", model.BusinesWebsite),
                         new SqlParameter("@BusinesTaxRegNo", model.BusinesTaxRegNo),
                         new SqlParameter("@LastTaxPayDate", model.LastTaxPayDate),
                         new SqlParameter("@BusinesStartDate", model.BusinesStartDate),
                         new SqlParameter("@NoOfYrInOperation", model.NoOfYrInOperation),
                         new SqlParameter("@NOofEmployee", model.NOofEmployee),
                         new SqlParameter("@NOofFemale", model.NOofFemale),
                         new SqlParameter("@NOofMale", model.NOofMale),
                         new SqlParameter("@EstimValOfBusTurnoverXLB", model.EstimValOfBusTurnoverXLB),
                         new SqlParameter("@BankName", model.BankName),
                         new SqlParameter("@AcctNo", model.AcctNo),
                         new SqlParameter("@BVN", model.BVN),
                         new SqlParameter("@AnyRunningLoanPriorToCovid", model.AnyRunningLoanPriorToCovid),
                         new SqlParameter("@TotalOutstandVolumeOfLoan", model.TotalOutstandVolumeOfLoan),
                         new SqlParameter("@HowOftenWereYouPaying", model.HowOftenWereYouPaying),
                         new SqlParameter("@HowMuchWereyouPaying", model.HowMuchWereyouPaying),
                         new SqlParameter("@DidYourRepaymentChange", model.DidYourRepaymentChange),
                         new SqlParameter("@RepaymntChangeToHowMuch", model.RepaymntChangeToHowMuch),
                         new SqlParameter("@findItDiffToMeetPaymentOblig", model.findItDiffToMeetPaymentOblig),
                         new SqlParameter("@BusinImpactByCovid", model.BusinImpactByCovid),
                         new SqlParameter("@BusiNegImpactByCovid", model.BusiNegImpactByCovid),
                         new SqlParameter("@specifyOther", model.specifyOther),
                         new SqlParameter("@PlanningToLayOffEmployeeDueToNeg", model.PlanningToLayOffEmployeeDueToNeg),
                         new SqlParameter("@DidYouLayOffEmployee", model.DidYouLayOffEmployee),
                         new SqlParameter("@NoOffEmployeeLayOff", model.NoOffEmployeeLayOff),
                         new SqlParameter("@DidBusContDuringCovidCrisis", model.DidBusContDuringCovidCrisis),
                         new SqlParameter("@WhtToolCudVeMadeItPosib", model.WhtToolCudVeMadeItPosib),
                         new SqlParameter("@WhtToolCudVeMadeBusMoreEffnt", model.WhtToolCudVeMadeBusMoreEffnt),
                         new SqlParameter("@EmployeeAbleToWrkFrmHom", model.EmployeeAbleToWrkFrmHom),
                         new SqlParameter("@WhtToolCudVeReqToMakeItPosib", model.WhtToolCudVeReqToMakeItPosib),
                         new SqlParameter("@fullname", model.fullname),
                         new SqlParameter("@AllSectnOfFormFilled", model.AllSectnOfFormFilled),
                         new SqlParameter("@IsBusinessCertAttach", model.IsBusinessCertAttach),
                         new SqlParameter("@IsEvidOfTaxPaymentAttach", model.IsEvidOfTaxPaymentAttach),
                         new SqlParameter("@IsEvOf1YrTOverAttachOrVerifid", model.IsEvOf1YrTOverAttachOrVerifid),
                         new SqlParameter("@VeBnkDetailProvided", model.VeBnkDetailProvided),
                         new SqlParameter("@IsImpactOfCovidOnBusnVrfd", model.IsImpactOfCovidOnBusnVrfd),
                         new SqlParameter("@GrantyTpe", model.GrantyTpe),
                         new SqlParameter("@CreditGrantAmount", model.CreditGrantAmount),
                         new SqlParameter("@OperatnGrantAmount", model.OperatnGrantAmount),
                         new SqlParameter("@ITEnhanceGrantAmount", model.ITEnhanceGrantAmount),
                         new SqlParameter("@authId", model.authId),
                         new SqlParameter("@userId", model.userId),
                         new SqlParameter("@Score", model.Score),
                         new SqlParameter("@T1835", model.C18_35),
                         new SqlParameter("@T3645", model.C36_45),
                         new SqlParameter("@T46andabove", model.C46andabove),
                         Retval1, RetMsg1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval1.Value);
            retVal.retmsg = RetMsg1.Value.ToString();

            if (retVal.retVal == 0)
            {
                var result = UpdateUploadDocument(model, LastTaxUpload, BusRegUpload, TurnoverUpload, ApplicantSignWdateUpload, passport, Lst3mnthExpUpload, EvOfRunningLoanUpload, CooperativeUpload, model.applicant_Id);
                if (result != 21)
                {
                    retVal.retVal = 0;
                    retVal.retmsg = "Update is successful.";
                    return retVal ?? new ReturnModel();
                }
            }
            return null; ;
        }
    }
}
