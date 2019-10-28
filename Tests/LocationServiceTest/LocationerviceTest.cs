using LocationService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace LocationServiceTest
{
    [TestClass]
    public class LocationerviceTest
    {
        [TestMethod]
        public void Test_LocationService_GeoLocation()
        {
            string target = "http://api.ipstack.com/201.200.0.127?access_key=065fbf702ae92e9a0d0806adbdac908a";

            GeoIpService service = new GeoIpService();

            var result = Task.Run(async () =>
            {
                return await service.GetLocation(target);
            })
            .GetAwaiter()
            .GetResult();

            Assert.IsNotNull(result);
        }
    }
}
