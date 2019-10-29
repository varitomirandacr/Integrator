using Infrastructure.Contracts;
using Integrator.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integrator.Models
{
    internal class IntegratorRequest : IIntegratorRequest
    {
        public string ServiceName { get; set; }

        public string RequestUrl { get; set; }

        public Func<string, Task<string>> ExecuteService { get; set; }

        public Action<string, List<string>> ExecuteAction { get; set; }

    }
}
