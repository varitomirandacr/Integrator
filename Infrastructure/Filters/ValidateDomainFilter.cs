using Infrastructure.Common;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Reflection;


namespace Infrastructure.Filters
{
    public class ValidateDomainFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var parameters = descriptor.MethodInfo.GetParameters();
                ParameterInfo parameter = parameters.Where(x => x.Name == "target").FirstOrDefault();
                if (parameter != null) 
                { 
                    string argument = context.ActionArguments[parameter.Name] as string;
                    if (!Validations.IsValidDomain(argument))
                    {
                        throw new Exception("The domain you are trying to use is invalid");
                    }
                }
            }            
        }
    }
}
