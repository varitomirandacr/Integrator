using Infrastructure.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace VirusServiceTest
{
    public abstract class VirusServiceTestBase
    {
        protected MockRepository MockRepository { get; private set; }
        protected Mock<IOptions<AppSettings>> MockAppSettings;

        public VirusServiceTestBase()
        {
            MockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Empty };

            MockAppSettings = new Mock<IOptions<AppSettings>>();
            MockAppSettings.Setup(x => x.Value).Returns(new AppSettings
            {
                VirusScanUrl = "https://urlscan.io/api/verdict/",
                GeoIpUrl = "https://freegeoip.net/json/",
                QueryDnsUrl = "https://dns.google.com/resolve?name=",
                GeoIpAccessKey = "?access_key=065fbf702ae92e9a0d0806adbdac908a",
            });
        }

        [TearDown]
        public void VerifyAndTearDown()
        {
            MockRepository.VerifyAll();
        }
    }
}
