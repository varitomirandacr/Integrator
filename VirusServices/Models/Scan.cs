using System;
using System.Collections.Generic;

namespace VirusService.Models
{
    public class Scan
    {
        public string Title { get; set; }
        public string Term { get; set; }
        public List<string> Rip4 { get; set; }
        public List<object> Rip6 { get; set; }
        public string Tld { get; set; }
        public string Fieldname { get; set; }
        public string Headline { get; set; }

        public CountryInfo CountryInfo { get; set; }
        public Geoip Geoip { get; set; }
        public Asn Asn { get; set; }
        public WhoisJson WhoisJson { get; set; }
        public Verdict Verdict { get; set; }
        public Gsb Gsb { get; set; }

        public string ErrorMessage{ get; set; }
        public string StackTrace { get; set; }
        public bool HasErrors { get; set; }
        
        public Scan()
        {
            this.Rip4 = new List<string>();
            this.Rip6 = new List<object>();
        }
    }

    public class CountryInfo
    {
        public string Alpha2 { get; set; }
        public string Alpha3 { get; set; }
        public List<string> CountryCallingCodes { get; set; }
        public List<string> Currencies { get; set; }
        public string Emoji { get; set; }
        public string Ioc { get; set; }
        public List<string> Languages { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }

    public class Geoip
    {
        public List<long> Range { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Eu { get; set; }
        public string Timezone { get; set; }
        public string City { get; set; }
        public List<double> Ll { get; set; }
        public long Metro { get; set; }
        public long Area { get; set; }
    }

    public class Asn
    {
        public string Ip { get; set; }
        public string Asnp { get; set; }
        public string Country { get; set; }
        public string Registrar { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
    }

    public class WhoisJson
    {
        public string Note { get; set; }
        public string Error { get; set; }
    }

    public class Verdict
    {
        public bool Dangerous { get; set; }
        public bool Phishing { get; set; }
        public bool Malware { get; set; }
        public bool Pua { get; set; }
        public DateTime LastSeen { get; set; }
    }

    public class Gsb
    {
        public Verdict Verdict { get; set; }
        public List<object> Raw { get; set; }
    }
}
