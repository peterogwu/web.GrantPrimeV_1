using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace web.GrantPrimeV_1.Models.StateTown
{
    public class CountryModel
    {
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountryMne { get; set; }
        public int Status { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyMne { get; set; }
        public string userid { get; set; }
        public string authid { get; set; }
        public DateTime? createdate { get; set; }
    }
}