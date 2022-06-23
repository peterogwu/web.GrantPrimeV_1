using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.GrantPrimeV_1.Models.UserData.ApplicantData;
using web.GrantPrimeV_1.Repository;

namespace web.GrantPrimeV_1.Controllers
{
    public class ReportController : Controller
    {
        private IReportRepo _entity;

        public ReportController()
        {
            _entity = new ReportRepo(new Models.PrimeGrantEntities());
        }
        public ReportController(IReportRepo entity)
        {
            _entity = entity;
        }

        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Chart()
        {
            var entities = new List<RatioModel>();

            entities = _entity.RatioDetail().ToList();

            return Json(entities, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllApplicants()
        {
            var result = new List<ApplicantViewModel>();
            try
            {
                result = _entity.GetAllApplicants().ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                return View(result);
            }
        }

        public ActionResult RejectedApplicants()
        {
            var result = new List<ApplicantViewModel>();
            try
            {
                result = _entity.GetAllRejectedApplicants().ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                return View(result);
            }
        }

        public ActionResult ApplicantsByGrantType(int? grantType)
        {
            var result = new List<ApplicantViewModel>();
            try
            {
                grantType = grantType == null ? 0 : grantType;
                result = _entity.GetAllApplicantsByType((int)grantType).ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                return View(result);
            }
        }

        public ActionResult DisbursedApplicants()
        {
            var result = new List<ApplicantViewModel>();
            try
            {
                
                result = _entity.GetAllDisbursedApplicants().ToList();
               
                return View(result);
            }
            catch (Exception ex)
            {
                return View(result);
            }
        }

        public ActionResult ApprovedApplicants()
        {
            var result = new List<ApplicantViewModel>();
            try
            {
                result = _entity.GetApplicantsByStatus(3).ToList();

                return View(result);
            }
            catch (Exception ex)
            {
                return View(result);
            }
        }
    }
}