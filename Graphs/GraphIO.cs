using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Graphs
{
    public static class GraphIO
    {
        public static void ToFile(string filename, Graph graph)
        {
            ToFileAdjacencyList(filename, graph);
        }
        public static void ToFile(string filename, OrgGraph graph)
        {
            ToFileAdjacencyList(filename, graph);
        }
        public static void ToFile(string filename, WeightGraph graph)
        {
            ToFileAdjacencyList(filename, graph);
        }
        public static void ToFile(string filename, WeightOrgGraph graph)
        {
            ToFileAdjacencyList(filename, graph);
        }

        public static T FromFile<T>(string filename)
        {
            using (var reader = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                var type = reader.ReadLine();
                if (type != null && type.Equals(typeof(T).ToString()))
                {
                    switch (type)
                    {
                        case "Graphs.Graph":
                            {
                                var gr = new Graph();
                                var edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (var edge in edges)
                                {
                                    var adgeLoc = edge.Split(new[] {'-','>', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    if(gr.Edges.Count(w => w.NextVertex.Name == adgeLoc[0] && w.Previous.Name == adgeLoc[1]) != 1)
                                    gr.AddEdge(adgeLoc[0], adgeLoc[1]);
                                }
                                return (T)(object)gr;
                            }
                        case "Graphs.OrgGraph":
                            {
                                var gr = new OrgGraph();
                                var edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (var edge in edges)
                                {
                                    var adgeLoc = edge.Split(new[] { '-', '>', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    gr.AddEdge(adgeLoc[0], adgeLoc[1]);
                                }
                                return (T)(object)gr;
                            }
                        case "Graphs.WeightGraph":
                            {
                                var gr = new WeightGraph();
                                var edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (var edge in edges)
                                {
                                    var adgeLoc = edge.Split(new[] { '-', '>', ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    if(gr.Edges.Count(w => w.NextVertex.Name == adgeLoc[0] && w.Previous.Name == adgeLoc[1]) != 1)
                                        gr.AddEdge(adgeLoc[0], adgeLoc[1], double.Parse(adgeLoc[2]));
                                }
                                return (T)(object)gr;
                            }
                        case "Graphs.WeightOrgGraph":
                            {
                                var gr = new WeightOrgGraph();
                                var edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (var edge in edges)
                                {
                                    var adgeLoc = edge.Split(new[] { '-', '>', ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    gr.AddEdge(adgeLoc[0], adgeLoc[1], double.Parse(adgeLoc[2]));
                                }
                                return (T)(object)gr;
                            }
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
                throw new Exception("Тип графа и графа в файле не совпадает");
            }
        }
        private static IEnumerable<string> FirstBoot(Action<string> addVertex, string datas)
        {
            var edges = new List<string>();
            var data = datas.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var dataOneStr in data)
            {
                var oneVertex = dataOneStr.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                addVertex(oneVertex[0].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                var c = oneVertex[1].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //если есть ребра
                if (c.SelectMany(ss => ss).Contains('-'))
                    edges.AddRange(c);
            }
            return edges;
        }

        private static void ToFileAdjacencyList(string filename, object graph)
        {
            File.WriteAllText(filename, graph.GetType() + "\r\n" + graph);
        }
    }
}
