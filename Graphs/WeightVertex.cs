using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WeightVertex : BaseVertex<WeightEdge>
    {
        public WeightVertex(string name) : base(name) { }

        public WeightVertex(string name, params WeightEdge[] vertex) : base(name, vertex) { }
    }
}
