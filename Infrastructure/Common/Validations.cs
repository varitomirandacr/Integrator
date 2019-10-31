using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Common
{
    public static class Validations
    {
        public static bool IsValidDomain(string domain)
        {
            // Complies to the RFC 1123 standard
            if (string.IsNullOrEmpty(domain) || domain.Length > 255)
            {
                return false;
            }

            if (new List<string> { "http://", "https://", "www." }.Any(x => domain.Contains(x)))
            {
                return false;
            }
                        
            if (!Uri.TryCreate($"http://{domain}", UriKind.Absolute, out Uri uri))
            {
                return false;
            }

            if (!string.Equals(uri.Host, domain, StringComparison.OrdinalIgnoreCase) || !uri.IsWellFormedOriginalString())
            {
                return false;
            }

            foreach (string part in uri.Host.Split('.'))
            {
                if (part.Length > 63)
                {
                    return false;
                }
            }

            if (!Regex.IsMatch(domain, @"^(?:[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?\.)+[a-z0-9][a-z0-9-]{0,61}[a-z0-9]$"))
            {
                return false;
            }

            return true;
        }
    }
}
