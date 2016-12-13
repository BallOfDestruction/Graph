using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Graph : BaseGraph<Vertex, Edge>
    {


        protected override void AddVertex(Vertex vertex)
        {
            this.Vertex.Add(vertex);
        }
    }
}
