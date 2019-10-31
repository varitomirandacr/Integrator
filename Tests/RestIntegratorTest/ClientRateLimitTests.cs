using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestIntegratorTest
{
    [TestClass]
    public class ClientRateLimitTests
    {
        private const string apiPath = "/api/clients";
        private const string apiRateLimitPath = "/api/clientratelimit";
        private const string ip = "::1";
                
        [TestMethod]
        public void SpecificClientRule()
        {
            // Arrange
            var clientId = "cl-key-1";

            int responseStatusCode = 0;

            var target = "https://restintegrator20191029101642.azurewebsites.net/api/Main";

            var request = new System.Net.Http.HttpRequestMessage(HttpMethod.Get, new Uri(target));
            request.Headers.Add("Accept", "application/json");

            // Act    
            for (int i = 0; i < 4; i++)
            {
                var client = new HttpClient(new WinHttpHandler() { WindowsProxyUsePolicy = WindowsProxyUsePolicy.UseWinInetProxy });
                //request.Headers.Add("X-ClientId", clientId);
                //request.Headers.Add("X-Real-IP", ip);

                var response = Task.Run(() => { return client.SendAsync(request); }).Result;
                responseStatusCode = (int)response.StatusCode;
            }

            // Assert
            Assert.AreEqual(429, responseStatusCode);
        }

        private static int RequestClient(string clientId, HttpRequestMessage request)
        {
            var client = new HttpClient(new WinHttpHandler() { WindowsProxyUsePolicy = WindowsProxyUsePolicy.UseWinInetProxy });
            request.Headers.Add("X-ClientId", clientId);
            request.Headers.Add("X-Real-IP", ip);

            var response = Task.Run(() => { return client.SendAsync(request); }).Result;
            return (int)response.StatusCode;
        }
    }
}
