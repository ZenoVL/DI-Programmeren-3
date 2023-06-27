using KdG.DI.container;
using KdG.DI.logging;
using Microsoft.Extensions.Logging;
using QuickGraph;
using QuickGraph.Algorithms;

namespace KdG.DI.quickGraph;

public class GraphClassScanning
{
    private readonly ILogger _logger;
    
    public GraphClassScanning()
    {
        _logger = LoggingInit.Logger.CreateLogger<GraphClassScanning>();
    }
    
    public BidirectionalGraph<Type, Edge<Type>> AddAllVertexes(DICollection collection)
    {
        var dependencyGraph = new BidirectionalGraph<Type, Edge<Type>>();

        foreach (var descriptor in collection.GetAllDescriptors())
        {
            dependencyGraph.AddVertex(descriptor.ImplementationType??descriptor.Type);
        }
        
        _logger.LogInformation("All vertexes added");

        return dependencyGraph;
    }

    public void AddEdges(BidirectionalGraph<Type, Edge<Type>> dependencyGraph, DICollection collection)
    {
        for (int i = 0; i < collection.GetAllDescriptors().Count; i++)
        {
            var type = collection.GetAllDescriptors()[i].ImplementationType ?? collection.GetAllDescriptors()[i].Type;
            
            var constructorToUse = type.GetConstructors().FirstOrDefault(t => t.GetParameters().Any());

            if (constructorToUse != null)
            {
                for (int j = 0; j < constructorToUse.GetParameters().Length; j++)
                {
                    var constructorToUseParam = constructorToUse.GetParameters()[j].ParameterType;
                    var constructorToUseParamDescriptor = collection.GetAllDescriptors().FirstOrDefault(d=>d.Type==constructorToUseParam);
                    dependencyGraph.AddEdge(new Edge<Type>(
                        collection.GetAllDescriptors()[i].ImplementationType ?? collection.GetAllDescriptors()[i].Type,
                        constructorToUseParamDescriptor.ImplementationType??constructorToUseParamDescriptor.Type));
                }
            }
        }

        _logger.LogInformation("All edges added");
    }
}