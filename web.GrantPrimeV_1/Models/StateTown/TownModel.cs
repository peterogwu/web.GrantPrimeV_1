using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.StateTown
{
    public class TownModel
    {
        public string TownCode { get; set; }
        public string TownName { get; set; }
        public string StateCode { get; set; }
        public Nullable<int> Status { get; set; }
        public string townshortname { get; set; }
        public string authid { get; set; }
        public string userid { get; set; }
        public Nullable<System.DateTime> createdate { get; set; }
    }
}