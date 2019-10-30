using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integrator.Contracts
{
    public interface IIntegratorService
    {
        Task<List<string>> ExecuteServices(string target, List<string> services);

        Task<string> RequestClient(string target);
    }
}
