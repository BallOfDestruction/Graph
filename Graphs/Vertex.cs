using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class Vertex : BaseVertex<Edge>
    {
        public Vertex(string name) : base(name) { }

        public Vertex(string name, params Edge[] vertex) : base(name, vertex) { }
    }
}
