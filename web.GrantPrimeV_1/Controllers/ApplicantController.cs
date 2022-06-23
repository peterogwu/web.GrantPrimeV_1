using PrimeRepo.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.UserData;
using web.GrantPrimeV_1.Models.UserData.ApplicantData;

namespace web.GrantPrimeV_1.Controllers
{
    [Authorize]
    public class ApplicantController : Controller
    {

        private IUserRepo _entity;
        public ApplicantController()
        {
            _entity = new UserRepo(new Models.PrimeGrantEntities());
        }
        public ApplicantController(IUserRepo entity)
        {
            _entity = entity;
        }
        // GET: Applicant
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Disburse()
        {
            return View();
        }


        public ActionResult Approval2()
        {
            return View();
        }

        public JsonResult GetApprovedApplicants(int btn,int dli,string Bvn)
        {
            if (btn == 1)
            {
                var result = _entity.GetAllTranchDisburseGrant();
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("No record found", JsonRequestBehavior.AllowGet);
                }
            }
            else if (btn == 2) {
                var result = _entity.GetAllAprovedApplicantsYetToDisburse(dli);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("No record found", JsonRequestBehavior.AllowGet);
                }
            }
                 return Json(null, JsonRequestBehavior.AllowGet); 
        }




        public ActionResult EditApplicant(long id)
        {
            var app_Id = _entity.GetCusterListBYId(id);
            if (app_Id == null)
            {
                var message = "Id not found!!!";
                ViewBag.message = message;
                return View();
            }

            var LGA = _entity.GetTown();
            ViewBag.LGA = LGA;
            var State = _entity.GetStates();
            ViewBag.State = State;
            var country = _entity.GetCountry();
            ViewBag.country = country;

            return View(app_Id);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditApplicant(ApplicantModel model, HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload, HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload)
        {
            var LGA = _entity.GetTown();
            ViewBag.LGA = LGA;
            var State = _entity.GetStates();
            ViewBag.State = State;
            var country = _entity.GetCountry();
            ViewBag.country = country;
            string message = "";
            bool Status = false;
            model.userId = User.Identity.Name;
            model.authId = User.Identity.Name;
            var returnModel = _entity.UpdateApplyForGrant(model, LastTaxUpload, BusRegUpload, TurnoverUpload, ApplicantSignWdateUpload, passport, Lst3mnthExpUpload, EvOfRunningLoanUpload, CooperativeUpload);
            if (returnModel.retVal == 0)
            {
                Status = true;
                message = "Application Edited successful";
            }
            else
            {
                Status = true;
                message = "OOps!!! Something went wrong.";
            }

            //}

            ViewBag.status = Status;
            ViewBag.Message = message;
            return View();
        }

        //public bool IsEmailExist(string emailID)
        //{
        //    using (PrimeGrantEntities db = new PrimeGrantEntities())
        //    {
        //        var v = db.tbl_Applicant.Where(a => a.Email == emailID).FirstOrDefault();
        //        return v != null;
        //    }

        //}
        //public bool IsMobileExist(string Mobile)
        //{
        //    using (PrimeGrantEntities db = new PrimeGrantEntities())
        //    {
        //        var user = db.tbl_Applicant.Where(a => a.Mobile == Mobile).FirstOrDefault();
        //        return user != null;
        //    }

        //}

        [HttpGet]
        public ActionResult Create()
        {
            var LGA = _entity.GetTown();
            ViewBag.LGA = LGA;
            var State = _entity.GetStates();
            ViewBag.State = State;
            var country = _entity.GetCountry();
            ViewBag.country = country;
            return View();
        }
        [HttpPost]
        public ActionResult Create(ApplicantModel model, HttpPostedFileBase LastTaxUpload, HttpPostedFileBase BusRegUpload, HttpPostedFileBase TurnoverUpload,HttpPostedFileBase ApplicantSignWdateUpload, HttpPostedFileBase passport, HttpPostedFileBase Lst3mnthExpUpload, HttpPostedFileBase EvOfRunningLoanUpload, HttpPostedFileBase CooperativeUpload)
        {
            var LGA = _entity.GetTown();
            ViewBag.LGA = LGA;
            var State = _entity.GetStates();
            ViewBag.State = State;
            var country = _entity.GetCountry();
            ViewBag.country = country;
            string message = "";
            bool Status = false;
            model.userId = User.Identity.Name;
            model.authId = User.Identity.Name;
            //if (ModelState.IsValid)
            //{
            //var isExist = IsEmailExist(model.Email);
            //var useExist = IsMobileExist(model.Mobile);
            //if (isExist)
            //{
            //    ModelState.AddModelError("EmailExist", "Email already exist");
            //    return View(model);
            //}
            //if (useExist)
            //{
            //    ModelState.AddModelError("MobileExist", "Phone Number already exist");
            //    return View(model);
            //}
            var returnModel = _entity.ApplyForGrant(model, LastTaxUpload, BusRegUpload, TurnoverUpload, ApplicantSignWdateUpload, passport, Lst3mnthExpUpload, EvOfRunningLoanUpload, CooperativeUpload);
                if (returnModel.retVal == 0)
                {
                    Status = true;
                    message = "Application for Grant is successful";
                }
                else
                {
                    Status = true;
                    message = "OOps!!! Something went wrong.";
                }
     
            //}

            ViewBag.status = Status;
            ViewBag.Message = message;
            return View();
        }
        public ActionResult DownloadTaxReceipt(long id)
        {

            DateTime time = DateTime.Now;
            var file = _entity.GetCusterListBYId(id);
            byte[] fileBytes = file.LstTaxDatePayEvidnce;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Image" + time + ".jpeg");

        }

        public ActionResult DownloadBusinessTurnOver(long id)
        {

            DateTime time = DateTime.Now;
            var file = _entity.GetCusterListBYId(id);
            byte[] fileBytes = file.Lst3YrAvrgeAnnualBusnTOverEvdnce;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Image" + time + ".jpeg");

        }
        public ActionResult DownloadCAC(long id)
        {

            DateTime time = DateTime.Now;
            var file = _entity.GetCusterListBYId(id);
            byte[] fileBytes = file.BusnRegNumEvdnce;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Image" + time + ".jpeg");

        }

        public ActionResult DownloadExpense(long id)
        {

            DateTime time = DateTime.Now;
            var file = _entity.GetCusterListBYId(id);
            byte[] fileBytes = file.EvidnceOfOpertnExp4pst3mnth;

            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, "Image" + time + ".jpeg");

        }
        public JsonResult GetPhoto(long id)
        {
            //DateTime time = DateTime.Now.ToLocalTime();
            var file = _entity.GetCusterListBYId(id);
            byte[] imageByteData = file.LstTaxDatePayEvidnce;
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            var imgSrc = String.Format("data:image/jpeg;base64,{0}", imageBase64Data);
            return Json(imgSrc, JsonRequestBehavior.AllowGet);

            //return File(imageDataURL);
        }

        public JsonResult GetApplicantList(int btn, int dli)
        {
            var user = User.Identity.Name;
            if (btn == 1)
            {
                var result = _entity.GetCusterList(user);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("No record found", JsonRequestBehavior.AllowGet);
                }
            }
            else if (btn == 2)
            {
                var result = _entity.GetRejectedApplication(dli);
                if (result != null)
                {
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("No record found", JsonRequestBehavior.AllowGet);
                }
            }
            return null;
        }

        public JsonResult GetApprovedAppLevel()
        {
            var user = User.Identity.Name;
            var result = _entity.GetCusterList(user);
            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No record found", JsonRequestBehavior.AllowGet);
            }
        }
        //public JsonResult GetTrancheDisburseGrant()
        //{
         
        //    var result = _entity.GetAllTranchDisburseGrant();
        //    if (result != null)
        //    {
        //        return Json(result, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json("No record found", JsonRequestBehavior.AllowGet);
        //    }
        //}
        public JsonResult GetApprovedAppLevel2()
        {
            var result = _entity.GetApprovedAppLevel2();
            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No record found", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult ValidateApplicant(string phoneNumber, string bvnumb, string emailNumb)
        {

            if (phoneNumber != null)
            {
                using (PrimeGrantEntities db = new PrimeGrantEntities())
                {
                    var user = db.tbl_Applicant.Where(a => a.Mobile == phoneNumber || a.Email == emailNumb || a.BVN == bvnumb).FirstOrDefault();
                    if (user != null)
                    {
                        return Json(user.GrantyTpe, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(0, JsonRequestBehavior.AllowGet);
                    }
                }

            }
            return null;
        }

        public JsonResult GetApplicantById(long id)
        {
            var result = _entity.GetCusterListBYId(id);
            if (result != null)
            {
                var jsonResult = Json(result, JsonRequestBehavior.AllowGet);
                jsonResult.MaxJsonLength = int.MaxValue;
                return jsonResult;

            }
            else
            {
                return Json("No record found", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DisburseGrant(AuthoriseModel model)
        {
            //model.userId = "Admi002";
            model.userId = User.Identity.Name;

            var result = _entity.DisburseGrant(model);
            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No record found", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Approve(AuthoriseModel model)
        {
            //model.userId = "Admi002";
            model.userId = User.Identity.Name;
        
            var result = _entity.ApproveApplication(model);
            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No record found", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Reject(AuthoriseModel model)
        {
            model.userId = User.Identity.Name;
            var result = _entity.RejectApplication(model);
            if (result != null)
            {
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No record found", JsonRequestBehavior.AllowGet);
            }
        }

        

    }

}