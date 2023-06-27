using Microsoft.Extensions.Logging;

namespace KdG.DI.logging;

public class LoggingInit
{
    public static ILoggerFactory Logger;
    
    public void Init()
    {
        Logger = LoggerFactory.Create(builder =>
        {
            builder
                .AddFilter("Microsoft", LogLevel.Warning)
                .AddFilter("System", LogLevel.Warning)
                .AddFilter("NonHostConsoleApp.Program", LogLevel.Debug)
                .AddConsole();
        });
    }
}