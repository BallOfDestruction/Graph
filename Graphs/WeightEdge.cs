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

        public WeightEdge(WeightVertex previousVertex, WeightVertex nextVertex , double weight) : base(previousVertex, nextVertex)
        {
            this.Weight = weight;
        }
        public override string ToString()
        {
            return base.ToString() + "|" + Weight;
        }
    }
}
