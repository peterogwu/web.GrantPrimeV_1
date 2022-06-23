using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.UserData.RoleData;
using web.GrantPrimeV_1.Repository;

namespace web.GrantPrimeV_1.Controllers
{
    [Authorize]
    public class RoleController : Controller
    {
        private IRoleRepo _entity;
        public RoleController()
        {
            _entity = new RoleRepo(new Models.PrimeGrantEntities());
        }
        public RoleController(IRoleRepo entity)
        {
            _entity = entity;
        }// GET: Role
        public ActionResult Index()
        {
            var model =_entity.GetRoles();
            return View(model);
        }
        [HttpGet]
        public ActionResult AddRole() {
            return View();
        }
        [HttpPost]
        public ActionResult AddRole(RoleModel model)
        {
            bool Status = false;
            string Message = "";
            var data = _entity.AddRole(model);
            if (data.retVal==0)
            {
                 Status = true;
                 Message = data.retmsg;
            }
            else if(data.retVal==21){
                Status = false;
                Message = data.retmsg;
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(model);
        }

        [HttpGet]
        public ActionResult EditRole(int? id)
        {
            var model = _entity.GetRolesBYID(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditRole(tbl_Role model)
        {
            bool Status = false;
            string Message = "";
            var data = _entity.EditRole(model);
            if (data.retVal == 0)
            {
                Status = true;
                Message = data.retmsg;
            }
            else if (data.retVal == 21)
            {
                Status = false;
                Message = data.retmsg;
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;
            return View(model);
        }
    }
}