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
            gr.AddVertex("1", "2", "3", "5","6");
            gr.AddEdge("1", "2");
            gr.AddEdge("1", "3");
            gr.AddEdge("2", "5");
            gr.AddEdge("2","3");
            gr.AddEdge("5","3");
            gr.AddEdge("5", "6");
            gr.AddEdge("6", "3");
            var c = gr.GetPathBFS("1", "6");
        }
    }
}
