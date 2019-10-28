using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class AppSettings
    {
        public string VirusScanUrl { get; set; }
        public string GeoIpUrl { get; set; }
        public string GeoIpAccessKey { get; set; }
        public string QueryDnsUrl { get; set; }
    }
}
