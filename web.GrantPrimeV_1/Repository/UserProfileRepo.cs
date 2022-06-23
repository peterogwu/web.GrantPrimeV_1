using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using web.GrantPrimeV_1.Models;
using web.GrantPrimeV_1.Models.UserData.UserProfile;

namespace web.GrantPrimeV_1.Repository
{
    public class UserProfileRepo : IUserProfileRepo
    {
        private readonly PrimeGrantEntities _entity = new PrimeGrantEntities();

        public UserProfileRepo(PrimeGrantEntities entity)
        {

            _entity = entity;
        }
        public IEnumerable<UserProfile> GetUserProfiles()
        {

           
                var AppList = new List<UserProfile>();
                try
                {
                    AppList = _entity.Database.SqlQuery<UserProfile>("Proc_GetUserProfile").ToList();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }



                return AppList;
            

        }


     


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}