using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graphs;

namespace TestGraphConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph gr = new Graph();
            gr.AddVertex("1", "2", "4", "5");
            gr.AddEdge("1", "2");
            gr.AddEdge("1", "5");
            gr.AddEdge(new Edge(gr.NameVertex["2"], gr.NameVertex["5"]));
            gr.RemoveVertex("5");
        }
    }
}
