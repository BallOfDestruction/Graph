using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            using (StreamReader reader = new StreamReader(new FileStream(filename, FileMode.Open)))
            {
                string type = reader.ReadLine();
                if (type.Equals(typeof(T).ToString()))
                {
                    switch (type)
                    {
                        case "Graphs.Graph":
                            {
                                Graph gr = new Graph();
                                List<string> edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (string edge in edges)
                                {
                                    string[] adgeLoc = edge.Split(new char[] {'-','>', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    if(!(gr.Edges.Where(w => w.NextVertex.Name == adgeLoc[0] && w.Previous.Name == adgeLoc[1]).Count() == 1))
                                    gr.AddEdge(adgeLoc[0], adgeLoc[1]);
                                }
                                return (T)(object)gr;
                            }
                        case "Graphs.OrgGraph":
                            {
                                OrgGraph gr = new OrgGraph();
                                List<string> edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (string edge in edges)
                                {
                                    string[] adgeLoc = edge.Split(new char[] { '-', '>', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    gr.AddEdge(adgeLoc[0], adgeLoc[1]);
                                }
                                return (T)(object)gr;
                            }
                        case "Graphs.WeightGraph":
                            {
                                WeightGraph gr = new WeightGraph();
                                List<string> edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (string edge in edges)
                                {
                                    string[] adgeLoc = edge.Split(new char[] { '-', '>', ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    if(!(gr.Edges.Where(w => w.NextVertex.Name == adgeLoc[0] && w.Previous.Name == adgeLoc[1]).Count() == 1))
                                        gr.AddEdge(adgeLoc[0], adgeLoc[1], Double.Parse(adgeLoc[2]));
                                }
                                return (T)(object)gr;
                            }
                        case "Graphs.WeightOrgGraph":
                            {
                                WeightOrgGraph gr = new WeightOrgGraph();
                                List<string> edges = FirstBoot(gr.AddVertex, reader.ReadToEnd());
                                foreach (string edge in edges)
                                {
                                    string[] adgeLoc = edge.Split(new char[] { '-', '>', ' ', '|' }, StringSplitOptions.RemoveEmptyEntries);
                                    //Если такое ребро уже есть
                                    gr.AddEdge(adgeLoc[0], adgeLoc[1], Double.Parse(adgeLoc[2]));
                                }
                                return (T)(object)gr;
                            }
                        default:
                            {
                                throw new Exception();
                            }
                    }
                }
                else
                {
                    throw new Exception("Тип графа и графа в файле не совпадает");
                }
            }
        }
        private static List<string> FirstBoot(Action<string> addVertex, string datas)
        {
            List<string> edges = new List<string>();
            string[] data = datas.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string dataOneStr in data)
            {
                string[] oneVertex = dataOneStr.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                addVertex(oneVertex[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0]);
                var c = oneVertex[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                //если есть ребра
                if (c.SelectMany(ss => ss).Contains('-'))
                    edges.AddRange(c);
            }
            return edges;
        }

        private static void ToFileAdjacencyList(string filename, object graph)
        {
            File.WriteAllText(filename, graph.GetType().ToString() + "\r\n" + graph.ToString());
        }
    }
}
