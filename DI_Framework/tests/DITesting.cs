using KdG.DI.components.attributes;
using KdG.DI.container;
using KdG.DI.exceptions;
using KdG.DI.logging;
using KdG.DI.quickGraph;

namespace tests;

[TestFixture]
public class DITesting
{
    private DICollection _collection;
    
    [SetUp]
    public void SetUp()
    {
        _collection = new DICollection();
        _collection.AddSingleton<FirstService>();
        _collection.AddSingleton<Interface, SecondService>();
        new LoggingInit().Init();
    }

    [Test]
    public void TestCreateServices()
    {
        DIContainer container = _collection.GenerateContainer();
        
        Assert.NotNull(container.GetService(typeof(FirstService)));
        Assert.NotNull(container.GetService(typeof(Interface)));
    }

    [Test]
    public void TestDependencyGraphNoCycle()
    {
        Assert.DoesNotThrow(()=>new GraphInit().CheckGraph(_collection));
    }

    [Test]
    public void TestDependencyGraphCycle()
    {
        _collection.AddSingleton<ThirdService>();
        _collection.AddSingleton<FourthService>();
        
        Assert.Throws(typeof(Exception),()=>new GraphInit().CheckGraph(_collection));
    }

    [Test]
    public void TestUncreatableServices()
    {
        _collection.AddSingleton<Interface, FifthService>();

        DIContainer container = _collection.GenerateContainer();

        Assert.Throws(typeof(InvalidOperationException),()=>container.GetService(typeof(Interface)));
    }

    [Test]
    public void TestTooMayconstructors()
    {
        _collection.AddSingleton<SixthService>();

        DIContainer container = _collection.GenerateContainer();

        Assert.Throws(typeof(TooManyContstructorsException), () => container.GetService(typeof(SixthService)));
    }

    [Test]
    public void TestInterception()
    {
        _collection.AddSingleton<SeventhService>();

        DIContainer container = _collection.GenerateContainer();

        var proxyClass = container.GetService(typeof(SeventhService));
        
        Assert.AreEqual("SeventhServiceProxy", proxyClass.GetType().Name);
    }
}

public class FirstService
{
    
}

public interface Interface
{
    
}

public class SecondService : Interface
{
    
}

public class ThirdService
{
    private readonly FourthService _serviceDep;
    
    public ThirdService(FourthService serviceDep)
    {
        _serviceDep = serviceDep;
    }
}

public class FourthService
{
    private readonly ThirdService _serviceDep;

    public FourthService(ThirdService serviceDep)
    {
        _serviceDep = serviceDep;
    }
}

public class FifthService : Interface
{
    
}

public class SixthService
{
    private readonly FirstService _firstService;
    private readonly Interface _interface;

    public SixthService(FirstService firstService)
    {
        _firstService = firstService;
        _interface = new SecondService();
    }

    public SixthService(FirstService firstService, Interface @interface)
    {
        _firstService = firstService;
        _interface = @interface;
    }
}

[EnableLogged]
public class SeventhService
{
    [Logged]
    public virtual void GetInterceptionLogged()
    {
        
    }
}