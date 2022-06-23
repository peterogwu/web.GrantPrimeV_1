using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using web.GrantPrimeV_1.Models.StateTown;
using web.GrantPrimeV_1.Models.UserData;
using web.GrantPrimeV_1.Models.UserData.ApplicantData;

namespace PrimeRepo.Repository
{
  public interface IUserRepo:IDisposable
    {

        IEnumerable<ApplicantModel> GetCusterList(string user);
        ApplicantModel GetCusterListBYId(long applicant_Id);
        ReturnModel ApproveApplication(AuthoriseModel mode);
        ReturnModel RejectApplication(AuthoriseModel mode);
        IEnumerable<CustomerModel> GetApprovedAppLevel1();
        IEnumerable<CustomerModel> GetApprovedAppLevel2();
        IEnumerable<ApplicantModel> GetRejectedApplication(int dli);
       int GetUserStatus(AuthoriseModel mode);
        IEnumerable<StateModel> GetStates();
        IEnumerable<TownModel> GetTown();
        IEnumerable<CountryModel> GetCountry();
        ReturnModel ApplyForGrant(ApplicantModel model, HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload, HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload);
        IEnumerable<ApplicantModel> GetApplicantsDisburse(int grantType, string bvn);
        IEnumerable<ApplicantModel> GetAllAprovedApplicantsYetToDisburse(int grantType);
        ReturnModel UpdateApplyForGrant(ApplicantModel model, HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload, HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload);
        ReturnModel DisburseGrant(AuthoriseModel mode);
        IEnumerable<ApplicantModel> GetAllTranchDisburseGrant();

    }
}
