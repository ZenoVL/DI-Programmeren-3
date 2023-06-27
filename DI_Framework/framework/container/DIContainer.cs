using Castle.DynamicProxy;
using KdG.DI.components.attributes;
using KdG.DI.exceptions;
using KdG.DI.Interceptors;
using KdG.DI.logging;
using Microsoft.Extensions.Logging;

namespace KdG.DI.container;

public class DIContainer
{
    private List<DIDescriptor> _descriptors;

    public DIContainer(List<DIDescriptor> descriptors)
    {
        _descriptors = descriptors;
    }

    public object GetService(Type type)
    {
        var logger = LoggingInit.Logger.CreateLogger<DIContainer>();
        var descriptor = _descriptors
            .SingleOrDefault(d => d.Type == type);

        if (descriptor == null)
        {
            //TODO: custom exception
            logger.LogError($"Service of type {type.Name} is not registered");
            throw new Exception($"Service of type {type.Name} is not registered");
        }

        if (descriptor.Implementation != null)
        {
            return descriptor.Implementation;
        }

        Type actualType;

        if (descriptor.ImplementationType != null)
        {
            actualType = descriptor.ImplementationType;
        }
        else
        {
            actualType = descriptor.Type;
        }

        if (actualType.IsAbstract || actualType.IsInterface)
        {
            logger.LogError("Cannot instantiate abstract classes or interfaces");
            throw new Exception("Cannot instantiate abstract classes or interfaces");
        }

        if (actualType.GetConstructors().Count(t => t.GetParameters().Any()) > 1)
        {
            logger.LogError("Too many constructors where found for type: " + actualType);
            throw new TooManyContstructorsException("Too many constructors where found for type: " + actualType);
        }

        var constructorToUse = actualType.GetConstructors().FirstOrDefault(t => t.GetParameters().Any());
        var parameterArray = Array.Empty<object>();

        if (constructorToUse == null)
        {
            logger.LogWarning(actualType.ToString()+" alleen een default constructor, als je gebruik wilt maken van de DIContainer geeft dan parameters mee in de constructor");
            constructorToUse = actualType.GetConstructor(Type.EmptyTypes);
        }
        else
        {
            parameterArray = new object?[constructorToUse.GetParameters().Count()];

            for (int i = 0; i < parameterArray.Length; i++)
            {
                parameterArray[i] = GetService(constructorToUse.GetParameters()[i].ParameterType);
            }
        }
        
        var generator = new ProxyGenerator();
        var implementation = new object();
        
        if (actualType.CustomAttributes.Any(a => a.AttributeType == typeof(EnableLoggedAttribute))&&actualType.CustomAttributes.Any(a=>a.AttributeType == typeof(EnableTimedAttribute)))
        {
            implementation = generator.CreateClassProxy(actualType,parameterArray, new LoggedInterceptor(),new TimedInterceptor());
        }
        else if (actualType.CustomAttributes.Any(a=>a.AttributeType == typeof(EnableLoggedAttribute)))
        {
            implementation = generator.CreateClassProxy(actualType,parameterArray, new LoggedInterceptor());
        }
        else if (actualType.CustomAttributes.Any(a => a.AttributeType == typeof(EnableTimedAttribute)))
        {
            implementation = generator.CreateClassProxy(actualType,parameterArray, new TimedInterceptor());
        }
        else
        {
            implementation = constructorToUse.Invoke(parameterArray);
        }

        descriptor.Implementation = implementation;

        logger.LogInformation(actualType+" is gecreëerd");
        
        return implementation;
    }

    public T GetService<T>()
    {
        return (T)GetService(typeof(T));
    }
}