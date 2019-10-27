using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Contracts
{
    public interface IEntityService
    {
        string Message { get; set; }
        string StackTrace { get; set; }
    }
}
