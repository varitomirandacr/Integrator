using DnsClient;
using Infrastructure.Contracts;
using NetworkService.Contracts;
using NetworkService.Model;
using NetworkService.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NetworkService.Services
{
    public class DnsLookupService : IDnsLookupService, IRequestService
    {
        #region IDnsLookupService Implementation

        /// <summary>
        /// https://www.example-code.com/dotnet-core/dnsLookup.asp
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public async Task<NetworkDnsQueryLookup> DnsChilkatLookup(string target)
        {
            Chilkat.Socket socket = new Chilkat.Socket();
            socket.UnlockComponent("unlock_code");

            int milliseconds = 10000;

            var address = await Task.Run(() => { return socket.DnsLookup(target, milliseconds); });

            var networkDnsQueryLookup = new NetworkDnsQueryLookup
            {
                IpAddress = address,
            };

            Debug.Assert(!string.IsNullOrEmpty(networkDnsQueryLookup.IpAddress), "IP Address empty? Why?");

            if (!socket.LastMethodSuccess)
            {
                Debug.WriteLine(socket.LastErrorText);
                return default;
            }

            return networkDnsQueryLookup;
        }

        /// <summary>
        /// https://github.com/MichaCo/DnsClient.NET
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public async Task<NetworkDnsClientLookup> DnsClientLookup(string target)
        {
            var lookup = new LookupClient();
            var result = await lookup.QueryAsync(target, QueryType.ANY);

            var record = result.Answers.ARecords()
                .Select(x => new NetworkDnsClientLookup
                {
                    IpAddress = x?.Address.ToString(),
                    DomainName = x?.DomainName
                })
                .FirstOrDefault();

            return record;
        } 

        #endregion

        /// <summary>
        /// https://www.ryadel.com/en/asp-net-c-helper-class-to-get-web-server-ip-address-and-other-network-related-methods/
        /// </summary>
        /// <returns></returns>
        private List<IPAddress> GetIPAddresses()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            return ipHostInfo.AddressList.ToList();
        }

        private async Task<IPAddress> GetIPAddress(int num = 0)
        {
            return await Task.Run(() => 
            {                
                return GetIPAddresses()[num];
            });
        }

        public bool HasIPAddress(IPAddress ipAddress)
        {
            return GetIPAddresses().Contains(ipAddress);
        }

        public bool HasIPAddress(string ipAddress)
        {
            return HasIPAddress(IPAddress.Parse(ipAddress));
        }
    }
}
