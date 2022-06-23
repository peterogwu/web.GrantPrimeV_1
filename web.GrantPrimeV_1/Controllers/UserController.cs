using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.UserData.UserProfile;

namespace web.GrantPrimeV_1.Controllers
{
   //[Authorize]
    public class UserController : Controller
    {
        private readonly PrimeGrantEntities db = new PrimeGrantEntities();

        public static int? userRoleId = 0;
        [Authorize]
        public ActionResult Index()
        {

            return View(db.tbl_userprofile.ToList());
            //return View(db.tbl_Users.Where(m => m.IsEmailVerified == true).ToList());

        }
        [Authorize]
        // Registration action 
        [HttpGet]
        public ActionResult Registration()
        {

            return View();
        }
        //registration Post Action 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration([Bind(Exclude = "IsEmailVerified, ActivationCode")] tbl_userprofile user)
        {
            
            bool Status = false;
            string Message = "";
            //Model Validation 
            if (ModelState.IsValid)
            {
                #region//Email is already exist
                var isExist = IsEmailExist(user.email);
                var useExist = IsUserExist(user.Userid);
                if (isExist)
                {
                    ModelState.AddModelError("EmailExist", "Email already exist");
                    return View(user);
                }
                if (useExist)
                {
                    ModelState.AddModelError("useExist", "User already exist");
                    return View(user);
                }
                #endregion

                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hashing
                user.userpassword = Crypto.Hash(user.userpassword);
                user.ConfirmPassword = Crypto.Hash(user.ConfirmPassword);//
                #endregion
                user.IsEmailVerified = false;
                #region save to database
                using (PrimeGrantEntities db = new PrimeGrantEntities())
                {
                    //db.tbl_userprofile.Add(user);
                    //db.SaveChanges();


                    SqlParameter Retval = new SqlParameter("@retval", SqlDbType.Int);
                    Retval.Direction = System.Data.ParameterDirection.Output;

                    SqlParameter RetMsg = new SqlParameter("@retmsg", SqlDbType.VarChar, 200);
                    RetMsg.Direction = System.Data.ParameterDirection.Output;

                    var ftr = db.Database.ExecuteSqlCommand("Proc_CreateUserProfile @Userid, @fullname,@email,@phone,@password,@confirmPassword," +
                        "@retval OUT, @retmsg OUT",
                        new SqlParameter("@Userid", user.Userid),
                        new SqlParameter("@fullname", user.fullname),
                        new SqlParameter("@email", user.email),
                        new SqlParameter("@phone", user.phoneno),
                        new SqlParameter("@password", user.userpassword),
                        new SqlParameter("@confirmPassword", user.ConfirmPassword),Retval, RetMsg);

                    int xRetval = Convert.ToInt32(Retval.Value.ToString());

                    string xRetMsg = RetMsg.Value.ToString();

                    if (xRetval == 0)
                    {
                        Message = " Registration successfully done. Account activation link " +
                   "has been sent to your email Id ";/*+ user.EmailId;*/
                        // send email to user
                        //SendVerificationEmailLink(user.EmailId, user.ActivationCode.ToString());
                        //Message = "Registration successfully done. Account activation link " +
                        //    "has been sent to your email Id " + user.EmailId;
                        Status = true;
                    }
                    else {
                        Message = "oops!! something went wrong";
                        Status = false;
                    }

             
                }
                #endregion
            }
            else
            {

                Message = "Invalid request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(user);
        }
        // Verify Account


        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            bool Status = false;
            using (PrimeGrantEntities dc = new PrimeGrantEntities())
            {
                dc.Configuration.ValidateOnSaveEnabled = false;//this line I have added here to avoid confirm password does not match issues
                                                               //on save
                var v = dc.tbl_userprofile.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    dc.SaveChanges();
                    Status = true;


                }
                else
                {

                    ViewBag.Message = "Invalid request";
                }


            }
            ViewBag.Status = Status;
            return View();


        }
        //Verify Email LINK

        //Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        //Login Post
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Login(UserLogin login, string returnUrl)
        {
            string message = "";
            bool Status = false;
            try {
                using (PrimeGrantEntities db = new PrimeGrantEntities())
                {
                    var v = db.tbl_userprofile.Where(a => a.Userid == login.UserId).FirstOrDefault();
                    var v1 = db.tbl_Role.Where(a => a.userid == login.UserId).FirstOrDefault();
                    if (v != null)
                    {
                        if (string.Compare(Crypto.Hash(login.Password), v.userpassword) == 0)
                        {
                            DateTime now = DateTime.Now;
                            HttpCookie mycookie = new HttpCookie("roleid");
                            mycookie.Value = v1.role_level.ToString();
                            mycookie.Expires = now.AddYears(50);

                            Response.Cookies.Add(mycookie);
                            int timeout = login.RememberMe ? 525600 : 20;//525600 mins ! year
                            var ticket = new FormsAuthenticationTicket(login.UserId, login.RememberMe, timeout);
                            string encrypted = FormsAuthentication.Encrypt(ticket);
                            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                            cookie.Expires = DateTime.Now.AddMinutes(timeout);
                            cookie.HttpOnly = true;
                            Response.Cookies.Add(cookie);
                            if (Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }

                        }
                        else
                        {
                            Status = true;
                            message = "Invalid credential provided";
                        }
                    }
                    else
                    {
                        Status = true;
                        message = "Incorrect Username and Password Please enter correct Username and password";
                    }
                }
            }
            catch(Exception e) {
                Status = true;
                message = "Oops!! something went wrong. "+e.Message;
            }
         
            ViewBag.status = Status;
            ViewBag.Message = message;
            return View();

        }
        // Logout
        //[Authorize]
        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");

        }

        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            using (PrimeGrantEntities db = new PrimeGrantEntities())
            {
                var v = db.tbl_userprofile.Where(a => a.email == emailID).FirstOrDefault();
                return v != null;
            }

        }
        public bool IsUserExist(string UserId)
        {
            using (PrimeGrantEntities db = new PrimeGrantEntities())
            {
                var user = db.tbl_userprofile.Where(a => a.Userid == UserId).FirstOrDefault();
                return user != null;
            }

        }
        [NonAction]
        public void SendVerificationEmailLink(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            try {
                var verifyUrl = "/User/" + emailFor + "/" + activationCode;
                var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
                var fromMail = new MailAddress("admin@ekitimeda.com", "MEDA");
                //var fromMail = new MailAddress("abbasjohn17@gmail.com", "MEDA");
                var ToMail = new MailAddress(emailID);
                var fromMailPassword = "ekiti@2020"; //Replace with actual password
                //var fromMailPassword = "Tabernacle@@@@@,"; //Replace with actual password
                                                         //var fromMailPassword = "Pass123...000"; //Replace with actual password
                string subject = "";
                string body = "";
                if (emailFor == "VerifyAccount")
                {
                    subject = "Your account is successfully created";
                    body = "</br></br> we are excited to tell you that you have been profiled on this system." +
                        " please click on the link below to verify your account" +
                        "</br></br><a href='" + link + "'>" + link + "</a>";
                }
                else if (emailFor == "ResetPassword")
                {
                    subject = "Reset Password";
                    body = "<br/><br/>We got request for reset your Account password. Please, click on the below link to reset your password" +
                        "<br/><br/> <a href=" + link + "> Reset password link</a>";
                }


                var smtp = new SmtpClient
                {
                    //Host = "sbsserver.bosakmfb.com",
                    Host = "mail.ekitimeda.com",
                    ////Host = "smtp.gmail.com",
                    //Port = 465,
                    Port = 587,
                    //Port = 25,
                    EnableSsl = false,
                    //EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromMail.Address, fromMailPassword)

                };
                using (var message = new MailMessage(fromMail, ToMail)
                {

                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true

                })
                    //smtp.EnableSsl = true;
                    //ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; }



                    smtp.Send(message);
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }

        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {

            return View();
        }

        //[HttpPost,ActionName("ForgotPassword1")]
        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            //verify Email
            //if valid
            //generate password reset link
            // send to Email Id
            string message = "";
            bool status = false;

            using (PrimeGrantEntities db = new PrimeGrantEntities())
            {
                var account = db.tbl_userprofile.Where(a => a.email == EmailID).FirstOrDefault();
                if (account != null)
                {
                    //send email for reset password
                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationEmailLink(account.email, resetCode, "ResetPassword");
                    account.PasswordResetCode = resetCode;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.SaveChanges();
                    status = true;
                    message = "Reset password link has been sent to your email";
                }
                else
                {
                    status = true;
                    message = "Account not found!";
                }
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View();
        }
        [HttpGet]
        public ActionResult ResetPassword(string id)
        {
            // verify the reset password link
            // find account associated with this link
            //redirect to reset password page
            if (string.IsNullOrWhiteSpace(id))
            {
                return HttpNotFound();
            }
            using (PrimeGrantEntities dc = new PrimeGrantEntities())
            {

                var user = dc.tbl_userprofile.Where(a => a.PasswordResetCode == id).FirstOrDefault();
                if (user != null)
                {
                    ResetPasswordmodel model = new ResetPasswordmodel();
                    model.ResetCode = id;
                    return View(model);

                }
                else
                {
                    return HttpNotFound();
                }
            }

        }

        //[HttpPost, ActionName("ResetPassword1")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordmodel model)
        {
            var message = "";
            bool status = false;
            if (ModelState.IsValid)
            {

                using (PrimeGrantEntities db = new PrimeGrantEntities())
                {
                    var user = db.tbl_userprofile.Where(a => a.PasswordResetCode == model.ResetCode).FirstOrDefault();
                    if (user != null)
                    {
                        user.userpassword = Crypto.Hash(model.NewPassword);
                        user.PasswordResetCode = "";
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        status = true;
                        message = "New password updated successfully";
                    }
                }
            }
            else
            {
                status = true;
                message = "Reset Code is Invalid";
            }
            ViewBag.Status = status;
            ViewBag.Message = message;
            return View(model);
        }

        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_userprofile tbl_recon_bank = db.tbl_userprofile.Find(id);
            if (tbl_recon_bank == null)
            {
                return HttpNotFound();
            }
            return View(tbl_recon_bank);
        }
        [Authorize]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_userprofile tbl_Users = db.tbl_userprofile.SingleOrDefault(m => m.id == id);
            //tbl_Users tbl_Users = db.tbl_Users.Find(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Users);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tbl_userprofile tbl_Users)
        {

            bool Status = false;
            string Message = "";
            try
            {


                //db.Entry(tbl_recon_bank).State = EntityState.Modified;
                //db.SaveChanges();

                SqlParameter Retval = new SqlParameter("@Retval", SqlDbType.VarChar, 4);
                Retval.Direction = System.Data.ParameterDirection.Output;

                SqlParameter RetMsg = new SqlParameter("@retmsg", SqlDbType.VarChar, 200);
                RetMsg.Direction = System.Data.ParameterDirection.Output;

                var ftr = db.Database.ExecuteSqlCommand("proc_Upd_User @Userid, @fullname,@email,@phone," +
                    "@retval OUT, @retMsg OUT",
                    new SqlParameter("@Userid", tbl_Users.Userid),
                    new SqlParameter("@fullname", tbl_Users.fullname),
                    new SqlParameter("@email", tbl_Users.email),
                    new SqlParameter("@phone", tbl_Users.phoneno), Retval, RetMsg);

                string xRetval = Retval.Value.ToString();

                string xRetMsg = RetMsg.Value.ToString();
                Message = xRetMsg;
                Status = true;
                //db.SaveChanges();
                ViewBag.Message = Message;
                ViewBag.Status = Status;
                return View(tbl_Users);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error!" + e.Message);
                Message = "Invalid request";
                Status = true;
            }



            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(tbl_Users);
        }


        [Authorize]
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_userprofile tbl_Users = db.tbl_userprofile.Find(id);
            if (tbl_Users == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            tbl_userprofile tbl_Users = db.tbl_userprofile.Find(id);
            //tbl_recon_bank.status = Convert.ToInt32(0).ToString();
            ////db.Entry(tbl_recon_bank).State= System.Data.Entity.EntityState.Modified;
            //db.tbl_recon_bank.Attach(tbl_recon_bank);
            //var entry = db.Entry(tbl_recon_bank);
            //entry.Property(e => e.status).IsModified = true;
            //db.SaveChanges();
            SqlParameter Retval = new SqlParameter("@Retval", SqlDbType.VarChar, 4);
            Retval.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg = new SqlParameter("@retmsg", SqlDbType.VarChar, 200);
            RetMsg.Direction = System.Data.ParameterDirection.Output;

            var ftr = db.Database.ExecuteSqlCommand("Proc_DeActivateBank @id," +
                "@retval OUT, @retMsg OUT",
                new SqlParameter("@id", id), Retval, RetMsg);

            int xRetval = Convert.ToInt32(Retval.Value.ToString());

            string xRetMsg = RetMsg.Value.ToString();
            if (xRetval == 0)
            {

                return RedirectToAction("Index");
            }
            else
            {
                return View(tbl_Users);
            }


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}