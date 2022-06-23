using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.UserData.RoleData
{
    public class RoleModel
    {
        public int role_id { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string role_name { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public string roledesc { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public int access_days { get; set; }
        public string userid { get; set; }
        public bool canauth { get; set; }
        public string authid { get; set; }
        public bool Commitee { get; set; }
        [Required(ErrorMessage = "field is required.")]
        public int role_level { get; set; }
        public DateTime createdate { get; set; }
    }
}