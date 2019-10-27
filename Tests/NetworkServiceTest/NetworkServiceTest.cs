using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkService.Model;
using NetworkService.Services;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace IntegratorTest
{
    [TestClass]
    public class NetworkServiceTest
    {
        [TestMethod]
        public void Test_NetworkService_ExecuteICMP()
        {
            // Using google information 
            string targetHost = "google.com";
            string family = "InterNetwork";

            PingReplyService service = new PingReplyService();

            NetworkReply result = Task.Run(async () =>
            {
                return await service.ExecuteICMP(targetHost);
            })
            .GetAwaiter()
            .GetResult();

            Assert.IsFalse(result.IsIPv4MappedToIPv6);
            Assert.IsFalse(result.IsIPv6LinkLocal);
            Assert.IsFalse(result.IsIPv6Multicast);
            Assert.IsFalse(result.IsIPv6SiteLocal);
            Assert.IsFalse(result.IsIPv6Teredo);

            Assert.AreEqual(result.AddressFamily.ToString(), family);
            Assert.IsTrue(result.Status);
        }

        [TestMethod]
        public void Test_NetworkService_DnsLookup()
        {
            string target = "google.com";

            DnsLookupService service = new DnsLookupService();

            string result = service.DnsLookup(target);
            
            Assert.IsFalse(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Test_NetworkService_DnsClient()
        {
            string target = "google.com";

            DnsLookupService service = new DnsLookupService();

            NetworkLookup result = Task.Run(async () => 
            { 
                return await service.DnsClientLookup(target); 
            })
            .GetAwaiter()
            .GetResult();

            Assert.IsFalse(string.IsNullOrEmpty(result.IpAddress));
        }
    }
}

