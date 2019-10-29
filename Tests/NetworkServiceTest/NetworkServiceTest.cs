using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetworkService.Model;
using NetworkService.Models;
using NetworkService.Services;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace IntegratorTest
{
    [TestClass]
    public class NetworkServiceTest : TestBase
    {
        [TestMethod]
        public void Test_NetworkService_ExecuteICMP()
        {
            // Using google information 
            string targetHost = "google.com";
            string family = "InterNetwork";

            PingReplyService service = new PingReplyService(this.MockAppSettings.Object);

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

            this.VerifyAndTearDown();
        }

        [TestMethod]
        public void Test_NetworkService_ExecuteDnsResolver()
        {
            // Using google information 
            string targetHost = "google.com";

            PingReplyService service = new PingReplyService(this.MockAppSettings.Object);

            NetworkDnsResolver resolver = Task.Run(async () =>
            {
                return await service.ExecuteDnsResolver(targetHost);
            })
            .GetAwaiter()
            .GetResult();

            Assert.IsNotNull(resolver);
            //Assert.IsFalse(string.IsNullOrEmpty(resolver.TC));
            Assert.IsTrue(string.IsNullOrEmpty(resolver.Message));
            Assert.IsTrue(string.IsNullOrEmpty(resolver.StackTrace));

            this.VerifyAndTearDown();
        }

        [TestMethod]
        public void Test_NetworkService_DnsChilkatLookup()
        {
            string target = "google.com";

            DnsLookupService service = new DnsLookupService();

            NetworkDnsQueryLookup lookup = Task.Run(async () =>
            {
                return await service.DnsChilkatLookup(target);
            })
            .GetAwaiter()
            .GetResult();

            Assert.IsNotNull(lookup);
            Assert.IsFalse(string.IsNullOrEmpty(lookup.IpAddress));
            Assert.IsTrue(string.IsNullOrEmpty(lookup.Message));
            Assert.IsTrue(string.IsNullOrEmpty(lookup.StackTrace));
        }

        [TestMethod]
        public void Test_NetworkService_DnsClientLookup()
        {
            string target = "google.com";

            DnsLookupService service = new DnsLookupService();

            NetworkDnsClientLookup lookup = Task.Run(async () => 
            { 
                return await service.DnsClientLookup(target); 
            })
            .GetAwaiter()
            .GetResult();

            Assert.IsNotNull(lookup);
            Assert.IsFalse(string.IsNullOrEmpty(lookup.IpAddress));
            Assert.IsTrue(string.IsNullOrEmpty(lookup.Message));
            Assert.IsTrue(string.IsNullOrEmpty(lookup.StackTrace));
        }
    }
}

