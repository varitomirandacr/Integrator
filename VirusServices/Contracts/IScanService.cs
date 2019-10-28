using Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirusService.Models;

namespace VirusService.Contracts
{
    public interface IScanService : IRequestService
    {
        Task<Scan> ScanWebsite(string target);
    }
}
