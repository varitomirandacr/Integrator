using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class IntegratorService : IIntegratorFacadeService
    {
        public void ExecuteServices<T>(List<T> services)
        {
            //Parallel.ForEach(services, service => )
        }
    }
}
