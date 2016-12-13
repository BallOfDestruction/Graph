using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class BaseEdge<T>
    {
        public BaseVertex<T> Previous { get; set; }
        public BaseVertex<T> NextVertex { get; set; }
        public bool IsSeen { get; set; } = false;

        public BaseEdge(BaseVertex<T> previousVertex, BaseVertex<T> nextVertex)
        {
            this.NextVertex = nextVertex;
            this.Previous = previousVertex;
        }

        public override string ToString()
        {
            return Previous.Name + " -> " + NextVertex.Name;
        }
    }
}
