using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    interface IIntegratorFacadeService
    {
        void ExecuteService<T>(List<T> services);
    }
}
