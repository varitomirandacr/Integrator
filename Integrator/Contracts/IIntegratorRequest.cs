using System;
using System.Threading.Tasks;

namespace Integrator.Contracts
{
    public interface IIntegratorRequest
    {
        string ServiceName { get; set; }

        string RequestUrl { get; set; }

        Func<string, Task<string>> ExecuteService { get; set; }
    }
}
