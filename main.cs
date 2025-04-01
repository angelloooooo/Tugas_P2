using System;
using System.Collections.Generic;

class Dijkstra
{
    public static Dictionary<string, int> FindShortestPaths(Dictionary<string, Dictionary<string, int>> graph, string start)
    {
        var distances = new Dictionary<string, int>();
        var priorityQueue = new SortedSet<(int, string)>();
        
        foreach (var node in graph)
            distances[node.Key] = int.MaxValue;
        
        distances[start] = 0;
        priorityQueue.Add((0, start));
        
        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentNode) = priorityQueue.Min;
            priorityQueue.Remove(priorityQueue.Min);
            
            foreach (var neighbor in graph[currentNode])
            {
                int newDist = currentDistance + neighbor.Value;
                
                if (newDist < distances[neighbor.Key])
                {
                    priorityQueue.Remove((distances[neighbor.Key], neighbor.Key));
                    distances[neighbor.Key] = newDist;
                    priorityQueue.Add((newDist, neighbor.Key));
                }
            }
        }
        
        return distances;
    }

    static void Main()
    {
        var graph = new Dictionary<string, Dictionary<string, int>>
        {
            { "A", new Dictionary<string, int> { { "B", 1 }, { "C", 4 } } },
            { "B", new Dictionary<string, int> { { "A", 1 }, { "C", 2 }, { "D", 5 } } },
            { "C", new Dictionary<string, int> { { "A", 4 }, { "B", 2 }, { "D", 1 } } },
            { "D", new Dictionary<string, int> { { "B", 5 }, { "C", 1 } } }
        };
        
        var shortestPaths = FindShortestPaths(graph, "A");
        
        foreach (var path in shortestPaths)
            Console.WriteLine($"Jarak terpendek dari A ke {path.Key}: {path.Value}");
    }
}
