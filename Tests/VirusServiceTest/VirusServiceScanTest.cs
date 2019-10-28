using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using VirusService.Services;
using Moq;

namespace VirusServiceTest
{
    [TestClass]
    public class VirusServiceScanTest : VirusServiceTestBase
    {
        [TestMethod]
        public void Test_VirusService_UrlScanService()
        {
            //string website = "https://urlscan.io/api/verdict/mp3-youtube.download";
            //string website = "https://urlscan.io/api/verdict/google.com";
            string website = "google.com";

            ScanService service = new ScanService(this.MockAppSettings.Object);

            var result = Task.Run(async () =>
            {
                return await service.ScanWebsite(website);
            })
            .GetAwaiter()
            .GetResult();

            Assert.IsNotNull(result);
            Assert.IsFalse(result.HasErrors);
            Assert.IsTrue(string.IsNullOrEmpty(result.Message));
        }

        [TestMethod]
        public void Test_VirusService_UrlScanService_InvalidUriFormat()
        {
            //string website = "http://www.google.com";
            string website = "www.google.com";

            ScanService service = new ScanService(this.MockAppSettings.Object);

            try
            {
                var result = Task.Run(async () =>
                {
                    return await service.ScanWebsite(website);
                })
                .GetAwaiter()
                .GetResult();
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Url has an invalid format", ex.Message);
            }            
        }

    }
}
