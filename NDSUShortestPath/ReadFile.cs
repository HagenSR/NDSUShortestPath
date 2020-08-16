using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using QuickGraph;

namespace NDSUShortestPath
{
    public class ReadFile
    {
        public static void buildGraph(out AdjacencyGraph<String, Edge<string>> graph, out Dictionary<Edge<string>, double> weight, string filePath)
        {
            graph = new AdjacencyGraph<string, Edge<string>>(true);
            weight = new Dictionary<Edge<string>, double>();
            using (var csv = new StreamReader(filePath))
            {
                while (!csv.EndOfStream)
                {
                    string line = csv.ReadLine();
                    string[] split = line.Split(',');

                    //This is hard coded, because each line should only have three values
                    //Vertex one, Vertex two and weight
                    if (!graph.Vertices.Contains(split[0]))
                    {
                        graph.AddVertex(split[0]);
                    }
                    if (!graph.Vertices.Contains(split[1]))
                    {
                        graph.AddVertex(split[1]);
                    }
                    Edge<string> edge = new Edge<string>(split[0], split[1]);
                    Edge<string> edgeTwo = new Edge<string>(split[1], split[0]);
                    graph.AddEdge(edge);
                    graph.AddEdge(edgeTwo);
                    weight.Add(edge, Double.Parse(split[2]));
                    weight.Add(edgeTwo, Double.Parse(split[2]));
                }
            }
        }
    }
}
