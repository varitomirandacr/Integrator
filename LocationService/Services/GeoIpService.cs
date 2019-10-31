using FreeGeoIPCore.Models;
using Infrastructure.Contracts;
using Infrastructure.Extensions;
using LocationService.Contract;
using System;
using System.Threading.Tasks;

/// <summary>
/// The based idea of this class was taken from the github below
/// https://github.com/mattosaurus/FreeGeoIPCore/blob/master/src/FreeGeoIPCore/FreeGeoIPClient.cs
/// I just made a couple of slight changes :-)
/// </summary>
namespace LocationService.Services
{
    public class GeoIpService : IGeoIpService, IRequestService
    {
        public async Task<Location> GetLocation(string target)
        {
            target.ValidateUrl(out Uri result);

            return await this.SendGetRequestAsync<Location>(result);
        }
    }
}
