using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VirusService.Models;

[assembly: InternalsVisibleTo("VirusServiceTest")]
namespace VirusService.Parsers
{
    public class JsonScanParser
    {
        internal static Scan Parse(string json)
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
                
                IterateJsonProperty(obj, urlScan.Rip4, "rip4");

                IterateJsonProperty(obj, urlScan.Rip6, "rip6");

                urlScan.Tld = obj.Property("tld").Value.ToString();
                urlScan.Fieldname = obj.Property("fieldname").Value.ToString();
                urlScan.Headline = obj.Property("headline").Value.ToString();
            }
            catch (Exception ex)
            {
                urlScan.Message = ex?.Message;
                urlScan.StackTrace = ex?.StackTrace;
                urlScan.HasErrors = true;
            }

            return urlScan;
        }

        private static void IterateJsonProperty(JObject obj, List<string> items, string propName)
        {
            foreach (var item in obj.Property(propName).Children())
            {
                items.Add(item.First?.ToString());
            }
        }
    }
}
