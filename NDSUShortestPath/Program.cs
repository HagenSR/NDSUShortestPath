using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.ShortestPath;
namespace NDSUShortestPath
{
    class Program
    {
        public static AdjacencyGraph<String, Edge<string>> graph;
        public static Dictionary<Edge<string>, double> weight;
        private static string writeTo = ".\\..\\..\\..\\..\\output.csv";
        private static string readFrom = ".\\..\\..\\..\\..\\GraphCSV.csv";

        public static void Main(string[] args)
        {
            Console.WriteLine("Begin Program");
            Console.WriteLine("Reading from file {0}", readFrom);
            Console.WriteLine("Save File is {0}", writeTo);

            ReadFile.buildGraph(out graph, out weight, readFrom);

            Func<QuickGraph.Edge<string>, double> edgeCost = QuickGraph.Algorithms.AlgorithmExtensions.GetIndexer(weight);
            DijkstraShortestPathAlgorithm<string, Edge<string>> dijkstra = new DijkstraShortestPathAlgorithm<string, Edge<string>>(graph, new Func<Edge<String>, double>(edgeCost));
            writeToFile(edgeCost);        
        }

        public static void writeToFile(Func<QuickGraph.Edge<string>, double> edgeCost)
        {
            string fileToWrite = "";
            fileToWrite += "How to get to a *building* while spending the least amount of time outside\n Starting Building, Path (multiple Collumns)\n\n";
            for (int j = 0; j < graph.Vertices.Count(); j++)
            {
                fileToWrite += "Starting at " + graph.Vertices.ElementAt(j) + "\n";
                fileToWrite += computeForVertex(graph.Vertices.ElementAt(j), edgeCost) + "\n";
            }
            File.WriteAllText(writeTo, fileToWrite);
        }


        public static string computeForVertex(string vertex, Func<QuickGraph.Edge<string>, double> edgeCost)
        {
            string output = "";
            var tryGetPaths = graph.ShortestPathsDijkstra(edgeCost, vertex);
            foreach (string vert in graph.Vertices)
            {
                output += "To " + vert + ",";
                IEnumerable<Edge<string>> path;
                if (tryGetPaths(vert, out path))
                    foreach (var edge in path)
                    {
                        output += edge + ",";
                    }
                if (path is null)
                {
                    output += "You are already at " + vert;
                }
                output += "\n";
            }
            return output;

        }
    }
}

