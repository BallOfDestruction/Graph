using System;
using System.Linq;
using Graphs;

namespace TestGraphConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var gr = new Graph();
            gr.AddVertex("0", "1", "2", "3","4", "5","6", "7");
            gr.AddEdge("0","1");
            gr.AddEdge("0", "2");
            gr.AddEdge("1", "3");
            gr.AddEdge("1", "4");
            gr.AddEdge("1", "2");
            gr.AddEdge("2", "4");
            gr.AddEdge("2", "5");
            gr.AddEdge("3", "6");
            gr.AddEdge("4", "6");
            gr.AddEdge("4", "5");
            gr.AddEdge("7", "5");
            gr.AddEdge("7", "6");
            
            gr.RemoveEdge("0", "1"); 
            Console.WriteLine(gr.ToString());
            Console.WriteLine("+++++++++++++++++++++++++");
            Console.WriteLine(gr.DegreeOutput["0"]);
            Console.WriteLine(gr.DegreeOutput["1"]);
            Console.WriteLine(gr.DegreeOutput["2"]);
            Console.WriteLine(gr.DegreeOutput["3"]);
            Console.WriteLine(gr.DegreeOutput["4"]);
            Console.WriteLine(gr.DegreeOutput["5"]);
            Console.WriteLine(gr.DegreeOutput["6"]);
            Console.WriteLine(gr.DegreeOutput["7"]);

            GraphIO.ToFile("1.txt", gr);
            var r = GraphIO.FromFile<Graph>("1.txt");
            Console.WriteLine("+++++++++++++++++++++++++");
            Console.WriteLine(gr.ToString());

            var c = gr.GetBFS("1");
            var p = gr.Vertex.ToDictionary(q => q.Name, w => gr.GetBFS(w.Name).Select(ww => ww.Depth).Max() - 1);
            p.Where(w => w.Value == 3).Select(w => w.Key);
            var str = c.Aggregate("", (current, ver) => current + (ver.Name + " "));
            Console.WriteLine(str);

            c = gr.GetDFS("1");
            str = c.Aggregate("", (current, ver) => current + (ver.Name + " "));
            Console.WriteLine(str);
            var f = r.IsConherenceOfGraph();
            Console.WriteLine(f);
            var ff = r.IsFullCount();
            Console.WriteLine(ff);
            Console.ReadLine();
        }
    }
}
