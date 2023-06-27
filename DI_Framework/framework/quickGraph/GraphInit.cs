using KdG.DI.container;
using KdG.DI.logging;
using Microsoft.Extensions.Logging;
using QuickGraph.Algorithms;

namespace KdG.DI.quickGraph;

public class GraphInit
{
    public void CheckGraph(DICollection collection)
    {
        var logger = LoggingInit.Logger.CreateLogger<GraphInit>();
        
        logger.LogInformation("Dependency scanning started...");
        
        GraphClassScanning scanning = new GraphClassScanning();
        var graph = scanning.AddAllVertexes(collection);
        scanning.AddEdges(graph, collection);

        if (graph.IsDirectedAcyclicGraph())
        {
            logger.LogInformation("Detected no cycle.");
        }
        else
        {
            //TODO: custom exception
            logger.LogCritical("Detected a cycle.");
            throw new Exception("Detected a cycle.");
        }
    }
}