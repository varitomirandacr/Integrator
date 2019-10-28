using FreeGeoIPCore.Models;
using Infrastructure.Contracts;
using System.Threading.Tasks;

namespace LocationService.Contract
{
    public interface IGeoIpService : IRequestService
    {
        Task<Location> GetLocation(string target);
    }
}
