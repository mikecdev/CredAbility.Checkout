using System;
using System.Collections.Generic;

namespace Checkout.MVC.Web.Models.Options
{
    public class CachingOptions
    {
        public int CacheSizeLimit { get; set; }
        public Dictionary<string, TimeSpan> Expiry { get; set; }

        public Dictionary<string, int> Size { get; set; }
    }
}
