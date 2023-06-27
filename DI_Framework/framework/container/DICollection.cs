using KdG.DI.logging;
using KdG.DI.quickGraph;
using KdG.DI.scanning;
using Microsoft.Extensions.Logging;

namespace KdG.DI.container;

public class DICollection
{
    //remove move to DIContainer
    
    private List<DIDescriptor> _descriptors = new List<DIDescriptor>();

    public void AddSingleton<Tclass>()
    {
        _descriptors.Add(new DIDescriptor(typeof(Tclass), DILifetime.SINGLETON));
    }

    public void AddSingleton(Type type)
    {
        _descriptors.Add(new DIDescriptor(type, DILifetime.SINGLETON));
    }

    public void AddSingleton<Tclass>(Tclass implementation)
    {
        _descriptors.Add(new DIDescriptor(implementation, DILifetime.SINGLETON));
    }

    public void AddSingleton<TInterface, TImplementation>() where TImplementation : TInterface
    {
        _descriptors.Add(new DIDescriptor(typeof(TInterface),typeof(TImplementation), DILifetime.SINGLETON));
    }

    public DIContainer GenerateContainer()
    {
        return new DIContainer(_descriptors);
    }

    public IList<DIDescriptor> GetAllDescriptors()
    {
        return _descriptors;
    }

    public void startDi(Type type)
    {
        LoggingInit logging = new LoggingInit();
        Scanning scanning = new Scanning();
        GraphInit init = new GraphInit();

        logging.Init();

        var logger = LoggingInit.Logger.CreateLogger<DICollection>();
        
        logger.LogInformation("Initialising Depency Injection framework");
        
        scanning.Scan(type, this);
        init.CheckGraph(this);
        scanning.Init(type,this);
    }
}