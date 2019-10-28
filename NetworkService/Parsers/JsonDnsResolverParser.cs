using NetworkService.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NetworkServiceTest")]
namespace NetworkService.Parsers
{
    public class JsonDnsResolverParser
    {
        internal static NetworkDnsResolver Parse(string json)
        {
            NetworkDnsResolver dnsResolver = new NetworkDnsResolver();

            try
            {
                var obj = JObject.Parse(json);

                dnsResolver.Status = (int.Parse(obj.Property("Status").Value.ToString()) == 0);
                dnsResolver.TC = bool.Parse(obj.Property("TC").Value.ToString());
                dnsResolver.RD = bool.Parse(obj.Property("RD").Value.ToString());
                dnsResolver.RA = bool.Parse(obj.Property("RA").Value.ToString());
                dnsResolver.AD = bool.Parse(obj.Property("AD").Value.ToString());
                dnsResolver.CD = bool.Parse(obj.Property("CD").Value.ToString());

                foreach (var item in obj.Property("Question").Children())
                {
                    var name = item.First?.Value<string>("name").ToString();
                    var type = int.Parse(item.First?.Value<string>("type").ToString());

                    dnsResolver.Questions.Add(new DnsResolverQuestion() 
                    {
                        Name = name,
                        Type = type
                    });
                }

                foreach (var item in obj.Property("Answer").Children())
                {
                    var name = item.First?.Value<string>("name").ToString();
                    var type = int.Parse(item.First?.Value<int>("type").ToString());
                    var ttl = int.Parse(item.First?.Value<int>("ttl").ToString());
                    var data = item.First?.Value<string>("data").ToString();

                    dnsResolver.Answers.Add(new DnsResolverAnswer()
                    {
                        Name = name,
                        Type = type,
                        TTL = ttl,
                        Data = data,
                    });
                }
            }
            catch (Exception ex)
            {
                dnsResolver.Message = ex?.Message;
                dnsResolver.StackTrace = ex?.StackTrace;
            }

            return dnsResolver;
        }
    }
}
