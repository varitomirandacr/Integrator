using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Models
{
    public class Endpoints
    {
        public string Icmp { get; set; }
        public string DnsResolver { get; set; }
        public string DnsChilkat { get; set; }
        public string DnsLookcup { get; set; }
        public string Geoip { get; set; }
        public string VirusScan { get; set; }

        public object this[string propertyName]
        {
            get { return this.GetType().GetProperty(propertyName).GetValue(this, null); }
            set { this.GetType().GetProperty(propertyName).SetValue(this, value, null); }
        }
    }
}
