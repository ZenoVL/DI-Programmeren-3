namespace KdG.DI.container;

public class DIDescriptor
{
    public Type Type { get;  }
    public Type ImplementationType { get; set; }
    public object Implementation { get; set; }
    public DILifetime Lifetime { get; }

    public DIDescriptor(object implementation, DILifetime lifetime)
    {
        Type = implementation.GetType();
        Implementation = implementation;
        Lifetime = lifetime;
    }

    public DIDescriptor(Type type, DILifetime lifetime)
    {
        Type = type;
        Lifetime = lifetime;
    }

    public DIDescriptor(Type type, Type implementationType, DILifetime lifetime)
    {
        Type = type;
        ImplementationType = implementationType;
        Lifetime = lifetime;
    }
}