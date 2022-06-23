using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.UserData;
using web.GrantPrimeV_1.Models.UserData.RoleData;

namespace web.GrantPrimeV_1.Repository
{
 public   interface IRoleRepo:IDisposable
    {
        ReturnModel AddRole(RoleModel model);
        ReturnModel EditRole(tbl_Role model);
        IEnumerable<tbl_Role> GetRoles();
        ReturnModel DeleteRole(RoleModel model);
        tbl_Role GetRolesBYID(int? id);
    }
}
