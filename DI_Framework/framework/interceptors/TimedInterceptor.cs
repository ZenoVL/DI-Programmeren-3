using System.Reflection;
using Castle.DynamicProxy;
using KdG.DI.components.attributes;
using KdG.DI.logging;
using Microsoft.Extensions.Logging;

namespace KdG.DI.Interceptors;

public class TimedInterceptor:IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        if (invocation.Method.GetCustomAttributes().Any(a => a.GetType() == typeof(TimedAttribute)))
        {
            ILogger logger = LoggingInit.Logger.CreateLogger<TimedInterceptor>();
            
            long startTime = DateTime.Now.Ticks;
            invocation.Proceed();
            long endTime = DateTime.Now.Ticks;
            long methodTime = endTime - startTime;
            TimeSpan ts = TimeSpan.FromTicks(methodTime);
            logger.LogInformation($"Method took {ts.TotalSeconds} seconds");
        }
    }
}