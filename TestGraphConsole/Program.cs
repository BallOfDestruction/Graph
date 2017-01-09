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
            OrgGraph gr = new OrgGraph();
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
            gr.AddEdge("6", "7");
            gr.AddEdge("7", "5");
            gr.AddEdge("7", "6");
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
            var r = GraphIO.FromFile<OrgGraph>("1.txt");
            Console.WriteLine("+++++++++++++++++++++++++");
            Console.WriteLine(gr.ToString());

            var c = gr.GetBFS("1");
            string str = "";
            foreach (Vertex ver in c)
                str += ver.Name + " ";
            Console.WriteLine(str);

            c = gr.GetDFS("1");
            str = "";
            foreach (Vertex ver in c)
                str += ver.Name + " ";
            Console.WriteLine(str);
            var f = r.IsConherenceOfGraph();
            Console.WriteLine(f);
            var ff = r.IsFullCount();
            Console.WriteLine(ff);
            Console.ReadLine();
        }
    }
}
