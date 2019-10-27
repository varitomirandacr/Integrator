using DnsClient;
using NetworkService.Contracts;
using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NetworkService.Services
{
    public class DnsLookupService : IDnsLookupService
    {
        public string DnsLookup(string target)
        {
            Chilkat.Socket socket = new Chilkat.Socket();
            socket.UnlockComponent("unlock_code");

            int milliseconds = 10000;
            var address = socket.DnsLookup(target, milliseconds);

            var networkLookup = new NetworkLookup
            {
                IpAddress = address,
            };

            Debug.Assert(!string.IsNullOrEmpty(networkLookup.IpAddress), "IP Address empty? Why?");

            if (socket.LastMethodSuccess != true)
            {
                Debug.WriteLine(socket.LastErrorText);
                return string.Empty;
            }

            return networkLookup.IpAddress;
        }

        public async Task<NetworkLookup> DnsClientLookup(string target)
        {
            var lookup = new LookupClient();
            var result = await lookup.QueryAsync(target, QueryType.ANY);

            var record = result.Answers.ARecords()
                .Select(x => new NetworkLookup
                {
                    IpAddress = x?.Address.ToString(),
                    DomainName = x?.DomainName
                })
                .FirstOrDefault();

            return record;
        }
    }
}
