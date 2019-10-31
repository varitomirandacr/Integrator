using Infrastructure.Common;
using Infrastructure.Contracts;
using Infrastructure.Filters;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace RestIntegrator.Filters
{
    public class JsonExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;

        public JsonExceptionFilter(ILoggerFactory log)
        {
            _logger = log.CreateLogger("REST Integrator API");
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogInformation(context.Exception, ThrowIf.GetInnerMostException(context.Exception).Message);

            Exception exception = ThrowIf.GetInnerMostException(context.Exception);

            IFilterMetadata rateLimitFilter = context.Filters.Where(x => x.GetType().Name == "RateLimitFilter").FirstOrDefault();

            Type objectType = null;
            if (rateLimitFilter != null) {
                objectType = ((RateLimitFilter)rateLimitFilter).ObjectType;
            }

            ObjectResult objectResult = null;
            if (objectType == null)
            {
                ApiErrorResult apiError = new ApiErrorResult();
                apiError.Message = exception != null ? exception.Message : "Unexpected Json Error";
                apiError.StackTrace = exception?.StackTrace;
                objectResult = new ObjectResult(apiError);
            }
            else
            {
                IEntityService entityService = Activator.CreateInstance(((RateLimitFilter)rateLimitFilter).ObjectType) as IEntityService;
                entityService.Message = exception != null ? exception.Message : "Unexpected Json Error";
                entityService.StackTrace = exception?.StackTrace;
                objectResult = new ObjectResult(entityService);
            }
            
            context.Result = new ObjectResult(objectResult)
            {
                StatusCode = context.HttpContext.Response.StatusCode
            };
        }
    }
}
