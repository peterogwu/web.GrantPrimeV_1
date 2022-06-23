using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData.UserProfile
{
    public class UserRegReturn
    {

        public UserProfile UserProfile { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }

    }
}