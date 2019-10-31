using Infrastructure.Contracts;
using System.Threading.Tasks;
using VirusService.Models;

namespace VirusService.Contracts
{
    public interface IScanService : IRequestService
    {
        Task<Scan> ScanWebsite(string target);
    }
}
