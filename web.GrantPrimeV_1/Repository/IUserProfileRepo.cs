using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using web.GrantPrimeV_1.Models.UserData.UserProfile;

namespace web.GrantPrimeV_1.Repository
{
  public  interface IUserProfileRepo: IDisposable
    {
        IEnumerable<UserProfile> GetUserProfiles();
    }
}
