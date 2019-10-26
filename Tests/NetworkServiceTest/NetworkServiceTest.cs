using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.NetworkInformation;

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

            NetworkService.Services.NetworkService service = new NetworkService.Services.NetworkService();

            PingReply result = service.ExecuteICMP(targetHost);

            Assert.IsFalse(result.Address.IsIPv4MappedToIPv6);
            Assert.IsFalse(result.Address.IsIPv6LinkLocal);
            Assert.IsFalse(result.Address.IsIPv6Multicast);
            Assert.IsFalse(result.Address.IsIPv6SiteLocal);
            Assert.IsFalse(result.Address.IsIPv6Teredo);

            Assert.AreEqual(result.Address.AddressFamily.ToString(), family);
            Assert.IsTrue(result.Status.Equals(IPStatus.Success));
        }
    }
}
