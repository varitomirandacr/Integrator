using System;

namespace Infrastructure.Extensions
{
    public static class UriExtensions
    {
        public static void ValidateUrl(this string target, out Uri uri)
        {
            if (!Uri.TryCreate(target, UriKind.Absolute, out uri))
            {
                throw new Exception("Url has an invalid format");
            }
        }
    }
}
