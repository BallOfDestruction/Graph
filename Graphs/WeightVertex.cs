namespace Graphs
{
    public class WeightVertex : BaseVertex<WeightEdge>
    {
        public WeightVertex(string name) : base(name) { }

        public WeightVertex(string name, params WeightEdge[] vertex) : base(name, vertex) { }
    }
}
