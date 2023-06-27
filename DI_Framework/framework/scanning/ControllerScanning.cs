using System.Reflection;
using System.Reflection.Metadata;
using KdG.DI.components;
using KdG.DI.components.attributes;
using KdG.DI.container;
using KdG.DI.exceptions;
using KdG.DI.logging;
using Microsoft.Extensions.Logging;

namespace KdG.DI.scanning;

public class ControllerScanning
{
    public void Scan(Type[] typesInput,DICollection collection)
    {
        var types = typesInput
        .Where(t => t.GetInterfaces().Any(i=>i==typeof(Controller)));

        foreach (var type1 in types)
        {
            collection.AddSingleton(type1);
        }

        var logger = LoggingInit.Logger.CreateLogger<ControllerScanning>();
        logger.LogInformation("All controllers added");
    }

    public void InitController(Type[] typesInput, DICollection collection)
    {
        var container = collection.GenerateContainer();
        var types = typesInput
            .Where(t => t.GetInterfaces().Any(i=>i==typeof(Controller)));
        
        #region InitType

        foreach (var item in types)
        {
            var createdType = container.GetService(item);

            var method = item.GetMethods()
                .FirstOrDefault(m => m.GetCustomAttributes().Any(a => a.GetType() == typeof(InitAttribute)));

            if (method != null)
            {
                method.Invoke(createdType, Array.Empty<object>());
            }
        }
        
        #endregion

        #region KeyInputGetByNumber

        while (true)
        {
            var input = ReadInput();

            MethodInfo method = null;

            foreach (var item in types)
            {
                method = item.GetMethods()
                    .SingleOrDefault(m =>
                        m.GetCustomAttributes().Any(a => Equals(a, new KeyInputGetByMethodName(input))));
                
                if (method != null)
                {
                    var createdType = container.GetService(item);
                    method.Invoke(createdType, Array.Empty<object>());
                }
            }

            if (method == null)
            {
                foreach (var item in types)
                {
                    method = item.GetMethods()
                        .SingleOrDefault(m => m.GetCustomAttributes().Any(a => Equals(a, new KeyInputGetByNumber(Convert.ToInt32(input)))));

                    if (method != null)
                    {
                        var createdType = container.GetService(item);
                        method.Invoke(createdType, Array.Empty<object>());
                    }
                }
            }
        }

        #endregion
    }

    private string ReadInput()
    {
        Console.WriteLine("Reading input...");

        var inputNumber = Console.ReadLine();

        return inputNumber;
    }
}