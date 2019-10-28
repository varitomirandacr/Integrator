using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirusService.Converters;
using VirusService.Models;
using VirusService.Services;
using static VirusService.Models.Scan;

namespace VirusServiceTest
{
    [TestClass]
    public class VirusServiceScanTest
    {
        [TestMethod]
        public void Test_VirusService_UrlScanService()
        {
            //string website = "https://urlscan.io/api/verdict/mp3-youtube.download";
            string website = "https://urlscan.io/api/verdict/google.com";

            ScanService service = new ScanService();

            string result = Task.Run(async () =>
            {
                return await service.ScanWebsite(website);
            })
            .GetAwaiter()
            .GetResult();

            var urlScan = UrlScanParser.Parse(result);

            Assert.IsNotNull(urlScan);
            Assert.IsFalse(urlScan.HasErrors);
            Assert.IsTrue(string.IsNullOrEmpty(urlScan.ErrorMessage));
        }

        [TestMethod]
        public void Test_VirusService_UrlScanService_InvalidUriFormat()
        {
            //string website = "http://www.google.com";
            string website = "www.google.com";

            ScanService service = new ScanService();
            
            try
            {
                string result = Task.Run(async () =>
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
