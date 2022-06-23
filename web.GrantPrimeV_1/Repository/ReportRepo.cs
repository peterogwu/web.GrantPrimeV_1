using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.UserData.ApplicantData;

namespace web.GrantPrimeV_1.Repository
{
    public class ReportRepo: IReportRepo
    {
        private readonly PrimeGrantEntities _entity = new PrimeGrantEntities();

        public ReportRepo(PrimeGrantEntities entity)
        {
            _entity = entity;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        
                    public IEnumerable<ApplicantViewModel> GetAllDisbursedApplicants()
        {
            var AppList = new List<ApplicantViewModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantViewModel>("Proc_GetAllDisbursedApplicant"
                    ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return AppList;
        }
        public IEnumerable<ApplicantViewModel> GetAllApplicants()
        {
            var AppList = new List<ApplicantViewModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantViewModel>("Proc_GetListAllApplicants"
                    ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return AppList;
        }

        public IEnumerable<ApplicantViewModel> GetApplicantsByStatus(int status)
        {
            var AppList = new List<ApplicantViewModel>();
            try
            {
                AppList = _entity.Database.SqlQuery<ApplicantViewModel>("Proc_GetAllApplicantByStatus @Status",
                    new SqlParameter("@Status", status)
                    ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return AppList;
        }

        public IEnumerable<ApplicantViewModel> GetAllRejectedApplicants()
        {
            var result = new List<ApplicantViewModel>();
            try
            {
                result = _entity.Database.SqlQuery<ApplicantViewModel>("Proc_GetAllRejected"
                    ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public IEnumerable<ApplicantViewModel> GetAllApplicantsByType(int grantType)
        {
            var result = new List<ApplicantViewModel>();
            try
            {
                result = _entity.Database.SqlQuery<ApplicantViewModel>("Proc_GetApplicantByGrantType @GrantType",
                    new SqlParameter("@GrantType", grantType)
                    ).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return result;
        }

        public IEnumerable<RatioModel> RatioDetail()
        {
            var result = new List<RatioModel>();
            try
            {
                result = _entity.Database.SqlQuery<RatioModel>("Proc_GetRatioOfGrant"
                    ).ToList();

                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }
    }
}