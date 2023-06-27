using KdG.DI.container;
using KdG.DI.logging;
using Microsoft.Extensions.Logging;

namespace KdG.DI.scanning;

public class Scanning
{
    private readonly ControllerScanning _controllerScanning;

    public Scanning()
    {
        _controllerScanning = new ControllerScanning();
    }
    
    public void Scan(Type type, DICollection collection)
    {
        var logger = LoggingInit.Logger.CreateLogger<Scanning>();
        
        logger.LogInformation("Scanning started...");
        
        var types = type.Assembly.GetTypes();

        _controllerScanning.Scan(types, collection);
        
        logger.LogInformation("Scanning completed");
    }

    public void Init(Type type, DICollection collection)
    {
        var types = type.Assembly.GetTypes();

        _controllerScanning.InitController(types, collection);
    }
}