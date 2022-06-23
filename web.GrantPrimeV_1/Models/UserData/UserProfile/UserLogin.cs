using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData.UserProfile
{
    public class UserLogin
    {
        public int id { get; set; }

        [Display(Name = "User ID")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User ID required")]
        public string UserId { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}