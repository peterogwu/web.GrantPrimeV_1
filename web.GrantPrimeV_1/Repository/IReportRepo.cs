using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.GrantPrimeV_1.Models.UserData.ApplicantData;

namespace web.GrantPrimeV_1.Repository
{
  public  interface IReportRepo
    {
        IEnumerable<ApplicantViewModel> GetAllDisbursedApplicants();
        IEnumerable<ApplicantViewModel> GetAllApplicants();
        IEnumerable<ApplicantViewModel> GetApplicantsByStatus(int status);
        IEnumerable<ApplicantViewModel> GetAllRejectedApplicants();
        IEnumerable<ApplicantViewModel> GetAllApplicantsByType(int grantType);
        IEnumerable<RatioModel> RatioDetail();
    }
}
