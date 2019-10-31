using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net;

namespace Infrastructure.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RateLimitFilter : ActionFilterAttribute
    {
        public string Name { get; set; }
        public int Seconds { get; set; }
        public Type ObjectType { get; set; }
        public static MemoryCache Cache { get; } = new MemoryCache(new MemoryCacheOptions());

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var ipAddress = context.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;
            var memoryCacheKey = $"{Name}-{ipAddress}";

            if (!Cache.TryGetValue(memoryCacheKey, out bool entry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions();
                cacheEntryOptions.SetAbsoluteExpiration(TimeSpan.FromSeconds(Seconds));
                Cache.Set(memoryCacheKey, true, cacheEntryOptions);
            }
            else
            {
                var message = $"Too many requests. Rate limit exceeded. Maximum admitted 1 per {Seconds} seconds";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;                
                context.Result = new ContentResult
                {
                    Content = message
                };
                throw new Exception(message);
            }
        }
    }
}
