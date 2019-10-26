using NetworkService.Contracts;
using System.Net.NetworkInformation;
using System.Text;

namespace NetworkService.Services
{
    public class NetworkService : INetworkFacadeService
    {
        public PingReply ExecuteICMP(string target)
        {
            //string targetHost = "bing.com";

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions
            {
                DontFragment = true
            };

            byte[] buffer = Encoding.ASCII.GetBytes(target);
            int timeout = 1024;
                        
            PingReply reply = pingSender.Send(target, timeout, buffer, options);
            
            return reply;
        }
    }
}
