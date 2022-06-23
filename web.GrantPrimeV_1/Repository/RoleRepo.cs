using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.UserData;
using web.GrantPrimeV_1.Models.UserData.RoleData;

namespace web.GrantPrimeV_1.Repository
{
    public class RoleRepo : IRoleRepo
    {

        private readonly PrimeGrantEntities _entity = new PrimeGrantEntities();

        public RoleRepo(PrimeGrantEntities entity)
        {

            _entity = entity;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }     
        public ReturnModel AddRole(RoleModel model)
        {
          
            var retVal = new ReturnModel();
            SqlParameter Retval = new SqlParameter("@retVal", SqlDbType.Int);
            Retval.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg = new SqlParameter("@retmesg", SqlDbType.VarChar, 150);
            RetMsg.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("proc_AddUserRole @userid,@rolename,@roledesc,@accessdays,@Commitee,@role_level,@canAuth,@retVal output,@retmesg output",
                         new SqlParameter("@userid", model.userid),
                         new SqlParameter("@rolename", model.role_name),
                         new SqlParameter("@roledesc", model.roledesc),
                         new SqlParameter("@accessdays", model.access_days),
                         new SqlParameter("@Commitee", model.Commitee),
                         new SqlParameter("@role_level", model.role_level),
                         new SqlParameter("@canAuth", model.canauth),
                         Retval, RetMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval.Value);
            retVal.retmsg = RetMsg.Value.ToString();
            return retVal ?? new ReturnModel();
        }

        public ReturnModel EditRole(tbl_Role model)
        {
            var retVal = new ReturnModel();
            SqlParameter Retval = new SqlParameter("@retVal", SqlDbType.Int);
            Retval.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg = new SqlParameter("@retmesg", SqlDbType.VarChar, 150);
            RetMsg.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("proc_EditUserRole @userid,@rolename,@roledesc,@committee,@accessdays,@canAuth,@retVal output,@retmesg output",
                         new SqlParameter("@userid", model.userid),
                         new SqlParameter("@rolename", model.role_name),
                         new SqlParameter("@roledesc", model.roledesc),
                         new SqlParameter("@committee", model.Commitee),
                         new SqlParameter("@accessdays", model.access_days),
                         new SqlParameter("@canAuth", model.canauth),
                         Retval, RetMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval.Value);
            retVal.retmsg = RetMsg.Value.ToString();
            return retVal ?? new ReturnModel();
        }

        public ReturnModel DeleteRole(RoleModel model)
        {
            var retVal = new ReturnModel();
            SqlParameter Retval = new SqlParameter("@retVal", SqlDbType.Int);
            Retval.Direction = System.Data.ParameterDirection.Output;

            SqlParameter RetMsg = new SqlParameter("@retmesg", SqlDbType.VarChar, 150);
            RetMsg.Direction = System.Data.ParameterDirection.Output;
            try
            {
                var AppList = _entity.Database.ExecuteSqlCommand("proc_EditUserRole @userid,@retVal output,@retmesg output",
                         new SqlParameter("@userid", model.userid),
                         Retval, RetMsg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            retVal.retVal = Convert.ToInt32(Retval.Value);
            retVal.retmsg = RetMsg.Value.ToString();
            return retVal ?? new ReturnModel();
        }
        public IEnumerable<tbl_Role> GetRoles() {

            

         var   model = _entity.tbl_Role.Where(a=>a.status==1).ToList();


            return model;
        }
        public tbl_Role GetRolesBYID(int? id)
        {



            var model = _entity.tbl_Role.Where(a => a.status == 1 && a.role_id==id).FirstOrDefault();


            return model;
        }

    }
}