using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using VirusService.Models;

namespace VirusService.Converters
{
    public class UrlScanParser
    {
        public static Scan Parse(string json)
        {
            Scan urlScan = new Scan();

            try
            {
                var obj = JObject.Parse(json);

                // Concrete objects
                var country_info = obj.GetValue("country_info").ToString();
                urlScan.CountryInfo = JsonConvert.DeserializeObject<CountryInfo>(country_info);

                var geoip = obj.GetValue("geoip").ToString();
                urlScan.Geoip = JsonConvert.DeserializeObject<Geoip>(geoip);

                var asn = obj.GetValue("asn").ToString();
                urlScan.Asn = JsonConvert.DeserializeObject<Asn>(asn);

                var whois_json = obj.GetValue("whois_json").ToString();
                urlScan.WhoisJson = JsonConvert.DeserializeObject<WhoisJson>(whois_json);

                var gsb = obj.GetValue("gsb").ToString();
                urlScan.Gsb = JsonConvert.DeserializeObject<Gsb>(gsb);

                // Properties
                urlScan.Title = obj.Property("title").Value.ToString();
                urlScan.Term = obj.Property("term").Value.ToString();

                foreach (var item in obj.Property("rip4").Children())
                {
                    urlScan.Rip4.Add(item.First.ToString());
                }

                foreach (var item in obj.Property("rip6").Children())
                {
                    urlScan.Rip6.Add(item.First?.ToString());
                }

                urlScan.Tld = obj.Property("tld").Value.ToString();
                urlScan.Fieldname = obj.Property("fieldname").Value.ToString();
                urlScan.Headline = obj.Property("headline").Value.ToString();

            }
            catch(Exception ex)
            {
                urlScan.ErrorMessage = ex?.Message;
                urlScan.StackTrace = ex?.StackTrace;
                urlScan.HasErrors = true;
            }

            return urlScan;
        }
    }
}
