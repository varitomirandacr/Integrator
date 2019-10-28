﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VirusService.Contracts
{
    public interface IUrlScanService
    {
        Task<string> ScanWebsite(string target);
    }
}