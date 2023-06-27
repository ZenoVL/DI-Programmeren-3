using System.Reflection;
using Castle.DynamicProxy;
using KdG.DI.components.attributes;
using KdG.DI.container;
using KdG.DI.logging;
using Microsoft.Extensions.Logging;

namespace KdG.DI.Interceptors;

public class LoggedInterceptor:IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        if (invocation.Method.GetCustomAttributes().Any(a => a.GetType() == typeof(LoggedAttribute)))
        {
            ILogger logger = LoggingInit.Logger.CreateLogger<LoggedInterceptor>();
            logger.LogInformation("Entry of method: "+invocation.Method.Name);
            invocation.Proceed();
            logger.LogInformation("Exit of method: "+invocation.Method.Name);
        }
    }
}