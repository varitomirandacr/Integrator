using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    interface IIntegratorFacadeService
    {
        void ExecuteServices<T>(List<T> services);
    }
}
