using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WeightEdge : BaseEdge<WeightEdge>
    {
        public double Weight { get; set; }

        public WeightEdge(WeightVertex NextVertex, double weight) : base(NextVertex)
        {
            this.Weight = weight;
        }
    }
}
