using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class BaseEdge<T>
    {
        public BaseVertex<T> NextVertex { get; set; }
        public bool IsSeen { get; set; } = false;

        public BaseEdge(BaseVertex<T> NextVertex)
        {
            this.NextVertex = NextVertex;
        }
    }
}
